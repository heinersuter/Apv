using System.Collections.Generic;
using System.Net;

using Apv.Data.Dtos;

using Newtonsoft.Json;

namespace Apv.Data.WindowsViewer.Service
{
    public class WebMemberService : IMemberService
    {
        private const string BaseUrl = "http://localhost:55897/api";

        public IEnumerable<MemberDto> GetMembers()
        {
            using (var client = new WebClient())
            {
                var json = client.DownloadString($"{BaseUrl}/members");
                return JsonConvert.DeserializeObject<IEnumerable<MemberDto>>(json);
            }
        }

        public MemberDetailsDto GetMemberDetails(MemberDto member)
        {
            using (var client = new WebClient())
            {
                var json = client.DownloadString($"{BaseUrl}/members/{member.Id}");
                return JsonConvert.DeserializeObject<MemberDetailsDto>(json);
            }
        }

        public void UpdateMember(MemberDetailsDto member)
        {
            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";

                var json = JsonConvert.SerializeObject(member);
                client.UploadString($"{BaseUrl}/members", "PUT", json);
            }
        }
    }
}

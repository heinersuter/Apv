using System.Collections.Generic;
using System.Net;

using Apv.Data.Dtos;
using Apv.Data.WindowsViewer.Properties;

using Newtonsoft.Json;

namespace Apv.Data.WindowsViewer.Service
{
    public class WebMemberService : IMemberService
    {
        private readonly string _baseUrl = Settings.Default.WebServiceBaseUrl;

        public IEnumerable<MemberDto> GetMembers()
        {
            using (var client = new WebClient())
            {
                var json = client.DownloadString($"{_baseUrl}/members");
                return JsonConvert.DeserializeObject<IEnumerable<MemberDto>>(json);
            }
        }

        public MemberDetailsDto GetMemberDetails(MemberDto member)
        {
            using (var client = new WebClient())
            {
                var json = client.DownloadString($"{_baseUrl}/members/{member.Id}");
                return JsonConvert.DeserializeObject<MemberDetailsDto>(json);
            }
        }

        public void UpdateMember(MemberDetailsDto member)
        {
            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";

                var json = JsonConvert.SerializeObject(member);
                client.UploadString($"{_baseUrl}/members", "PUT", json);
            }
        }
    }
}

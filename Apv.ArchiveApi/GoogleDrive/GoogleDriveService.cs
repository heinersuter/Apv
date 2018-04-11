using System;
using System.Collections.Generic;
using System.Linq;
using Apv.ArchiveApi.Api;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;

namespace Apv.ArchiveApi.GoogleDrive
{
    public class GoogleDriveService
    {
        private readonly DriveService _driveService;

        public GoogleDriveService()
        {
            var credential = GoogleCredential
                .FromFile(@"C:\Users\hsu\OneDrive\APV\WebArchive\ApvApi-d95898437aca.json")
                .CreateScoped(DriveService.Scope.DriveReadonly);

            _driveService = new DriveService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = "ApvApi"
            });
        }

        public IEnumerable<Event> GetAllDirectories()
        {
            var archiveRequest = _driveService.Files.List();
            archiveRequest.PageSize = 1;
            archiveRequest.Q = "name = 'Archiv' and mimeType='application/vnd.google-apps.folder'";

            var archiveResponse = archiveRequest.Execute();
            if (archiveResponse.Files.Count != 1)
            {
                Console.WriteLine("Root folder 'Archiv' not found.");
                return null;

            }

            Console.WriteLine($"Archive found: ID = {archiveResponse.Files[0].Id}.");

            var listRequest = _driveService.Files.List();
            listRequest.PageSize = 1000;
            listRequest.Q = $"'{archiveResponse.Files[0].Id}' in parents";

            var listResponse = listRequest.Execute();
            var i = 1;
            foreach (var file in listResponse.Files.OrderBy(file => file.Name))
            {
                Console.WriteLine($"{i++:D3} {file.Name} - {file.MimeType}");
            }

            if (listResponse.NextPageToken != null)
            {
                Console.WriteLine("Not all values returned.");
            }

            return listResponse.Files.Select(file => Event.Parse(file.Name));
        }
    }
}

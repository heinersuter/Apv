using System.IO;

namespace Apv.MemberExcel.Services
{
    public static class FileSystemService
    {
        public const string GoogleDirveDirectory = @"C:\Users\hsu\Google Drive";

        public static readonly string CurrentDocsDirectory = Path.Combine(GoogleDirveDirectory, "Aktuelle-Dokumente");

        public static readonly string AddressDirectory = Path.Combine(GoogleDirveDirectory, @"Vorstand\Adressen");

        public static readonly string ProfilePhotosDirectory = Path.Combine(AddressDirectory, @"Profil-Fotos");
    }
}

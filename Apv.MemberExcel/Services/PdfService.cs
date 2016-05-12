using System.Collections.Generic;
using System.IO;
using Apv.MemberExcel.Pdfs;

namespace Apv.MemberExcel.Services
{
    public class PdfService
    {
        public PdfService(string outputFolderPath)
        {
            OutputFolderPath = outputFolderPath;
        }

        public string OutputFolderPath { get; }

        public void WriteEnvelopes(IEnumerable<LetterAddress> addresses, string fileName)
        {
            var filePath = Path.Combine(OutputFolderPath, fileName);
            Envelope.Write(addresses, filePath);
        }

        public void WriteFullMailingLetter(IEnumerable<LetterAddress> addresses, string fileName)
        {
            var filePath = Path.Combine(OutputFolderPath, fileName);
            FullMailingLetter.Write(addresses, filePath);
        }

        public void WriteDepositSlipLetter(IEnumerable<LetterAddress> addresses, string fileName, bool requiresDepositSlipUnknown)
        {
            var filePath = Path.Combine(OutputFolderPath, fileName);
            DepositSlipLetter.Write(addresses, filePath, requiresDepositSlipUnknown);
        }
    }
}

using iTextSharp.text;

namespace Apv.MemberExcel.Pdfs
{
    public abstract class Pdf
    {
        protected static readonly Font FontBig = FontFactory.GetFont(FontFactory.HELVETICA, 20f);
        protected static readonly Font FontNormal = FontFactory.GetFont(FontFactory.HELVETICA, 10f);
        protected static readonly Font FontSmall = FontFactory.GetFont(FontFactory.HELVETICA, 7.5f);

        protected static float Mm(float mm)
        {
            return mm / 25.4f * 72f;
        }

        protected static void SetLeading(Paragraph paragraph)
        {
            paragraph.SetLeading(0f, 1.25f);
        }

        protected static void SetIndentation(Paragraph paragraph)
        {
            paragraph.IndentationLeft = Mm(96);
        }
    }
}

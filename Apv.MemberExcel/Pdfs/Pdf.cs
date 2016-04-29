using iTextSharp.text;

namespace Apv.MemberExcel.Pdfs
{
    public abstract class Pdf
    {
        protected static readonly Font Font24 = FontFactory.GetFont(FontFactory.HELVETICA, 24f, Font.BOLD);
        protected static readonly Font Font11 = FontFactory.GetFont(FontFactory.HELVETICA, 11f);
        protected static readonly Font Font8 = FontFactory.GetFont(FontFactory.HELVETICA, 8f);

        protected static float Mm(float mm)
        {
            return mm / 25.4f * 72f;
        }

        protected static void SetLeading(Paragraph paragraph)
        {
            paragraph.SetLeading(0f, 1.5f);
        }

        protected static void SetIndentation(Paragraph paragraph)
        {
            paragraph.IndentationLeft = Mm(96);
        }
    }
}

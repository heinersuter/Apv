namespace Apv.MemberExcel.Pdfs
{
    public abstract class Pdf
    {
        protected static float Mm(float mm)
        {
            return mm / 25.4f * 72f;
        }
    }
}

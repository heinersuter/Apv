namespace Apv.MemberExcel.Services
{
    public struct GeoCode
    {

        public GeoCode(double lat, double lng)
        {
            Lat = lat;
            Lng = lng;
        }

        public double Lat { get; set; }

        public double Lng { get; set; }

        public static GeoCode? Parse(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }
            var array = value.Split(',');
            var lat = double.Parse(array[0]);
            var lng = double.Parse(array[1]);
            return new GeoCode(lat, lng);
        }

        public override string ToString()
        {
            return $"{Lat},{Lng}";
        }
    }
}
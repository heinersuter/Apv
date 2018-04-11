namespace Apv.ArchiveApi.Api
{
    public class Event
    {
        public string Title { get; set; }

        public string Location { get; set; }

        public string Group { get; set; }

        public string Date { get; set; }

        public static Event Parse(string directoryName)
        {
            var parts = directoryName.Split('_');
            return new Event { Date = parts[0], Title = parts[1], Group = parts[2], Location = parts[3] };
        }
    }
}

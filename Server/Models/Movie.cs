namespace Server.Models
{
    public class Movie
    {
        public string _id { get; set; }
        public string title { get; set; }
        public string genres { get; set; }
        public string rating { get; set; }
        public string description { get; set; }
        public string url { get; set; }
        public string imgUrl { get; set; }
        public string director { get; set; }
        public string script { get; set; }
        public string mainCast { get; set; }
        public string releaseDate { get; set; }
        public string originCountry { get; set; }
        public string budget { get; set; }
        public string runtime { get; set; }
        public string location { get; set; }
        public string knownAs { get; set; }
        public string productionCompanies { get; set; }
    }
}

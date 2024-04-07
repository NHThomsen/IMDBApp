namespace IMDBApp.Model
{
    public class Title
    {
        public string? Tconst { get; set; }
        public int TitleTypeID { get; set; }
        public string? PrimaryTitle { get; set; }
        public string? OriginalTitle { get; set; }
        public bool IsAdult { get; set; }
        public int? StartYear { get; set; }
        public int? EndYear { get; set; }
        public int RunTimeMinutes { get; set; }


        public Title(string tconst, string primaryTitle, string originalTitle) 
        {
            Tconst = tconst;
            PrimaryTitle = primaryTitle;
            OriginalTitle = originalTitle;
        }
        public Title() { }
    }
}

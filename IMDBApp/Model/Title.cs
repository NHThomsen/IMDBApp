namespace IMDBApp.Model
{
    public class Title
    {
        public string Tconst { get; set; }
        public string PrimaryTitle { get; set; }
        public string OriginalTitle { get; set; }

        public Title(string tconst, string primaryTitle, string originalTitle) 
        {
            Tconst = tconst;
            PrimaryTitle = primaryTitle;
            OriginalTitle = originalTitle;
        }
    }
}

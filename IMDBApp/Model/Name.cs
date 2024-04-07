namespace IMDBApp.Model
{
    public class Name
    {
        public string Nconst { get; set; }
        public string PrimaryName { get; set; }

        public Name(string nconst, string primaryName)
        {
            Nconst = nconst;
            PrimaryName = primaryName;
        }
    }
}

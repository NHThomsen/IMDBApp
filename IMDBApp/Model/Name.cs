namespace IMDBApp.Model
{
    public class Name
    {
        public string Nconst { get; set; }
        public string PrimaryName { get; set; }
        public int? BirthYear { get; set; }
        public int? DeadYear { get; set; }

        public Name() { }
        public Name(string nconst, string primaryName)
        {
            Nconst = nconst;
            PrimaryName = primaryName;
        }
    }
}

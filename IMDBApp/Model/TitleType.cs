namespace IMDBApp.Model
{
    public class TitleType
    {
        public int TitleTypeID { get; set; }
        public string TitleTypeName { get; set; }

        public TitleType(int titleTypeID, string titleTypeName)
        {
            TitleTypeID = titleTypeID;
            TitleTypeName = titleTypeName;
        }
    }
}

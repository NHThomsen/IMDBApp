using IMDBApp.Model;
using System.Data;
using System.Data.SqlClient;

namespace IMDBApp.SQL
{
    public static class IMDBdal
    {
        private static string searchConnectionString = "Persist Security Info=False;Integrated Security=true; Initial Catalog=imdbDB;Server=LAPTOP-1OS9C9II; User Id=searchUser; Password=Pa$$w0rd;";

        public static List<Title> SearchMovieTitle(string movieTitle)
        {
            List<Title> titles = new List<Title>();
            using (SqlConnection conn = new SqlConnection(searchConnectionString)) 
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SearchMovieTitle", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@movieTitle", SqlDbType.VarChar).Value = movieTitle;
                    using(SqlDataReader reader = cmd.ExecuteReader()) 
                    {
                        if(reader.HasRows) 
                        {
                            while(reader.Read()) 
                            {
                                titles.Add(new Title(
                                    reader.GetString(0),
                                    reader.GetString(1),
                                    reader.GetString(2)));
                            }
                        }
                    }
                }
            }
            return titles;
        }
    }
}

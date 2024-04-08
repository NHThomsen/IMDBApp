using IMDBApp.Model;
using System.Data;
using System.Data.SqlClient;
using System.IO.Pipes;

namespace IMDBApp.SQL
{
    public static class IMDBdal
    {
        private readonly static string searchConnectionString = "Persist Security Info=False;Integrated Security=true; Initial Catalog=imdbDB;Server=LAPTOP-1OS9C9II; User Id=searchUser; Password=Pa$$w0rd;";
        private readonly static string crudConnectionString = "Persist Security Info=False;Integrated Security=true; Initial Catalog=imdbDB;Server=LAPTOP-1OS9C9II; User Id=CRUDUser; Password=Pa$$w0rd;";

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
        public static List<Name> SearchPerson(string name)
        {
            List<Name> persons = new List<Name>();
            using (SqlConnection conn = new SqlConnection(searchConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SearchPerson", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@name", SqlDbType.VarChar).Value = name;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                persons.Add(new Name(
                                    reader.GetString(0),
                                    reader.GetString(1)));
                            }
                        }
                    }
                }
            }
            return persons;
        }
        public static List<TitleType> GetTitleTypes()
        {
            List<TitleType> TitleTypes = new List<TitleType>();
            using(SqlConnection conn = new SqlConnection(crudConnectionString)) 
            {
                conn.Open();
                using (SqlCommand sqlCommand = new SqlCommand("SELECT * FROM GetTitleTypes", conn))
                {
                    using(SqlDataReader reader = sqlCommand.ExecuteReader()) 
                    {
                        if(reader.HasRows) 
                        {
                            while (reader.Read()) 
                            {
                                TitleTypes.Add(new TitleType(
                                    reader.GetInt32(0),
                                    reader.GetString(1)));
                            }
                        }
                    }
                }
            }
            return TitleTypes;
        }
        public static void InsertTitle(Title title)
        {
            using(SqlConnection con = new SqlConnection(crudConnectionString))
            {
                con.Open();
                using(SqlCommand sqlCommand = new SqlCommand("InsertTitle",con))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@titleTypeID", SqlDbType.Int).Value = title.TitleTypeID;
                    ParameterAdd(sqlCommand, "@primaryTitle", SqlDbType.VarChar, title.PrimaryTitle);
                    ParameterAdd(sqlCommand, "@originalTitle", SqlDbType.VarChar, title.OriginalTitle);
                    ParameterAdd(sqlCommand, "@isAdult", SqlDbType.Bit, title.IsAdult);
                    ParameterAdd(sqlCommand, "@startYear", SqlDbType.SmallInt, title.StartYear);
                    ParameterAdd(sqlCommand, "@endYear", SqlDbType.SmallInt, title.EndYear);
                    ParameterAdd(sqlCommand, "@runtimeMinutes", SqlDbType.Int, title.RunTimeMinutes);
                    
                    sqlCommand.ExecuteNonQuery();
                }
            }
        }
        public static void InsertName(Name name)
        {
            using(SqlConnection con = new SqlConnection(crudConnectionString))
            {
                con.Open();
                using(SqlCommand cmd = new SqlCommand("InsertName",con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    ParameterAdd(cmd, "@primaryName", SqlDbType.VarChar,name.PrimaryName);
                    ParameterAdd(cmd, "@birthYear", SqlDbType.SmallInt, name.BirthYear);
                    ParameterAdd(cmd, "@deadYear", SqlDbType.SmallInt, name.DeadYear);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static Title? DoesTitleExist(string tconst)
        {
            using(SqlConnection conn = new SqlConnection(crudConnectionString))
            {
                conn.Open();
                using(SqlCommand sqlCommand = new SqlCommand("DoesTitleExist",conn))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@tconst", SqlDbType.VarChar).Value = tconst;
                    using(SqlDataReader reader = sqlCommand.ExecuteReader()) 
                    {
                        if (reader.HasRows) 
                        {
                            while(reader.Read())
                            {
                                Title title = new Title();
                                title.Tconst = reader.GetString(0);
                                title.TitleTypeID = reader.GetInt32(1);
                                title.PrimaryTitle = (string?)GetValue(reader, 2, "string");
                                title.OriginalTitle = (string?)GetValue(reader, 3, "string");
                                title.IsAdult = reader.GetBoolean(4);
                                title.StartYear = (short?)GetValue(reader, 5, "smallint");
                                title.EndYear = (short?)GetValue(reader, 6, "smallint");
                                title.RunTimeMinutes = (int?)GetValue(reader, 7, "int");
                                return title;
                            }
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            return null;
        }
        public static void UpdateTitle(Title title)
        {
            using(SqlConnection sqlConnection = new SqlConnection(crudConnectionString))
            {
                sqlConnection.Open();
                using(SqlCommand updateTitle = new SqlCommand("UpdateTitle",sqlConnection))
                {
                    updateTitle.CommandType = CommandType.StoredProcedure;
                    updateTitle.Parameters.AddWithValue("@tconst", SqlDbType.VarChar).Value = title.Tconst;
                    ParameterAdd(updateTitle, "@primaryTitle", SqlDbType.VarChar, title.PrimaryTitle);
                    ParameterAdd(updateTitle, "@originalTitle", SqlDbType.VarChar, title.OriginalTitle);
                    updateTitle.Parameters.AddWithValue("@isAdult", SqlDbType.Bit).Value = title.IsAdult;
                    ParameterAdd(updateTitle, "@startYear", SqlDbType.SmallInt, title.StartYear);
                    ParameterAdd(updateTitle, "@endYear", SqlDbType.SmallInt, title.EndYear);
                    ParameterAdd(updateTitle, "@runtimeMinutes", SqlDbType.Int, title.PrimaryTitle);

                    updateTitle.ExecuteNonQuery();
                }
            }
        }
        public static void DeleteTitle(string tconst)
        {
            using(SqlConnection connection = new SqlConnection(crudConnectionString)) 
            {
                connection.Open();
                using(SqlCommand deleteTitle = new SqlCommand("DeleteTitle",connection)) 
                {
                    deleteTitle.CommandType = CommandType.StoredProcedure;
                    deleteTitle.Parameters.AddWithValue("@tconst", SqlDbType.VarChar).Value=tconst;
                    deleteTitle.ExecuteNonQuery();
                }
            }
        }
        private static void ParameterAdd(SqlCommand cmd, string name, SqlDbType type, object? value)
        {
            if (value == null) 
            {
                cmd.Parameters.AddWithValue(name, type).Value = DBNull.Value;
            }
            else
            {
                cmd.Parameters.AddWithValue(name,type).Value = value;
            }
        }
        private static object? GetValue(SqlDataReader reader, int index, string type) 
        {
            if(type == "string")
            {
                if(!reader.IsDBNull(index))
                {
                    return reader.GetString(index);
                }
            }
            else if(type == "int") 
            {
                if(!reader.IsDBNull(index))
                {
                    return reader.GetInt32(index);
                }
            }
            else if(type == "smallint")
            {
                if(!reader.IsDBNull(index))
                {
                    return reader.GetInt16(index);
                }
            }
            return null;
        }
    }
}

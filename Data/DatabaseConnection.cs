using Microsoft.Data.SqlClient;
using System.Reflection;

namespace BlazorApp1.Data
{
    public class DatabaseConnection
    {
        private IConfiguration _config;

        private Models _models;
        private string _connectionString => $"{_config["Database:ConnString"]}";
        public DatabaseConnection(Models models, IConfiguration config)
        {
            _models = models;
            _config = config;

        }

        //Get notes from table
        public List<Models> GetNotes()
        {
            List<Models> notes = new List<Models>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string sqlQuery = "SELECT * FROM Notebook";
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Models note = new Models();
                        note.ID = Convert.ToInt32(reader["ID"]);
                        note.Title = reader["Title"].ToString();
                        note.Body = reader["Body"].ToString();
                        notes.Add(note);
                    }
                }
            }
            return notes;
        }

        //Add note to table
        public void AddNote()
        {
            using (SqlConnection connection = new(_connectionString))
            {
                connection.Open();
                {
                    string SqlQuery = "INSERT INTO Notes (Title, Body) VALUES (@Title, @Body)";
                    using (SqlCommand command = new(SqlQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Title", _models.Title);
                        command.Parameters.AddWithValue("@Body", _models.Body);
                        command.ExecuteNonQuery();
                    }
                }
                connection.Close();
            }
        }

        
    }
}

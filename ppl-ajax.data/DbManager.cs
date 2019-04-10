using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ppl_ajax.data
{
    public class DbManager
    {
        private string _connString;
        public DbManager(string connString)
        {
            _connString = connString;
        }

        public void AddPerson(Person person)
        {
            SqlConnection conn = new SqlConnection(_connString);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO People VALUES (@firstname, @lastname, @age)";
            conn.Open();
            cmd.Parameters.AddWithValue("@firstname", person.FirstName);
            cmd.Parameters.AddWithValue("@lastname", person.LastName);
            cmd.Parameters.AddWithValue("@age", person.Age);
            cmd.ExecuteNonQuery();
            conn.Close();
            conn.Dispose();
        }

        public IEnumerable<Person> GetPeople()
        {
            var conn = new SqlConnection(_connString);
            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM People";
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            List<Person> ppl = new List<Person>();
            while (reader.Read())
            {
                ppl.Add(new Person
                {
                    FirstName = (string)reader["FirstName"],
                    LastName = (string)reader["LastName"],
                    Age = (int)reader["Age"],
                    Id = (int)reader["Id"]
                });
            }
            conn.Close();
            conn.Dispose();
            return ppl;
        }

        public Person GetPerson(int id)
        {
            SqlConnection conn = new SqlConnection(_connString);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT TOP 1 * FROM People WHERE id = @id";
            cmd.Parameters.AddWithValue("@id", id);
            conn.Open();
            var reader = cmd.ExecuteReader();
            if(!reader.Read())
            {
                return null;
            }
            Person p = new Person
            {
                FirstName = (string)reader["FirstName"],
                LastName = (string)reader["LastName"],
                Age = (int)reader["Age"],
                Id = (int)reader["Id"]
            };
            conn.Close();
            conn.Dispose();
            return p;
        }

        public void UpdatePerson(Person person)
        {
            SqlConnection conn = new SqlConnection(_connString);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UPDATE People SET FirstName = @firstname, LastName = @lastname, Age = @age " +
                "WHERE Id = @id";
            conn.Open();
            cmd.Parameters.AddWithValue("@firstname", person.FirstName);
            cmd.Parameters.AddWithValue("@lastname", person.LastName);
            cmd.Parameters.AddWithValue("@age", person.Age);
            cmd.Parameters.AddWithValue("@id", person.Id);
            cmd.ExecuteNonQuery();
            conn.Close();
            conn.Dispose();
        }

        public void Delete(int id)
        {
            SqlConnection conn = new SqlConnection(_connString);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DELETE People WHERE id = @id";
            conn.Open(); 
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            conn.Close();
            conn.Dispose();
        }
    }
}

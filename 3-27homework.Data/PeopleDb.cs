using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3_27homework.Data
{
    public class PeopleDb
    {
        private string _connectionString;
        public PeopleDb(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void AddPeople(List<Person> people)
        {
            foreach (var person in people)
            {
                using var connection = new SqlConnection(_connectionString);
                using var command = connection.CreateCommand();
                command.CommandText = "INSERT INTO People (FirstName, LastName, Age)" +
                                    "VALUES (@firstName, @lastName, @age)";
                command.Parameters.AddWithValue("@firstName", person.FirstName);
                command.Parameters.AddWithValue("@lastName", person.LastName);
                command.Parameters.AddWithValue("@age", person.Age);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public List<Person> GetAll()
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM People";
            var people = new List<Person>();
            connection.Open();
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                people.Add(new()
                {
                    Id = (int)reader["Id"],
                    FirstName = (string)reader["FirstName"],
                    LastName = (string)reader["LastName"],
                    Age = (int)reader["Age"]
                });
            }
            return people;
        }
    }
}

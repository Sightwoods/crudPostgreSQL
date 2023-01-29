using Npgsql;
using System;
using System.Data;

class CRUDExample
{
    private static string connString = "Server=localhost;Port=5432;User Id=postgres;Password=password;Database=mydb;";

    static void Main()
    {
        // Insert a new record
        InsertRecord("John Doe", "johndoe@example.com");

        // Read all records
        ReadRecords();

        // Update a record
        UpdateRecord(1, "Jane Doe", "janedoe@example.com");

        // Read all records
        ReadRecords();

        // Delete a record
        DeleteRecord(1);

        // Read all records
        ReadRecords();
    }

    private static void InsertRecord(string name, string email)
    {
        using (var conn = new NpgsqlConnection(connString))
        {
            conn.Open();

            using (var cmd = new NpgsqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO users (name, email) VALUES (@name, @email)";
                cmd.Parameters.AddWithValue("name", name);
                cmd.Parameters.AddWithValue("email", email);
                cmd.ExecuteNonQuery();
            }
        }
    }

    private static void ReadRecords()
    {
        using (var conn = new NpgsqlConnection(connString))
        {
            conn.Open();

            using (var cmd = new NpgsqlCommand("SELECT * FROM users", conn))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("ID: " + reader.GetInt32(0) + ", Name: " + reader.GetString(1) + ", Email: " + reader.GetString(2));
                    }
                }
            }
        }
    }

    private static void UpdateRecord(int id, string name, string email)
    {
        using (var conn = new NpgsqlConnection(connString))
        {
            conn.Open();

            using (var cmd = new NpgsqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = 
                    "UPDATE users SET name = @name, email = @email WHERE id = @id";
                cmd.Parameters.AddWithValue("id", id);
                cmd.Parameters.AddWithValue("name", name);
                cmd.Parameters.AddWithValue("email", email);
                cmd.ExecuteNonQuery();
            }
        }
    }

    private static void DeleteRecord(int id)
    {
        using (var conn = new NpgsqlConnection(connString))
        {
            conn.Open();

            using (var cmd = new NpgsqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = "DELETE FROM users WHERE id = @id";
                cmd.Parameters.AddWithValue("id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
using Npgsql;
using System;
using System.Data;

class crudPostgreSQL
{
    private static string connString = "" +
        "Server=localhost;" +
        "Port=5432;" +
        "User Id=postgres;" +
        "Password=Pg_Root_14;" +
        "Database=mydb;";

    static void Main()
    {
        // Insert a new record
        //InsertRecord("John", "john@example.com");

        // Read all records
        ReadRecords();

        // Update a record
        //UpdateRecord(6, "Jane Doe", "janedoe@example.com");

        // Read all records
        //ReadRecords();

        // Delete a record
        //DeleteRecord(6);

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
                cmd.CommandText = "SELECT insert_users(@name, @email)";
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.ExecuteNonQuery();
            }
        }
    }

    private static void ReadRecords()
    {
        using (var conn = new NpgsqlConnection(connString))
        {
            conn.Open();

            using (var cmd = new NpgsqlCommand("SELECT * FROM get_all_users('users');", conn))
            {
                cmd.CommandType = CommandType.Text;
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("ID: " + reader["id"] + ", Name: " + reader["name"] + ", Email: " + reader["email"]);
                    }
                    Console.WriteLine("");
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
                cmd.CommandText = "SELECT update_users(@id,@name,@email)";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@email", email);
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
                cmd.CommandText = "SELECT delete_user(@id)";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
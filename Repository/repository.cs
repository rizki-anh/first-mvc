using MySql.Data.MySqlClient;
using register.Models;
namespace MyApp.Repository // 🔑 ganti jadi namespace konsisten
{
    public class UserRepository
    {
        private readonly string _connectionString;

        // ✅ Constructor dengan 1 parameter string
        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Adduser(UserRegister user)
        {
            using var conneection = new MySqlConnection(_connectionString);
            conneection.Open();
            string sql = "INSERT INTO users (username, password, role, NUPTK, email, id) VALUES (@username, @password, @role, @NUPTK, @email, @id)";
            using var command = new MySqlCommand(sql, conneection);
            command.Parameters.AddWithValue("@id", user.id.ToString());
            command.Parameters.AddWithValue("@username", user.username);
            command.Parameters.AddWithValue("@password", user.password);
            command.Parameters.AddWithValue("@role", user.role);
            command.Parameters.AddWithValue("@NUPTK", user.NUPTK = 0);
            command.Parameters.AddWithValue("@email", user.email);
            command.ExecuteNonQuery();
        }
        public UserRegister? Getbyusername(string username)
        {
            using var conneection = new MySqlConnection(_connectionString);
            conneection.Open();

            string sql = "SELECT * FROM users WHERE username = @username";
            using var command = new MySqlCommand(sql, conneection);
            command.Parameters.AddWithValue("@username", username);
            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new UserRegister
                {
                    username = reader.GetString("username"),
                    password = reader.GetString("password")
                };
            }
            return null;
        }
        public async Task<List<long>?> GetAllNuptkAsync()
        {
            var nuptkList = new List<long>();

            await using var connection = new MySqlConnection(_connectionString);
            await connection.OpenAsync();

            string sql = "SELECT NUPTK FROM users WHERE NUPTK IS NOT NULL";
            await using var command = new MySqlCommand(sql, connection);
            await using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                long nuptk = reader.GetInt64(reader.GetOrdinal("NUPTK"));
                nuptkList.Add(nuptk);
            }

            if (nuptkList.Count == 0)
            {
                return null; // tidak ada data
            }

            return nuptkList;
        }
        public UserRegister? GetLogin(string email)
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            string sql = "SELECT * FROM users WHERE email = @email";
            using var command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@email", email);
            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new UserRegister
                {
                    email = reader.GetString("email"),
                    password = reader.GetString("password"),
                    role = reader.GetString("role")
                };
            }
            return null;
        }

    }
}

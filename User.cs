using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class User
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
    }
    class UtilsUser
    {
        public static MySqlConnection Connection = new MySqlConnection(new ConnectToDB().GetConnection());
        public User Authenticate()
        {
            User user = null;

            Console.Clear();
            Console.WriteLine("Введіть ваш email:");
            string email = Console.ReadLine();

            Console.WriteLine("Введіть ваш пароль:");
            string password = Console.ReadLine();

            try
            {
                Connection.Open();

                string query = "SELECT * FROM users WHERE email = @email AND password = @password";

                using (MySqlCommand command = new MySqlCommand(query, Connection))
                {
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@password", password);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Ініціалізуємо об'єкт User на основі отриманих даних з бази даних
                            user = new User
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                Email = reader["email"].ToString(),
                                Password = reader["password"].ToString(),
                                Role = reader["role"].ToString()
                            };

                            Console.WriteLine("Авторизація успішна.");
                        }
                        else
                        {
                            Console.WriteLine("Неправильний email або пароль. Перевірте введені дані та спробуйте знову.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Помилка авторизації: " + ex.Message);
            }
            finally
            {
                Connection.Close();
            }

            return user;
        }

        public void Register()
        {
            string email = "";
            string password = "";
            string role = "";

            Console.Clear();

            while (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(role) || role != "Librarian" || role != "Reader")
            {
                Console.WriteLine("Введіть email:");
                email = Console.ReadLine();

                Console.WriteLine("Введіть пароль:");
                password = Console.ReadLine();

                Console.WriteLine("Виберіть (Librarian або Reader):");
                role = Console.ReadLine();

                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(role))
                {
                    Console.WriteLine("Ви не ввели всі обов'язкові дані.");
                }
            }

            try
            {
                Connection.Open();

                string query = "INSERT INTO users (email, password, role) VALUES (@email, @password, @role)";

                using (MySqlCommand command = new MySqlCommand(query, Connection))
                {
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@password", password);
                    command.Parameters.AddWithValue("@role", role);

                    int affectedRows = command.ExecuteNonQuery();

                    if (affectedRows > 0)
                    {
                        Console.WriteLine("Реєстрація успішна.");
                    }
                    else
                    {
                        Console.WriteLine("Помилка при реєстрації, спробуйте знову.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Помилка: " + ex.Message);
            }
            finally
            {
                Connection.Close();
            }
        }
    }
}

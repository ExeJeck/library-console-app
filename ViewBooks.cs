using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    internal class ViewBooks
    {
        public static MySqlConnection Connection = new MySqlConnection(new ConnectToDB().GetConnection());
        public void PrintLine()
        {
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
        }
        public void ViewAllBooks()
        {
            try
            {
                Connection.Open();

                string query = "SELECT * FROM books";

                using (MySqlCommand command = new MySqlCommand(query, Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        DisplayRecords(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Помилка при отриманні списку книг: " + ex.Message);
            }
            finally
            {
                Connection.Close();
            }
        }
        public void DisplayBooksByTitle(string searchValue)
        {
            try
            {
                Connection.Open();

                string query = $"SELECT * FROM books WHERE name LIKE @searchValue ORDER BY " +
                               $"CASE " +
                               $"WHEN name = @searchValue THEN 0 " +
                               $"WHEN name LIKE CONCAT(@searchValue, '%') THEN 1 " +
                               $"WHEN name LIKE CONCAT('%', @searchValue) THEN 2 " +
                               $"ELSE 3 " +
                               $"END, name";

                using (MySqlCommand command = new MySqlCommand(query, Connection))
                {
                    command.Parameters.AddWithValue("@searchValue", $"%{searchValue}%");

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        DisplayRecords(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Помилка при виведенні книг: " + ex.Message);
            }
            finally
            {
                Connection.Close();
            }
        }
        public void DisplayBooksByAuthor(string searchValue)
        {
            try
            {
                Connection.Open();

                string query = $"SELECT * FROM books WHERE author LIKE @searchValue ORDER BY " +
                               $"CASE " +
                               $"WHEN author = @searchValue THEN 0 " +
                               $"WHEN author LIKE CONCAT(@searchValue, '%') THEN 1 " +
                               $"WHEN author LIKE CONCAT('%', @searchValue) THEN 2 " +
                               $"ELSE 3 " +
                               $"END, author";

                using (MySqlCommand command = new MySqlCommand(query, Connection))
                {
                    command.Parameters.AddWithValue("@searchValue", $"%{searchValue}%");

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        DisplayRecords(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Помилка при виведенні книг: " + ex.Message);
            }
            finally
            {
                Connection.Close();
            }
        }
        public void DisplayBooksByGenre(string searchValue)
        {
            try
            {
                Connection.Open();

                string query = $"SELECT * FROM books WHERE genre LIKE @searchValue ORDER BY " +
                               $"CASE " +
                               $"WHEN genre = @searchValue THEN 0 " +
                               $"WHEN genre LIKE CONCAT(@searchValue, '%') THEN 1 " +
                               $"WHEN genre LIKE CONCAT('%', @searchValue) THEN 2 " +
                               $"ELSE 3 " +
                               $"END, genre";

                using (MySqlCommand command = new MySqlCommand(query, Connection))
                {
                    command.Parameters.AddWithValue("@searchValue", $"%{searchValue}%");

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        DisplayRecords(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Помилка при виведенні книг: " + ex.Message);
            }
            finally
            {
                Connection.Close();
            }
        }
        public void DisplayBooksByMaxPageCount(int maxPageCount)
        {
            try
            {
                Connection.Open();

                string query = $"SELECT * FROM books WHERE NumberOfPages < @maxPageCount ORDER BY NumberOfPages";

                using (MySqlCommand command = new MySqlCommand(query, Connection))
                {
                    command.Parameters.AddWithValue("@maxPageCount", maxPageCount);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        DisplayRecords(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Помилка при виведенні книг: " + ex.Message);
            }
            finally
            {
                Connection.Close();
            }
        }
        public void DisplayBooksByExactPageCount(int exactPageCount)
        {
            try
            {
                Connection.Open();

                string query = $"SELECT * FROM books WHERE NumberOfPages = @exactPageCount ORDER BY NumberOfPages";

                using (MySqlCommand command = new MySqlCommand(query, Connection))
                {
                    command.Parameters.AddWithValue("@exactPageCount", exactPageCount);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        DisplayRecords(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Помилка при виведенні книг: " + ex.Message);
            }
            finally
            {
                Connection.Close();
            }
        }
        public void DisplayBooksByPageRange(int minPageCount, int maxPageCount)
        {
            try
            {
                Connection.Open();

                string query = $"SELECT * FROM books WHERE NumberOfPages BETWEEN @minPageCount AND @maxPageCount ORDER BY NumberOfPages";

                using (MySqlCommand command = new MySqlCommand(query, Connection))
                {
                    command.Parameters.AddWithValue("@minPageCount", minPageCount);
                    command.Parameters.AddWithValue("@maxPageCount", maxPageCount);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        DisplayRecords(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Помилка при виведенні книг: " + ex.Message);
            }
            finally
            {
                Connection.Close();
            }
        }
        public void DisplayBooksBySpecificPageCount(int specificPageCount)
        {
            try
            {
                Connection.Open();

                string query = $"SELECT * FROM books WHERE NumberOfPages = @specificPageCount ORDER BY NumberOfPages";

                using (MySqlCommand command = new MySqlCommand(query, Connection))
                {
                    command.Parameters.AddWithValue("@specificPageCount", specificPageCount);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        DisplayRecords(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Помилка при виведенні книг: " + ex.Message);
            }
            finally
            {
                Connection.Close();
            }
        }
        public void DisplayRecords(MySqlDataReader reader)
        {
            while (reader.Read())
            {
                Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                Console.WriteLine($"ID: {reader["id"]}");
                Console.WriteLine($"Назва: {reader["name"]}");
                Console.WriteLine($"Автор: {reader["author"]}");
                Console.WriteLine($"Жанр: {reader["genre"]}");
                Console.WriteLine($"Кількість сторінок: {reader["NumberOfPages"]}");
                Console.WriteLine($"Кількість книг в бібліотеці: {reader["NumberOfBooksInLibrary"]}");
                Console.WriteLine($"Рік видання: {reader["YearOfPublisher"]}");
            }
        }
    }
}

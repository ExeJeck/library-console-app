using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class UtilsLibrary
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public int NumberOfPages { get; set; }
        public int NumberOfBooksInLibrary { get; set; }
        public int YearOfPublisher { get; set; }
        public static MySqlConnection Connection = new MySqlConnection(new ConnectToDB().GetConnection());
        public void AddBook()
        {
            InputValueForBook();

            try
            {
                Connection.Open();

                string query = "INSERT INTO books (name, author, genre, NumberOfPages, NumberOfBooksInLibrary, YearOfPublisher) " +
                               "VALUES (@name, @author, @genre, @numberOfPages, @numberOfBooksInLibrary, @yearOfPublisher)";

                using (MySqlCommand command = new MySqlCommand(query, Connection))
                {
                    command.Parameters.AddWithValue("@name", Name);
                    command.Parameters.AddWithValue("@author", Author);
                    command.Parameters.AddWithValue("@genre", Genre);
                    command.Parameters.AddWithValue("@numberOfPages", NumberOfPages);
                    command.Parameters.AddWithValue("@numberOfBooksInLibrary", NumberOfBooksInLibrary);
                    command.Parameters.AddWithValue("@yearOfPublisher", YearOfPublisher);

                    int affectedRows = command.ExecuteNonQuery();

                    if (affectedRows > 0)
                    {
                        Console.WriteLine("Книга успішно додана до бази даних.");
                    }
                    else
                    {
                        Console.WriteLine("Помилка при додаванні книги. Будь ласка, спробуйте знову.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Помилка при додаванні книги: " + ex.Message);
            }
            finally
            {
                Connection.Close();
            }
        }
        public void DeleteBookById(int bookId)
        {
            try
            {
                Connection.Open();

                string query = "DELETE FROM books WHERE id = @bookId";

                using (MySqlCommand command = new MySqlCommand(query, Connection))
                {
                    command.Parameters.AddWithValue("@bookId", bookId);

                    int affectedRows = command.ExecuteNonQuery();

                    if (affectedRows > 0)
                    {
                        Console.WriteLine($"Книга з id {bookId} успішно видалена з бази даних.");
                    }
                    else
                    {
                        Console.WriteLine($"Книга з id {bookId} не знайдена в базі даних.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Помилка при видаленні книги: " + ex.Message);
            }
            finally
            {
                Connection.Close();
            }
        }
        public void UpdateBookById(int bookId)
        {
            InputValueForBook();

            try
            {
                Connection.Open();

                string query = "UPDATE books SET name = @newName, author = @newAuthor, genre = @newGenre, NumberOfPages = @newNumberOfPages, NumberOfBooksInLibrary = @newNumberOfBooks, YearOfPublisher = @newYearOfPublisher WHERE id = @bookId";

                using (MySqlCommand command = new MySqlCommand(query, Connection))
                {
                    command.Parameters.AddWithValue("@newName", Name);
                    command.Parameters.AddWithValue("@newAuthor", Author);
                    command.Parameters.AddWithValue("@newGenre", Genre);
                    command.Parameters.AddWithValue("@newNumberOfPages", NumberOfPages);
                    command.Parameters.AddWithValue("@newNumberOfBooks", NumberOfBooksInLibrary);
                    command.Parameters.AddWithValue("@newYearOfPublisher", YearOfPublisher);
                    command.Parameters.AddWithValue("@bookId", bookId);

                    int affectedRows = command.ExecuteNonQuery();

                    if (affectedRows > 0)
                    {
                        Console.WriteLine($"Книга з id {bookId} успішно оновлена в базі даних.");
                    }
                    else
                    {
                        Console.WriteLine($"Книга з id {bookId} не знайдена в базі даних.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Помилка при оновленні книги: " + ex.Message);
            }
            finally
            {
                Connection.Close();
            }
        }
        public void InputValueForBook()
        {
            Console.WriteLine("Введіть назву книги:");
            string name = Console.ReadLine();
            Name = name;

            Console.WriteLine("Введіть автора книги:");
            string author = Console.ReadLine();
            Author = author;

            Console.WriteLine("Введіть жанр книги:");
            string genre = Console.ReadLine();
            Genre = genre;

            Console.WriteLine("Введіть кількість сторінок:");
            int numberOfPages;
            while (!int.TryParse(Console.ReadLine(), out numberOfPages) || numberOfPages < 0)
            {
                Console.WriteLine("Будь ласка, введіть дійсне ціле число для кількості сторінок.");
            }
            NumberOfPages = numberOfPages;

            Console.WriteLine("Введіть кількість книг в бібліотеці:");
            int numberOfBooksInLibrary;
            while (!int.TryParse(Console.ReadLine(), out numberOfBooksInLibrary) || numberOfBooksInLibrary < 0)
            {
                Console.WriteLine("Будь ласка, введіть дійсне ціле число для кількості книг в бібліотеці.");
            }
            NumberOfBooksInLibrary = numberOfBooksInLibrary;

            Console.WriteLine("Введіть рік видання:");
            int yearOfPublisher;
            while (!int.TryParse(Console.ReadLine(), out yearOfPublisher) || yearOfPublisher < 0)
            {
                Console.WriteLine("Будь ласка, введіть дійсне ціле число для року видання.");
            }
            YearOfPublisher = yearOfPublisher;
        }
    }
}

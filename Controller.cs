using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    internal class Controller
    {
        private UtilsLibrary UtilsLibrary = new UtilsLibrary();
        private User User = new User();
        private UtilsUser utilsUser = new UtilsUser();
        private ViewBooks ViewBooks = new ViewBooks();
        private bool AuthorizationCheck = false;
        private bool KeepWork = true;
        public void MainLogic()
        {
            while (KeepWork)
            {
                if (User.Id != 0 && User.Email != null && User.Password != null && User.Role != null)
                {
                    Console.WriteLine("Користувач авторизований");

                    if(User.Role == "Librarian")
                    {
                        Console.Clear();
                        Console.WriteLine("Ви увійшли як бібліотекар");
                        Console.WriteLine("1 - Переглянути список книг \n2 - Додати книгу \n3 - Змінити інформацію про книгу \n4 - Видалити книгу");

                        int librarianSelection = Convert.ToInt32(Console.ReadLine());

                        switch (librarianSelection)
                        {
                            case 1:
                                Console.Clear();
                                Console.WriteLine("1 - Знайти всі книги \n2 - Знайти по назві\n3 - Знайти книги за автором \n4 - Знайти книги за жанром" +
                                    "\n5 - Знайти книги за кількістю сторінок");

                                int userSelect = Convert.ToInt32(Console.ReadLine());
                                switch (userSelect)
                                {
                                    case 1:
                                        ViewBooks.ViewAllBooks();
                                        break;
                                    case 2:
                                        Console.WriteLine("Введіть назву");
                                        string name = Console.ReadLine();

                                        ViewBooks.DisplayBooksByTitle(name);
                                        break;
                                    case 3:
                                        Console.WriteLine("Введіть автора");
                                        string author = Console.ReadLine();

                                        ViewBooks.DisplayBooksByAuthor(author);
                                        break;
                                    case 4:
                                        Console.WriteLine("Ведіть жанр");
                                        string genre = Console.ReadLine();

                                        ViewBooks.DisplayBooksByGenre(genre);
                                        break;
                                    case 5:
                                        Console.WriteLine("1 - До \n2 - Від\n3 - Від - До\n4 - Значення (якщо хочете знайти книжку з заданою кількістю сторінок");
                                        int selectPages = Convert.ToInt32(Console.ReadLine());

                                        switch(selectPages)
                                        {
                                            case 1:
                                                int maxPageCount;
                                                while (!int.TryParse(Console.ReadLine(), out maxPageCount) || maxPageCount < 0)
                                                {
                                                    Console.WriteLine("Будь ласка, введіть дійсне ціле число для максимальної кількості сторінок.");
                                                }
                                                ViewBooks.DisplayBooksByMaxPageCount(maxPageCount);
                                                break;
                                            case 2:
                                                int minPageCount;
                                                while (!int.TryParse(Console.ReadLine(), out minPageCount) || minPageCount < 0)
                                                {
                                                    Console.WriteLine("Будь ласка, введіть дійсне ціле число для точної кількості сторінок.");
                                                }
                                                ViewBooks.DisplayBooksByExactPageCount(minPageCount);
                                                break;
                                            case 3:
                                                Console.WriteLine("Введіть мінімальну кількість сторінок:");
                                                while (!int.TryParse(Console.ReadLine(), out minPageCount) || minPageCount < 0)
                                                {
                                                    Console.WriteLine("Будь ласка, введіть дійсне ціле число для мінімальної кількості сторінок.");
                                                }

                                                Console.WriteLine("Введіть максимальну кількість сторінок:");
                                                while (!int.TryParse(Console.ReadLine(), out maxPageCount) || maxPageCount < 0 || maxPageCount < minPageCount)
                                                {
                                                    Console.WriteLine("Будь ласка, введіть дійсне ціле число для максимальної кількості сторінок, більше або рівно мінімальній кількості сторінок.");
                                                }

                                                ViewBooks.DisplayBooksByPageRange(minPageCount, maxPageCount);
                                                break;
                                            case 4:
                                                int specificPageCount;
                                                while (!int.TryParse(Console.ReadLine(), out specificPageCount) || specificPageCount < 0)
                                                {
                                                    Console.WriteLine("Будь ласка, введіть дійсне ціле число для кількості сторінок.");
                                                }

                                                ViewBooks.DisplayBooksBySpecificPageCount(specificPageCount);
                                                break;
                                        }
                                        break;
                                    default:
                                        Console.WriteLine("Неправильний вибір. Будь ласка, введіть число від 1 до 6.");
                                        break;
                                }
                                break;
                            case 2:
                                Console.Clear();
                                UtilsLibrary.AddBook();
                                break;
                            case 3:
                                Console.Clear();
                                Console.WriteLine("Введіть Id книги данні про яку хочете змінити");
                                int idForUpdate = Convert.ToInt32(Console.ReadLine());
                                UtilsLibrary.UpdateBookById(idForUpdate);
                                break;
                            case 4:
                                Console.Clear();
                                Console.WriteLine("Введіть Id книги яку хочете видалити з бази данних");
                                int idForDelete = Convert.ToInt32(Console.ReadLine());
                                UtilsLibrary.DeleteBookById(idForDelete);
                                break;
                            default:
                                Console.WriteLine("Неправильний вибір. Будь ласка, введіть число від 1 до 4.");
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Нажаль для користувача з ролью Reader поки що функціоналу немає");
                    }
                    Console.ReadLine();
                }
                else
                {
                    while (User == null || User.Id == 0 || User.Email == null || User.Password == null || User.Role == null)
                    {
                        Console.Clear();
                        Console.WriteLine("Вам потрібно пройти авторизацію, або зареєструватися");

                        Console.WriteLine("1 - Увійти в аккаунт");
                        Console.WriteLine("2 - Зареєструватися");
                        string userSelection = Console.ReadLine();

                        switch (userSelection)
                        {
                            case "1":
                                User = utilsUser.Authenticate();
                                break;
                            case "2":
                                utilsUser.Register();
                                break;
                            default:
                                Console.WriteLine("Неправильний вибір. Будь ласка, введіть 1 або 2.");
                                break;
                        }
                    }
                }
            }
        }
    }
}

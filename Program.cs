using System;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Library
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Controller app = new Controller();

            app.MainLogic();
        }
    }
}
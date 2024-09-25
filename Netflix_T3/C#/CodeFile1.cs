using System;
namespace CodeFile
{
    class CodeFile
    {
        static void main(string[] args)
        {
            int counter = 0;
            string message = "Te amo Rory <3";
            do
            {
                Console.WriteLine(message);
                counter+=1;
            } while (counter <= 100);
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miners.TestConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> content = new List<string>();
            //List<string> characters = new List<string>();
            try
            {
                Random random = new Random();
                content = File.ReadAllLines($"C:/Users/User/source/repos/Miners-Game/Miners/Assets/Level/{random.Next(1, 3)}.txt")
                              .SelectMany(line => line.Split(' ').Where(character => !string.IsNullOrWhiteSpace(character)))
                              .ToList();

         
            }
            catch (Exception e)
            {
                throw new Exception("Ошибка чтения файла: " + e.Message);
            }

            foreach (var item in content)
            {
                Console.WriteLine(item);
            }
            Console.ReadLine();
        }
    }
}

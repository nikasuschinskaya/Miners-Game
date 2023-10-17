using Miners.Presentation.Level.Readers.Base;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

namespace Miners.Presentation.Level.Readers
{
    public class TXTReader : IReader
    {
        private readonly string _levelPath = ConfigurationManager.AppSettings["levelPath"].ToString();
        public List<string> Read()
        {
            var content = new List<string>();

            try
            {
                var random = new Random();
                content = File.ReadAllLines(_levelPath + $"{random.Next(1, 3)}.txt")
                              .SelectMany(line => line.Split(' ').Where(character => !string.IsNullOrWhiteSpace(character)))
                              .ToList();
            }
            catch (Exception e)
            {
                throw new Exception("Ошибка чтения файла: " + e.Message);
            }

            return content;
        }
    }
}

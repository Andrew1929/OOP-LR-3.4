using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace OOP_LR_3._4
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\TextFiles"; // шлях до каталогу з текстовими файлами

            Func<string, IEnumerable<string>> tokenizer = text =>
            {
                // розділяємо текст на слова і повертаємо список слів
                return text.Split(new char[] { ' ', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            };

            Func<IEnumerable<string>, IDictionary<string, int>> wordCounter = words =>
            {
                // обчислюємо частоту кожного слова у списку і повертаємо словник з результатами
                var result = new Dictionary<string, int>();
                foreach (string word in words)
                {
                    if (result.ContainsKey(word))
                        result[word]++;
                    else
                        result[word] = 1;
                }
                return result;
            };

            Action<IDictionary<string, int>> display = stats =>
            {
                // виводимо на екран статистику по кожному слову
                foreach (var pair in stats.OrderByDescending(p => p.Value))
                {
                    Console.WriteLine("{0}: {1}", pair.Key, pair.Value);
                }
            };

            // зчитуємо файли з каталогу та обчислюємо статистику по кожному файлу
            foreach (string file in Directory.GetFiles(path, "*.txt"))
            {
                string text = File.ReadAllText(file);
                var words = tokenizer(text);
                var stats = wordCounter(words);
                Console.WriteLine("Statistics for file {0}:", file);
                display(stats);
            }
        }
    }
}

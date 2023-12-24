using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace algorithm7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string inputString = string.Empty;
            Console.WriteLine("[Program] Введите строку для кодирования");
            Console.Write(">>> "); inputString = Console.ReadLine();

            // Создание дерева Хаффмана и кодирование строки
            HuffmanTree huffmanTree = new HuffmanTree();
            string encodedString = huffmanTree.Encode(inputString);

            // Вывод закодированной строки
            Console.WriteLine($"[Program] Закодированная строка: {encodedString}");

            //Декодирование строки и вывод
            string decodedString = huffmanTree.Decode(encodedString);
            Console.WriteLine($"[Program] Раскодированная строка: {decodedString}");
            
            Console.ReadKey();
        }
    }
}

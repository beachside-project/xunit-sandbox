using System;
using System.Collections.Generic;
using System.Reflection.Metadata;

namespace ConsoleApp31
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            ICollection<string> items = new List<string>()
            {
                "A","B,","C"        
            };


            Console.WriteLine($"before clear: {items.Count}");

            items.Clear();

            Console.WriteLine($"Clear 1: {items.Count}");
            
            items.Clear();

            Console.WriteLine($"Clear2: {items.Count}");
        }
    }
}
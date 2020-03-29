using System;
using System.IO;
using System.Collections.Generic;
using System.Globalization;
using System.Linq; 
using LinqWithLambdaExercise.Entities;

namespace LinqWithLambdaExercise
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter full file path: ");
            string path = Console.ReadLine();            
            List < Product > list = new List<Product>();
            
            try
            {
                using (StreamReader sr = File.OpenText(path)) { 
                while (!sr.EndOfStream)
                {
                    string[] field = sr.ReadLine().Split(';');
                    string name = field[0];
                    double price = double.Parse(field[1], CultureInfo.InvariantCulture);
                    list.Add(new Product (name, price));                                        
                }
                }

                var averagePrice = list.Select(x => x.Price).DefaultIfEmpty(0.0).Average();
                Console.WriteLine("Average price: " + averagePrice.ToString("F2", CultureInfo.InvariantCulture));
                var products = list.Where(x => x.Price < averagePrice).OrderByDescending(x => x.Name).Select(x => x.Name);
                foreach (string p in products)
                {
                    Console.WriteLine(p);
                }

            }
            catch (IOException e)
            {
                Console.WriteLine("An error accurred");
                Console.WriteLine(e.Message);
            }            
        }
    }
}

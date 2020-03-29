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
            FileStream fs = null;
            StreamReader sr = null;
            List < Product > list = new List<Product>();
            
            try
            {
                fs = new FileStream(path, FileMode.Open);
                sr = new StreamReader(fs);
                while (!sr.EndOfStream)
                {
                    string[] field = sr.ReadLine().Split(';');
                    string name = field[0];
                    double price = double.Parse(field[1], CultureInfo.InvariantCulture);
                    list.Add(new Product (name, price));                                        
                }

                var averagePrice = list.Select(x => x.Price).DefaultIfEmpty(0.0).Average();
                Console.WriteLine("Average price: " + averagePrice.ToString("F2", CultureInfo.InvariantCulture));
                var products = list.Where(x => x.Price < averagePrice).Select(x => x.Name);
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
            finally
            {
                if (sr != null) sr.Close(); //Necessário fechar o file e stream reader pois o C# não fecha sozinho ele
                if (fs != null) fs.Close();
            }
        }
    }
}

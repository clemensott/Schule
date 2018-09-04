using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqExample
{
    class Program
    {
        static void Main(string[] args)
        {
            IEnumerable<Fahrzeug> enumerable = FahrzeugGenerator.GetEnumerable();
            
            PrintWhile(enumerable.Where( f => f.PS > 200));

            Console.ReadLine();
        }

        private static void PrintForeach(IEnumerable enumerable)
        {
            foreach (object obj in enumerable)
            {
                Console.WriteLine(obj);
            }
        }

        private static void PrintWhile(IEnumerable enumerable)
        {
            IEnumerator enumerator = enumerable.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Console.WriteLine(enumerator.Current);
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqExample
{
    static class Extention
    {
        public static void PrintForeach(this IEnumerable enumerable)
        {
            foreach (object obj in enumerable)
            {
                Console.WriteLine(obj);
            }
        }

        public static void PrintWhile(this IEnumerable enumerable)
        {
            IEnumerator enumerator = enumerable.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Console.WriteLine(enumerator.Current);
            }
        }
    }
}

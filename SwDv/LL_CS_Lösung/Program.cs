
using System;
using System.Collections;
using System.Text;

namespace LL_CS
{
    class Program
    {
        //IHLContainer _ll = new CsLinkedList();
        IHLContainer _ll = new CsArrayList();

        static void Main(string[] args)
        {
            Program pg = new Program();
            pg.Test5();
        }



        // AddHead(), Print(), First(), Next(), RemoveHead()
        void Test1()
        {
            _ll.AddHead("aaa"); _ll.AddHead("bbb"); _ll.AddHead("ccc");
            _ll.Print();

            _ll.RemoveHead();
            _ll.Print();

            _ll.RemoveHead();
            _ll.Print();

            _ll.RemoveHead();
            _ll.Print();
        }

        void Test2()
        {
            _ll.AddTail("aaa"); _ll.AddTail("bbb"); _ll.AddTail("ccc");
            _ll.Print();

            _ll.RemoveTail();
            _ll.Print();

            _ll.RemoveTail();
            _ll.Print();

            _ll.RemoveTail();
            _ll.Print();
        }

        void Test3()
        {
            _ll.AddHead(new Student("Franz", 10));
            _ll.AddHead(new Student("Klaus", 3));
            _ll.AddHead(new Student("Heinrich", 8));
            _ll.AddHead(new Student("Karl", 13));

            // testObj um den Klaus zu suchen
            Student testObj = new Student("Klaus", 3);

            // mit StudentNameCompare sage ich dem Find() nach welchem Attribut ich suchen will
            object elem = _ll.Find(testObj, new StudentCatNrCompare());
            CheckObj(elem);
        }

        void Test4()
        {
            _ll.AddHead(new Student("Franz", 10));
            _ll.AddHead(new Student("Klaus", 3));
            _ll.AddHead(new Student("Heinrich", 8));
            _ll.AddHead(new Student("Karl", 13));

            _ll.Print();

            object student = _ll.At(2);
            CheckObj(student);
        }

        void Test5()
        {
            IComparer cmp1 = new StudentNameCompare();

            _ll.InsertSorted(new Student("Franz", 10), cmp1);
            _ll.InsertSorted(new Student("Klaus", 3), cmp1);
            _ll.InsertSorted(new Student("Heinrich", 8), cmp1);
            _ll.InsertSorted(new Student("Karl", 13), cmp1);
            _ll.Print();

            _ll.Clear();

          IComparer cmp2 = new StudentCatNrCompare();

            _ll.InsertSorted(new Student("Franz", 10), cmp2);
            _ll.InsertSorted(new Student("Klaus", 3), cmp2);
            _ll.InsertSorted(new Student("Heinrich", 8), cmp2);
            _ll.InsertSorted(new Student("Karl", 13), cmp2);
            _ll.Print();

        }

        void Test6()
        {
            for (int i = 0; i <= 10; i++)
                _ll.AddHead(i);
            _ll.Print();

            IComparer cmp = new IntCompare();

            object elem = _ll.Remove(_ll.Find(2, cmp));
            ClearObj(elem);
            _ll.Print();

            elem = _ll.Remove(_ll.Find(5, cmp));
            ClearObj(elem);
            _ll.Print();
        }

        void CheckObj(object elem)
        {

            if (elem != null)
                Console.WriteLine("{0} found ", elem);
            else
                Console.WriteLine("{0} not found ", elem);
        }

        void ClearObj(object elem)
        {

            if (elem != null)
                Console.WriteLine("{0} cleared ", elem);
            else
                Console.WriteLine("{0} not found ", elem);
        }

    }
}

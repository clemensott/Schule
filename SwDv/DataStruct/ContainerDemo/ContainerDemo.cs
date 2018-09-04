using System;
using System.Collections;


namespace ContainerDemo
{
  class ContainerDemo
  {
    ArrayList list = new ArrayList();

    static void Main(string[] args)
    {
      ContainerDemo cd = new ContainerDemo();
      cd.FillStudents1();
    }

    void FillInts()
    {
      Random rnd = new Random();

      for (int i = 1; i <= 10; i++)
        list.Add(rnd.Next(1,100));

      GenericPrint();
      list.Sort();
      GenericPrint();
      
      /* int val;
      for(int j=0; j<list.Count; j++)
      {
        val = (int)list[j];
        Console.Write("{0} ", val);
      }
      Console.WriteLine(); */
    }

    void FillStrings()
    {
      list.Add("Sepp"); list.Add("Franz"); list.Add("Otto");
      list.Add("Hugo"); list.Add("Anton");

      int idx = list.BinarySearch("Franz");

      GenericPrint();
      list.Sort();
      GenericPrint();
    }

    void FillStudents1()
    {
      list.Add(new Student("Hugo", 17));
      list.Add(new Student("Sepp", 23));
      list.Add(new Student("Franz", 7));
      list.Add(new Student("Otto", 34));
      GenericPrint();

      // sort by name
      list.Sort(new StudentNameComparer());
      GenericPrint();

      // sort by CatNr
      list.Sort(new StudentCatNrComparer());
      GenericPrint();
    }

    void GenericPrint()
    {
      foreach (object obj in list)
        Console.Write("{0}   ", obj.ToString());
      Console.WriteLine();
    }
  }
}

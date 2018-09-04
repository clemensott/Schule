using System;
using System.Text;

namespace smt
{
  class Program
  {
    string txt = "Hallo   Echo   123";
    string res;
    
    static void Main(string[] args)
    {
      Program prg = new Program();
      prg.StringBuilderTest2();
    }

    void StringBuilderTest1()
    {
      StringBuilder sb = new StringBuilder(50);
      foreach (char ch in txt)
      {
        sb.Append(ch);
      }
      res = sb.ToString();
    }

    void StringBuilderTest2()
    {
      StringBuilder sb = new StringBuilder(50);
      foreach (char ch in txt)
      {
        sb.Append(ch);
        if (Char.IsLetterOrDigit(ch))
          sb.Append('_');
      }
      res = sb.ToString();
    }
  }
}

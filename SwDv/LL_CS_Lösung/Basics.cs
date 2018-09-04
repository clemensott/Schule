
using System;
using System.Collections;
using System.Text;

namespace LL_CS
{
    class DemoLink
    {
        public DemoLink next;
        public string data;

        public DemoLink(string aData)
        {
            data = aData;
        }
    }


    // Zeigt die prinzipielle Funktionsweise und den Aufbau einer Linked-List
    class LLDemo
    {
        DemoLink root; // Zeiger auf den Anfang der Liste

        static void Main(string[] args)
        {
            LLDemo ld = new LLDemo();
            ld.CreateList();
            ld.IterateList();
            ld.RemoveElements();
        }

        // Liste erzeugen ( Befüllen ) Liste erzeugen ( Befüllen )Liste erzeugen ( Befüllen )
        void CreateList()
        {
            DemoLink obj; // Hilfsvariable

            obj = new DemoLink("aaa"); // Ein Listenelement erzeugen

            root = obj; // 1'stes Listenelement in die Liste einhängen

            obj = new DemoLink("bbb"); // 2'tes Listenelement erzeugen

            // 2'tes Listenelement einhängen
            obj.next = root; root = obj;

            obj = new DemoLink("ccc"); // 3'tes Listenelement erzeugen

            // 3'tes Listenelement einhängen
            obj.next = root; root = obj;
        }

        // Alle Listenelemente besuchen um Sie z.B. auszugeben
        void IterateList()
        {
            // Einen Iterator zum Besuchen aller Listenelemente erzeugen
            // und auf die Wurzel ( root ) der Liste setzen
            DemoLink iter = root;
            while (iter != null) // Das Next des letzen Listenelements ist 0 => die Abbruchbedingung der Iteration
            {
                Console.WriteLine("{0}", iter.data);
                iter = iter.next;
            }
        }

        // Listenelemente entfernen Listenelemente entfernen Listenelemente entfernen
        void RemoveElements()
        {
            DemoLink obj;

            // 1'tes Listenelement aus der Liste ausketten
            obj = root;
            root = root.next;
            obj.next = null;    //das ausgekettete objekt soll nicht mehr in der Liste verbunden sein

            // nächstes Listenelement aus der Liste ausketten
            obj = root;
            root = root.next;
            obj.next = null;
        }
    }
}

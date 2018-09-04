using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqExample
{
    class Fahrzeug
    {
        public double PS { get; set; }

        public double Leergewicht { get; set; }  // in kg

        public double Höchstgeschwindigkeit { get; set; }   // in km/h

        public double Beschleunigung { get; set; }  // von 0 auf 100 in Sekunden

        public int Türen { get; set; }

        public int Plätze { get; set; }

        public string Marke { get; set; }

        public string Modell { get; set; }

        public Color Farbe { get; set; }

        public Fahrzeug()
        {
            PS = 100;
            Leergewicht = 1000;
            Höchstgeschwindigkeit = 150;
            Beschleunigung = 20;
            Türen = 3;
            Plätze = 5;
            Marke = "Unbekannt";
            Modell = "Unbekannt";
            Farbe = Color.White;
        }

        public double GetLeistungsGewicht()
        {
            return Leergewicht / PS;
        }

        public double GetPersonenLeistung()
        {
            return (Plätze - 1) / Höchstgeschwindigkeit;
        }

        public override string ToString()
        {
            return Marke + " " + Modell;
        }
    }
}

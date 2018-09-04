using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqExample
{
    static class FahrzeugGenerator
    {
        public static IEnumerable<Fahrzeug> GetEnumerable()
        {
            yield return new Fahrzeug()
            {
                Marke = "Opel",
                Modell = "Zafira",
                PS = 110,
                Leergewicht = 1503,
                Höchstgeschwindigkeit = 179,
                Beschleunigung = 13.4,
                Türen = 5,
                Plätze = 7,
                Farbe = Color.LightSlateGray
            };

            yield return new Fahrzeug()
            {
                Marke = "Opel",
                Modell = "Astra",
                PS = 100,
                Leergewicht = 1244,
                Höchstgeschwindigkeit = 185,
                Beschleunigung = 13.4,
                Türen = 5,
                Plätze = 5,
                Farbe = Color.Black
            };

            yield return new Fahrzeug()
            {
                Marke = "Opel",
                Modell = "Corsa",
                PS = 90,
                Leergewicht = 1066,
                Höchstgeschwindigkeit = 175,
                Beschleunigung = 13.2,
                Türen = 3,
                Plätze = 5,
                Farbe = Color.Black
            };

            yield return new Fahrzeug()
            {
                Marke = "VW",
                Modell = "Golf",
                PS = 115,
                Leergewicht = 1226,
                Höchstgeschwindigkeit = 198,
                Beschleunigung = 10.2,
                Türen = 5,
                Plätze = 5,
                Farbe = Color.DarkBlue
            };

            yield return new Fahrzeug()
            {
                Marke = "VW",
                Modell = "e-Golf",
                PS = 136,
                Leergewicht = 1540,
                Höchstgeschwindigkeit = 150,
                Beschleunigung = 9.6,
                Türen = 5,
                Plätze = 5,
                Farbe = Color.YellowGreen
            };

            yield return new Fahrzeug()
            {
                Marke = "VW",
                Modell = "Passat",
                PS = 125,
                Leergewicht = 1368,
                Höchstgeschwindigkeit = 206,
                Beschleunigung = 9.9,
                Türen = 5,
                Plätze = 5,
                Farbe = Color.White
            };

            yield return new Fahrzeug()
            {
                Marke = "VW",
                Modell = "Multivan",
                PS = 102,
                Leergewicht = 2077,
                Höchstgeschwindigkeit = 157,
                Beschleunigung = 17.9,
                Türen = 5,
                Plätze = 9,
                Farbe = Color.Yellow
            };

            yield return new Fahrzeug()
            {
                Marke = "VW",
                Modell = "Beetle",
                PS = 150,
                Leergewicht = 1346,
                Höchstgeschwindigkeit = 202,
                Beschleunigung = 8.9,
                Türen = 3,
                Plätze = 4,
                Farbe = Color.Yellow
            };

            yield return new Fahrzeug()
            {
                Marke = "VW",
                Modell = "Käfer",
                PS = 50,
                Leergewicht = 760,
                Höchstgeschwindigkeit = 130,
                Beschleunigung = 21,
                Türen = 2,
                Plätze = 5,
                Farbe = Color.WhiteSmoke
            };

            yield return new Fahrzeug()
            {
                Marke = "VW",
                Modell = "Sicrocco",
                PS = 184,
                Leergewicht = 1395,
                Höchstgeschwindigkeit = 230,
                Beschleunigung = 7.5,
                Türen = 3,
                Plätze = 4,
                Farbe = Color.WhiteSmoke
            };

            yield return new Fahrzeug()
            {
                Marke = "VW",
                Modell = "Up!",
                PS = 60,
                Leergewicht = 851,
                Höchstgeschwindigkeit = 162,
                Beschleunigung = 14.4,
                Türen = 3,
                Plätze = 5,
                Farbe = Color.WhiteSmoke
            };

            yield return new Fahrzeug()
            {
                Marke = "Audi",
                Modell = "A3",
                PS = 115,
                Leergewicht = 1185,
                Höchstgeschwindigkeit = 211,
                Beschleunigung = 9.9,
                Türen = 5,
                Plätze = 5,
                Farbe = Color.OrangeRed
            };

            yield return new Fahrzeug()
            {
                Marke = "Audi",
                Modell = "S3",
                PS = 310,
                Leergewicht = 1465,
                Höchstgeschwindigkeit = 250,
                Beschleunigung = 4.6,
                Türen = 5,
                Plätze = 5,
                Farbe = Color.Orange
            };

            yield return new Fahrzeug()
            {
                Marke = "Audi",
                Modell = "R8",
                PS = 610,
                Leergewicht = 1225,
                Höchstgeschwindigkeit = 330,
                Beschleunigung = 3.2,
                Türen = 3,
                Plätze = 2,
                Farbe = Color.Pink
            };

            yield return new Fahrzeug()
            {
                Marke = "Mercedes",
                Modell = "Citaro G",
                PS = 354,
                Leergewicht = 10037,
                Höchstgeschwindigkeit = 90,
                Beschleunigung = double.NaN,
                Türen = 4,
                Plätze = 155,
                Farbe = Color.DarkCyan
            };

            yield return new Fahrzeug()
            {
                Marke = "Lamborgini",
                Modell = "R8",
                PS = 269,
                Leergewicht = 9430,
                Höchstgeschwindigkeit = 50,
                Beschleunigung = double.NaN,
                Türen = 2,
                Plätze = 2,
                Farbe = Color.Silver
            };

            yield return new Fahrzeug()
            {
                Marke = "Audi",
                Modell = "R10 TDI",
                PS = 646,
                Leergewicht = 925,
                Höchstgeschwindigkeit = 335,
                Beschleunigung = 2.8,
                Türen = 0,
                Plätze = 1,
                Farbe = Color.Silver
            };

            yield return new Fahrzeug()
            {
                Marke = "Tesla",
                Modell = "Model S",
                PS = 691,
                Leergewicht = 2100,
                Höchstgeschwindigkeit = 260,
                Beschleunigung = 3.4,
                Türen = 5,
                Plätze = 7,
                Farbe = Color.Red
            };

            yield return new Fahrzeug()
            {
                Marke = "Weineck",
                Modell = "Cobra",
                PS = 1200,
                Leergewicht = 980,
                Höchstgeschwindigkeit = 320,
                Beschleunigung = 2.3,
                Türen = 2,
                Plätze = 2,
                Farbe = Color.Blue
            };

            yield return new Fahrzeug()
            {
                Marke = "Leopard",
                Modell = "2A5",
                PS = 1500,
                Leergewicht = 62000,
                Höchstgeschwindigkeit = 72,
                Beschleunigung = double.NaN,
                Türen = 1,
                Plätze = 4,
                Farbe = Color.Gray
            };

            yield return new Fahrzeug()
            {
                Marke = "Smart",
                Modell = "Fortwo",
                PS = 82,
                Leergewicht = 945,
                Höchstgeschwindigkeit = 130,
                Beschleunigung = 10.2,
                Türen = 2,
                Plätze = 2,
                Farbe = Color.LightBlue
            };

            yield return new Fahrzeug()
            {
                Marke = "Shelby",
                Modell = "GT 500 Super Snake",
                PS = 811,
                Leergewicht = 1946,
                Höchstgeschwindigkeit = 250,
                Beschleunigung = 4.9,
                Türen = 2,
                Plätze = 4,
                Farbe = Color.Green
            };

            yield return new Fahrzeug()
            {
                Marke = "Ford",
                Modell = "Focus",
                PS = 105,
                Leergewicht = 1473,
                Höchstgeschwindigkeit = 180,
                Beschleunigung = 12.5,
                Türen = 5,
                Plätze = 5,
                Farbe = Color.LightGreen
            };

            yield return new Fahrzeug()
            {
                Marke = "Dacia",
                Modell = "Lodgy",
                PS = 90,
                Leergewicht = 1238,
                Höchstgeschwindigkeit = 169,
                Beschleunigung = 12.4,
                Türen = 5,
                Plätze = 7,
                Farbe = Color.DimGray
            };

            yield return new Fahrzeug()
            {
                Marke = "Dodge",
                Modell = "Charger",
                PS = 344,
                Leergewicht = 1830,
                Höchstgeschwindigkeit = 250,
                Beschleunigung = 6,
                Türen = 4,
                Plätze = 5,
                Farbe = Color.DarkRed
            };

            yield return new Fahrzeug()
            {
                Marke = "Infiniti",
                Modell = "G35",
                PS = 315,
                Leergewicht = 1720,
                Höchstgeschwindigkeit = 204,
                Beschleunigung = 6.2,
                Türen = 4,
                Plätze = 5,
                Farbe = Color.DeepSkyBlue
            };

            yield return new Fahrzeug()
            {
                Marke = "Porsche",
                Modell = "918 Spyder",
                PS = 887,
                Leergewicht = 1674,
                Höchstgeschwindigkeit = 345,
                Beschleunigung = 2.6,
                Türen = 2,
                Plätze = 2,
                Farbe = Color.DarkSalmon
            };

            yield return new Fahrzeug()
            {
                Marke = "Mitsubishi",
                Modell = "Lancer",
                PS = 117,
                Leergewicht = 1300,
                Höchstgeschwindigkeit = 188,
                Beschleunigung = 11.1,
                Türen = 5,
                Plätze = 5,
                Farbe = Color.LightCoral
            };

            yield return new Fahrzeug()
            {
                Marke = "Seat",
                Modell = "Leon",
                PS = 115,
                Leergewicht = 1161,
                Höchstgeschwindigkeit = 198,
                Beschleunigung = 9.8,
                Türen = 5,
                Plätze = 5,
                Farbe = Color.DarkGray
            };

            yield return new Fahrzeug()
            {
                Marke = "Seat",
                Modell = "Ibiza",
                PS = 90,
                Leergewicht = 1164,
                Höchstgeschwindigkeit = 181,
                Beschleunigung = 11.8,
                Türen = 5,
                Plätze = 5,
                Farbe = Color.DarkGray
            };

            yield return new Fahrzeug()
            {
                Marke = "Mini",
                Modell = "Cooper",
                PS = 211,
                Leergewicht = 1205,
                Höchstgeschwindigkeit = 238,
                Beschleunigung = 6.8,
                Türen = 5,
                Plätze = 5,
                Farbe = Color.DarkGray
            };

            yield return new Fahrzeug()
            {
                Marke = "Gumpert",
                Modell = "Apollo",
                PS = 750,
                Leergewicht = 1200,
                Höchstgeschwindigkeit = 360,
                Beschleunigung = 3.1,
                Türen = 2,
                Plätze = 2,
                Farbe = Color.DarkOliveGreen
            };

            yield return new Fahrzeug()
            {
                Marke = "Suzuki",
                Modell = "Swift",
                PS = 90,
                Leergewicht = 840,
                Höchstgeschwindigkeit = 180,
                Beschleunigung = 11.9,
                Türen = 5,
                Plätze = 5,
                Farbe = Color.IndianRed
            };

            yield return new Fahrzeug()
            {
                Marke = "Suzuki",
                Modell = "Splash",
                PS = 94,
                Leergewicht = 1064,
                Höchstgeschwindigkeit = 175,
                Beschleunigung = 12,
                Türen = 5,
                Plätze = 5,
                Farbe = Color.Cyan
            };

            yield return new Fahrzeug()
            {
                Marke = "Saleen",
                Modell = "S7 Twinturbo",
                PS = 760,
                Leergewicht = 1250,
                Höchstgeschwindigkeit = 380,
                Beschleunigung = 2.8,
                Türen = 2,
                Plätze = 2,
                Farbe = Color.Gold
            };

            yield return new Fahrzeug()
            {
                Marke = "Ford",
                Modell = "GT",
                PS = 557,
                Leergewicht = 1610,
                Höchstgeschwindigkeit = 330,
                Beschleunigung = 3.6,
                Türen = 2,
                Plätze = 2,
                Farbe = Color.White
            };

            yield return new Fahrzeug()
            {
                Marke = "Ford",
                Modell = "F-150 Raptor",
                PS = 310,
                Leergewicht = 2660,
                Höchstgeschwindigkeit = 160,
                Beschleunigung = 8.5,
                Türen = 4,
                Plätze = 5,
                Farbe = Color.LightPink
            };

            yield return new Fahrzeug()
            {
                Marke = "Ford",
                Modell = "KA",
                PS = 85,
                Leergewicht = 934,
                Höchstgeschwindigkeit = 169,
                Beschleunigung = 13.3,
                Türen = 5,
                Plätze = 5,
                Farbe = Color.BlueViolet
            };

            yield return new Fahrzeug()
            {
                Marke = "Ford",
                Modell = "Fiesta Gym",
                PS = 85,
                Leergewicht = 934,
                Höchstgeschwindigkeit = 169,
                Beschleunigung = 13.3,
                Türen = 5,
                Plätze = 5,
                Farbe = Color.BlueViolet
            };

            yield return new Fahrzeug()
            {
                Marke = "Pagani",
                Modell = "Zonda",
                PS = 800,
                Leergewicht = 1070,
                Höchstgeschwindigkeit = 350,
                Beschleunigung = 2.6,
                Türen = 2,
                Plätze = 2,
                Farbe = Color.CadetBlue
            };
        }

        public static IEnumerable<int> GetFacts(int count)
        {
            int fact = 1;

            for (int i = 1; i < count; i++)
            {
                fact *= i;

                yield return fact;
            }
        }

        public static Fahrzeug[] GetArray()
        {
            return GetEnumerable().ToArray();
        }

        public static FahrzeugIter GetIter()
        {
            IEnumerator<Fahrzeug> enumerator = GetEnumerable().GetEnumerator();

            return enumerator.MoveNext() ? new FahrzeugIter(enumerator) : null;
        }
    }
}

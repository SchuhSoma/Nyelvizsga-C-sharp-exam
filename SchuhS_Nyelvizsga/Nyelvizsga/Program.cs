using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.CompilerServices;

namespace Nyelvizsga
{
    class Program
    {
        static List<Sikeres> SikeresList;
        static List<Sikertelen> SikertelenList;
        static List<int> Osszegek;
        static Dictionary<string, int> NyelvOsszeg;
        static List<double> AranySz;
        static Dictionary<string, double> VizsgaArany;
        static Dictionary<string, double> NyelvekNullKeres;
        static int Ev;
        static void Main(string[] args)
        {
            Feladat1Beolvasas(); Console.WriteLine("\n--------------------------\n");
            Feladat2SikeresEvek(); Console.WriteLine("\n--------------------------\n");
            Feladat3Evbeker(); Console.WriteLine("\n--------------------------\n");
            Feladat6Osszesites(); Console.WriteLine("\n--------------------------\n");

            Console.ReadKey();
        }

        private static void Feladat6Osszesites()
        {
            Console.WriteLine("6.Feladat: Összesítés készítése, sikeres vizsga aránnyal");
            var sr = new StreamWriter(@"Osszesites.csv", false, Encoding.UTF8);
            foreach (var s1 in SikeresList)
            {
                double VizsgakOsszege = 0;
                double SikeresVizsvakOsszege = 0;
                double SikeresVizsgakAranya = 0;
                foreach (var s2 in SikertelenList)
                {
                    if(s1.Nyelv==s2.Nyelv)
                    {
                        SikeresVizsvakOsszege += s1.OsszesNyelviVizsga1;
                        VizsgakOsszege += s1.OsszesNyelviVizsga1 + s2.OsszesNyelviVizsga2;
                        SikeresVizsgakAranya = (SikeresVizsvakOsszege / VizsgakOsszege)*100;
                    }
                }
                Console.WriteLine("{0,-12} -> Összeg: {1:00000} -> Arány: {2:0.00} %", s1.Nyelv, VizsgakOsszege,SikeresVizsgakAranya );
                sr.WriteLine("{0,-10};{1:00000};{2:0.00} %", s1.Nyelv, VizsgakOsszege, SikeresVizsgakAranya);               
            }
            sr.Close();
        }

        private static void Feladat3Evbeker()
        {
            Console.WriteLine("3.Feladat: év bekérése 2009-2017 között");
            Ev = 0;
            do
            {
                Console.Write("\tKérem adjon meg egy évet amit vizsgálni szeretne : ");
                Ev = int.Parse(Console.ReadLine());

            } while (Ev < 2009 || 2018 < Ev);
            Feladat4();            
        }

        private static void Feladat4()
        {
            Console.WriteLine("\n--------------------------\n");
            Console.WriteLine("4.Feladat: a korábban megadott év szerint legsikertelenebb nyelv");
            /*int i = Ev - 2009;
            Console.WriteLine("\t"+i+"\n");*/
            AranySz = new List<double>();
            VizsgaArany = new Dictionary<string, double>();
            NyelvekNullKeres = new Dictionary<string, double>();
            foreach (var s1 in SikeresList)
            { 
                double AranySzam = 0;
                double Osszeg = 0;                
                foreach (var s2 in SikertelenList)
                {
                    if (s1.Nyelv == s2.Nyelv)
                    {
                        Osszeg = (double)s1.Sikerestmb[Ev - 2009] + (double)s2.Sikertelentmb[Ev - 2009];
                        AranySzam = (((double)s2.Sikertelentmb[Ev - 2009]) / Osszeg) * 100;
                    }                   
                }
               
                if (!AranySz.Contains(AranySzam))
                {
                    AranySz.Add(AranySzam);
                }
                if (!VizsgaArany.ContainsKey(s1.Nyelv))
                {
                    VizsgaArany.Add(s1.Nyelv, AranySzam);
                } 
                if(!NyelvekNullKeres.ContainsKey(s1.Nyelv))
                {
                    NyelvekNullKeres.Add(s1.Nyelv, Osszeg);
                }
            }
            /*
            foreach (var a in Arany)
            {
                Console.WriteLine(a);
            }
            */
            AranySz.Sort();
            AranySz.Reverse();
            foreach (var v in VizsgaArany)
            {   
                if(v.Value==AranySz[0])
                {
                    Console.WriteLine("\t{0,-10} -> {1:0.00}", v.Key, v.Value);
                }                    
            }
            Console.WriteLine("\n--------------------------\n");
            Console.WriteLine("5.Feladat: Határozza meg az adott évben melyik nyelvből nem volt vizsga");
            int db = 0;
            foreach (var ny in NyelvekNullKeres)
            {
                if(ny.Value==0)
                {
                    db++;
                    Console.WriteLine("\t{0,-10} -> {1:0.00}", ny.Key, ny.Value);
                }
                
            }
            if(db==0)
            {
                Console.WriteLine("\tAz adott évben nem volt olyan nyelv amiből nem volt vizsga");
            }

        }

        private static void Feladat2SikeresEvek()
        {
            Console.WriteLine("2.Feladat:Határozza meg és írja ki a képernyőre, hogy a kilenc év \n\t  sikeres és sikertelen vizsgáit"+
                              "összegezve melyik 3 nyelv volt a legnépszerűbb\n");            
            Osszegek = new List<int>();
            NyelvOsszeg = new Dictionary<string, int>();
            foreach (var sk in SikeresList)
            {
                int Osszeg = 0;
                foreach (var st in SikertelenList)
                {
                    if(sk.Nyelv==st.Nyelv)
                    {
                        Osszeg = sk.OsszesNyelviVizsga1 + st.OsszesNyelviVizsga2;
                        if (!Osszegek.Contains(Osszeg))
                        {
                            Osszegek.Add(Osszeg);
                        }
                        if (!NyelvOsszeg.ContainsKey(sk.Nyelv))
                        {
                            NyelvOsszeg.Add(sk.Nyelv, Osszeg);
                        }
                    }
                    
                }
                
            }
            Osszegek.Sort();
            Osszegek.Reverse();
            foreach (var o in Osszegek)
            {
             //Console.WriteLine(o);
            }
            foreach (var ny in NyelvOsszeg)
            {
                //Console.WriteLine("{0} -> {1}", ny.Key, ny.Value);
            }
            for (int i = 0; i < 3; i++)
            {
                foreach (var ny in NyelvOsszeg)
                {
                    if(Osszegek[i]==ny.Value)
                    {
                        Console.WriteLine("\t{0} -> {1}", ny.Key, ny.Value);
                    }
                }
            }

        }

        private static void Feladat1Beolvasas()
        {
            Console.WriteLine("1.Feladat: Beolvasás");
            SikeresList = new List<Sikeres>();
            
            var sr1 = new StreamReader(@"sikeres.csv", Encoding.UTF8);
            int db1 = 0;
            while(!sr1.EndOfStream)
            {
                db1++;
                SikeresList.Add(new Sikeres(sr1.ReadLine()));
            }
            Console.WriteLine("\tSikeres beolvasás 1 -> beolvasott sorok száma {0}", db1);
            SikertelenList = new List<Sikertelen>();
            var sr2 = new StreamReader(@"sikertelen.csv", Encoding.UTF8);
            int db2 = 0;
            while (!sr2.EndOfStream)
            {
                db2++;
                SikertelenList.Add(new Sikertelen(sr2.ReadLine()));
            }
            Console.WriteLine("\tSikeres beolvasás 2 -> beolvasott sorok száma {0}", db2);
        }
    }
}

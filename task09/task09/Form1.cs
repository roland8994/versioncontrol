using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using task09.Entities;

namespace task09
{
    public partial class Form1 : Form
    {
        Random rng = new Random(1234); //seed megadása, hogy a teszteknél mindig ugyanzokat a számokat kapjam
        List<Person> Population = new List<Person>();
        List<BirthProbability> BirthProbabilities = new List<BirthProbability>();
        List<DeathProbability> DeathProbabilities = new List<DeathProbability>();
        public Form1()
        {
            InitializeComponent();
            Population.AddRange(GetPopulation(@"C:\Temp\nép-teszt.csv"));
            BirthProbabilities.AddRange(GetBirthProbabilities(@"C:\Temp\születés.csv"));
            DeathProbabilities.AddRange(GetDeathProbabilities(@"C:\Temp\halál.csv"));

            for (int year = 2005; year <= 2024; year++)
            {
                // Végigmegyünk az összes személyen
                for (int i = 0; i < Population.Count; i++)
                {
                    SimStep(year, Population[i]);
                }

                int nbrOfMales = (from x in Population
                                  where x.Gender == Gender.Male && x.IsAlive
                                  select x).Count();
                int nbrOfFemales = (from x in Population
                                    where x.Gender == Gender.Female && x.IsAlive
                                    select x).Count();

                Console.WriteLine(string.Format(
                    "Év:{0} " +
                    "Fiúk:{1} " +
                    "Lányok:{2}",
                    year, 
                    nbrOfMales, 
                    nbrOfFemales));
            }

            //Gender g = Gender.Male; //nem példányosítok, hanem enumerációt hozok létre
            //var x = (Gender)2;  //így tudja, hogy nő


        }
        public List<Person> GetPopulation(string csvpath)
        {
            var population = new List<Person>(); //ez lesz a visszatérő érték, csak közben feltöltjük adatokkal, lásd return population

            using (var sr = new StreamReader(csvpath, Encoding.Default))
            {
                
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    /* vagy így lehet:  
                    var p = new Person()
                    {

                    };
                    population.Add(p)
                    //kisbetűs populationhöz adni. Vagy:*/

                    population.Add(new Person()
                    {
                        BirthYear = int.Parse(line[0]),
                        Gender = (Gender)Enum.Parse(typeof(Gender), line[1]),
                        NbrOfChildren = byte.Parse(line[2])
                    });


                }
            }
            return population;
        }
        public List<BirthProbability> GetBirthProbabilities(string csvpath)
        {
            var birthProbabilities = new List<BirthProbability>(); //ez lesz a visszatérő érték, csak közben feltöltjük adatokkal, lásd return population

            using (var sr = new StreamReader(csvpath, Encoding.Default))
            {
               
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    /* vagy így lehet:  
                    var p = new Person()
                    {

                    };
                    population.Add(p)
                    //kisbetűs populationhöz adni. Vagy:*/

                    birthProbabilities.Add(new BirthProbability()
                    {
                        Age = int.Parse(line[0]),                       
                        NbrOfChildren = byte.Parse(line[1]),
                        P = double.Parse(line[2])
                    });


                }
            }
            return birthProbabilities;
        }
        public List<DeathProbability> GetDeathProbabilities(string csvpath)
        {
            var deathProbabilities = new List<DeathProbability>(); //ez lesz a visszatérő érték, csak közben feltöltjük adatokkal, lásd return population

            using (var sr = new StreamReader(csvpath, Encoding.Default))
            {
                //sr.ReadLine(); ez most nem kell mivel, nincs fejléc
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    /* vagy így lehet:  
                    var p = new Person()
                    {

                    };
                    population.Add(p)
                    //kisbetűs populationhöz adni. Vagy:*/

                    deathProbabilities.Add(new DeathProbability()
                    {
                        Age = int.Parse(line[0]),
                        Gender = (Gender)Enum.Parse(typeof(Gender), line[1]),
                        P = double.Parse(line[2])
                    });


                }
            }
            return deathProbabilities;
        }
        private void SimStep(int year, Person person)
        {
            //Ha halott akkor kihagyjuk, ugrunk a ciklus következő lépésére
            if (!person.IsAlive) return;

            // Letároljuk az életkort, hogy ne kelljen mindenhol újraszámolni
            byte age = (byte)(year - person.BirthYear);

            // Halál kezelése
            // Halálozási valószínűség kikeresése
            double pDeath = (from x in DeathProbabilities
                             where x.Gender == person.Gender && x.Age == age
                             select x.P).FirstOrDefault();
            // Meghal a személy?
            if (rng.NextDouble() <= pDeath)  //NextDouble 0 és 1 közötti valószínűség
                person.IsAlive = false;

            //Születés kezelése - csak az élő nők szülnek
            if (person.IsAlive && person.Gender == Gender.Female)
            {
                //Szülési valószínűség kikeresése
                double pBirth = (from x in BirthProbabilities
                                 where x.Age == age
                                 select x.P).FirstOrDefault();
                //Születik gyermek?
                if (rng.NextDouble() <= pBirth)
                {
                    Person újszülött = new Person();
                    újszülött.BirthYear = year;
                    újszülött.NbrOfChildren = 0;
                    újszülött.Gender = (Gender)(rng.Next(1, 3));
                    Population.Add(újszülött);
                }
            }
        }
    }
}

using EFCodeFirst;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EFCodeFirst
{
    internal class Program
    {
        static void Main(string[] args)
        {
            FeedDb(); // ->> Added 
            using (var db = new UniversityDbContext()) 
            {
                var corso = db.Corso 
                    .Where(x => x.Name == "Informatica")
                    .Include(s => s.Students).First(); // JOIN 

                Console.WriteLine($"Corso di laurea in {corso.Name}");
                Console.WriteLine($" ---- > studenti associati: ");
                corso.Students.ForEach(x => Console.WriteLine($"          - " + x.Name));
            }
        } 
        public  static void FeedDb()
        {
            Corso corso = new Corso()
            {
                Name = "Informatica",
                DatePublished = DateTime.Now,
                Students = null
            };
            List<Studente> students = new List<Studente>()
            {
                new Studente() { Name = "Bruno Ferreira", DatePublished = DateTime.Now },
                new Studente() { Name = "Bruno Ferreira", DatePublished = DateTime.Now },
                new Studente() { Name = "Bruno Ferreira", DatePublished = DateTime.Now },
                new Studente() { Name = "Bruno Ferreira", DatePublished = DateTime.Now }
            }; 

            corso.Students.AddRange(students) ;

            using(var db = new UniversityDbContext())
            {
                db.Corso.Add(corso);

                try
                {
                    db.SaveChanges(); 
                }
                catch (Exception ex)
                {

                    throw;
                }

            }


        }



    }
    public class Studente
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DatePublished { get; set; }
        public virtual Corso Corso { get; set; }
    }
    public class Corso
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DatePublished { get; set; }
        public virtual List<Studente> Students { get; set; }
    }

}





using EFrameworkDbFirst.Lesson.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace EFrameworkDbFirst.Lesson
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var db = new corsodotnetapplicantsContext())
            {
                var items = db.Applicants.ToList();
                foreach (var item in items) 
                    Console.WriteLine(  item.Name);
            }
        }
    }
}

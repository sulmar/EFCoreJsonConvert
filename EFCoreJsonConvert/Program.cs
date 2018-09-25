using EFCoreJsonConvert.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EFCoreJsonConvert
{
    class Program
    {
        static void Main(string[] args)
        {

            MyContext context = new MyContext();

            context.Database.EnsureCreated();

            AddPerson(context);

            GetPerson(context);

        }

        private static void GetPerson(MyContext context)
        {
            var person = context.Persons.FirstOrDefault();
        }

        private static void AddPerson(MyContext context)
        {
            var person = new Person
            {
                FirstName = "Marcin",
                LastName = "Sulecki",
                Addresses = new List<Address>
                {
                    new Address { Company = "Company 1", City = "Warsaw", Street = "Radiowa"},
                    new Address { Company = "Company 2", City = "Bydgoszcz", Street = "Dworcowa"}
                },

                Device = new Device
                {
                    Firmware = "1.0.0",
                    Model = "D001",
                    Ratio = 0.6f
                }
            };


            context.Persons.Add(person);

            context.SaveChanges();
        }
    }
}

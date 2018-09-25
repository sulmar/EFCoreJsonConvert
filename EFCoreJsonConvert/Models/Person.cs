using System;
using System.Collections.Generic;
using System.Text;

namespace EFCoreJsonConvert.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public IList<Address> Addresses { get; set; }

        public Device Device { get; set; }
    }

    public class Address
    {
        public string Type { get; set; }
        public string Company { get; set; }
        public string Number { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
    }

    public class Device
    {
        public string Firmware { get; set; }
        public string Model { get; set; }
        public float Ratio { get; set; }
    }
}

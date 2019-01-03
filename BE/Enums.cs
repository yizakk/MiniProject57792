using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public enum Gender
    {
        Male,
        Female
    }

    public enum Gear
    {
        Auto,
        Manual
    }

    public enum CarType
    {
        Private,
        Bike,
        SemiTrailer,
        FullTrailer
    }

   

    public struct Parameters
    {
        public bool distance;
        public bool ReversePark;
        public bool usingMirrors;
        public bool speed;
        public bool usingVinkers;
    }


    public class Address
    {

        public string city { get; set; }
        public string Street { get; set; }
        public int BuildingNumber { get; set; }

        public Address() { }
        public Address(Address other)
        {
            city = other.city;
            Street = other.Street;
            BuildingNumber = other.BuildingNumber;
        }

    }


    public class Print
    {
        public enum PrintOptions
        {
            print_all_trainees,
            הדפסת_כל_הבוחנים,
            הדפסת_מבחנים_לפי_תאריך

        }

        public PrintOptions a = PrintOptions.print_all_trainees;

        public Print() { }
    }
}

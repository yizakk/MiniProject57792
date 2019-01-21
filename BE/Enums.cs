
using System.Collections;
using System.Reflection;

namespace BE
{
    public enum Gender
    {
        זכר,
        נקבה
    }

    public enum Gear
    {
        אוטומטי,
        ידני
    }

    public enum CarType
    {
        פרטי,
        אופנוע,
        משאית_קטנה,
        פול_טריילר
    }


    public class Parameters
    {
        public Parameters() { }

        public Parameters(Parameters other)
        {
            if (other != null)
            {
                Distance = other.Distance;
                ReversePark = other.ReversePark;
                UsingMirrors = other.UsingMirrors;
                Speed = other.Speed;
                UsingVinkers = other.UsingVinkers;
            }
        }

        private  bool _distance;
        private  bool _reversePark;
        private  bool _usingMirrors;
        private  bool _speed;
        private  bool _usingVinkers;

        public bool Distance { get; set; }
        public bool ReversePark { get => _reversePark; set => _reversePark = value; }
        public bool UsingMirrors { get => _usingMirrors; set => _usingMirrors = value; }
        public bool Speed { get => _speed; set => _speed = value; }
        public bool UsingVinkers { get => _usingVinkers; set => _usingVinkers = value; }
        //  public int  count_parameters = 5;
    }

    public class Address
    {
        public string City { get; set; }
        public string Street { get; set; }
        public int? BuildingNumber { get; set; }


        public Address()
        {
            City = "";
            Street = "";
            BuildingNumber = null;
        }

        public Address(Address other)
        {
            City = other.City;
            Street = other.Street;
            BuildingNumber = other.BuildingNumber;
        }

        public override string ToString()
        {
            return City + " "+ Street +" "+ BuildingNumber.ToString();
        }
    }

}

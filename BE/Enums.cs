
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

        public Address()
        {
            city = "";
            Street = "";
        }

        public Address(Address other)
        {
            city = other.city;
            Street = other.Street;
            BuildingNumber = other.BuildingNumber;
        }
    }
}

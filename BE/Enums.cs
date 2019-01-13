
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
            if (other!=null)
            {
                Distance = other.Distance;
                ReversePark = other.ReversePark;
                UsingMirrors = other.UsingMirrors;
                Speed = other.Speed;
                UsingVinkers = other.UsingVinkers;
            }
        }
        public bool Distance { get; set; }
        public bool ReversePark { get; set; }
        public bool UsingMirrors { get; set; }
        public bool Speed { get; set; }
        public bool UsingVinkers { get; set; }
        public int  count_parameters = 5;

        //public static int Count {
        //    get
        //    {
        //        foreach(PropertyInfo item in Parameters )
        //    }
        //}

        //public static string ToStringProperty<T>(this T t)
        //{
        //    string str = "";
        //    foreach (PropertyInfo item in t.GetType().GetProperties())
        //        str += "\n" + item.Name + ": " + item.GetValue(t, null);
        //    return str;
        //}

        //public IEnumerator GetEnumerator()
        //{
        //    foreach (var item in Yomit)
        //    {
        //        yield return item;
        //    }
        //}
    }

    public class Address
    {
        public string City { get; set; }
        public string Street { get; set; }
        public int BuildingNumber { get; set; }

        public Address()
        {
            City = "";
            Street = "";
        }

        public Address(Address other)
        {
            City = other.City;
            Street = other.Street;
            BuildingNumber = other.BuildingNumber;
        }
    }
}

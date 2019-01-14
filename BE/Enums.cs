
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

        public override string ToString()
        {
            return City + " "+ Street +" "+ BuildingNumber.ToString();
        }
    }

    public class Schedule
    {
        public bool[][] Data { get; set; } = new bool[5][];

        //public override string ToString()
        //{
        //    int starttime = 9;
        //    bool oved = false;
        //    string result = null;
        //    string hayom = null;
        //    for (int i = 0; i < 5; i++)
        //    {
        //        oved = false;
        //        hayom = null;
        //        //result += ((Day)i).ToString() + "\n";
        //        for (int j = 0; j < 6; j++)
        //        {
        //            if (Data[i][j] == true)
        //            {
        //                oved = true;
        //                hayom += "\t" + (starttime + j) + ":00-";
        //                hayom += (starttime + j + 1).ToString() + ":00\n";
        //            }
        //        }
        //        if (oved == true)
        //        {
        //            result += ((Day)i).ToString() + "\n";
        //            result += hayom;
        //        }
        //    }
        //    return result.Substring(0, result.Length - 1);
        //}

        //public Schedule()
        //{
        //    for (int i = 0; i < Configuration.WorkDays; i++)
        //        for (int j = 0; j < Configuration.WorkHours; j++)
        //            m_WorkSchedule[i, j] = false;
        //}

        //public Schedule(bool value = true)
        //{
        //    for (int i = 0; i < Configuration.WorkDays; i++)
        //        for (int j = 0; j < Configuration.WorkHours; j++)
        //            m_WorkSchedule[i, j] = value;
        //}
        //public bool[,] m_WorkSchedule = new bool[Configuration.WorkDays, Configuration.WorkHours];
        // public bool [,] workSChedule { get { return m_WorkSchedule; } set { m_WorkSchedule = value; } }
    }
}

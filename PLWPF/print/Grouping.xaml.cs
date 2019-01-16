using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for Grouping.xaml
    /// </summary>
    public partial class Grouping : UserControl
    {

        public Grouping()
        {
            InitializeComponent();

            //dg.ItemsSource = BL.BlFactory.GetBL().TraineesGroupedBySchoolName();
            DataGridHierarchialData b = new DataGridHierarchialData();
            DataGridHierarchialDataModel a = new DataGridHierarchialDataModel();
            a.Data = BL.BlFactory.GetBL().TraineesGroupedBySchoolName();
            b.Initialize();


            return;
        }

        private void Dg_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }
    }

    public class LevelToMarginConverter : IValueConverter
    {

        public int LeftMargin { get; set; }
        public int OtherMargin { get; set; }

        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int lvl = (int)value;
            return new Thickness(6 * lvl, 2, 2, 2);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    public class DataGridHierarchialDataModel
    {

        public DataGridHierarchialDataModel() { Children = new List<DataGridHierarchialDataModel>(); }

        public DataGridHierarchialDataModel Parent { get; set; }
        public DataGridHierarchialData DataManager { get; set; }
        public void AddChild(DataGridHierarchialDataModel t)
        {
            t.Parent = this;
            Children.Add(t);
        }

        #region LEVEL
        private int _level = -1;
        public int Level
        {
            get
            {
                if (_level == -1)
                {
                    _level = (Parent != null) ? Parent.Level + 1 : 0;
                }
                return _level;
            }
        }
        #endregion
        public bool IsExpanded
        {
            get { return _expanded; }
            set
            {
                if (_expanded != value)
                {
                    _expanded = value;
                    if (_expanded == true)
                        Expand();
                    else
                        Collapse();
                }
            }
        }
        public bool IsVisible
        {
            get { return _visible; }
            set
            {
                if (_visible != value)
                {
                    _visible = value;
                    if (_visible)
                        ShowChildren();
                    else
                        HideChildren();
                }
            }
        }
        public bool HasChildren { get { return Children.Count > 0; } }
        public List<DataGridHierarchialDataModel> Children { get; set; }

        public object Data { get; set; } // the Data (Specify Binding as such {Binding Data.Field})

        public IEnumerable<DataGridHierarchialDataModel> VisibleDescendants
        {
            get
            {
                return Children
                    .Where(x => x.IsVisible)
                    .SelectMany(x => (new[] { x }).Concat(x.VisibleDescendants));
            }
        }

        // Expand Collapse
        private bool _expanded = false;
        private bool _visible = false;
        private void Collapse()
        {
            DataManager.RemoveChildren(this);
            foreach (DataGridHierarchialDataModel d in Children)
                d.IsVisible = false;
        }

        private void Expand()
        {
            DataManager.AddChildren(this);
            foreach (DataGridHierarchialDataModel d in Children)
                d.IsVisible = true;
        }

        // Only if this is Expanded
        private void HideChildren()
        {
            if (IsExpanded)
            {
                // Following Order is Critical
                DataManager.RemoveChildren(this);
                foreach (DataGridHierarchialDataModel d in Children)
                    d.IsVisible = false;
            }
        }
        private void ShowChildren()
        {
            if (IsExpanded)
            {
                // Following Order is Critical
                DataManager.AddChildren(this);
                foreach (DataGridHierarchialDataModel d in Children)
                    d.IsVisible = true;
            }
        }
    }

    public class DataGridHierarchialData : ObservableCollection<DataGridHierarchialDataModel>
    {

        public List<DataGridHierarchialDataModel> RawData { get; set; }
        public DataGridHierarchialData() { RawData = new List<DataGridHierarchialDataModel>(); }

        public void Initialize()
        {
            this.Clear();
            foreach (DataGridHierarchialDataModel m in RawData.Where(c => c.IsVisible).SelectMany(x => new[] { x }.Concat(x.VisibleDescendants)))
            {
                this.Add(m);
            }
        }

        public void AddChildren(DataGridHierarchialDataModel d)
        {
            if (!this.Contains(d))
                return;
            int parentIndex = this.IndexOf(d);
            foreach (DataGridHierarchialDataModel c in d.Children)
            {
                parentIndex += 1;
                this.Insert(parentIndex, c);
            }
        }

        public void RemoveChildren(DataGridHierarchialDataModel d)
        {
            foreach (DataGridHierarchialDataModel c in d.Children)
            {
                if (this.Contains(c))
                    this.Remove(c);
            }
        }

        //DataTable accTable = await DB.getDataTable("SELECT * FROM Fm3('l1')");
        //accTable.DefaultView.Sort = "iParent";

        //DataGridHierarchialData data = new DataGridHierarchialData();


    }
}



////DataTable accTable = await DB.getDataTable("SELECT * FROM Fm3('l1')");
////accTable.DefaultView.Sort = "iParent";
//var accTable = BL.BlFactory.GetBL().TraineesGroupedBySchoolName();
//Action<DataRowView, DataGridHierarchialDataModel> Sort = null;
//Sort = new Action<DataRowView, DataGridHierarchialDataModel>((row, parent) =>
//{
//    DataGridHierarchialDataModel t = new DataGridHierarchialDataModel() { Data = row, DataManager = b };
//    if (row["iGroup"].ToString() == "1")
//    {
//        foreach (var r in accTable)//.DefaultView.FindRows(row["iSmajId"]))
//            Sort(r, t);
//    }
//    parent.AddChild(t);
//});

//foreach (var r in accTable)//.DefaultView.FindRows(0))
//{
//    DataGridHierarchialDataModel t = new DataGridHierarchialDataModel() { Data = r, DataManager = b };
//    if (r["iGroup"].ToString() == "1")
//    {
//        foreach (var rf in accTable)//.DefaultView.FindRows(r["iSmajId"]))
//            Sort(rf, t);
//    }

//    t.IsVisible = true; // first layer
//    b.RawData.Add(t);
//}
//b.Initialize();
//dg.ItemsSource = b;

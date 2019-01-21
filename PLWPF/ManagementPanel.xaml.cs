using System;
using System.Collections.Generic;
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
    /// Interaction logic for ManagementPanel.xaml
    /// </summary>
    public partial class ManagementPanel : UserControl
    {
        BL.IBL bl = BL.BlFactory.GetBL();
        public ManagementPanel()
        {
            InitializeComponent();
            // setting the data context of the left grid, which has the editable content - to the properies
            // of the configuration class (using reflection)
            FieldsPanel.DataContext = typeof(BE.Configuration).GetProperties();

            #region Adding each field of Configuration as a text block + box
            //foreach (var item in typeof(BE.Configuration).GetProperties())
            //{
            //    if (!item.Name.Contains("Pass"))
            //    {
            //        TextBlock block = new TextBlock();
            //        block.Name = item.Name + "Block";
            //        block.Text = item.Name;
            //        LabelPanel.Children.Add(block);
            //        TextBox box = new TextBox();
            //        box.Name = item.Name + "Box";
            //        box.KeyDown += TesterPass_KeyDown;
            //        box.Text = Convert.ToString(item.GetValue(item));
            //        FieldsPanel.Children.Add(box);
            //    }
            //}
            #endregion
        }
        /// <summary>
        /// The button is disabled , we use "enter" key instead
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    BE.Configuration.MasterPassword = MasterPassword.Password;
            //    BE.Configuration.TesterPassword = TesterPassword.Password;
            //    bl.UpdateConfig();
            //}
            //catch
            //{
            //    MessageBox.Show("קרתה תקלה בשינוי , אנא נסה שנית!");
            //    return;
            //}
            //MessageBox.Show("העדכון בוצע בהצלחה!");
            //Data.MainUserControl = new HomePanel();
        }
        /// <summary>
        /// This event handler is made to update the fields in the configuration to the user-input data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TesterPass_KeyDown(object sender, KeyEventArgs e)
        {
            // the update trigger is - when user presses the "ENTER" key
            if (e.Key == Key.Enter)
            {
                // if sender is a PasswordBox field
                if (sender is PasswordBox box)
                {
                    try
                    {
                        typeof(BE.Configuration).GetProperty(box.Name).SetValue(box.Name, box.Password);
                        bl.UpdateConfig();
                    }
                    catch
                    {
                        MessageBox.Show("קרתה תקלה בשינוי , אנא נסה שנית");
                        return;
                    }

                    MessageBox.Show("העדכון בוצע בהצלחה!");
                    Data.MainUserControl = new ManagementPanel();
                }

                else if(sender is TextBox TBox)
                {
                    //try
                    //{
                        typeof(BE.Configuration).GetProperty(TBox.Name).SetValue(TBox.Name, TBox.Text);
                        bl.UpdateConfig();
                    //}
                    //catch
                    //{
                    //    MessageBox.Show("קרתה תקלה בשינוי , אנא נסה שנית");
                    //    return;
                    //}

                    MessageBox.Show("העדכון בוצע בהצלחה!");
                    Data.MainUserControl = new ManagementPanel();
                }
            }
        }
    }
}

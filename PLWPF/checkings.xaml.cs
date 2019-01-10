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
    /// Interaction logic for checkings.xaml
    /// </summary>
    public partial class checkings : UserControl
    {
        BL.IBL bl = BL.BlFactory.GetBL();
        public checkings()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                //Load your data here and assign the result to the CollectionViewSource.
                System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["Resource Key for CollectionViewSource"];
                myCollectionViewSource.Source = bl.TestersGroupedByCarType();
            }
        }
    }
}



/*
     <Style TargetType="{x:Type ComboBox}">
        <Setter>
            <Setter.Property>
                ItemsPanel
            </Setter.Property>
            <Setter.Value>
                <ItemsPanelTemplate>
                    <ItemsPanelTemplate.Resources>
                        <StackPanel x:Key="Sta">

                        </StackPanel>
                    </ItemsPanelTemplate.Resources>
                </ItemsPanelTemplate>
            </Setter.Value>
        </Setter>

    </Style>
     
     





חלונית הוספת טסט - 
DATA GRID 
שלא גולל , 
ניסיון לשנות אותו ל
LIST VIEW
            <DataGrid x:Name="traineeDataGrid" VerticalAlignment="Center" HorizontalAlignment="Center" Grid.Row="2" Grid.Column="0" Grid.ColumnSpan="2" AutoGenerateColumns="False" EnableRowVirtualization="True" ItemsSource="{Binding}" Grid.RowSpan="1" RowDetailsVisibilityMode="VisibleWhenSelected">
                <DataGrid.Columns>
                    <DataGridTemplateColumn x:Name="idColumn" Header="Id" Width="auto">
                        <DataGridTemplateColumn.CellTemplate>
                            <DataTemplate>
                                <TextBlock Text="{Binding Id}"/>
                            </DataTemplate>
                        </DataGridTemplateColumn.CellTemplate>
                    </DataGridTemplateColumn>
                    <DataGridTemplateColumn x:Name="fullNameColumn" Header="Full Name" IsReadOnly="True" Width="auto">
                        <DataGridTemplateColumn.CellTemplate>
                            <DataTemplate>
                                <TextBlock Text="{Binding FullName}"/>
                            </DataTemplate>
                        </DataGridTemplateColumn.CellTemplate>
                    </DataGridTemplateColumn>
                    <DataGridTemplateColumn x:Name="lastTestColumn" Header="Last Test" Width="SizeToHeader">
                        <DataGridTemplateColumn.CellTemplate>
                            <DataTemplate>
                                <TextBlock Text="{Binding LastTest}"/>
                            </DataTemplate>
                        </DataGridTemplateColumn.CellTemplate>
                    </DataGridTemplateColumn>
                </DataGrid.Columns>
            </DataGrid>

     */

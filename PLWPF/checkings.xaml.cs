﻿using System;
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
            //DataGridMain.DataContext = bl.TestersGroupedByCarType(true);
        }


        private void TraineeButton_Click(object sender, RoutedEventArgs e)
        {
            Data.MainUserControl = new AddStudent();

        }

        private void TesterButton_Click(object sender, RoutedEventArgs e)
        {
            Data.MainUserControl = new AddTester();

        }

        private void AddTestButton_Click(object sender, RoutedEventArgs e)
        {
            Data.MainUserControl = new AddTest();

        }

        private void UpTraineeButton_Click(object sender, RoutedEventArgs e)
        {
            Data.MainUserControl = new UpStudent();
            //Data.MainUserControl = new HomePanel();




        }

        private void UpTesterButton_Click(object sender, RoutedEventArgs e)
        {
            Data.MainUserControl = new UpTester();

        }

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            Data.MainUserControl = new PrintOptions();

        }

        private void DelTesterButton_Click(object sender, RoutedEventArgs e)
        {
            Data.MainUserControl = new DeleteTester();

        }

        private void DelTraineeButton_Click(object sender, RoutedEventArgs e)
        {
            Data.MainUserControl = new DeleteTrainee();

        }

        private void UpTestButton1_Click(object sender, RoutedEventArgs e)
        {
            Data.MainUserControl = new UpdateTest();

        }


        private void GroupingButton_Click(object sender, RoutedEventArgs e)
        {

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





    ניסיון ל
    hierarchicak data grid



    <UserControl x:Class="PLWPF.GroupingPrinting"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006" 
             xmlns:d="http://schemas.microsoft.com/expression/blend/2008" 
             mc:Ignorable="d" 
             d:DesignHeight="450" d:DesignWidth="800">

    <UserControl.Resources>
        <StaticResource x:Key="MetroDataGridCell" >
            
        </StaticResource>
    </UserControl.Resources>
    <Grid>
        <DataGrid x:Name="dg" AutoGenerateColumns="False" IsReadOnly="False" CanUserAddRows="False" GridLinesVisibility="All" ColumnWidth="*">
            <DataGrid.Columns>
                <DataGridTextColumn Header="Name" Binding="{Binding Data.sName}">
                    <DataGridTextColumn.CellStyle>
                        <Style TargetType="DataGridCell" BasedOn="{StaticResource MetroDataGridCell}">
                            <Setter Property="Template">
                                <Setter.Value>

                                    <ControlTemplate TargetType="DataGridCell">
                                        <Border BorderBrush="{TemplateBinding BorderBrush}"
                                        BorderThickness="{TemplateBinding BorderThickness}"
                                        Background="{TemplateBinding Background}"
                                        SnapsToDevicePixels="{TemplateBinding SnapsToDevicePixels}">

                                            <StackPanel Orientation="Horizontal">
                                                <ToggleButton x:Name="Expander"                                               
                                          Margin="{Binding Level,Converter={StaticResource LevelToIndentConverter}}"
                                          IsChecked="{Binding Path=IsExpanded, UpdateSourceTrigger=PropertyChanged}"
                                          ClickMode="Press" >
                                                    <ToggleButton.Style>
                                                        <Style  TargetType="{x:Type ToggleButton}">
                                                            <Setter Property="Focusable" Value="False"/>
                                                            <Setter Property="Width" Value="19"/>
                                                            <Setter Property="Height" Value="13"/>
                                                            <Setter Property="Template">
                                                                <Setter.Value>
                                                                    <ControlTemplate TargetType="{x:Type ToggleButton}">
                                                                        <Border Width="19" Height="13" Background="Transparent">
                                                                            <Border Width="9" Height="9"
                                                                              BorderThickness="0"
                                                                              BorderBrush="#FF7898B5"
                                                                              CornerRadius="1"
                                                                              SnapsToDevicePixels="true">
                                                                                <Border.Background>
                                                                                    <SolidColorBrush Color="Transparent"/>
                                                                                    <!--
                                                                                    <LinearGradientBrush StartPoint="0,0"
                                                                                        EndPoint="1,1">
                                                                                        <LinearGradientBrush.GradientStops>
                                                                                            <GradientStop Color="White"
                                                                                    Offset=".2"/>
                                                                                            <GradientStop Color="#FFC0B7A6"
                                                                                    Offset="1"/>
                                                                                        </LinearGradientBrush.GradientStops>
                                                                                    </LinearGradientBrush>
                                                                                -->
                                                                                </Border.Background>
                                                                                <Path x:Name="ExpandPath"                                      
                                                                            Data="M0,0 L0,6 L6,0 z"
                                                                            Fill="Transparent"
                                                                            Stroke="{DynamicResource BlackBrush}" Margin="1,2,1,1">
                                                                                    <Path.RenderTransform>
                                                                                        <RotateTransform Angle="135"
                                                                                     CenterY="3"
                                                                                     CenterX="3" />
                                                                                    </Path.RenderTransform>
                                                                                </Path>
                                                                                <!--
                                                                            <Path x:Name="ExpandPath"
                                                                            Margin="1,1,1,1"
                                                                            Fill="Black"
                                                                            Data="M 0 2 L 0 3 L 2 3 L 2 5 L 3 5 L 3 3 L 5 3 L 5 2 L 3 2 L 3 0 L 2 0 L 2 2 Z"/>
                                                                            -->
                                                                            </Border>
                                                                        </Border>
                                                                        <ControlTemplate.Triggers>
                                                                            <Trigger Property="IsChecked"
                                                                            Value="True">
                                                                                <Setter Property="RenderTransform"
                                                                                TargetName="ExpandPath">
                                                                                    <Setter.Value>
                                                                                        <RotateTransform Angle="180"
                                                                                     CenterY="3"
                                                                                     CenterX="3" />
                                                                                    </Setter.Value>
                                                                                </Setter>
                                                                                <Setter Property="Fill"
                                                                                TargetName="ExpandPath"
                                                                                Value="{DynamicResource GrayBrush1}" />
                                                                                <Setter Property="Stroke"
                                                                                TargetName="ExpandPath"
                                                                                Value="{DynamicResource BlackBrush}" />

                                                                                <!--
                                                                                    <Setter Property="Data"
                                                                            TargetName="ExpandPath"
                                                                            Value="M 0 2 L 0 3 L 5 3 L 5 2 Z"/>
                                                                            -->
                                                                            </Trigger>
                                                                        </ControlTemplate.Triggers>
                                                                    </ControlTemplate>
                                                                </Setter.Value>
                                                            </Setter>
                                                        </Style>
                                                    </ToggleButton.Style>
                                                </ToggleButton>

                                                <ContentPresenter ContentTemplate="{TemplateBinding ContentTemplate}"
                                                        Content="{TemplateBinding Content}"
                                                        ContentStringFormat="{TemplateBinding ContentStringFormat}"
                                                        Margin="{TemplateBinding Padding}"
                                                        SnapsToDevicePixels="{TemplateBinding SnapsToDevicePixels}"
                                                        VerticalAlignment="{TemplateBinding VerticalContentAlignment}"
                                                        HorizontalAlignment="{TemplateBinding HorizontalContentAlignment}" />


                                            </StackPanel>
                                        </Border>
                                        <ControlTemplate.Triggers>
                                            <DataTrigger Binding="{Binding HasChildren}" Value="False">
                                                <Setter TargetName="Expander" Property="Visibility" Value="Hidden"/>
                                            </DataTrigger>
                                        </ControlTemplate.Triggers>
                                    </ControlTemplate>
                                </Setter.Value>
                            </Setter>
                        </Style>
                    </DataGridTextColumn.CellStyle>
                </DataGridTextColumn>
                <DataGridTextColumn Header="Code" Binding="{Binding Data.sCode}"/>
                <DataGridTextColumn Header="Type" Binding="{Binding Data.c867x1}"/>

            </DataGrid.Columns>
        </DataGrid>
    </Grid>
 
</UserControl>
     */

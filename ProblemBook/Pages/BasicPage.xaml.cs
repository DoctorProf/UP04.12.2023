using ProblemBook.DataBase;
using ProblemBook.DataBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
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
using ProblemBook.Converter;
using Microsoft.EntityFrameworkCore;

namespace ProblemBook.Pages
{
    /// <summary>
    /// Логика взаимодействия для BasicPage.xaml
    /// </summary>
    public partial class BasicPage : Page
    {
        public static DataGridTextColumn CreateColumn(string header, string binding)
        {
            DataGridTextColumn column = new()
            {
                Header = header,
                IsReadOnly = true,
            };
            Binding textBinding = new(binding)
            {
                Converter = new TruncateStringConverter()
            };
            column.Binding = textBinding;
            return column;
        }
        public static void UpdateTable(DataGrid ProblemTable)
        {
            ProblemTable.ItemsSource = null;
            ProblemTable.ItemsSource = DataBaseContext.Instance.Problems.Include("Type").ToList();
        }
        public BasicPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ProblemTable.Columns.Clear();
            ProblemTable.Columns.Add(CreateColumn("Created", "CreateDate"));
            ProblemTable.Columns.Add(CreateColumn("ShortName", "ShortName"));
            ProblemTable.Columns.Add(CreateColumn("Tags", "Tags"));
            ProblemTable.Columns.Add(CreateColumn("Description", "FullDescription"));
            ProblemTable.Columns.Add(CreateColumn("PlannedDate", "PlannedDate"));
            ProblemTable.Columns.Add(CreateColumn("DaysLeft", "DaysLeft"));
            ProblemTable.Columns.Add(CreateColumn("Completion", "DateСompletion"));
            ProblemTable.Columns.Add(CreateColumn("Type", "Type.Name"));
            UpdateTable(ProblemTable);
        }
        private void TableDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ProblemTable.SelectedItem != null)
            {
                Problem problem = (Problem)ProblemTable.SelectedItem;
                EditPage editPage = new(problem);
                Navigate.Navigate.СurrentFrame.Navigate(editPage);
            }
        }
        private void ClickOnBack(object sender, RoutedEventArgs e)
        {
            EntryPage entryPage = new();
            Navigate.Navigate.СurrentFrame.Navigate(entryPage);
        }
        private void ClickOnAdd(object sender, RoutedEventArgs e)
        {
            Problem problem = new();
            DataBaseContext.Instance.Problems.Add(problem);
            EditPage editPage = new(problem);
            Navigate.Navigate.СurrentFrame.Navigate(editPage);
        }
        private void ClickOnRemove(object sender, RoutedEventArgs e)
        {
            if (ProblemTable.SelectedItem != null)
            {
                Problem problem = (Problem)ProblemTable.SelectedItem;
                DataBaseContext.Instance.Problems.Remove(problem);
                DataBaseContext.Instance.SaveChanges();
                UpdateTable(ProblemTable);
            }

        }
        private void ClickOnOpenDescription(object sender, RoutedEventArgs e)
        {
            if (ProblemTable.SelectedItem != null)
            {
                Description discriptionPage = new((Problem)ProblemTable.SelectedItem);
                Navigate.Navigate.СurrentFrame.Navigate(discriptionPage);
            }
        }
        private void ClickOnOResetFilters(object sender, RoutedEventArgs e)
        {
            RadioToday.IsChecked = false;
            RadioYasterday.IsChecked = false;
            RadioTask.IsChecked = false;
            RadioNote.IsChecked = false;
            RadioCompletion.IsChecked = false;
            RadioNoCompletion.IsChecked = false;
            FilterDate.SelectedDate = null;
            UpdateTable(ProblemTable);
        }
        private void RadioButtonToday(object sender, RoutedEventArgs e)
        {
            RadioTask.IsChecked = false;
            RadioNote.IsChecked = false;
            RadioCompletion.IsChecked = false;
            RadioNoCompletion.IsChecked = false;
            ProblemTable.ItemsSource = null;
            string date = DateTime.Now.ToString("d");
            ProblemTable.ItemsSource = DataBaseContext.Instance.Problems.Include("Type").Where(p => p.CreateDate == date).ToList();
            FilterDate.SelectedDate = null;
        }

        private void RadioButtonYasterday(object sender, RoutedEventArgs e)
        {
            RadioTask.IsChecked = false;
            RadioNote.IsChecked = false;
            RadioCompletion.IsChecked = false;
            RadioNoCompletion.IsChecked = false;
            ProblemTable.ItemsSource = null;
            string yeasterdayDateStr = DateTime.Now.AddDays(-1).Date.ToString("d");
            ProblemTable.ItemsSource = DataBaseContext.Instance.Problems.Include("Type").Where(p => p.CreateDate == yeasterdayDateStr).ToList();
            FilterDate.SelectedDate = null;
        }

        private void FilterDateChanged(object sender, SelectionChangedEventArgs e)
        {
            RadioToday.IsChecked = false;
            RadioYasterday.IsChecked = false;
            RadioCompletion.IsChecked = false;
            RadioNoCompletion.IsChecked = false;
            ProblemTable.ItemsSource = null;
            if (FilterDate.SelectedDate == null) return;
            string date = ((DateTime)FilterDate.SelectedDate).ToString("d");
            ProblemTable.ItemsSource = DataBaseContext.Instance.Problems.Include("Type").Where(p => p.CreateDate == date || p.PlannedDate == date).ToList();
        }
        private void RadioButtonTask(object sender, RoutedEventArgs e)
        {
            RadioToday.IsChecked = false;
            RadioYasterday.IsChecked = false;
            RadioCompletion.IsChecked = false;
            RadioNoCompletion.IsChecked = false;
            ProblemTable.ItemsSource = null;
            ProblemTable.ItemsSource = DataBaseContext.Instance.Problems.Include("Type").Where(p => p.Type ==
            DataBaseContext.Instance.ProblemTypes.ToList()[1]).ToList();
        }
        private void RadioButtonNote(object sender, RoutedEventArgs e)
        {
            RadioToday.IsChecked = false;
            RadioYasterday.IsChecked = false;
            RadioCompletion.IsChecked = false;
            RadioNoCompletion.IsChecked = false;
            ProblemTable.ItemsSource = null;
            ProblemTable.ItemsSource = DataBaseContext.Instance.Problems.Include("Type").Where(p => p.Type ==
                        DataBaseContext.Instance.ProblemTypes.ToList()[0]).ToList();
        }
        private void RadioButtonCompletion(object sender, RoutedEventArgs e)
        {
            RadioToday.IsChecked = false;
            RadioYasterday.IsChecked = false;
            RadioNote.IsChecked = false;
            RadioTask.IsChecked = false;
            ProblemTable.ItemsSource = null;
            ProblemTable.ItemsSource = DataBaseContext.Instance.Problems.Include("Type").Where(p => p.Type ==
            DataBaseContext.Instance.ProblemTypes.ToList()[1] && p.DateСompletion != "").ToList();
        }
        private void RadioButtonNoCompletion(object sender, RoutedEventArgs e)
        {
            RadioToday.IsChecked = false;
            RadioYasterday.IsChecked = false;
            RadioNote.IsChecked = false;
            RadioTask.IsChecked = false;
            ProblemTable.ItemsSource = null;
            ProblemTable.ItemsSource = DataBaseContext.Instance.Problems.Include("Type").Where(p => p.Type ==
                        DataBaseContext.Instance.ProblemTypes.ToList()[1] && p.DateСompletion == "").ToList();
        }
        private void FilterShortName_TextChanged(object sender, TextChangedEventArgs e)
        {
            ProblemTable.ItemsSource = null;
            ProblemTable.ItemsSource = DataBaseContext.Instance.Problems.Where(p => p.ShortName.Contains(FilterShortName.Text)).ToList();
        }

        private void FilterTags_TextChanged(object sender, TextChangedEventArgs e)
        {
            ProblemTable.ItemsSource = null;
            ProblemTable.ItemsSource = DataBaseContext.Instance.Problems.Where(p => p.Tags.Contains(FilterTags.Text)).ToList();
        }
    }
}

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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProblemBook.Pages
{
    /// <summary>
    /// Логика взаимодействия для BasicPage.xaml
    /// </summary>
    public partial class BasicPage : Page
    {
        public static int FilterDateCheck {  get; set; }
        public static int FilterTypeCheck { get; set; }
        public static int FilterCompletionCheck { get; set; }
        public static int FilterFieldsCheck { get; set; }


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
        
        public BasicPage()
        {
            InitializeComponent();
        }
        public void UpdateTable(DataGrid ProblemTable)
        {
            ProblemTable.ItemsSource = null;
            List<Problem> problems = DataBaseContext.Instance.Problems.Include("Type").ToList();
            if (FilterDateCheck == 1)
            {
                string date = DateTime.Now.ToString("d");
                problems = problems.Where(p => p.CreateDate == date).ToList();
            }
            if (FilterDateCheck == 2)
            {
                string yeasterdayDateStr = DateTime.Now.AddDays(-1).Date.ToString("d");
                problems = problems.Where(p => p.CreateDate == yeasterdayDateStr).ToList();
            }
            if (FilterDateCheck == 3)
            {
                if (FilterDatePick.SelectedDate != null)
                {
                    string date = ((DateTime)FilterDatePick.SelectedDate).ToString("d");
                    problems = problems.Where(p => p.CreateDate == date || p.PlannedDate == date).ToList();
                }
            }
            if (FilterTypeCheck == 1)
            {

                problems = problems.Where(p => p.Type == DataBaseContext.Instance.ProblemTypes.ToList()[1]).ToList();

            }
            if (FilterTypeCheck == 2)
            {
                 problems = problems.Where(p => p.Type == DataBaseContext.Instance.ProblemTypes.ToList()[0]).ToList();
            }
            if (FilterCompletionCheck == 1)
            {
                problems = problems.Where(p => p.Type ==
                           DataBaseContext.Instance.ProblemTypes.ToList()[1] && p.DateСompletion != "").ToList();
            }
            if (FilterCompletionCheck == 2)
            {
                problems = problems.Where(p => p.Type ==
                           DataBaseContext.Instance.ProblemTypes.ToList()[1] && p.DateСompletion == "").ToList();
            }
            if (FilterFieldsCheck == 1)
            {
                problems = problems.Where(p => p.ShortName.ToLower().Contains(FilterShortName.Text.ToLower())).ToList();
            }
            if (FilterFieldsCheck == 2)
            {
                problems = problems.Where(p => p.Tags.ToLower().Contains(FilterTags.Text.ToLower())).ToList();
            }
            ProblemTable.ItemsSource = problems;
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ProblemTable.Columns.Clear();
            ProblemTable.Columns.Add(CreateColumn("Created", "CreateDate"));
            ProblemTable.Columns.Add(CreateColumn("ShortName", "ShortName"));
            ProblemTable.Columns.Add(CreateColumn("Tags", "Tags"));
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
                MessageBoxResult result = MessageBox.Show("Хотите удалить запись?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if(result == MessageBoxResult.Yes) 
                {
                    Problem problem = (Problem)ProblemTable.SelectedItem;
                    DataBaseContext.Instance.Problems.Remove(problem);
                    DataBaseContext.Instance.SaveChanges();
                    UpdateTable(ProblemTable);
                }
                else
                {
                    return;
                }
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
            FilterDatePick.SelectedDate = null;
            FilterTags.Text = null;
            FilterShortName.Text = null;
            FilterDateCheck = 0;
            FilterFieldsCheck = 0;
            FilterTypeCheck = 0;
            FilterCompletionCheck = 0;
            UpdateTable(ProblemTable);
        }
        private void RadioButtonToday(object sender, RoutedEventArgs e)
        {
            FilterDateCheck = 1;
            UpdateTable(ProblemTable);
        }

        private void RadioButtonYasterday(object sender, RoutedEventArgs e)
        {
            FilterDateCheck = 2;
            UpdateTable(ProblemTable);
        }

        private void FilterDateChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterDateCheck = 3;
            UpdateTable(ProblemTable);
        }
        private void RadioButtonTask(object sender, RoutedEventArgs e)
        {
            FilterTypeCheck = 1;
            UpdateTable(ProblemTable);
        }
        private void RadioButtonNote(object sender, RoutedEventArgs e)
        {
            FilterTypeCheck = 2;
            UpdateTable(ProblemTable);
        }
        private void RadioButtonCompletion(object sender, RoutedEventArgs e)
        {
            FilterCompletionCheck = 1;
            UpdateTable(ProblemTable);
        }
        private void RadioButtonNoCompletion(object sender, RoutedEventArgs e)
        {
            FilterCompletionCheck = 2;
            UpdateTable(ProblemTable);
        }
        private void FilterShortName_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterFieldsCheck = 1;
            UpdateTable(ProblemTable);
        }

        private void FilterTags_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterFieldsCheck = 2;
            UpdateTable(ProblemTable);
        }
    }
}

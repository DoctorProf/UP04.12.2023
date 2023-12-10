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
            Binding textBinding = new (binding)
            {
                Converter = new TruncateStringConverter()
            };
            column.Binding = textBinding;
            return column;
        }
        public static void UpdateTable(DataGrid ProblemTable)
        {
            ProblemTable.ItemsSource = null;
            ProblemTable.ItemsSource = DataBaseContext.Instance.Problems.ToList();
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
            if(ProblemTable.SelectedItem != null) 
            {
                Problem problem = (Problem)ProblemTable.SelectedItem;
                EditPage editPage = new(problem);
            }
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
            if(ProblemTable.SelectedItem != null)
            {
                Problem problem = (Problem)ProblemTable.SelectedItem;
                DataBaseContext.Instance.Problems.Remove(problem);
                DataBaseContext.Instance.SaveChanges();
                UpdateTable(ProblemTable);
            }
        }
    }
}

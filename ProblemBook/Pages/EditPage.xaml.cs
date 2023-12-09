using ProblemBook.DataBase;
using ProblemBook.DataBase.Models;
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

namespace ProblemBook.Pages
{
    /// <summary>
    /// Логика взаимодействия для EditPage.xaml
    /// </summary>
    public partial class EditPage : Page
    {
        public static Problem currentProblem {  get; set; }
        public EditPage(Problem problem)
        {
            InitializeComponent();
            currentProblem = problem;
        }

        private void ClickOnButtonBack(object sender, RoutedEventArgs e)
        {
            BasicPage basicPage = new();
            Navigate.Navigate.СurrentFrame.Navigate(basicPage);
        }
        private void ClickOnButtonSave(object sender, RoutedEventArgs e)
        {
            Problem problem = new()
            {
                CreateDate = CreateDatePick.SelectedDate.ToString(),
                ShortName = ShortNameBox.Text,
                Tags = TagsBox.Text,
                FullDescription = FullDescriptionBox.Text,
                PlannedDate = PlannedDatePick.SelectedDate.ToString(),
                DaysLeft = DaysLeftBox.Text,
                DateСompletion = СompletionDatePick.SelectedDate.ToString(),
                Type = (ProblemType)TypesCombo.SelectedItem
            };
            DataBaseContext.Instance.Problems.Add(problem);
            DataBaseContext.Instance.SaveChanges();
            BasicPage basicPage = new();
            Navigate.Navigate.СurrentFrame.Navigate(basicPage);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            TypesCombo.ItemsSource = DataBaseContext.Instance.ProblemTypes.ToList();
        }
    }
}

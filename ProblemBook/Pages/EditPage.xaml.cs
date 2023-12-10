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
            bool correct = true;
            string createDate = "";
            string shortName = ShortNameBox.Text;
            string tags = TagsBox.Text;
            string fullDescription = FullDescriptionBox.Text;
            string plannedDate = "";
            string daysLeft = "";
            string dateСompletion = "";
            ProblemType typeProblem = (ProblemType)TypesCombo.SelectedItem;
            if(createDate.Trim() == "")
            {
                createDate = DateTime.Now.ToString("d");
            }
            if(shortName.Trim() == "")
            {
                correct = false;
            } 
            else
            {
                currentProblem.ShortName = shortName;
            }
            if(tags.Trim() == "")
            {
                correct = false;
            }
            else
            {
                currentProblem.Tags = tags;
            }
            if(fullDescription.Trim() == "")
            {
                correct = false;
            }
            else
            {
                currentProblem.FullDescription = fullDescription;
            }
            if(typeProblem == null) 
            {
                correct = false;
            }  
            else
            {
                ProblemType currentType = (ProblemType)TypesCombo.SelectedItem;
                if ( currentType == DataBaseContext.Instance.ProblemTypes.ToList()[0])
                {
                    daysLeft = "-";
                    plannedDate = "-";
                    dateСompletion = "-";
                }
                else
                {
                    if(PlannedDatePick.SelectedDate.ToString().Trim() == "")
                    {
                        correct = false;
                    }
                    else
                    {
                        DateTime parsedDate;
                        if (DateTime.TryParseExact(createDate, "d", null, System.Globalization.DateTimeStyles.None, out parsedDate))
                        {
                            daysLeft = ((int)((TimeSpan)(PlannedDatePick.SelectedDate - parsedDate)).TotalDays).ToString();
                        }
                        plannedDate = ((DateTime)PlannedDatePick.SelectedDate).ToString("d");
                    }
                    dateСompletion = (bool)СompletionCheck.IsChecked ? DateTime.Now.ToString("d") : "";
                }
            }
            if (correct)
            {
                currentProblem.CreateDate = createDate;
                currentProblem.ShortName = shortName;
                currentProblem.Tags = tags;
                currentProblem.FullDescription = fullDescription;
                currentProblem.PlannedDate = plannedDate;
                currentProblem.DaysLeft = daysLeft;
                currentProblem.DateСompletion = dateСompletion;
                currentProblem.Type = typeProblem;
                DataBaseContext.Instance.SaveChanges();
                BasicPage basicPage = new();
                Navigate.Navigate.СurrentFrame.Navigate(basicPage);
            }
            else
            {
                MessageBox.Show("Тot all fields are filled in", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            TypesCombo.ItemsSource = DataBaseContext.Instance.ProblemTypes.ToList();
        }

        private void TypesCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(TypesCombo.SelectedItem != null)
            {
                if((ProblemType)TypesCombo.SelectedItem == DataBaseContext.Instance.ProblemTypes.ToList()[0])
                {
                    PlannedDatePick.IsEnabled = false;
                    СompletionCheck.IsEnabled = false;
                    PlannedDatePick.SelectedDate = DateTime.Now;
                    СompletionCheck.IsChecked = false;
                } 
                else
                {
                    PlannedDatePick.IsEnabled = true;
                    СompletionCheck.IsEnabled = true;
                }
            }
        }
    }
}

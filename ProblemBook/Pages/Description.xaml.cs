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
    /// Логика взаимодействия для Description.xaml
    /// </summary>
    public partial class Description : Page
    {
        public Problem currentProblem { get; set; }
        public Description(Problem problem)
        {
            InitializeComponent();
            currentProblem = problem;
        }
        private void ClickOnBack(object sender, RoutedEventArgs e)
        {
            BasicPage basicPage = new();
            Navigate.Navigate.СurrentFrame.Navigate(basicPage);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DescriptionBox.Text = currentProblem.FullDescription;
        }
    }
}

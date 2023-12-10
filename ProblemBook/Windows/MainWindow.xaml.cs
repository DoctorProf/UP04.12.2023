using ProblemBook.DataBase;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ProblemBook.DataBase.Models;
using ProblemBook.Pages;
using ProblemBook.Navigate;

namespace ProblemBook
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if(DataBaseContext.Instance.ProblemTypes.Count() == 0)
            {
                List<ProblemType> problemTypes = new() 
                { 
                    new ProblemType() { Name = "Note"},
                    new ProblemType() { Name = "Task"},
                };
                DataBaseContext.Instance.ProblemTypes.AddRange(problemTypes);
                DataBaseContext.Instance.SaveChanges();
            }
            Navigate.Navigate.СurrentFrame = NavigateFrame;
            Navigate.Navigate.СurrentFrame.Navigate(new EntryPage());
        }
    }
}
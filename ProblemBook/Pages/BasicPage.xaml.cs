using ProblemBook.DataBase;
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

namespace ProblemBook.Pages
{
    /// <summary>
    /// Логика взаимодействия для BasicPage.xaml
    /// </summary>
public partial class BasicPage : Page
    {
        public static DataGridTextColumn CreateColumn(string header, string binding)
        {
            DataGridTextColumn column = new DataGridTextColumn
            {
                Binding = new Binding(binding),
                Header = header,
                IsReadOnly = true
            };
            return column;
        }
        public BasicPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ProblemTable.Columns.Add(CreateColumn("Дата создания", "CreateDate"));
            ProblemTable.Columns.Add(CreateColumn("Краткое название", "ShortName"));
            ProblemTable.Columns.Add(CreateColumn("Теги", "Tags"));
            ProblemTable.Columns.Add(CreateColumn("Полное описание", "FullDescription"));
            ProblemTable.Columns.Add(CreateColumn("Планируемая дата", "PlannedDate"));
            ProblemTable.Columns.Add(CreateColumn("Дата выполнения", "DateСompletion"));
            ProblemTable.Columns.Add(CreateColumn("Задача", "Task"));
            ProblemTable.ItemsSource = DataBaseContext.Instance.Problems.ToList();
        }
        private void TableDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
    }
}

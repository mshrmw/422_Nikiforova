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
using System.Windows.Forms.DataVisualization.Charting;
using Word = Microsoft.Office.Interop.Word;
using Excel = Microsoft.Office.Interop.Excel;

namespace _422_Nikiforova.Pages
{
    /// <summary>
    /// Логика взаимодействия для DiagrammPage.xaml
    /// </summary>
    public partial class DiagrammPage : Page
    {
        private DB_PaymentEntities _context = new DB_PaymentEntities();
        public DiagrammPage()
        {
            InitializeComponent();
            ChartPayments.ChartAreas.Add(new ChartArea("Main"));

            var currentSeries = new Series("Платежи")
            {
                IsValueShownAsLabel = true
            };
            ChartPayments.Series.Add(currentSeries);

            UserComboBox.ItemsSource = _context.User.ToList(); 
            DiagrammComboBox.ItemsSource = Enum.GetValues(typeof(SeriesChartType));
        }
        private void UpdateChart(object sender, SelectionChangedEventArgs e)
        {
            if (UserComboBox.SelectedItem is User currentUser && DiagrammComboBox.SelectedItem is SeriesChartType currentType)
            {
                Series currentSeries = ChartPayments.Series.FirstOrDefault();

                currentSeries.ChartType = currentType;
                currentSeries.Points.Clear();

                var categoriesList = _context.Category.ToList();
                foreach (var category in categoriesList)
                {
                    currentSeries.Points.AddXY(category.Name, _context.Payment.ToList().Where(u => u.User == currentUser && u.Category == category).Sum(u => u.Price * u.Num));
                }
            }
        }
        private void ExcelButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void WordButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

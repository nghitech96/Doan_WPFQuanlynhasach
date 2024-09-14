using LiveCharts;
using LiveCharts.Wpf;
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
using System.Windows.Shapes;

namespace DemoRibbon
{
    /// <summary>
    /// Interaction logic for Statistic.xaml
    /// </summary>
    public partial class Statistic : Window
    {
        public Statistic()
        {
            InitializeComponent();
        }

        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {
            var screen = new Order();
            screen.Show();
        }

        private void ProductButton_Click(object sender, RoutedEventArgs e)
        {
            var screen = new Category();
            screen.Show();
        }

        private void StatisticButton_Click(object sender, RoutedEventArgs e)
        {
            var screen = new Statistic();
            screen.Show();
        }

        private void SettingButton_Click(object sender, RoutedEventArgs e)
        {
            var screen = new Setting();
            screen.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            chart.Series = new LiveCharts.SeriesCollection()
            {
                new LineSeries()
                {
                    Title = "Doanh thu trong năm 2023 (đơn vị: triệu VND)",
                    Values = new ChartValues<double> {3,5,7,4,6,3,2,3,1,3,10,11}
                },
                new ColumnSeries()
                {
                    Title = "Tổng số sách bán chạy (đơn vị: triệu cuốn)",
                    Values = new ChartValues<double> {5,6,2,7,6,7,4,2,6,5,6,3}
                }
            };

            chart.AxisX.Add(new Axis() {
                Title = "Tháng",
                Labels = new List<string>() { "1", "2", "3", "4", "5", "6", "7","8", "9", "10", "11", "12"}
            });
        }     
    }
}

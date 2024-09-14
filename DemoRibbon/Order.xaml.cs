using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
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
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Wordprocessing;
using MaterialDesignThemes.Wpf;
using ExcelDataReader;
using System.IO;
using System.Data;
using Microsoft.Win32;
using System.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using OfficeOpenXml;
using System.Threading;

namespace DemoRibbon
{
    /// <summary>
    /// Interaction logic for Order.xaml
    /// </summary>
    public partial class Order : Window
    {
        private DataSet ds;
        private object dt;
        private string rowIndex;
        private string _selectedCellValue;
        private readonly object dataGrid;

        public Order()
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

        private void SettingButton_Click(object sender, RoutedEventArgs e)
        {
            var screen = new Setting();
            screen.Show();
        }

        private void StatisticButton_Click(object sender, RoutedEventArgs e)
        {
            var screen = new Statistic();
            screen.Show();
        }

   

        private void deleteorder_Click(object sender, RoutedEventArgs e)
        {
            if (orderDataGrid.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)orderDataGrid.SelectedItem;
                selectedRow.Row.Delete();
            }
        }

        private void updateorder_Click(object sender, RoutedEventArgs e)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Set the LicenseContext

            if (orderDataGrid.ItemsSource != null)
            {
                using (ExcelPackage excelPackage = new ExcelPackage(new FileInfo("C:\\Users\\DANGNGHI\\source\\repos\\21880087\\DemoRibbon\\bin\\Debug\\net6.0-windows\\Don_hang.xlsx")))
                {
                    ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets["don_hang"];

                    // Xóa dữ liệu cũ trong sheet
                    worksheet.Cells.Clear();

                    // Lấy dữ liệu từ datagrid
                    var itemsSource = orderDataGrid.ItemsSource as IEnumerable<string>;
                    if (itemsSource != null)
                    {
                        int rowIndex = 1;
                        foreach (var item in itemsSource)
                        {
                            // Gán dữ liệu vào từng ô trong sheet
                            worksheet.Cells[rowIndex, 1].Value = itemsSource;

                            rowIndex++;
                        }
                    }

                    // Lưu file Excel
                    excelPackage.Save();
                }
            }
        }


        private void loadorder_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel Workbook|*.xlsx|Excel Workbook 97-2003|*.xls";
            openFileDialog.ValidateNames = true;

            if (openFileDialog.ShowDialog() == true)
            {
                using (var stream = File.Open(openFileDialog.FileName, FileMode.Open, FileAccess.Read))
                {
                    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                    IExcelDataReader reader;
                    if (openFileDialog.FilterIndex == 2)
                    {
                        reader = ExcelReaderFactory.CreateBinaryReader(stream);
                    }
                    else
                    {
                        reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                    }

                    ds = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true
                        }
                    });

                   cborder_sheet.Items.Clear();
                    foreach (DataTable dt in ds.Tables)
                    {
                        cborder_sheet.Items.Add(dt.TableName);
                    }
                    reader.Close();
                }
            }
        }

            private void cborder_sheet_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {
            if (ds != null && cborder_sheet.SelectedIndex >= 0 && cborder_sheet.SelectedIndex < ds.Tables.Count)
            {
                orderDataGrid.ItemsSource = ds.Tables[cborder_sheet.SelectedIndex].DefaultView;
            }
        }
            private Sach GetDataCurrentfromGridView(DataGridView dgv)
            {

                var item = (Sach)dgv.CurrentRow.DataBoundItem;
                //txtSach.Text = item.Ten;
                return item;
            }

            private void DataGridCell_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
            {
                if (sender is DataGridCell cell && !cell.IsEditing && !cell.IsReadOnly)
                {
                    // Lấy giá trị của ô được chọn
                    var selectedCellValue = cell.Content.ToString();
                }
            }

        private void orderDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ds != null && cborder_sheet.SelectedIndex >= 0 && cborder_sheet.SelectedIndex < ds.Tables.Count)
            {
                orderDataGrid.ItemsSource = ds.Tables[cborder_sheet.SelectedIndex].DefaultView;
            }
        }
    }
  }


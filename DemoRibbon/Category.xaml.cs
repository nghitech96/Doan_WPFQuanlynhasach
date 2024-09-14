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
using System.Collections;
using Avalonia.Controls;
using DataGrid = System.Windows.Controls.DataGrid;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.InkML;
using System.Diagnostics;
using Orc.Automation;
using SelectionChangedEventArgs = Avalonia.Controls.SelectionChangedEventArgs;
using ReactiveUI;

namespace DemoRibbon
{
    /// <summary>
    /// Interaction logic for Categories.xaml
    /// </summary>
    public partial class Category : System.Windows.Window
    {
        public Category() => InitializeComponent();

        SqlConnection _connection = new SqlConnection();

        private void connectButton_Click(object sender, RoutedEventArgs e)
        {
           /* _db = new TestContext();

            if (!_db.CanConnect())
            {
                MessageBox.Show("DB connected");
            } else
            {
                MessageBox.Show("Cannot connect");
            }*/
        }

        ObservableCollection<Category> _categories;
        private DataSet ds;
        private object dt;
        private string rowIndex;
        private string _selectedCellValue;
        private readonly object dataGrid;

        private void load_Click(object sender, RoutedEventArgs e)
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

                    cb_sheet.Items.Clear();
                    foreach (DataTable dt in ds.Tables)
                    {
                        cb_sheet.Items.Add(dt.TableName);
                    }
                    reader.Close();
                }
            }
        }


        /* string sql = "select * from Categories";
         var command = new SqlCommand(sql, _connection);
         var reader = command.ExecuteReader();

         _categories = new ObservableCollection<Category>();
         while (reader.Read())
         {
             int id = (int)reader["ID"];
             string name = (string)reader["Name"];

             var category = new Category()
             {
                 ID = id,
                 Name = name

             };
             _categories.Add(category);
         }*/

      //  private void cb_sheet_SelectionChanged(object sender, SelectionChangedEventArgs e) => productDataGrid.ItemsSource = ds.Tables[cb_sheet.SelectedIndex].DefaultView;

         /* private void cb_sheet_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {
            /if (ds != null && cb_sheet.SelectedIndex >= 0 && cb_sheet.SelectedIndex < ds.Tables.Count)
            {
                productDataGrid.ItemsSource = ds.Tables[cb_sheet.SelectedIndex].DefaultView;
            
        }*/

        private Sach GetDataCurrentfromGridView(DataGridView dgv)
        {

            var item = (Sach)dgv.CurrentRow.DataBoundItem;
            //txtSach.Text = item.Ten;
            return item;
        }

        private void DataGridCell_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is System.Windows.Controls.DataGridCell cell && !cell.IsEditing && !cell.IsReadOnly)
            {
                // Lấy giá trị của ô được chọn
                var selectedCellValue = cell.Content.ToString();      
            }
        }
       /* private void add_Click(object sender, RoutedEventArgs e, ColumnIndex columnIndex)
        {
             var screen = new Add();
            screen.Show(); 
            /*var excelPackage = new ExcelPackage(new FileInfo("Book1.xlsx"));
            var worksheet = excelPackage.Workbook.Worksheets["Loại sản phẩm"];
            worksheet.Cells[rowIndex, columnIndex].Value = "";
            worksheet.Cells[rowIndex, ColumnIndex + 1].Value = "";
            excelPackage.Save();

            // Reset các trường nhập liệu

            // Update the data grid with the selected cell value
            var items = new List<string> { _selectedCellValue };
            productDataGrid.ItemsSource = items; 


        }*/

        /*private void AutoResetEvent()
        {
            throw new NotImplementedException(); 
        }
        */
        /* private void Window_Load(object sender, RoutedEventArgs e)
         {
             AppConfig.Reload();
             string connectionString = AppConfig.ConnectionString();
             _connection = new SqlConnection(connectionString);
             _connection.Open();

             string sql = "select * from Category";
             var command = new SqlCommand(sql, _connection);
             var reader = command.ExecuteReader();
         } */

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            if (productDataGrid.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)productDataGrid.SelectedItem;
                selectedRow.Row.Delete();
            }
        }

        private void ProductButton_Click(object sender, RoutedEventArgs e)
        {
            var screen = new Category();
            screen.Show();
        }

        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {
            var screen = new Order();
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

        private void update_Click(object sender, RoutedEventArgs e)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Set the LicenseContext

            if (productDataGrid.ItemsSource != null)
            {
                using (ExcelPackage excelPackage = new ExcelPackage(new FileInfo("C:\\Users\\DANGNGHI\\source\\repos\\21880087\\DemoRibbon\\bin\\Debug\\net6.0-windows\\Sach.xlsx")))
                {
                    ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets["Loại sách"];

                    // Xóa dữ liệu cũ trong sheet
                    worksheet.Cells.Clear();

                    // Lấy dữ liệu từ datagrid
                    var itemsSource = productDataGrid.ItemsSource as IEnumerable<string>;
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
            /* ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var excelPackage = new ExcelPackage())
            {
                var worksheet = excelPackage.Workbook.Worksheets.Add("Sheet1");

                // Lấy dữ liệu từ DataGrid
                var itemsSource = productDataGrid.ItemsSource as IEnumerable;
                if (itemsSource != null)
                {
                    var items = itemsSource.Cast<object>().ToList();

                    // Đổ dữ liệu vào Excel
                    for (int i = 0; i < items.Count; i++)
                    {
                        var item = items[i];
                        var properties = item.GetType().GetProperties();

                        for (int j = 0; j < properties.Length; j++)
                        {
                            var property = properties[j];
                            var value = property.GetValue(item)?.ToString();
                            worksheet.Cells[i + 1, j + 1].Value = value;
                        }
                    }
                }

                // Lưu file Excel
                var filePath = "C:\\Users\\DANGNGHI\\source\\repos\\21880087\\DemoRibbon\\bin\\Debug\\net6.0-windows\\Book1.xlsx";
                FileInfo excelFile = new FileInfo(filePath);
                excelPackage.SaveAs(excelFile);
            } */
        }

        private void cb_sheet_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (ds != null && cb_sheet.SelectedIndex >= 0 && cb_sheet.SelectedIndex < ds.Tables.Count)
            {
                productDataGrid.ItemsSource = ds.Tables[cb_sheet.SelectedIndex].DefaultView;

            }
        }
    }
}

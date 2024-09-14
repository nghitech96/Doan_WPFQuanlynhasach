using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace 21880087
{
    /// <summary>
    /// Interaction logic for Add.xaml
    /// </summary>
    public partial class Add : Window
    {
        public Add()
        {
            InitializeComponent();
        }

        private ObservableCollection<Category> items = new ObservableCollection<Category>();

        public object productDataGrid { get; private set; }

        private void additem_Click(object sender, RoutedEventArgs e)
        {
            /*var txtadd = "";
            if (!string.IsNullOrEmpty(txtadd))
            {
                // Create a new item from the TextBox value
                var newItem = new Category { Name = txtadd };

                // Add the new item to the collection
                items.Add(newItem);

                // Refresh the DataGrid to display the new item
                productDataGrid.Items.Refresh();
            }
            productDataGrid.ItemsSource = items; */
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using WpfApp1;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string connectionString = "Data Source=localhost;Initial Catalog=TestBase;Integrated Security=True";

        public ObservableCollection<Partner> Partners { get; set; } = new ObservableCollection<Partner>();
        public ObservableCollection<PurchaseHistoryItem> PurchaseHistory { get; set; } = new ObservableCollection<PurchaseHistoryItem>();
        public MainWindow()
        {
            InitializeComponent();
            PartnersDataGrid.ItemsSource = Partners;
            PurchaseHistoryDataGrid.ItemsSource = PurchaseHistory;
            LoadPartners();
        }
        private void LoadPartners()
        {
            Partners.Clear();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT ID, [Тип партнера], [Наименование партнера], [Директор], [Электронная почта партнера], [Телефон партнера] FROM Partners";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Partners.Add(new Partner
                        {
                            ID = reader.GetInt32(0),
                            PartnerType = reader.IsDBNull(1) ? "" : reader.GetString(1),
                            Name = reader.IsDBNull(2) ? "" : reader.GetString(2),
                            Director = reader.IsDBNull(3) ? "" : reader.GetString(3),
                            Email = reader.IsDBNull(4) ? "" : reader.GetString(4),
                            Phone = reader.IsDBNull(5) ? "" : reader.GetString(5)
                        });
                    }
                }
            }
        }

        private void AddPartner_Click(object sender, RoutedEventArgs e)
        {
            // Логика для добавления нового партнера
        }

        private void EditPartner_Click(object sender, RoutedEventArgs e)
        {
            // Логика для редактирования выбранного партнера
        }

        private void DeletePartner_Click(object sender, RoutedEventArgs e)
        {
            // Логика для удаления выбранного партнера
        }

        private void RefreshPartners_Click(object sender, RoutedEventArgs e)
        {
            LoadPartners();
        }

        private void PartnersDataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            // Логика для отображения истории покупок выбранного партнера
        }
    }

    public class Partner
    {
        public int ID { get; set; }
        public string PartnerType { get; set; }
        public string Name { get; set; }
        public string Director { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }

    public class PurchaseHistoryItem
    {
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public DateTime SaleDate { get; set; }
    }
}

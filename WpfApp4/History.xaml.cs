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

namespace WpfApp4
{
    /// <summary>
    /// Логика взаимодействия для History.xaml
    /// </summary>
    public partial class History : Window
    {
        private readonly TestBaseEntities1 _db = new TestBaseEntities1();
        private readonly int _partnerId;

        public History(int partnerId)
        {
            InitializeComponent();
            _partnerId = partnerId;
            LoadPartnerInfo();
            LoadPurchaseHistory();
        }

        private void LoadPartnerInfo()
        {
            var partner = _db.Partners.FirstOrDefault(p => p.ID == _partnerId);
            if (partner != null)
            {
                lblPartnerInfo.Content = $"История покупок: {partner.Наименование_партнера} (ИНН: {partner.ИНН})";
            }
        }

        private void LoadPurchaseHistory()
        {
            var history = _db.Partners_product
                .Where(pp => pp.ID_Partner == _partnerId)
                .Join(_db.Product,
                    pp => pp.ID_Product,
                    p => p.ID,
                    (pp, p) => new
                    {
                        ProductName = p.Наименование_продукции,
                        Quantity = pp.Количество_продукции,
                        Date = pp.Дата_продажи,
                        Price = p.Минимальная_стоимость_для_партнера,
                        Sum = pp.Количество_продукции * p.Минимальная_стоимость_для_партнера
                    })
                .OrderByDescending(x => x.Date)
                .ToList();

            historyListView.ItemsSource = history;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
    }
}
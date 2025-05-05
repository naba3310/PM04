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

namespace WpfApp4
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly TestBaseEntities1 _db = new TestBaseEntities1();

        public MainWindow()
        {
            InitializeComponent();
            LoadPartners();
        }

        private void LoadPartners()
        {
            var listpartners = new List<Partner>();
            foreach (var a in _db.Partners.ToList())
            {
                var sum = _db.Partners_product
                    .Where(y => a.ID == y.ID_Partner)
                    .Sum(x => x.Количество_продукции);

                string sale = CalculateDiscount(sum ?? 0); // Также исправляем здесь для nullable

                listpartners.Add(new Partner
                {
                    ID = a.ID,
                    Директор = a.Директор,
                    Наименование_партнера = a.Наименование_партнера,
                    Рейтинг = "Рейтинг: " + (a.Рейтинг ?? 0).ToString(),
                    Телефон_партнера = a.Телефон_партнера,
                    Тип_партнера = a.Тип_партнера,
                    Скидка = "Скидка: " + sale
                });
            }

            listPartner.ItemsSource = listpartners;
        }

        private string CalculateDiscount(double sum)
        {
            if (sum < 10000) return "0%";
            if (sum >= 10000 && sum < 50000) return "5%";
            if (sum >= 50000 && sum < 300000) return "10%";
            return "15%";
        }

        private void btnAddPartner_Click(object sender, RoutedEventArgs e)
        {
            var addForm = new AddEditPartnerWindow();
            if (addForm.ShowDialog() == true)
            {
                var newPartner = new Partners
                {
                    Тип_партнера = addForm.PartnerType,
                    Наименование_партнера = addForm.PartnerName,
                    Директор = addForm.Director,
                    Электронная_почта_партнера = addForm.Email,
                    Телефон_партнера = addForm.Phone,
                    Юридический_адрес_партнера = addForm.Address,
                    ИНН = addForm.INN,
                    Рейтинг = addForm.Rating
                };

                _db.Partners.Add(newPartner);
                _db.SaveChanges();
                LoadPartners();
            }
        }

        private void listPartner_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (listPartner.SelectedItem is Partner selectedPartner)
            {
                var partner = _db.Partners.FirstOrDefault(p => p.ID == selectedPartner.ID);
                if (partner != null)
                {
                    var editForm = new AddEditPartnerWindow(partner);
                    if (editForm.ShowDialog() == true)
                    {
                        partner.Тип_партнера = editForm.PartnerType;
                        partner.Наименование_партнера = editForm.PartnerName;
                        partner.Директор = editForm.Director;
                        partner.Электронная_почта_партнера = editForm.Email;
                        partner.Телефон_партнера = editForm.Phone;
                        partner.Юридический_адрес_партнера = editForm.Address;
                        partner.ИНН = editForm.INN;
                        partner.Рейтинг = editForm.Rating;

                        _db.SaveChanges();
                        LoadPartners();
                    }
                }
            }
        }

        private void btnProduct_Click(object sender, RoutedEventArgs e)
        {
            if (listPartner.SelectedItem is Partner selectedPartner)
            {
                var history = new History(selectedPartner.ID);
                history.Show();
            }
            else
            {
                MessageBox.Show("Выберите партнера для просмотра истории покупок", "Информация", 
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnDeletePartner_Click(object sender, RoutedEventArgs e)
        {
            if (listPartner.SelectedItem is Partner selectedPartner)
            {
                var result = MessageBox.Show($"Вы уверены, что хотите удалить партнера {selectedPartner.Наименование_партнера}?", 
                    "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    var partner = _db.Partners.FirstOrDefault(p => p.ID == selectedPartner.ID);
                    if (partner != null)
                    {
                        // Сначала удаляем связанные записи о покупках
                        var purchases = _db.Partners_product.Where(pp => pp.ID_Partner == partner.ID).ToList();
                        _db.Partners_product.RemoveRange(purchases);

                        // Затем удаляем самого партнера
                        _db.Partners.Remove(partner);
                        _db.SaveChanges();
                        LoadPartners();
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите партнера для удаления", "Информация", 
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }

    public class Partner
    {
        public int ID { get; set; }
        public string Тип_партнера { get; set; }
        public string Наименование_партнера { get; set; }
        public string Директор { get; set; }
        public string Телефон_партнера { get; set; }
        public string Рейтинг { get; set; }
        public string Скидка { get; set; }
    }
}
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
    /// Логика взаимодействия для AddEditPartnerWindow.xaml
    /// </summary>
    public partial class AddEditPartnerWindow : Window
    {
        public string PartnerType => (cmbPartnerType.SelectedItem as ComboBoxItem)?.Content.ToString();
        public string PartnerName => txtName.Text;
        public string Director => txtDirector.Text;
        public string Email => txtEmail.Text;
        public string Phone => txtPhone.Text;
        public string Address => txtAddress.Text;
        public double INN => double.TryParse(txtINN.Text, out var inn) ? inn : 0;
        public double Rating => sliderRating.Value;

        public string WindowTitle { get; private set; }

        public AddEditPartnerWindow()
        {
            InitializeComponent();
            WindowTitle = "Добавление нового партнера";
            Title = WindowTitle;
            sliderRating.Value = 5;
        }

        public AddEditPartnerWindow(Partners partner) : this()
        {
            WindowTitle = "Редактирование партнера";
            Title = WindowTitle;

            // Установка значений из переданного партнера
            foreach (ComboBoxItem item in cmbPartnerType.Items)
            {
                if (item.Content.ToString() == partner.Тип_партнера)
                {
                    cmbPartnerType.SelectedItem = item;
                    break;
                }
            }

            txtName.Text = partner.Наименование_партнера;
            txtDirector.Text = partner.Директор;
            txtEmail.Text = partner.Электронная_почта_партнера;
            txtPhone.Text = partner.Телефон_партнера;
            txtAddress.Text = partner.Юридический_адрес_партнера;
            txtINN.Text = partner.ИНН.ToString();
            sliderRating.Value = partner.Рейтинг ?? 5;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(PartnerName))
            {
                MessageBox.Show("Введите наименование партнера", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (cmbPartnerType.SelectedItem == null)
            {
                MessageBox.Show("Выберите тип партнера", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            DialogResult = true;
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
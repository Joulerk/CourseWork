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
using PostalService.Controllers;
using System.Windows.Shapes;
using PostalService.Models;

namespace PostalService.Views
{
    /// <summary>
    /// Логика взаимодействия для PublicationWindow.xaml
    /// </summary>
    public partial class PublicationWindow : Window
    {
        private ServiceController _serviceController;

        public PublicationWindow() : this(new ServiceController()) { }
        public PublicationWindow(ServiceController serviceController)
        {
            _serviceController = serviceController;

            InitializeComponent();

            CmbPubType.ItemsSource = _serviceController.GetPubType().Select(p=>p.Name).ToList();
            CmbPubType.SelectedIndex = 0;
        }

        // кнопка ОК
        private void OK_Command(object sender, RoutedEventArgs e)
        {
            int price = 0;

            try
            {
                price= Int32.Parse(TbPrice.Text);
                if (price <= 0){
                    MessageBox.Show($"Цена не может быть меньше либо равна нулю! '{TbPrice.Text}'");
                    return;
                } // if
            }
            catch (FormatException)
            {
                MessageBox.Show($"Это не число '{TbPrice.Text}'");
                return;
            }

            if (string.IsNullOrWhiteSpace(TbIndex.Text) ||
                string.IsNullOrWhiteSpace(TbName.Text))

                MessageBox.Show("Введены неверные данные");
            else
            {

                var Publ = _serviceController.PostalDB.Publications
                .Where(p => p.Title == TbName.Text &&
                p.IdPubType == CmbPubType.SelectedIndex + 1 &&
                p.Price == price &&
                p.PubIndex == TbIndex.Text)
                .FirstOrDefault();

                // проверка, есть ли такое издание
                if (Publ == null)
                {
                        // добавить новое издание
                        Publ = _serviceController.PostalDB.Publications
                            .Add(new Publication
                            {
                                IdPubType = CmbPubType.SelectedIndex + 1,
                                Title = TbName.Text,
                                Price = price,
                                PubIndex = TbIndex.Text
                            });

                }
                else
                    MessageBox.Show("Такое издание уже есть!");

                _serviceController.PostalDB.SaveChanges();
            }

            DialogResult = true;
        } // OK_Command
    }
}

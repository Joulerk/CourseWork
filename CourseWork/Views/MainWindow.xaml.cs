using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Collections;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PostalService.Models;
using PostalService.Controllers;
using PostalService.Views;
using Microsoft.Win32;
using System.IO;
using PostalService.Helpers;
using Microsoft.VisualBasic;

namespace PostalService.Views
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // контроллер
        private ServiceController _serviceController;

        public MainWindow() : this(new ServiceController()) { }

        public MainWindow(ServiceController serviceController)
        {
            InitializeComponent();
            _serviceController = serviceController;


            // привязки коллекций к datagrid
            BindCollection(_serviceController.GetSubscribersView(), DgSubscribers);
            BindCollection(_serviceController.GetPostmansView(), DgPostmans);
            BindCollection(_serviceController.GetPublications(), DgPublications);
            BindCollection(_serviceController.GetSubscriptionsView(), DgSubscriptions);
            BindCollection(_serviceController.GetAdressesView(), DgAdresses);

        }

        // выполнение привязки коллекции
        private void BindCollection(IEnumerable list, DataGrid dataGrid)
        {
            // остановить привязку
            dataGrid.ItemsSource = null;

            // задать привязку
            dataGrid.ItemsSource = list;
        } // BindCollection


        // Закрыть приложение
        private void Exit_Click(object sender, RoutedEventArgs e) => Close();

        private void Query1_Command(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("В разработке!", "К сведению");
        } // Query1_Command

        private void AddPostman_Command(object sender, RoutedEventArgs e)
        {
            PostmanWindow postmanWindow = new PostmanWindow();

            if (postmanWindow.ShowDialog() == true)
                BindCollection(_serviceController.GetPostmansView(), DgPostmans);

        } // AddPostman_Command

        private void DeletePostman_Command(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("В разработке!", "К сведению");

            // TODO:
            //      Доделать удаление


            /*
             string input = Interaction.InputBox("Введите индекс почтальона, на которого перейдет участок:", "Индекс почтальона", "1", 10, 10);

            int index = 1;
            try
            {
                index = Int32.Parse(input);
            }
            catch (FormatException)
            {
                MessageBox.Show($"Это не число '{input}'");
            }

            if (index > 0 && index <= DgPostmans.Items.Count)
            {
              // заменить почтальона, обслуживающего районы на другого
              _serviceController.PostalDB.Districts
                  .ToList().ForEach(d=>
                  { if (d.IdPostman == DgPostmans.SelectedIndex + 1)
                          d.IdPostman = index;
                      });


                // удалить выбранного почтальона
                _serviceController.PostalDB.Postmans
                    .Remove(_serviceController.PostalDB.Postmans.Find(DgPostmans.SelectedIndex + 1));

                // сохранить изминения
                _serviceController.PostalDB.SaveChanges();

                // перезаполнить датагриды
                BindCollection(_serviceController.GetPostmansView(), DgPostmans);
                BindCollection(_serviceController.GetAdressesView(), DgAdresses);
            }
            else MessageBox.Show("Введенного вами индекса не обнаружено!");
            */
        } // DeletePostman_Command

        private void AddPublication_Command(object sender, RoutedEventArgs e)
        {
            PublicationWindow publicationWindow = new PublicationWindow();

            if (publicationWindow.ShowDialog() == true)
                BindCollection(_serviceController.GetPublicationsViews(), DgPublications);
        } // AddPublications_Command

        private void AddSubscription_Command(object sender, RoutedEventArgs e)
        {
            SubscriptionWindow subscriptionWindow = new SubscriptionWindow();

            if (subscriptionWindow.ShowDialog() == true)
                BindCollection(_serviceController.GetSubscriptionsView(), DgSubscriptions);

        } // AddSubscriptions_Command
    }
}

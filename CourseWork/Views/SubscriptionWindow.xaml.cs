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
using PostalService.Controllers;
using PostalService.Models;

namespace PostalService.Views
{
    /// <summary>
    /// Логика взаимодействия для SubscriptionWindow.xaml
    /// </summary>
    public partial class SubscriptionWindow : Window
    {
        private ServiceController _serviceController;

        public SubscriptionWindow() : this(new ServiceController()) { }

        public SubscriptionWindow(ServiceController serviceController)
        {
            _serviceController = serviceController;

            InitializeComponent();

            CmbSubscriber.ItemsSource = _serviceController.GetSubscribersAddView().Select(s => s.SurnameNP).ToList();
            CmbSubscriber.SelectedIndex = 0;

            CmbPublication.ItemsSource = _serviceController.GetPublicationsAddView().Select(p => p.Title).ToList();
            CmbPublication.SelectedIndex = 0;

            DpDateStart.SelectedDate = DateTime.Now;

        }

        // Команда ок
        private void OK_Command(object sender, RoutedEventArgs e)
        {
            int duration = 0;

            try
            {
                duration = Int32.Parse(TbDuration.Text);
                if(duration <=0 || duration > 24){
                    MessageBox.Show($"Длительность не может быть больше 24 месяцев или меньше 0! '{TbDuration.Text}'");
                    return;
                } // if

            }
            catch (FormatException)
            {
                MessageBox.Show($"Это не число '{TbDuration.Text}'");
                return;
            }

            // получить id выбранного издания
            int i = _serviceController.PostalDB.Publications.Where(p => p.Title == (string)CmbPublication.SelectedItem).FirstOrDefault().Id;

            var Sub = _serviceController.PostalDB.Subscriptions
                .Where(p => p.IdSubscriber == CmbSubscriber.SelectedIndex+1 && 
                            p.IdPublication == i &&
                            p.StartDate == DpDateStart.SelectedDate&&
                            p.Duration == duration)
                .FirstOrDefault();


            // проверка, есть ли такая подписка в подписках
            if (Sub == null){
                    // добавить новую подписку
                    Sub = _serviceController.PostalDB.Subscriptions
                        .Add(new Subscription{ 
                            IdSubscriber = CmbSubscriber.SelectedIndex + 1,
                            IdPublication= i,
                            StartDate = (DateTime)DpDateStart.SelectedDate,
                            Duration = duration });
            }else
                MessageBox.Show("Такая подписка уже есть!");

            _serviceController.PostalDB.SaveChanges();

            MessageBox.Show("Оформление квитанции в разработке!");

            DialogResult = true;
        } // OK_Command
    }
}

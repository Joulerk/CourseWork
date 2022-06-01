using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using PostalService.Controllers;
using PostalService.Models;

namespace PostalService.Views
{
    /// <summary>
    /// Логика взаимодействия для QueriesWindow.xaml
    /// </summary>
    public partial class QueriesWindow : Window
    {
        private ServiceController _serviceController;

        public QueriesWindow()
        {
            InitializeComponent();

            // очистка DataGrid
            ClearGrid(DgQueries);
        }

        // очистка DataGrid
        public void ClearGrid(DataGrid grid)=>        
            Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)(() =>
            {
                grid.ItemsSource = null;
            }));

        // обновление привязки в DataGrid
        public void UpdateBinding(DataGrid grid, IEnumerable collection)
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)(() =>
            {
                grid.ItemsSource = null;
                grid.ItemsSource = collection;
            }));
        }
        /*
        private async void Query1_Exec(object sender, ExecutedRoutedEventArgs e) => await Task.Run(() =>
        {
            
            // очистка DataGrid
            ClearGrid(DgQueries);

            // текущая дата
            DateTime date = DateTime.Now;

            // список изданий и их кол-ва
            var d =  _serviceController.PostalDB.AddressPostman("пр.Ленинский", "1", 11).ToList().Where(a=> { a.Subscriber && a.Street && a.Building && a.Apartment && a.Postman}).ToList();

            int number = 0;

            if (publications.Count > 0)
                // номер комнаты
                number = rooms[Utils.GetRand(1, rooms.Count)].Number;

            // заполнение DataGrid
            UpdateBinding(DgQueries, _serviceController.PostalDB.EachPubAmount()
                                                   .Select(c => new {
                                                       c.Id,
                                                       c.Person.Surname,
                                                       c.Person.Name,
                                                       c.Person.Patronymic,
                                                       c.Passport,
                                                       c.IsDeleted
                                                   }));
        });
            */


    }
}

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
    /// Логика взаимодействия для PostmanWindow.xaml
    /// </summary>
    public partial class PostmanWindow : Window
    {

        private ServiceController _serviceController;

        public PostmanWindow() : this(new ServiceController()) { }

        public PostmanWindow(ServiceController serviceController)
        {
            _serviceController = serviceController;

            InitializeComponent();
        }

        // кнопка ОК
        private void OK_Command(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(TbSurname.Text) ||
                string.IsNullOrWhiteSpace(TbName.Text) ||
                string.IsNullOrWhiteSpace(TbPatronymic.Text))

                MessageBox.Show("Введены неверные данные");
            else
            {

                var Pers = _serviceController.PostalDB.Persons
                    .Where(p =>
                    p.Surname == TbSurname.Text &&
                    p.Name == TbName.Text &&
                    p.Patronymic == TbPatronymic.Text)
                    .FirstOrDefault();

                // проверка, есть ли такая персона в персонах
                if (Pers == null)
                {

                    // добавить новую персону в персоны
                    Pers = _serviceController.PostalDB.Persons
                        .Add(new Person
                        {
                            Surname = TbSurname.Text,
                            Name = TbName.Text,
                            Patronymic = TbPatronymic.Text
                        });

                    // проверка на то, есть ли такой почтальон
                    if (_serviceController.PostalDB.Postmans.Where(p => p.IdPerson == Pers.Id).FirstOrDefault() == null)
                        // добавить почтальона
                        _serviceController.PostalDB.Postmans.Add(new Postman { IdPerson = Pers.Id });
                    else
                        MessageBox.Show("Такой почтальон уже есть!");

                }
                else
                     // проверка на то, есть ли такой почтальон
                     if (_serviceController.PostalDB.Postmans.Where(p => p.IdPerson == Pers.Id).FirstOrDefault() == null)
                    // добавить почтальона
                    _serviceController.PostalDB.Postmans.Add(new Postman { IdPerson = Pers.Id });
                else
                    MessageBox.Show("Такой почтальон уже есть!");


                _serviceController.PostalDB.SaveChanges();
            }

            DialogResult = true;
        } // OK_Command
    }
}

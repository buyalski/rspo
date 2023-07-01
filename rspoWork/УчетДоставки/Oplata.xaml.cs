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

namespace УчетДоставки
{
    /// <summary>
    /// Логика взаимодействия для Oplata.xaml
    /// </summary>
    public partial class Oplata : Window
    {

        public string NamePhone { get; set; }
        public int Cost { get; set; }
        public string Polom { get; set; }
        public int Balance { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public Oplata(string namePhone, int cost, string polom, int balance, string username, string password)
        {
            NamePhone = namePhone;
            Cost = cost;
            Polom = polom;
            Balance = balance;
            InitializeComponent();
            Username = username;
            Password = password;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_2(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            {
                string textBox1Value = tb1.Text;
                string textBox2Value = tb2.Text;
                string textBox3Value = tb3.Text;

                string fio = tb1.Text;
                string phoneNum = tb2.Text;
                string address = tb3.Text;

                // Проверяем, что все текстовые поля заполнены
                if (!string.IsNullOrEmpty(textBox1Value) &&
                    !string.IsNullOrEmpty(textBox2Value) &&
                    !string.IsNullOrEmpty(textBox3Value))
                {
                    // Создаем новое окно Oplata2
                    Olpata2 oplata2Window = new Olpata2(NamePhone, Cost, Polom, Balance, Username, Password, fio, phoneNum, address);

                    // Передаем необходимые значения из текстовых полей в новое окно
                    oplata2Window.l2.Content = textBox1Value;
                    oplata2Window.l3.Content = textBox2Value;
                    oplata2Window.l4.Content = textBox3Value;

                    // Закрываем текущее окно
                    this.Close();

                    // Открываем новое окно
                    oplata2Window.Show();
                }
                else
                {
                    MessageBox.Show("Please select all options before proceeding to Oplata.");
                }
            }
        }
    }
}

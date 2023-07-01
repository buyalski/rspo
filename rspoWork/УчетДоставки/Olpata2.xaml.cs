using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net;
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
using System.Xml.Linq;

namespace УчетДоставки
{
    /// <summary>
    /// Логика взаимодействия для Olpata2.xaml
    /// </summary>
    public partial class Olpata2 : Window
    {
        public string NamePhone { get; set; }
        public int Cost { get; set; }
        public string Polom { get; set; }
        public int Balance { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Fio { get; set; }
        public string PhoneNum { get; set; }
        public string Address { get; set; }
        public string connectionString = "Server=NEK1T\\MSSQLSERVER01;Database=rspo;Integrated Security=true;";
        public Olpata2(string namePhone, int cost, string polom, int balance, string username, string password, string fio, string phoneNum, string address)
        {
            NamePhone = namePhone;
            Cost = cost;
            Polom = polom;
            Balance = balance;
            Username = username;
            Password = password;
            Fio = fio;
            PhoneNum = phoneNum;
            Address = address;

            InitializeComponent();
            Load();

        }

        private void Load()
        {
            l1.Content = NamePhone;
            l6.Content = Cost;
            l5.Content = Polom;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Balance >= Cost)
            {
                Balance -= Cost;
                MessageBox.Show("Заявка успешно отправлена");

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Обновление баланса пользователя
                    string updateUserQuery = $"UPDATE users SET balance = balance - {Cost} WHERE username = '{Username}'";
                    using (SqlCommand updateUserCommand = new SqlCommand(updateUserQuery, connection))
                    {
                        // Выполнение запроса обновления баланса пользователя
                        int userRowsAffected = updateUserCommand.ExecuteNonQuery();
                    }

                    // Вставка данных в таблицу order
                    string insertOrderQuery = $"INSERT INTO [order] (name, model, mobilePhone, address, price, polom) " +
                        $"VALUES (@name, @model, @mobilePhone, @address, @price, @polom)";
                    using (SqlCommand insertOrderCommand = new SqlCommand(insertOrderQuery, connection))
                    {
                        // Параметры для вставки данных
                        insertOrderCommand.Parameters.AddWithValue("@name", Fio);
                        insertOrderCommand.Parameters.AddWithValue("@model", NamePhone);
                        insertOrderCommand.Parameters.AddWithValue("@mobilePhone", PhoneNum);
                        insertOrderCommand.Parameters.AddWithValue("@address", Address);
                        insertOrderCommand.Parameters.AddWithValue("@price", Cost);
                        insertOrderCommand.Parameters.AddWithValue("@polom", Polom);

                        // Выполнение запроса вставки данных в таблицу order
                        int orderRowsAffected = insertOrderCommand.ExecuteNonQuery();
                    }

                    connection.Close();
                }
            }
            else
            {
                MessageBox.Show("Заявка не отправлена, так как на балансе не хватает денег");
            }
        }

    }
}

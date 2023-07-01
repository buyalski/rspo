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
using System.Data.SqlClient;
namespace УчетДоставки
{
    /// <summary>
    /// Логика взаимодействия для Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        private string connectionString;
        public string User { get; set; }
        public Register()
        {
            InitializeComponent();
            connectionString = "Server=NEK1T\\MSSQLSERVER01;Database=rspo;Integrated Security=true;";

        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Image_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void ToolBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void Image_MouseDown_2(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Grid.Visibility == Visibility.Hidden)
            {
                Grid.Visibility = Visibility.Visible;
            }
            else
            {
                Grid.Visibility = Visibility.Hidden;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string username = tb1.Text;
            string password = tb2.Text;
            string confirmPassword = tb3.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }

            if (!username.EndsWith("@gmail.com"))
            {
                MessageBox.Show("Имя пользователя должно заканчиваться на @gmail.com");
                return;
            }

            if (password.Length < 6)
            {
                MessageBox.Show("Пароль должен содержать минимум 6 символов.");
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Пароли не совпадают.");
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO users (username, password, balance) VALUES (@username, @password, 0)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);

                connection.Open();
                int result = command.ExecuteNonQuery();
                connection.Close();

                if (result > 0)
                {
                    MessageBox.Show("Регистрация прошла успешно!");
                    MenuForm menuForm = new MenuForm(username, password);
                    menuForm.bal.Content = 0;
                    menuForm.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Регистрация не удалась.");
                }
            }
        }
    }
}

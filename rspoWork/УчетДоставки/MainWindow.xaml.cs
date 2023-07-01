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
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace УчетДоставки
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        private string connectionString;
        public MainWindow()
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
            if(e.ChangedButton == MouseButton.Left)
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

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string username = tb1.Text;
            string password = tb2.Text;
            Name = tb1.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Заполните все поля");
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM users WHERE username = @username AND password = @password";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);

                connection.Open();
                int result = (int)command.ExecuteScalar();

                if (result > 0)
                {
                    query = "SELECT balance FROM users WHERE username = @username";
                    command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@username", username);
                    int balance = (int)command.ExecuteScalar();

                    connection.Close();

                    MessageBox.Show("Авторизация успешна");

                    MenuForm menuForm = new MenuForm(username, password);
                    menuForm.bal.Content = balance; // Set the Balance property in MenuForm
                    menuForm.Show();

                    this.Close();
                }
                else
                {
                    connection.Close();
                    MessageBox.Show("Неправильный логин или пароль");
                }
            }
        }


        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Register register = new Register();
            register.Show();
            this.Close();
        }
    }
}

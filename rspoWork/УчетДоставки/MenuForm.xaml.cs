using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    /// Логика взаимодействия для MenuForm.xaml
    /// </summary>


    public partial class MenuForm : Window
    {
        private string connectionString;
        public string Username { get; set; }
        public string Password { get; set; }
        public MenuForm(string username, string password)
        {
            InitializeComponent();
            connectionString = "Server = NEK1T\\MSSQLSERVER01; Database = rspo; Integrated Security = true;";
            LoadBrandValues();
            LoadTypes();
            Username = username;
            Password = password;
            comboBox1.SelectionChanged += ComboBox_SelectionChanged_1;
        }

        private void LoadBrandValues()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT brand FROM brand";

                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        comboBox1.Items.Add(reader["brand"].ToString());
                    }

                    reader.Close();
                }
            }
            catch (SqlException ex)
            {
                // Обработка ошибки подключения к базе данных
                MessageBox.Show($"Ошибка подключения к базе данных: {ex.Message}");
            }
        }

        private void LoadTypes()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT typeError FROM errors"; 

                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        comboBox3.Items.Add(reader["typeError"].ToString());
                    }

                    reader.Close();
                }
            }
            catch (SqlException ex)
            {
                // Обработка ошибки подключения к базе данных
                MessageBox.Show($"Ошибка подключения к базе данных: {ex.Message}");
            }
        }
        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            string selectedBrand = comboBox1.SelectedItem as string;

            if (selectedBrand == "Apple IPhone             ")
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        string query = "SELECT model FROM modelApple";

                        SqlCommand command = new SqlCommand(query, connection);
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            comboBox2.Items.Add(reader["model"].ToString());
                        }

                        reader.Close();
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Ошибка подключения к базе данных: {ex.Message}");
                }
            }
            if (selectedBrand == "Samsung                  ")
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        string query = "SELECT model FROM modelSamsung";

                        SqlCommand command = new SqlCommand(query, connection);
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            comboBox2.Items.Add(reader["model"].ToString());
                        }

                        reader.Close();
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Ошибка подключения к базе данных: {ex.Message}");
                }
            }
            if (selectedBrand == "Xiaomi                   ")
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        string query = "SELECT model FROM modelXiaomi";

                        SqlCommand command = new SqlCommand(query, connection);
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            comboBox2.Items.Add(reader["model"].ToString());
                        }

                        reader.Close();
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Ошибка подключения к базе данных: {ex.Message}");
                }
            }
        }

        private void ComboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Проверяем, что все comboBox заполнены
            if (!string.IsNullOrEmpty(comboBox1.SelectedItem as string) &&
                !string.IsNullOrEmpty(comboBox2.SelectedItem as string) &&
                !string.IsNullOrEmpty(comboBox3.SelectedItem as string))
            {

                string namePhone = comboBox1.SelectedItem as string + " " + comboBox2.SelectedItem as string + " " + comboBox3.SelectedItem as string;
                if (!int.TryParse(costlb.Content.ToString().Replace("руб.", ""), out int cost))
                {
                    MessageBox.Show("Invalid cost value.");
                    return;
                }

                if (!int.TryParse(bal.Content.ToString(), out int balance))
                {
                    MessageBox.Show("Invalid balance value.");
                    return;
                }
                string polom = comboBox3.SelectedItem as string;
                
                // Создаем новое окно Oplata

                Oplata oplataWindow = new Oplata(namePhone, cost, polom, balance, Username, Password);

                // Закрываем текущее окно
                this.Close();

                // Открываем новое окно
                oplataWindow.Show();
            }
            else
            {
                MessageBox.Show("Please select all options before proceeding to Oplata.");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string selectedTypeError = comboBox3.SelectedItem as string;

            if (!string.IsNullOrEmpty(selectedTypeError))
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        string query = "SELECT costError FROM errors WHERE typeError = @TypeError"; // Замените на имя вашей таблицы

                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@TypeError", selectedTypeError);

                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            string cost = reader["costError"].ToString();
                            costlb.Content = cost + "руб.";
                        }
                        else
                        {
                            MessageBox.Show("No data found for the selected typeError.");
                        }

                        reader.Close();
                    }
                }
                catch (SqlException ex)
                {
                    // Обработка ошибки подключения к базе данных
                    MessageBox.Show($"Ошибка подключения к базе данных: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Please select a typeError.");
            }
        }
    }
}

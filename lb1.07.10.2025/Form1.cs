using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace lb1._07._10._2025
{
    public partial class Form1 : Form
    {
        private string connectionString = "Data Source=DESKTOP-7GJ7CVV\\MSSQLSERVER01;Initial Catalog=PROVAIDER;Integrated Security=True";

        public Form1()
        {
            InitializeComponent();
            LoadProducts();
        }

        private void LoadProducts()
        {
            try
            {
                comboBox1.Items.Clear();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT id, name, price FROM products";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string productInfo = $"{reader["name"]} - {reader["price"]} руб.";
                        comboBox1.Items.Add(productInfo);
                    }
                    reader.Close();
                }

                if (comboBox1.Items.Count > 0)
                    comboBox1.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Метод LoadProducts уже вызывается в конструкторе
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                listAddProducts.Items.Add(comboBox1.SelectedItem);
            }
            else
            {
                MessageBox.Show("Выберите продукт из списка");
            }
        }

        private void listAddProducts_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            listAddProducts.Items.Clear();
            textBox23.Text = "";
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            decimal total = 0;

            foreach (var item in listAddProducts.Items)
            {
                string itemString = item.ToString();
                // Извлекаем цену из строки формата "Название - цена руб."
                int lastDashIndex = itemString.LastIndexOf('-');
                if (lastDashIndex != -1)
                {
                    string priceString = itemString.Substring(lastDashIndex + 1)
                        .Replace("руб.", "")
                        .Trim();

                    if (decimal.TryParse(priceString, out decimal price))
                    {
                        total += price;
                    }
                }
            }
            textBox23.Text = $"{total:F2} руб.";
        }
    }
}

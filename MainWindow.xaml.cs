
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
using System.Data;
using Microsoft.Data.SqlClient;

namespace Wpf_sql_two_streams_27_02_2023
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }
        public string connectString = (@"Data Source = (localdb)\MSSQLLocalDB; " +
        "Initial Catalog = library; Integrated Security = true");
        public string sqlsel1 = "SELECT * FROM Books";
        public string sqlsel2 = "SELECT * FROM Authors";
        public async Task LoadDataAsync(string sqlsel)
        {
            DataTable dt = new DataTable();

            SqlConnection con = null;
            MessageBox.Show($"{sqlsel} началось");
            //описываем соединение 
            con = new SqlConnection(connectString);
            // создаем асинхронное подключение
           
            await con.OpenAsync();

            //создаем комманду на соединение
            SqlCommand cmd = new SqlCommand(sqlsel, con);
            //открываем соединение
           SqlDataReader data = cmd.ExecuteReader();

            using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
            {
                int line = 0;
                do
                {
                    while (await reader.ReadAsync())
                    {
                        if (line == 0)
                        {
                            for (int i = 0; i <
                            reader.FieldCount; i++)
                            {
                                dt.Columns.Add(reader.
                                GetName(i));
                            }
                            line++;
                        }
                        DataRow row = dt.NewRow();
                        for (int i = 0; i < reader.
                        FieldCount; i++)
                        {
                            row[i] = await reader.
                            GetFieldValueAsync<Object>(i);
                        }
                       dt.Rows.Add(row);
                    }
                } while (reader.NextResult());

                //выполнить запрос, занесенный
                //в объект command
                MessageBox.Show($"{sqlsel} кончилось");
            }
            con.Close();
        }

        private void BT_1_Click(object sender, RoutedEventArgs e)
        {
            LoadDataAsync(sqlsel1);
            LoadDataAsync(sqlsel2);
        }
    }
}
    


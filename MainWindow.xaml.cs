using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sql;
using System.Data;
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
        public void LoadData(string sqlsel,ref DataGrid DG)
        {
            SqlConnection con = null;
            MessageBox.Show($"{sqlsel} началось");
            //описываем соединение 
            con = new SqlConnection(connectString);
            //создаем комманду на соединение
            SqlCommand cmd = new SqlCommand(sqlsel, con);
            //открываем соединение
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
           DG.ItemsSource = dt.DefaultView;
            //выполнить запрос, занесенный
            //в объект command
            MessageBox.Show($"{sqlsel} кончилось");

            con.Close();
        }

        private void BT_1_Click(object sender, RoutedEventArgs e)
        {
            LoadData(sqlsel1, ref DG_1);
            LoadData(sqlsel2, ref DG_2);
        }
    }
}
    


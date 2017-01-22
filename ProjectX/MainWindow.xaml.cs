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
using System.Data.SQLite;
using System.Data.Linq;

namespace ProjectX
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

        private void button_Click(object sender, RoutedEventArgs e)
        {
            connectToSQLite();
        }

        private void connectToSQLite()
        {
            var conn = new SQLiteConnection(@"data source=C:\Users\Martin\Documents\Visual Studio 2015\DBSQLite\dbTest.db;Version=3;");
            conn.Open();

            var context = new DataContext(conn);
            var x = "select * from Screens";
            SQLiteCommand command = new SQLiteCommand(x, conn);
            SQLiteDataReader reader = command.ExecuteReader();

            
        }
    }
}

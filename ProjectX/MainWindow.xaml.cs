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
using System.Data.Linq.Mapping;
using System.IO;
using System.Drawing;


namespace ProjectX
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int imagePosition = 0;
        public bool rendered = false;

        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            imagePosition = 0;
           rendered = connectToSQLite(imagePosition);
            
        }

        private bool connectToSQLite(int imagePosition)
        {
            var conn = new SQLiteConnection(@"data source=C:\Users\Martin\Documents\Visual Studio 2015\DBSQLite\dbTest.db;Version=3;");
            conn.Open();

            var context = new DataContext(conn);

            

           

            var y = context.GetTable<Screens>().ToList();

           



            var item = y;
            
            var cc = y.Count();
            if (cc <= imagePosition || imagePosition < 0)
            {
                if (imagePosition < 0)
                    imagePosition = 0;
                else imagePosition--;
                return false;
            }
                



            //var labelContent  = y.FirstOrDefault().Desc;
            var description = item.ElementAt<Screens>(imagePosition).Desc;
            //var name = item.FirstOrDefault().Desc;
            label.Content = "Image Description: "+  description;

            


           // glass.Source = new BitmapImage(new Uri("lavka.jpg",UriKind.Relative));

            

             
            var imageData = item.ElementAt<Screens>(imagePosition).Screen;

            //  var ms = new MemoryStream(imageData);
            //  System.Drawing.Image image = System.Drawing.Image.FromStream(ms);

            MemoryStream memoryStream = new MemoryStream(imageData);
            
                var imageSource = new BitmapImage();
                imageSource.BeginInit();
                imageSource.StreamSource = memoryStream;
                imageSource.EndInit();

                // Assign the Source property of your image
                glass.Source = imageSource;

            



            

        //   image.Save(@"C:\Users\Martin\Documents\Visual Studio 2015\Projects\ProjectX\ProjectX\images\newImage.jpg");

            

         //  glass.Source = new BitmapImage(new Uri(@"C:\Users\Martin\Documents\Visual Studio 2015\Projects\ProjectX\ProjectX\images\newImage.jpg", UriKind.Absolute));

            conn.Close();
            return true;

        }



        [Table]
        public class Screens
        {
            [Column]
            public int Id { get; set; }
            [Column]
            public byte[] Screen { get; set; }
            [Column]
            public string Desc { get; set; }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if(rendered)
                imagePosition = imagePosition + 1;
            connectToSQLite(imagePosition);
            //imagePosition = imagePosition + 1;
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            if (imagePosition!=0)
                imagePosition = imagePosition - 1;
            connectToSQLite(imagePosition);
        }
    }
}

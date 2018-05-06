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
using System.IO;
using Finisar.SQLite;

namespace English_Test
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        private void btn3_Click(object sender, RoutedEventArgs e)
        {

        }
        int questions = 0;
        int number = -1;
        int[] id = { 0 };
        private int get()
        {
            SQLiteConnection connect = new SQLiteConnection("Data Source=questions.db;Compress=true;Version=3;");
            connect.Open();
            var cmd = new SQLiteCommand("SELECT id FROM questions;", connect);
            SQLiteDataReader reader = cmd.ExecuteReader();
            if (number == -1)
            {
                while (reader.Read())
                {
                    number++;
                }
            }
            return number;
        }
        void loadQuestion(int qId = 0)
        {
            if (questions < 10)
            {
                numLabel.Content = (++questions).ToString();
                SQLiteConnection connect = new SQLiteConnection("Data Source=questions.db;Compress=true;Version=3;");
                connect.Open();
                Random rand = new Random();
                var cmd = new SQLiteCommand("SELECT * FROM questions WHERE id=" + qId + ";", connect);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    questionLabel.Content = reader["question"].ToString();
                    int a;
                    rand = new Random();
                    a = rand.Next(1, 4);
                    rBtn1.Content = reader["answer" + a.ToString()].ToString();
                    a=func(a, reader["answer" + a.ToString()].ToString());
                    rBtn2.Content = reader["answer" + a.ToString()].ToString();
                    a=func(a, reader["answer" + a.ToString()].ToString());
                    rBtn3.Content = reader["answer" + a.ToString()].ToString();
                    a=func(a, reader["answer" + a.ToString()].ToString());
                    rBtn4.Content = reader["answer" + a.ToString()].ToString();
                    a=func(a, reader["answer" + a.ToString()].ToString());
                }
            }

        }
        string correct = "";
        private int func(int a1,string ans)
        {
            if (a1 == 4) correct = ans;
            if (a1 < 4) a1++;
            else a1 = 1;
            return a1;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            if (q == -1) q = get();
            else
            {
                if (q < number) q--;
                else q = 0;
            }
            loadQuestion(q);
        }
        int q = -1;
        private void prvBtn_Click(object sender, RoutedEventArgs e)
        {
           
            if (q == -1) q = get();
            else
            {
                if (q < number) q--;
                else q = 0;
            }
            if (questions > 1)
            {
                questions--;
                questions--;
                loadQuestion(q);
            }
           
        }

        private void nxtBtn_Click(object sender, RoutedEventArgs e)
        {
           
            if (q == -1) q = get();
            else
            {
                if (q < number) q++;
                else q = 0;
            }
            loadQuestion(q);
            return_rdb();
        }

        private void rBtn1_Checked(object sender, RoutedEventArgs e)
        {
            check_rdb();
        }
        private void check_rdb()
        {
            foreach (WrapPanel wr in st.Children)
            {
                (wr.Children[0] as RadioButton).IsEnabled = false;
                if ((wr.Children[0] as RadioButton).IsChecked == true)
                {
                    wr.Background = Brushes.Red;
                    (wr.Children[0] as RadioButton).Foreground = Brushes.White;
                }
                if (correct == (wr.Children[0] as RadioButton).Content.ToString())
                {
                    wr.Background = Brushes.Green;
                    (wr.Children[0] as RadioButton).Foreground = Brushes.White;

                }

            }
        }
        private void return_rdb()
        {
            foreach (WrapPanel wr in st.Children)
            {
                (wr.Children[0] as RadioButton).IsEnabled = true;
                wr.Background = new SolidColorBrush(Color.FromRgb(20,20,20));
                (wr.Children[0] as RadioButton).Foreground = Brushes.Red;
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Finisar.SQLite;
using System.IO;

namespace English_Test
{
    public partial class Editor : Form
    {
        public Editor()
        {
            InitializeComponent();
        }

        private void Editor_Load(object sender, EventArgs e)
        {
            FileInfo file = new FileInfo("questions.db");
            if (!file.Exists)
            {
                SQLiteConnection connection = new SQLiteConnection("Data Source=questions.db; Version=3;New=True;Compress=True;");
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "CREATE TABLE questions (id integer primary key, question varchar(400), correctAnswer varchar(200), answer1 varchar(200), answer2 varchar(200), answer3 varchar(200));";
                command.ExecuteNonQuery();
            }
        }
        SQLiteConnection connection = new SQLiteConnection("Data Source=questions.db; Version=3;Compress=True;");
        private void button1_Click(object sender, EventArgs e)
        {
            SQLiteCommand cmd = new SQLiteCommand("INSERT INTO questions (question, correctAnswer, answer1, answer2, answer3) values ('" + richTextBox1.Text + "','" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "');", connection);
            connection.Open();
            cmd.ExecuteNonQuery();
            richTextBox1.Text = "";
            textBox4.Text = "";
            textBox3.Text = "";
            textBox2.Text = "";
            textBox1.Text = "";
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SQL_twolist
{
    public partial class Form1: Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static string connectionstring = "Server=anna\\SQLEXPRESS;Database=test2;Trusted_Connection=True;";
        public static string query;
        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                query = "SELECT * FROM [dbo].[Orders]";
                using (SqlConnection conn = new SqlConnection(connectionstring))
                {
                    SqlCommand comm = new SqlCommand(query, conn);
                    conn.Open();
                    using (SqlDataReader reader = comm.ExecuteReader())
                    {
                        dataGridView1.Rows.Clear();
                        dataGridView1.Columns.Clear();
                        dataGridView1.Columns.Add("OrderID", "OrderID");
                        dataGridView1.Columns.Add("UserID", "UserID");
                        dataGridView1.Columns.Add("Product", "Product");
                        while (reader.Read())
                        {
                            dataGridView1.Rows.Add(reader["OrderID"], reader["UserID"], reader["Product"]);
                        }
                    }
                }
            }
            else if(radioButton2.Checked)
            {
                query = "SELECT * FROM [dbo].[Users]";
                using (SqlConnection conn = new SqlConnection(connectionstring))
                {
                    SqlCommand comm = new SqlCommand(query, conn);
                    conn.Open();
                    using (SqlDataReader reader = comm.ExecuteReader())
                    {
                        dataGridView1.Rows.Clear();
                        dataGridView1.Columns.Clear();
                        dataGridView1.Columns.Add("UserID", "UserID");
                        dataGridView1.Columns.Add("Name", "Name");
                        while (reader.Read())
                        {
                            dataGridView1.Rows.Add(reader["UserID"], reader["Name"]);
                        }
                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //using(SqlConnection conn = new SqlConnection(connectionstring))
            //{
            //    try
            //    {
            //        conn.Open();
            //        MessageBox.Show("ok");
            //    }
            //    catch
            //    {
            //        MessageBox.Show("error");
            //    }
            //}
        }

        private void button2_Click(object sender, EventArgs e)
        {
            query = "SELECT [Orders].[OrderID],[Users].[Name],[Orders].[Product] FROM [dbo].[Users] JOIN [dbo].[Orders] ON [Orders].[UserID] = @UserID AND [Users].[UserID] = @UserID;";
            using(SqlConnection conn = new SqlConnection(connectionstring))
            {
                SqlCommand comm = new SqlCommand(query, conn);
                comm.Parameters.AddWithValue("@UserID", numericUpDown1.Value);

                conn.Open();
                comm.ExecuteNonQuery();
                using(SqlDataReader reader = comm.ExecuteReader())
                {
                    dataGridView1.Rows.Clear();
                    dataGridView1.Columns.Clear();
                    dataGridView1.Columns.Add("OrderID", "OrderID");
                    dataGridView1.Columns.Add("Name", "Name");
                    dataGridView1.Columns.Add("Product", "Product");

                    while (reader.Read())
                    {
                        dataGridView1.Rows.Add(reader["OrderID"], reader["Name"], reader["Product"]);
                    }
                }
            }
        }
    }
}

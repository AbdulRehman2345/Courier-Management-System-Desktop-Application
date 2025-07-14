using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FastR_CMS
{
    public partial class Form3 : Form
    {
        // Declare the connection variable 
        private MySqlConnection conn;
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            // Connection variables
            const string server = "localhost";
            const string database = "fastr_db";
            const string uid = "root";
            const string password = "";

            // Connection string
            string connectionString = "SERVER=" + server + ";" +
                                      "DATABASE=" + database + ";" +
                                      "UID=" + uid + ";" +
                                      "PASSWORD=" + password + ";";

            // Initialize Connection variable
            conn = new MySqlConnection(connectionString);

        }
        ///// Function For parcel count according to Parcel status
        private  int CountParcel(string parcelStatus)
        {
            int parcelCount = 0;

            try
            {
                conn.Open();
                // SQL query 
                string query = "SELECT COUNT(id) FROM parcels WHERE parcel_status = @parcelStatus;";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    // Add parameter to the query
                    cmd.Parameters.AddWithValue("@parcelStatus", parcelStatus);

                    // Execute the query and get the count
                    parcelCount = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Close the connection
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            return parcelCount;
        }
        private void panel4_Paint_1(object sender, PaintEventArgs e)
        {
            int Allparcel = 0;

            try
            {
                // Open the database connection
                conn.Open();

                // SQL query 
                string query = "SELECT COUNT(*) FROM parcels";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    // Execute the query and get the count
                    Allparcel = Convert.ToInt32(cmd.ExecuteScalar());


                    label30.Text = Allparcel.ToString();


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Close the connection
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

            }
        }
        ////// parcel status Parcels

        private void panel6_Paint(object sender, PaintEventArgs e)
        {
            string status = "Collected";
            int count = CountParcel(status);
            label32.Text = count.ToString();
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {
            string status = "Accepted by courier";
            int count = CountParcel(status);
            label31.Text = count.ToString();
        }
        private void panel7_Paint(object sender, PaintEventArgs e)
        {
            string status = "Shipped";
            int count = CountParcel(status);
            label33.Text = count.ToString();
        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {
            string status = "Arrived at destination";
            int count = CountParcel(status);
            label34.Text = count.ToString();
        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {
            string status = "Out for delivery";
            int count = CountParcel(status);
            label35.Text = count.ToString();
        }

        private void panel10_Paint(object sender, PaintEventArgs e)
        {
            string status = "Delivered";
            int count = CountParcel(status);
            label36.Text = count.ToString();
        }

        private void panel12_Paint(object sender, PaintEventArgs e)
        {
            string status = "Picked up";
            int count = CountParcel(status);
            label37.Text = count.ToString();
        }

        private void panel14_Paint(object sender, PaintEventArgs e)
        {
            string status = "Unsuccessfull delivery";
            int count = CountParcel(status);
            label38.Text = count.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.Show();
            this.Hide();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();
            form5.Show();
            this.Hide();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Form6 form6 = new Form6();
            form6.Show();
            this.Hide();
        }

       
    }
}
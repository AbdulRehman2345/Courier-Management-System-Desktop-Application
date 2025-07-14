using MySql.Data.MySqlClient;
using Org.BouncyCastle.Tls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FastR_CMS
{
    public partial class Form5 : Form
    {  // Declare the connection variable 
        private MySqlConnection conn;
        public Form5()
        {
            InitializeComponent();
          
        }
       private void Form5_Load_1(object sender, EventArgs e)
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
        ////// CHECK PARCEL STATUS
        private void insert_Click(object sender, EventArgs e)
        {

          

                try
                {
                    // Open the connection
                    conn.Open();

                    // Validate and parse the tracking number
                    if (!int.TryParse(trackingnumber.Text, out int trackingNumberValue))
                    {
                        MessageBox.Show("Invalid tracking number. Please enter a valid number.");
                        return; // Exit the method if parsing fails
                    }

                    // SQL query
                    string query = "SELECT * FROM parcel_status WHERE tracking_number = @value1";

                    // Prepare command
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@value1", trackingNumberValue); // Pass parsed integer value

                    // Execute the query and get the data
                    MySqlDataReader reader = cmd.ExecuteReader();

                    // If any record is returned
                    if (reader.HasRows)
                    {
                        // Clear the RichTextBox content
                        richTextBox1.Clear();

                        while (reader.Read())
                        {
                            // Safely retrieve data, handling potential null values
                            string status = reader["parcel_status"].ToString();
                            string updatedDate = reader["tstamp"].ToString();

                            // Append formatted heading
                            richTextBox1.SelectionStart = richTextBox1.TextLength;
                            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
                            richTextBox1.SelectionColor = Color.FromArgb(57, 83, 95); // Custom color for heading
                            richTextBox1.AppendText(" Parcel Status : ");

                            // Append data
                            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Regular);
                            richTextBox1.SelectionColor = Color.Black;
                            richTextBox1.AppendText($"{status}\n");

                            // Append formatted heading
                            richTextBox1.SelectionStart = richTextBox1.TextLength;
                            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
                            richTextBox1.SelectionColor = Color.FromArgb(57, 83, 95); // Custom color for heading
                            richTextBox1.AppendText(" Updated On : ");

                            // Append data
                            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Regular);
                            richTextBox1.SelectionColor = Color.Black;
                            richTextBox1.AppendText($"{updatedDate}\n\n");
                        }
                    }
                    else
                    {
                        richTextBox1.Clear();
                        richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
                        richTextBox1.SelectionColor = Color.Red;
                        richTextBox1.AppendText("Parcel Not Found.");
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
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


        ////// UPDATE PARCEL STATUS
        private void button5_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(parcelstatus.Text) == true)
            {
                parcelstatus.Focus();
                errorProvider1.SetError(this.parcelstatus, "Please Enter Parcel status");
            }
            else
            {
                errorProvider1.Clear();
                try
                {
                    // Open the connection
                    conn.Open();

                    // SQL Query 1  
                    string query = "INSERT INTO parcel_status (tracking_number, parcel_status) VALUES (@value1, @value2);";

                    // Prepare command for Query 1
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@value1", trackingnumber.Text);
                    cmd.Parameters.AddWithValue("@value2", parcelstatus.Text);

                    // Execute the command for Query 1
                    int rowsAffected = cmd.ExecuteNonQuery();

                    // SQL Query 2 FOR PARCEL STATUS
                    string query2 = "UPDATE parcels SET parcel_status = @value2 WHERE tracking_number = @value1;";

                    // Prepare command for Query 2
                    MySqlCommand cmd1 = new MySqlCommand(query2, conn);
                    cmd1.Parameters.AddWithValue("@value1", trackingnumber.Text);
                    cmd1.Parameters.AddWithValue("@value2", parcelstatus.Text);

                    // Execute the command for Query 2
                    int rowsAffected1 = cmd1.ExecuteNonQuery();

                    // Check if parcel was inserted and status updated
                    if (rowsAffected > 0 && rowsAffected1 > 0)
                    {
                        MessageBox.Show("Parcel Status Has Been Updated!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Oops! Parcel Status Could Not Be Updated. Please Try Again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
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
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form6 form6 = new Form6();
            form6.Show();
            this.Hide();
        }
    }
}
    

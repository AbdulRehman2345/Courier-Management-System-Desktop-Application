using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Tls;

namespace FastR_CMS
{
    public partial class Form1 : Form
    {
        // Declare the connection variable 
        private MySqlConnection conn;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
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

        private void email_Enter(object sender, EventArgs e)
        {
            if (email.Text == "Enter G-mail")
            {
                email.Text = "";
                email.ForeColor = Color.Black;
            }
        }

        private void email_Leave(object sender, EventArgs e)
        {
            if (email.Text == "")
            {
                email.Text = "Enter G-mail";
                email.ForeColor = Color.Silver;
            }
        }

        private void password_Enter(object sender, EventArgs e)
        {
            if (password.Text == "Enter Password")
            {
                password.Text = "";
                password.ForeColor = Color.Black;
            }
        }

        private void password_Leave(object sender, EventArgs e)
        {
            if (password.Text == "")
            {
                password.Text = "Enter Password";
                password.ForeColor = Color.Silver;

            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (email.Text == "Enter G-mail")
            {
                email.Focus();
                errorProvider1.SetError(this.email, "Please Enter Email");
            }
            else if (password.Text == "Enter Password")
            {
                password.Focus();
                errorProvider2.SetError(this.password, "Please Enter Password");

             }
            else
            {
                errorProvider1.Clear();
                errorProvider2.Clear();

                try
                {

                    // Open the connection
                    conn.Open();


                    // SQL SELECT Query (corrected syntax)
                    string query = "SELECT * FROM users WHERE email = @value1 AND password = @value2;";

                    // Prepare command
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@value1", email.Text); // Get value from email TextBox
                    cmd.Parameters.AddWithValue("@value2", password.Text); // Get value from password TextBox

                    // Execute the command and get the result
                    int userCount = Convert.ToInt32(cmd.ExecuteScalar());

                    // Check if user exists
                    if (userCount > 0)
                    {
                        MessageBox.Show("LOGIN SUCCESSFUL!");
                        Form7 form7 = new Form7(email.Text);
                        form7.Show();
                        this.Hide();

                    }
                    else
                    {
                        MessageBox.Show("INVALID CREDENTIALS FOR USER!");
                    }
                }
                catch (Exception ex)
                {
                    // Handle errors
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
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (email.Text == "Enter G-mail")
            {
                email.Focus();
                errorProvider1.SetError(this.email, "Please Enter Email");
            }
            else if (password.Text == "Enter Password")
            {
                password.Focus();
                errorProvider2.SetError(this.password, "Please Enter Password");

            }
            else
            {
                errorProvider1.Clear();
                errorProvider2.Clear();
                try
                {

                    // Open the connection
                    conn.Open();


                    // SQL SELECT Query (corrected syntax)
                    string query = "SELECT * FROM staff WHERE email = @value1 AND password = @value2;";

                    // Prepare command
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@value1", email.Text); // Get value from email TextBox
                    cmd.Parameters.AddWithValue("@value2", password.Text); // Get value from password TextBox

                    // Execute the command and get the result
                    int userCount = Convert.ToInt32(cmd.ExecuteScalar());

                    // Check if user exists
                    if (userCount > 0)
                    {
                        MessageBox.Show("LOGIN SUCCESSFUL!");
                        Form3 form3 = new Form3();
                        form3.Show();
                        this.Hide();

                    }
                    else
                    {
                        MessageBox.Show("INVALID CREDENTIALS FOR STAFF!");
                    }
                }
                catch (Exception ex)
                {
                    // Handle errors
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


    }
}
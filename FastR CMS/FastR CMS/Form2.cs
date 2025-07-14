using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FastR_CMS
{
    public partial class Form2 : Form
    {    // Declare the connection variable 
        private MySqlConnection conn;
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {   // Connection variables
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

        private void f_name_Enter(object sender, EventArgs e)
        {
            if (f_name.Text == "Enter First Name")
            {
                f_name.Text = "";
                f_name.ForeColor = Color.Black;
            }
        }

        private void f_name_Leave(object sender, EventArgs e)
        {
            if (f_name.Text == "")
            {
                f_name.Text = "Enter First Name";
                f_name.ForeColor = Color.Silver;
            }

        }

        private void l_name_Enter(object sender, EventArgs e)
        {
            if (l_name.Text == "Enter Last Name")
            {
                l_name.Text = "";
                l_name.ForeColor = Color.Black;
            }
        }

        private void l_name_Leave(object sender, EventArgs e)
        {
            if (l_name.Text == "")
            {
                l_name.Text = "Enter Last Name";
                l_name.ForeColor = Color.Silver;
            }
        }

        private void number_Enter(object sender, EventArgs e)
        {
            if (number.Text == "Enter Number")
            {
                number.Text = "";
                number.ForeColor = Color.Black;
            }
        }

        private void number_Leave(object sender, EventArgs e)
        {
            if (number.Text == "")
            {
                number.Text = "Enter Number";
                number.ForeColor = Color.Silver;
            }
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
        private void button1_Click(object sender, EventArgs e)
        {
            if (f_name.Text == "Enter First Name")
            {
                f_name.Focus();
                errorProvider1.SetError(this.f_name, "Please Enter Your First Name");
            }
            else if (l_name.Text == "Enter Last Name")
            {
                l_name.Focus();
                errorProvider2.SetError(this.l_name, "Please Enter Your Last Name");

            }
            else if (number.Text == "Enter Number")
            {
                number.Focus();
                errorProvider2.SetError(this.number, "Please Enter Your Number");

            }
            else if (email.Text == "Enter G-mail")
            {
                email.Focus();
                errorProvider2.SetError(this.password, "Please Enter Your Email");

            }
            else if (password.Text == "Enter Password")
            {
                password.Focus();
                errorProvider2.SetError(this.password, "Please Enter Your  Password");

            }

            else
            {
                errorProvider1.Clear();
                errorProvider2.Clear();
                errorProvider3.Clear();
                errorProvider4.Clear();
                errorProvider5.Clear();
                try
                {

                    // Open the connection
                    conn.Open();


                    // SQL SELECT Query (corrected syntax)
                    string query = "INSERT INTO users (first_name, last_name, number, email, password) VALUES (@value1, @value2, @value3, @value4, @value5);";

                    // Prepare command
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@value1", f_name.Text);
                    cmd.Parameters.AddWithValue("@value2", l_name.Text);
                    cmd.Parameters.AddWithValue("@value3", number.Text);
                    cmd.Parameters.AddWithValue("@value4", email.Text);
                    cmd.Parameters.AddWithValue("@value5", password.Text);

                    // Execute the command and get the result
                    int rowsaffected = cmd.ExecuteNonQuery();

                    // Check if user exists
                    if (rowsaffected > 0)
                    {
                        MessageBox.Show("Data has been successfully inserted!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Form1 form1 = new Form1();
                        form1.Show();
                        this.Hide();


                    }
                    else
                    {
                        MessageBox.Show("Oops! Data could not be inserted. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

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

        private void label8_Click_1(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }
    }
}

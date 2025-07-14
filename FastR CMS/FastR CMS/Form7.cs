using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace FastR_CMS
{
    public partial class Form7 : Form
    {  // Declare the connection variable 
        private MySqlConnection conn;
        private string userEmail;
        public Form7(string email)
        {
            InitializeComponent();
            userEmail = email;
        }

        private void Form7_Load(object sender, EventArgs e)
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

            try
            {
                // Open database connection
                conn.Open();
                string query = "SELECT tracking_number, sender_name,  sender_email, recipient_name, recipient_contact, weight, charges, del_date, parcel_status " +
                     "FROM parcels " +
                     "WHERE sender_email = @userEmail";


                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    // Add parameter for user email
                    cmd.Parameters.AddWithValue("@userEmail", userEmail);

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        // Fill the DataTable
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        // Check if data exists
                        if (dt.Rows.Count > 0)
                        {
                            // Bind data to DataGridView
                            dataGridView1.DataSource = dt;

                            // Apply font styles
                            dataGridView1.DefaultCellStyle.Font = new Font("Arial", 11);
                            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial Narrow", 13, FontStyle.Bold);

                            // Adjust column widths
                            foreach (DataGridViewColumn column in dataGridView1.Columns)
                            {
                                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                            }

                            // Center-align column headers
                            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                            // Automatically resize columns to fit content
                            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                        }
                        else
                        {
                            MessageBox.Show("No Data Found For Your Account.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Close the connection
                conn.Close();
            }
        }

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
        private void button1_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.PrintPreviewControl.Zoom = 1;
            printPreviewDialog1.ShowDialog();
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            try
            {
                // Create a Bitmap to hold the DataGridView content
                int width = dataGridView1.Width;
                int height = dataGridView1.Height;

                Bitmap imagebmp = new Bitmap(width, height);
                dataGridView1.DrawToBitmap(imagebmp, new Rectangle(0, 0, width, height));

                // Calculate scaling to fit the page
                float scaleWidth = e.MarginBounds.Width / (float)width;
                float scaleHeight = e.MarginBounds.Height / (float)height;
                float scale = Math.Min(scaleWidth, scaleHeight);

                // Apply scaling
                e.Graphics.ScaleTransform(scale, scale);

                // Draw the bitmap with scaling applied
                e.Graphics.DrawImage(imagebmp, e.MarginBounds.Left, e.MarginBounds.Top);

                // If the data is larger than one page, set HasMorePages to true
                if (height * scale > e.MarginBounds.Height)
                {
                    e.HasMorePages = true;
                }
                else
                {
                    e.HasMorePages = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during print: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}

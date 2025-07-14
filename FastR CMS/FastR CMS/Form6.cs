using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace FastR_CMS
{
    public partial class Form6 : Form
    {
        // Declare the connection variable 
        private MySqlConnection conn;

        public Form6()
        {
            InitializeComponent();
        }

        private void Form6_Load(object sender, EventArgs e)
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

        private void button5_Click(object sender, EventArgs e)
        {

            // Validate parcelstatus.SelectedItem
            if (parcelstatus.SelectedItem == null)
            {
                MessageBox.Show("Please select a parcel status.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Get parcel status
            string parcelStatus = parcelstatus.SelectedItem.ToString();

            // Validate date range
            DateTime dateFrom = dateTimePicker1.Value.Date;
            DateTime dateTo = dateTimePicker2.Value.Date;
            if (dateFrom > dateTo)
            {
                MessageBox.Show("The 'From' date cannot be later than the 'To' date.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // SQL query with mandatory filters
            string query = "SELECT tracking_number, sender_name, sender_contact, sender_email, recipient_name, recipient_contact, weight, charges, del_date, parcel_status " +
                           "FROM parcels " +
                           "WHERE parcel_status = @parcelStatus AND date_created BETWEEN @dateFrom AND @dateTo";

            try
            {
                // Open database connection
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    // Add parameters
                    cmd.Parameters.AddWithValue("@parcelStatus", parcelStatus);
                    cmd.Parameters.AddWithValue("@dateFrom", dateFrom);
                    cmd.Parameters.AddWithValue("@dateTo", dateTo);

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
                            MessageBox.Show("No data found for the given filters.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            Form5 form5 = new Form5();
            form5.Show();
            this.Hide();
        }
    }
}


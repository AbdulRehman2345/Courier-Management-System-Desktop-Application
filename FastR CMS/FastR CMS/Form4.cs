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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace FastR_CMS
{
    public partial class Form4 : Form
    {   // Declare the connection variable 
        private MySqlConnection conn;
        public Form4()
        {
            InitializeComponent();

        }

        private void Form4_Load(object sender, EventArgs e)
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
        //////////////// RESET FUNCTION START //////////////////
        private void reset()
        {
            sendername.Text = string.Empty;
            senderadd.Text = string.Empty;
            senderemail.Text = string.Empty;
            sendercontact.Text = string.Empty;
            recievername.Text = string.Empty;
            recieveradd.Text = string.Empty;
            recievercontact.Text = string.Empty;
            weight.Text = string.Empty;
            charges.Text = string.Empty;
            deliverydate.Text = string.Empty;
            sendername.Focus();
        }
        //////////////// RESET FUNCTION END //////////////////
        //////////////// VIEW FUNCTION START //////////////////
        private void viewparcel()
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                string query = "SELECT id, tracking_number, sender_name, recipient_name, charges, parcel_status FROM parcels";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dataGridView1.DataSource = dt;
                    dataGridView1.DefaultCellStyle.Font = new Font("Arial", 10);
                    dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial Narrow", 12, FontStyle.Bold);

                    foreach (DataGridViewColumn column in dataGridView1.Columns)
                    {
                        column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }
        //////////////// VIEW FUNCTION START //////////////////
        //////////////// INSERT BUTTON START //////////////////
        private void insert_Click(object sender, EventArgs e)
        {   // error handling when the textboxes are not filled //
            if (string.IsNullOrEmpty(sendername.Text) == true)
            {
                sendername.Focus();
                errorProvider1.SetError(this.sendername, "Please Enter Sender Name");
            }
            else if (string.IsNullOrEmpty(senderadd.Text) == true)
            {
                senderadd.Focus();
                errorProvider2.SetError(this.senderadd, "Please Enter Sender Address");

            }
            else if (string.IsNullOrEmpty(sendercontact.Text) == true)
            {
                sendercontact.Focus();
                errorProvider3.SetError(this.sendercontact, "Please Enter Sender Contact");

            }
            else if (string.IsNullOrEmpty(senderemail.Text) == true)
            {
                senderemail.Focus();
                errorProvider4.SetError(this.senderemail, "Please Enter Sender Email");

            }
            else if (string.IsNullOrEmpty(recievername.Text) == true)
            {
                recievername.Focus();
                errorProvider5.SetError(this.recievername, "Please Enter Reciever Name");

            }
            else if (string.IsNullOrEmpty(recievercontact.Text) == true)
            {
                recievercontact.Focus();
                errorProvider6.SetError(this.recievercontact, "Please Enter Reciever Contact");

            }
            else if (string.IsNullOrEmpty(recieveradd.Text) == true)
            {
                recieveradd.Focus();
                errorProvider7.SetError(this.recieveradd, "Please Enter Reciever Address");
            }
            else if (string.IsNullOrEmpty(weight.Text) == true)
            {
                weight.Focus();
                errorProvider8.SetError(this.weight, "Please Enter Weight Of The Parcel");
            }
            else if (string.IsNullOrEmpty(charges.Text) == true)
            {
                charges.Focus();
                errorProvider9.SetError(this.charges, "Please Enter Charges Of The Parcel");
            }
            else if (string.IsNullOrEmpty(deliverydate.Text) == true)
            {
                deliverydate.Focus();
                errorProvider10.SetError(this.deliverydate, "Please Enter Delivery Date Of The Parcel");
            }
            else
            {
                errorProvider1.Clear();
                errorProvider2.Clear();
                errorProvider3.Clear();
                errorProvider4.Clear();
                errorProvider5.Clear();
                errorProvider6.Clear();
                errorProvider7.Clear();
                errorProvider8.Clear();
                errorProvider9.Clear();
                errorProvider10.Clear();
                try
                {

                    // Open the connection
                    conn.Open();
                    int GenerateTrackingNumber()
                    {
                        Random random = new Random(Guid.NewGuid().GetHashCode());
                        return random.Next(1000000000, 1999999999);
                    }
                    int trackingNumber = GenerateTrackingNumber();
                    // SQL Query 1
                    string query = "INSERT INTO parcels (tracking_number, sender_name, sender_address, sender_contact, sender_email, recipient_name, recipient_address, recipient_contact, weight, charges, del_date) VALUES (@value1, @value2, @value3, @value4, @value5, @value6, @value7, @value8, @value9, @value10, @value11);";

                    // Prepare command for Query 1
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@value1", trackingNumber);
                    cmd.Parameters.AddWithValue("@value2", sendername.Text);
                    cmd.Parameters.AddWithValue("@value3", senderadd.Text);
                    cmd.Parameters.AddWithValue("@value4", sendercontact.Text);
                    cmd.Parameters.AddWithValue("@value5", senderemail.Text);
                    cmd.Parameters.AddWithValue("@value6", recievername.Text);
                    cmd.Parameters.AddWithValue("@value7", recieveradd.Text);
                    cmd.Parameters.AddWithValue("@value8", recievercontact.Text);
                    cmd.Parameters.AddWithValue("@value9", weight.Text);
                    cmd.Parameters.AddWithValue("@value10", charges.Text);
                    cmd.Parameters.AddWithValue("@value11", deliverydate.Text);

                    // Execute the command for Query 1
                    int rowsaffected = cmd.ExecuteNonQuery();

                    // SQL Query 2 FOR PARCEL STATUS
                    string query2 = "INSERT INTO parcel_status (tracking_number) VALUES (@value1);";

                    // Prepare command for Query 2
                    MySqlCommand cmd1 = new MySqlCommand(query2, conn);
                    cmd1.Parameters.AddWithValue("@value1", trackingNumber);

                    // Execute the command for Query 2
                    int rowsaffected1 = cmd1.ExecuteNonQuery();


                    // Check if parcel inserted
                    if (rowsaffected > 0 && rowsaffected1 > 0)
                    {
                        MessageBox.Show("Data Has Been Successfully Inserted!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        reset();
                        viewparcel();

                    }
                    else
                    {
                        MessageBox.Show("Oops! Data Could Not Be Inserted. Please Try Again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

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
        //////////////// INSERT BUTTON END   //////////////////
        //////////////// VIEW BUTTON START  //////////////////
        private void view_Click_1(object sender, EventArgs e)
        {
            viewparcel();
        }
        //////////////// VIEW BUTTON END //////////////////
        //////////////// DATA GRID START //////////////////
        private void dataGridView1_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.RowIndex >= 0)
                {
                    // Get the selected row
                    DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                    // Fetch and store the parcel ID
                    selectedParcelId = Convert.ToInt32(selectedRow.Cells["id"].Value);
                }
                FetchParcelDetails(selectedParcelId);
            }
        }
        private void FetchParcelDetails(int parcelId)
        {
            try
            {
                // Open the connection
                conn.Open();

                // SQL query
                string query = "SELECT * FROM parcels WHERE id = @parcelId";

                // Prepare command 
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@parcelId", parcelId);

                // Execute the query and get the data
                MySqlDataReader reader = cmd.ExecuteReader();

                // if a record is returned then
                if (reader.Read())
                {
                    // Fill the TextBoxes with the parcel data of that id
                    sendername.Text = reader["sender_name"].ToString();
                    senderadd.Text = reader["sender_address"].ToString();
                    senderemail.Text = reader["sender_email"].ToString();
                    sendercontact.Text = reader["sender_contact"].ToString();
                    recievername.Text = reader["recipient_name"].ToString();
                    recieveradd.Text = reader["recipient_address"].ToString();
                    recievercontact.Text = reader["recipient_contact"].ToString();
                    weight.Text = reader["weight"].ToString();
                    charges.Text = reader["charges"].ToString();
                    deliverydate.Text = reader["del_date"].ToString();
                }
                else
                {
                    MessageBox.Show("Parcel Not Found.");
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
        //////////////// DATA GRID END //////////////////
        //////////////// RESET BUTTON START //////////////////
        private void button8_Click(object sender, EventArgs e)
        {
            reset();
        }
        //////////////// RESET BUTTON END //////////////////
        /// ////////////////UPDATE BUTTON START //////////////////
        private int selectedParcelId = -1;
        private void button5_Click(object sender, EventArgs e)
        {
            if (selectedParcelId == -1)
            {
                MessageBox.Show("Please Select A Parcel To Update.");
                return;
            }
            try
            {
                // Open the connection
                conn.Open();

                //SQL Query
                string query = "UPDATE parcels SET sender_name = @sendername, sender_address = @senderadd, sender_contact = @sendercontact, sender_email = @senderemail, recipient_name = @recievername, recipient_address = @recieveradd, recipient_contact = @recievercontact, weight = @weight, charges = @charges, del_date = @deliverydate WHERE id = @id;";

                // Prepare command
                MySqlCommand cmd = new MySqlCommand(query, conn);

                // Add parameters for the query
                cmd.Parameters.AddWithValue("@id", selectedParcelId);
                cmd.Parameters.AddWithValue("@sendername", sendername.Text);
                cmd.Parameters.AddWithValue("@senderadd", senderadd.Text);
                cmd.Parameters.AddWithValue("@sendercontact", sendercontact.Text);
                cmd.Parameters.AddWithValue("@senderemail", senderemail.Text);
                cmd.Parameters.AddWithValue("@recievername", recievername.Text);
                cmd.Parameters.AddWithValue("@recieveradd", recieveradd.Text);
                cmd.Parameters.AddWithValue("@recievercontact", recievercontact.Text);
                cmd.Parameters.AddWithValue("@weight", weight.Text);
                cmd.Parameters.AddWithValue("@charges", charges.Text);
                cmd.Parameters.AddWithValue("@deliverydate", deliverydate.Text);

                // Execute the command 
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Parcel Data Updated Successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    reset();
                    viewparcel();
                    selectedParcelId = -1;
                }
                else
                {
                    MessageBox.Show("Update Failed. Please Try Again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        /////////////////// UPDATE BUTTON END //////////////////
        /////////////////// DELETE BUTTON START //////////////////
        private void button6_Click(object sender, EventArgs e)
        {
            if (selectedParcelId == -1)
            {
                MessageBox.Show("Please Select A Parcel To Delete.");
                return;
            }
            try
            {
                // Confirmation BOX
                DialogResult dialogResult = MessageBox.Show(
                    "Are You Sure You Want To Delete This Parcel?",
                    "Confirm Deletion",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );
                if (dialogResult == DialogResult.Yes)
                {
                    // Open the connection
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }

                    //// GET TRACKING NUMBER 
                    string gettrackingnumberquery = "SELECT tracking_number FROM parcels WHERE id = @id;";
                    MySqlCommand getTrackingNumberCmd = new MySqlCommand(gettrackingnumberquery, conn);
                    getTrackingNumberCmd.Parameters.AddWithValue("@id", selectedParcelId);
                    string trackingNumber = getTrackingNumberCmd.ExecuteScalar().ToString();
                    //// DELETE THE PARCEL
                    string deleteparcelquery = "DELETE FROM parcels WHERE id = @id;";
                    MySqlCommand deleteParcelCmd = new MySqlCommand(deleteparcelquery, conn);
                    deleteParcelCmd.Parameters.AddWithValue("@id", selectedParcelId);
                    int rowsaffected = deleteParcelCmd.ExecuteNonQuery();
                    //// DELETE THE PARCEL STATUS
                    string deleteParcelStatusQuery = "DELETE FROM parcel_status WHERE tracking_number = @trackingNumber;";
                    MySqlCommand deleteParcelStatusCmd = new MySqlCommand(deleteParcelStatusQuery, conn);
                    deleteParcelStatusCmd.Parameters.AddWithValue("@trackingNumber", trackingNumber);
                    int statusrowsaffected = deleteParcelStatusCmd.ExecuteNonQuery();

                    if (rowsaffected > 0 && statusrowsaffected > 0)
                    {
                        MessageBox.Show("Parcel And Its Status Deleted Successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        viewparcel();
                        reset();
                        selectedParcelId = -1;
                    }
                    else
                    {
                        MessageBox.Show("Deletion Failed. Please Try Again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
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
        /////////////////// DELETE BUTTON END //////////////////
        /////////////////// SEARCH BUTTON START //////////////////
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                    conn.Open();
                // SQL Query 
                string query = @"
            SELECT id, tracking_number, sender_name, recipient_name, charges, parcel_status 
            FROM parcels 
            WHERE sender_name LIKE @name OR recipient_name LIKE @name;";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    // Add parameter with the '%' wildcard
                    string searchValue = textBox11.Text.Trim() + "%";
                    cmd.Parameters.AddWithValue("@name", searchValue);

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        // Bind the results to the DataGridView
                        dataGridView1.DataSource = dt;
                        dataGridView1.DefaultCellStyle.Font = new Font("Arial", 10);
                        dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial Narrow", 12, FontStyle.Bold);

                        foreach (DataGridViewColumn column in dataGridView1.Columns)
                        {
                            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                        }

                        if (dt.Rows.Count == 0)
                        {
                            MessageBox.Show("No Results Found.");
                            viewparcel();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        /////////////////// SEARCH BUTTON END //////////////////
        private void button1_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();
            form5.Show();
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





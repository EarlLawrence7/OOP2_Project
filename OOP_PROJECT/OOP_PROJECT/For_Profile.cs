using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;

namespace OOP_PROJECT
{
    public partial class For_Profile : Form
    {
        private string loggedInUsername;
        OleDbConnection con = new OleDbConnection();
        string dbProvider = "Provider=Microsoft.ACE.OLEDB.12.0;";
        string dbsource = @"Data Source=C:\Users\bagui\OneDrive\Documents\MOVIES.accdb";

        public For_Profile(string username)
        {
            InitializeComponent();
            loggedInUsername = username;
            con.ConnectionString = dbProvider + dbsource; // Set the connection string
            LoadUserInfo(); // Load user information when the form is initialized
            tbxPassWord.UseSystemPasswordChar = true;
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            For_Home for_home = new For_Home(loggedInUsername);
            for_home.Show();
            this.Hide();
        }

        private void btnProfile_Click(object sender, EventArgs e)
        {

        }

        private void btnBookTickets_Click(object sender, EventArgs e)
        {
            For_BookTickets for_bookTickets = new For_BookTickets(loggedInUsername);
            for_bookTickets.Show();
            this.Hide();
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {

        }
        private void LoadUserInfo()
        {
            try
            {
                con.Open(); // Open the database connection
                string query = "SELECT * FROM Accounts WHERE Username = ?";
                OleDbCommand cmd = new OleDbCommand(query, con);
                cmd.Parameters.AddWithValue("@username", loggedInUsername);

                OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    // Populate the profile fields with user information
                    tbxUserName.Text = dt.Rows[0]["Username"].ToString();
                    tbxPassWord.Text = dt.Rows[0]["Password"].ToString();
                    tbxUserID.Text = dt.Rows[0]["UserID"].ToString();
                    tbxFirstName.Text = dt.Rows[0]["FirstName"].ToString();
                    tbxLastName.Text = dt.Rows[0]["LastName"].ToString();
                    comboBox1.Text = dt.Rows[0]["Gender"].ToString();
                    tbxEmailAdd.Text = dt.Rows[0]["Email_address"].ToString();
                    tbxHomeAdd.Text = dt.Rows[0]["Home_address"].ToString();
                    tbxCity.Text = dt.Rows[0]["City"].ToString();
                    long phoneNumber = Convert.ToInt64(dt.Rows[0]["Phone_no"]);
                    tbxPhoneNum.Text = phoneNumber.ToString();
                    // Populate other fields similarly
                }
                else
                {
                    MessageBox.Show("User information not found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {
                con.Close(); // Close the database connection
            }
        }
        private void For_Profile_Load(object sender, EventArgs e)
        {
            // Enable editing for text boxes and combo boxes
            tbxFirstName.ReadOnly = false;
            tbxLastName.ReadOnly = false;
            comboBox1.Enabled = true;
            tbxEmailAdd.ReadOnly = false;
            tbxHomeAdd.ReadOnly = false;
            tbxCity.ReadOnly = false;
            tbxPhoneNum.ReadOnly = false;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select a gender.");
                    return; // Exit the method
                }

                con.Open();
                string query = "UPDATE Accounts SET FirstName = ?, LastName = ?, Gender = ?, Email_address = ?, Home_address = ?, City = ?, Phone_no = ?, [Password] = ? WHERE Username = ?";

                OleDbCommand cmd = new OleDbCommand(query, con);
                cmd.Parameters.AddWithValue("@firstName", tbxFirstName.Text);
                cmd.Parameters.AddWithValue("@lastName", tbxLastName.Text);
                cmd.Parameters.AddWithValue("@gender", comboBox1.SelectedItem.ToString()); // Ensure comboBox1 has a selected item
                cmd.Parameters.AddWithValue("@email", tbxEmailAdd.Text);
                cmd.Parameters.AddWithValue("@homeAddress", tbxHomeAdd.Text);
                cmd.Parameters.AddWithValue("@city", tbxCity.Text);
                cmd.Parameters.AddWithValue("@phoneNumber", tbxPhoneNum.Text);
                cmd.Parameters.AddWithValue("@password", tbxPassWord.Text);
                cmd.Parameters.AddWithValue("@username", loggedInUsername);

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Information updated successfully!");
                }
                else
                {
                    MessageBox.Show("Failed to update information.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void btnShowHide_Click(object sender, EventArgs e)
        {
            tbxPassWord.UseSystemPasswordChar = !tbxPassWord.UseSystemPasswordChar;
        }

        private void btnLogOut_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("SUCCESSFULLY LOGGED OUT!");
            For_Login for_login = new For_Login();
            for_login.Show();
            this.Hide();
        }
    }
}

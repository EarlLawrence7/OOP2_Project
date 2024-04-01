using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace OOP_PROJECT
{
    public partial class For_Login : Form
    {
        OleDbConnection con = new OleDbConnection();
        string dbProvider = "Provider=Microsoft.ACE.OLEDB.12.0;";
        string dbsource = @"Data Source=C:\Users\bagui\OneDrive\Documents\MOVIES.accdb";
        public string loggedInUsername;

        public For_Login()
        {
            InitializeComponent();
            con.ConnectionString = dbProvider + dbsource; // Set the connection string
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open(); // Open the database connection
                string query = "SELECT * FROM Accounts WHERE Username = ? AND [Password] = ?";
                OleDbCommand cmd = new OleDbCommand(query, con);
                cmd.Parameters.AddWithValue("@username", tbxUsername.Text);
                cmd.Parameters.AddWithValue("@password", tbxPassword.Text);
                OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                tbxUsername.Focus();
                if (dt.Rows.Count > 0)
                {
                    loggedInUsername = tbxUsername.Text; // Set loggedInUsername property
                    string firstName = dt.Rows[0]["FirstName"].ToString();
                    string lastName = dt.Rows[0]["LastName"].ToString();
                    MessageBox.Show($"Login successful! You have logged in as {firstName} {lastName}");
                    // Credentials are correct, open Form2
                    new For_Home(loggedInUsername).Show();
                    this.Hide();
                }
                else
                {
                    // Invalid credentials
                    MessageBox.Show("The Username or Password you entered is incorrect! Please try again.");
                    tbxUsername.Clear();
                    tbxPassword.Clear();
                    tbxUsername.Focus();
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

        private void label4_Click(object sender, EventArgs e)
        {
            tbxUsername.Clear();
            tbxPassword.Clear();
            tbxUsername.Focus();
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            For_Signup For_Signup = new For_Signup();
            For_Signup.Show();
            this.Hide();
        }
    }
}

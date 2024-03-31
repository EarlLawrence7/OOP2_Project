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

namespace OOP_PROJECT
{
    public partial class For_BookTickets : Form
    {
        private string loggedInUsername;

        public For_BookTickets(string username)
        {
            InitializeComponent();
            loggedInUsername = username;
        }

        private const string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\bagui\OneDrive\Documents\MOVIES.accdb";

        public For_BookTickets()
        {
            InitializeComponent();
        }
        private void btnHome_Click(object sender, EventArgs e)
        {           
            For_Home for_home = new For_Home(loggedInUsername);
            for_home.Show();
            this.Hide();
        }

        private void btnProfile_Click(object sender, EventArgs e)
        {           
            For_Profile for_Profile = new For_Profile(loggedInUsername);
            for_Profile.Show();
            this.Hide();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {

        }

        private void btnAbout_Click(object sender, EventArgs e)
        {

        }


        private void For_BookTickets_Load(object sender, EventArgs e)
        {
            DataTable dt = GetDataFromAccess("SELECT * FROM Movies");

            // Bind the DataTable to the DataGridView
            dgvBookTickets.DataSource = dt;
        }
        private DataTable GetDataFromAccess(string query)
        {
            DataTable dt = new DataTable();

            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    conn.Open();
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }

            return dt;
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            MessageBox.Show("SUCCESSFULLY LOGGED OUT!");
            For_Login for_login = new For_Login();
            for_login.Show();
            this.Hide();
        }
    }
}

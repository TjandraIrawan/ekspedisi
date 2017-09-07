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

namespace Aplikasi_Ekspedisi
{
    public partial class Login : Form
    {
 
        public Login()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void login_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

                Connection con = new Connection();
                MySqlDataAdapter sda = new MySqlDataAdapter("select * from login_user where UserId = '" + txtUserName.Text + "' and Password = '" + txtPassword.Text + "' ",con.ActiveCon());
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count == 1)
                {
                    Menu_Utama obj = new Menu_Utama();
                    obj.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("User Name dan Password salah !!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPassword.Clear();
                    txtUserName.Clear();
                    txtUserName.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Perhatian",MessageBoxButtons.OK,MessageBoxIcon.Error);

            }
        }

        private void login_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();

        }
    }
}

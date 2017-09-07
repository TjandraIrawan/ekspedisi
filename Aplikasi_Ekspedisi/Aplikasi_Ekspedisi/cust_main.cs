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
    public partial class cust_main : Form
    {
        public int currRow;

        public cust_main()
        {
            InitializeComponent();
            
        }

        private void cust_main_Load(object sender, EventArgs e)
        {
            ViewGrid();
            Tampil();
            btn_Save.Enabled = false;
            btn_cancel.Enabled = false;
        }

        private void Btn_New_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox9.Clear();
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            textBox2.Focus();
            btn_Save.Enabled = true;
            btn_cancel.Enabled = true;
            btn_Update.Enabled = false;
            btn_Delete.Enabled = false;
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
            
                Connection con = new Connection();
                MySqlDataAdapter sda_cek = new MySqlDataAdapter("select * from CustomerInfo where CustomerNama = '" + textBox2.Text + "'", con.ActiveCon());
                MySqlCommand command_cek = new MySqlCommand();
                DataTable dt_cek = new DataTable();
                sda_cek.Fill(dt_cek);
                if (dt_cek.Rows.Count > 1)
                {
                    MessageBox.Show("Nama Pelanggan : " + textBox2.Text + " sudah ada", "Perhatian", MessageBoxButtons.OK);
                }
                else
                {
                    MySqlDataAdapter sda = new MySqlDataAdapter("tambah_no_cust", con.ActiveCon());
                    MySqlCommand command = new MySqlCommand
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    //MySqlCommand command = new MySqlCommand();
                    //command.CommandType = CommandType.StoredProcedure;
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    textBox1.Text = dt.Rows[0][0].ToString();

                    AddNewRecord();
                    ViewGrid();
                    btn_Save.Enabled = false;
                    btn_cancel.Enabled = false;
                    btn_Update.Enabled = true;
                    btn_Delete.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Proses Error");

            }
        }

        void AddNewRecord()
        {
            if (textBox7.Text == "")
            {
                textBox7.Text = "0";

            }
            try
            {
                Connection con = new Connection();
                string insertquery = @"insert into 
                    customerinfo(CustomerId, CustomerNama, CustomerAlamat, CustomerKota, CustomerTlp, CustomerStatus,
                    CustomerCr, CustomerDpp,CustomerMkt,CustomerPic) value 
                    ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "', '" + textBox5.Text + "','" + comboBox1.Text + "', '" + textBox7.Text + "','" + comboBox2.Text + "', '" + textBox9.Text + "','" + textBox6.Text + "')";

                //string insertquery = "insert into customerinfo(" +
                //    "CustomerId," +
                //    "CustomerNama," +
                //    "CustomerAlamat," +
                //    "CustomerKota," +
                //    "CustomerTlp," +
                //    "CustomerStatus," +
                //    "CustomerCr," +
                //    "CustomerDpp," +
                //    "CustomerMkt," +
                //    "CustomerPic) value (" +
                //    "'" + textBox1.Text + "'," +
                //    "'" + textBox2.Text + "'," +
                //    "'" + textBox3.Text + "'," +
                //    "'" + textBox4.Text + "'," +
                //    "'" + textBox5.Text + "'," +
                //    "'" + comboBox1.Text + "'," +
                //    "'" + textBox7.Text + "'," +
                //    "'" + comboBox2.Text + "'," +
                //    "'" + textBox9.Text + "'," +
                //    "'" + textBox6.Text + "')";

                MySqlCommand command = new MySqlCommand(insertquery,con.ActiveCon());
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Connection Error");

            }
        }

        void UpdateRecord()
        {
            if (textBox7.Text == "")
            {
                textBox7.Text = "0";

            }
            try
            {
                Connection con = new Connection();
                MySqlDataAdapter sda_cek = new MySqlDataAdapter("select * from CustomerInfo where CustomerId != '" + textBox1.Text + "' && CustomerNama = '" + textBox2.Text + "'", con.ActiveCon());
                MySqlCommand command_cek = new MySqlCommand();
                DataTable dt_cek = new DataTable();
                sda_cek.Fill(dt_cek);
                if (dt_cek.Rows.Count == 1)
                {
                    MessageBox.Show("Nama Pelanggan : " + textBox2.Text + " sudah ada", "Perhatian", MessageBoxButtons.OK);
                }
                else
                {     
                    string insertquery = "update customerinfo set " +
                    "CustomerId = '" + textBox1.Text + "'," +
                    "CustomerNama = '" + textBox2.Text + "'," +
                    "CustomerAlamat = '" + textBox3.Text + "'," +
                    "CustomerKota = '" + textBox4.Text + "'," +
                    "CustomerTlp = '" + textBox5.Text + "'," +
                    "CustomerStatus = '" + comboBox1.Text + "'," +
                    "CustomerCr = '" + textBox7.Text + "'," +
                    "CustomerDpp = '" + comboBox2.Text + "'," +
                    "CustomerMkt = '" + textBox9.Text + "'," +
                    "CustomerPic = '" + textBox6.Text + "'" +
                    "where CustomerId = '" + textBox1.Text + "'";

                MySqlCommand command = new MySqlCommand(insertquery, con.ActiveCon());
                command.ExecuteNonQuery();
                MessageBox.Show("Data sudah di UPDATE");
                ViewGrid();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Connection Error");

            }
        }

        void DeleteRecord()
        {
            DialogResult dialogResult = MessageBox.Show("Apakah data : "+textBox2.Text+" dihapus?", "Pertanyaan", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    Connection con = new Connection();
                    string insertquery = "delete from customerinfo where CustomerId = '" + textBox1.Text + "'";

                    MySqlCommand command = new MySqlCommand(insertquery, con.ActiveCon());
                    command.ExecuteNonQuery();
                    ViewGrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Connection Error");

                }
            }
        }

        void ViewGrid()
        {
            try
            {
                Connection con = new Connection();
                MySqlDataAdapter sda = new MySqlDataAdapter("select * from customerinfo ORDER by CustomerNama", con.ActiveCon());
                DataTable dt = new DataTable();
                sda.Fill(dt);
                dataGridView1.Rows.Clear();
                foreach (DataRow item in dt.Rows)
                {
                    int n = dataGridView1.Rows.Add();
                    dataGridView1.Rows[n].Cells[0].Value = item["CustomerNama"].ToString();
                    dataGridView1.Rows[n].Cells[1].Value = item["CustomerId"].ToString();
                    dataGridView1.Rows[n].Cells[2].Value = item["CustomerAlamat"].ToString();
                    dataGridView1.Rows[n].Cells[3].Value = item["CustomerKota"].ToString();
                    dataGridView1.Rows[n].Cells[4].Value = item["CustomerTlp"].ToString();
                    dataGridView1.Rows[n].Cells[5].Value = item["CustomerPic"].ToString();
                    dataGridView1.Rows[n].Cells[6].Value = item["CustomerCr"].ToString();
                    dataGridView1.Rows[n].Cells[7].Value = item["CustomerStatus"].ToString();
                    dataGridView1.Rows[n].Cells[8].Value = item["CustomerMkt"].ToString();
                    dataGridView1.Rows[n].Cells[9].Value = item["CustomerDpp"].ToString();
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Connection Error");

            }
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            //this.currRow = dataGridView1.CurrentRow.Index;
            Tampil();
            btn_Save.Enabled = false;
            btn_cancel.Enabled = false;
            btn_Update.Enabled = true;
            btn_Delete.Enabled = true;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        void Tampil()
        {
            //int n = dataGridView1.SelectedRows[0].Index;
            textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            textBox6.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            textBox7.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            textBox9.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
            comboBox2.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
            
        }


        private void btn_Update_Click(object sender, EventArgs e)
        {
            UpdateRecord();
            Tampil();
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            DeleteRecord();
            Tampil();
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsControl(e.KeyChar) && !Char.IsDigit(e.KeyChar)) 
            {
                e.Handled = true;
            }
            //char ch = e.KeyChar;
            //if (!Char.IsDigit(ch) && ch != 8 && ch != 46 && ch !=13)
            //{
            //    e.Handled = true;
            //}

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            Tampil();
            btn_Save.Enabled = false;
            btn_cancel.Enabled = false;
            btn_Update.Enabled = true;
            btn_Delete.Enabled = true;
        }

        private void txt_Search_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Connection con = new Connection();
                MySqlDataAdapter sda = new MySqlDataAdapter("select * from customerinfo where CustomerNama like '%"+ txt_Search.Text +"%' ORDER by CustomerNama", con.ActiveCon());
                DataTable dt = new DataTable(); 
                sda.Fill(dt);
                dataGridView1.Rows.Clear();
                foreach (DataRow item in dt.Rows)
                {
                    int n = dataGridView1.Rows.Add();
                    dataGridView1.Rows[n].Cells[0].Value = item["CustomerNama"].ToString();
                    dataGridView1.Rows[n].Cells[1].Value = item["CustomerId"].ToString();
                    dataGridView1.Rows[n].Cells[2].Value = item["CustomerAlamat"].ToString();
                    dataGridView1.Rows[n].Cells[3].Value = item["CustomerKota"].ToString();
                    dataGridView1.Rows[n].Cells[4].Value = item["CustomerTlp"].ToString();
                    dataGridView1.Rows[n].Cells[5].Value = item["CustomerPic"].ToString();
                    dataGridView1.Rows[n].Cells[6].Value = item["CustomerCr"].ToString();
                    dataGridView1.Rows[n].Cells[7].Value = item["CustomerStatus"].ToString();
                    dataGridView1.Rows[n].Cells[8].Value = item["CustomerMkt"].ToString();
                    dataGridView1.Rows[n].Cells[9].Value = item["CustomerDpp"].ToString();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Connection Error");

            }

        }

        private void dataGridView1_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_CursorChanged(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            
        }

        private void dataGridView1_Move(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_RowDefaultCellStyleChanged(object sender, DataGridViewRowEventArgs e)
        {
            
        }
    }
}

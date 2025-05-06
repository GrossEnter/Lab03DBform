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

namespace Lab03DBform
{
    public partial class fMain : Form
    {
        MySqlConnection conn;
        MySqlDataAdapter adapter;
        DataTable dataTable;
        public fMain()
        {
            InitializeComponent();
        }

        private void fMain_Load(object sender, EventArgs e)
        {
            conn = DBUtils.GetDBConnection();

            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не вдалося підключитися." + ex.Message, "Повідомлення.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                conn.Close();
                conn.Dispose();
                Application.Exit();
            }
        }
                
        private void fMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            conn.Close();
            conn.Dispose();
        }
        private void ViewData(string tableName)
        {
            string sql = $"select * from {tableName}";

            adapter = new MySqlDataAdapter(sql, conn);
            MySqlCommandBuilder builder = new MySqlCommandBuilder(adapter);
            dataTable = new DataTable();
            adapter.Fill(dataTable);
            dgv.DataSource = dataTable;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            ViewData("manufacturers");
        }

        private void btn_cars_Click(object sender, EventArgs e)
        {
            ViewData("cars");
        }

        private void btn_customers_Click(object sender, EventArgs e)
        {
            ViewData("customers");
        }

        private void btn_orders_Click(object sender, EventArgs e)
        {
            ViewData("orders");
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            try
            {
                adapter.Update(dataTable);
                MessageBox.Show("Дані оновлено.", "Повідомлення", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Не вдалося оновити дані." + ex.Message,"Помилка.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void btn_del_Click(object sender, EventArgs e)
        {
            if(dgv.CurrentRow == null)
            {
                MessageBox.Show("Оберіть запис для видалення.", "Повідомлення.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }   
            DialogResult result = MessageBox.Show("Ви впевнені, що хочете видалити запис?","Підтвердження.",MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
            {
                return;
            }

            dgv.Rows.RemoveAt(dgv.CurrentRow.Index);

            try
            {
                adapter.Update(dataTable);
                MessageBox.Show("Успішно видалено.", "Повідомлення", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не вдалося видалити запис." + ex.Message, "Помилка.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

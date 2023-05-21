using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demoapp
{
    public partial class FormUser : Form
    {
        List<string> listAccountType =new List<string>() {"管理", "スタッフ" };

        int index = -1;

        public FormUser()
        {
            InitializeComponent();
        }

        void LoadListUser()
        {
            // Lấy dữ liệu từ CSDL
            using (SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\Demoapp\\Demoapp\\csdl.mdf;Integrated Security=True"))
            {
                string query = "SELECT * FROM dbUser";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                dgvUser.DataSource = dataTable;
                dgvUser.Refresh();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void FormUser_Load(object sender, EventArgs e)
        {
            cboStaff.DataSource = listAccountType;
            LoadListUser();
        }

        private void dgvUser_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            index = e.RowIndex;

            if (index < 0)
                return;

            txbUserName.Text = dgvUser.Rows[index].Cells[0].Value.ToString();
            txbPassword.Text = dgvUser.Rows[index].Cells[1].Value.ToString();

            switch (bool.Parse(dgvUser.Rows[index].Cells[2].Value.ToString()))
            {
                case true:
                    cboStaff.Text = "管理";
                    break;
                case false:
                    cboStaff.Text = "スタッフ";
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string userName = txbUserName.Text;
            string password = txbPassword.Text;
            bool accountType = false;

            switch (cboStaff.Text)
            {
                case "管理":
                    accountType = true;
                    break;
                case "スタッフ":
                    accountType = false;
                    break;
            }

            // Thêm dữ liệu vào CSDL
            using (SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\Demoapp\\Demoapp\\csdl.mdf;Integrated Security=True"))
            {
                string query = "INSERT INTO dbUser (UserName, Password, AccountType) VALUES (@UserName, @Password, @AccountType)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserName", userName);
                command.Parameters.AddWithValue("@Password", password);
                command.Parameters.AddWithValue("@AccountType", accountType);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }

            LoadListUser();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (index < 0)
            {
                MessageBox.Show("レコードを選択");
                return;
            }

            string userName = txbUserName.Text;
            string password = txbPassword.Text;
            bool accountType = false;

            switch (cboStaff.Text)
            {
                case "管理":
                    accountType = true;
                    break;
                case "スタッフ":
                    accountType = false;
                    break;
            }

            // Cập nhật dữ liệu trong CSDL
            using (SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\Demoapp\\Demoapp\\csdl.mdf;Integrated Security=True"))
            {
                string query = "UPDATE dbUser SET UserName=@UserName, Password=@Password, AccountType=@AccountType WHERE UserName=@OldUserName";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserName", userName);
                command.Parameters.AddWithValue("@Password", password);
                command.Parameters.AddWithValue("@AccountType", accountType);
                command.Parameters.AddWithValue("@OldUserName", dgvUser.Rows[index].Cells[0].Value.ToString());
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }

            LoadListUser();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (index < 0)
            {
                MessageBox.Show("レコードを選択");
                return;
            }
           
            string userName = dgvUser.Rows[index].Cells[0].Value.ToString();
            string password = dgvUser.Rows[index].Cells[1].Value.ToString();
            bool accountType = (bool)dgvUser.Rows[index].Cells[2].Value;

            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\Demoapp\\Demoapp\\csdl.mdf;Integrated Security=True";
            string sql = "DELETE FROM dbUser WHERE UserName = @UserName AND Password = @Password AND AccountType = @AccountType;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@UserName", userName);
                command.Parameters.AddWithValue("@Password", password);
                command.Parameters.AddWithValue("@AccountType", accountType);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("レコードを削除しました");
                    ListUser.Instance.ListAccountUser.RemoveAt(index);
                    LoadListUser();
                }
                else
                {
                    MessageBox.Show("レコードを削除できませんでした");
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

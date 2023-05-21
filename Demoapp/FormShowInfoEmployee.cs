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
    public partial class FormShowInfoEmployee : Form
    {
        public FormShowInfoEmployee()
        {
            InitializeComponent();
        }

        void LoadInfo()
        {
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\Demoapp\\Demoapp\\csdl.mdf;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Tạo truy vấn SQL để lấy thông tin nhân viên từ CSDL
                string sql = $"SELECT * FROM dbEmployee WHERE Id = {Const.NewEmploy.EmployeeCode}";
                SqlCommand command = new SqlCommand(sql, connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        txbName.Text = reader["Name"].ToString();
                        txbSex.Text = reader["Sex"].ToString();
                        txbBirthDay.Text = Convert.ToDateTime(reader["BirthDay"]).ToShortDateString().ToString();
                        txbAdress.Text = reader["Address"].ToString();
                        txbPhone.Text = reader["Phone"].ToString();
                        txbExpertise.Text = reader["Expertise"].ToString();
                        txbEducation.Text = reader["Education"].ToString();
                        txbMail.Text = reader["Mail"].ToString();
                        txbId.Text = Const.NewEmploy.EmployeeCode.ToString();
                    }
                }
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void FormShowInfoEmployee_Load(object sender, EventArgs e)
        {
            LoadInfo();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txbBirthDay_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

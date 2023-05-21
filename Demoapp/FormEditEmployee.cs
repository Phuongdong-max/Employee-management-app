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
    public partial class FormEditEmployee : Form
    {
        public FormEditEmployee()
        {
            InitializeComponent();
        }

        void LoadInfo()
        {
            txbName.Text = Const.NewEmploy.Name;
            cboSex.Text = Const.NewEmploy.Sex;
            dtpkBirthDay.Value = Const.NewEmploy.BirthDay;
            txbAdress.Text = Const.NewEmploy.Address;
            txbPhone.Text = Const.NewEmploy.Phone;
            txbExpertise.Text = Const.NewEmploy.Expertise;
            txbEducation.Text = Const.NewEmploy.Education;
            txbMail.Text = Const.NewEmploy.Mail;


            txbId.Text = Const.NewEmploy.EmployeeCode.ToString();

        }

        private void FormEditEmployee_Load(object sender, EventArgs e)
        {
            cboSex.DataSource = Const.listSex;
            LoadInfo();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = txbName.Text;
            DateTime birthDay = dtpkBirthDay.Value;
            string sex = cboSex.Text;
            string address = txbAdress.Text;
            string phone = txbPhone.Text;
            string expertise = txbExpertise.Text;
            string education = txbEducation.Text;
            string mail = txbMail.Text;

            int employeeId = Convert.ToInt32(txbId.Text);

            // Kết nối CSDL
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\Demoapp\\Demoapp\\csdl.mdf;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Tạo truy vấn SQL để cập nhật thông tin nhân viên
                string sql = "UPDATE dbEmployee SET Name=@name, BirthDay=@birthDay, Sex=@sex, Address=@address, Phone=@phone, Expertise=@expertise, Education=@education, Mail=@mail WHERE Id = @Id";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@birthDay", birthDay);
                command.Parameters.AddWithValue("@sex", sex);
                command.Parameters.AddWithValue("@address", address);
                command.Parameters.AddWithValue("@phone", phone);
                command.Parameters.AddWithValue("@expertise", expertise);
                command.Parameters.AddWithValue("@education", education);
                command.Parameters.AddWithValue("@mail", mail);
                command.Parameters.AddWithValue("@Id", employeeId);

                // Thực thi truy vấn SQL
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    // Thông báo thành công và đóng form
                    MessageBox.Show("従業員情報の更新成功！");
                    this.Close();
                }
                else
                {
                    // Thông báo lỗi nếu không cập nhật được dữ liệu
                    MessageBox.Show("従業員情報を更新できません!");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

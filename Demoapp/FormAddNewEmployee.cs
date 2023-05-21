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
    public partial class FormAddNewEmployee : Form
    {
        public FormAddNewEmployee()
        {
            InitializeComponent();
        }

        private FormMain mainForm;
        public FormAddNewEmployee(FormMain mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
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

            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\Demoapp\\Demoapp\\csdl.mdf;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "INSERT INTO dbEmployee (Id, Name, BirthDay, Sex, Address, Phone, Education, Expertise, Mail) VALUES (@Id, @Name, @BirthDay, @Sex, @Address, @Phone, @Education, @Expertise, @Mail)";

                    SqlCommand command = new SqlCommand(query, connection);

                    command.Parameters.AddWithValue("@Id", employeeId);
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@BirthDay", birthDay);
                    command.Parameters.AddWithValue("@Sex", sex);
                    command.Parameters.AddWithValue("@Address", address);
                    command.Parameters.AddWithValue("@Phone", phone);
                    command.Parameters.AddWithValue("@Education", education);
                    command.Parameters.AddWithValue("@Expertise", expertise);
                    command.Parameters.AddWithValue("@Mail", mail);

                    int result = command.ExecuteNonQuery();

                    if (result > 0)
                    {
                        MessageBox.Show("従業員が正常に追加されました。");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("従業員の追加に失敗しました。");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }



            this.DialogResult = DialogResult.OK; // Đánh dấu form đã được đóng bằng cách nhấn nút OK
            this.Close();





        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        
        //check input

        private void FormAddNewEmployee_Load(object sender, EventArgs e)
        {
            cboSex.DataSource = Const.listSex;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

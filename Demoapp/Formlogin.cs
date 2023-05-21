using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demoapp
{
    public partial class Formlogin : Form
    {
        public Formlogin()
        {
            InitializeComponent();
        }

        private void txbUserName_TextChanged(object sender, EventArgs e)
        {

        }

        private void Formlogin_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        bool CheckLogin(string userName, string password)
        {
            for(int i = 0; i < ListUser.Instance.ListAccountUser.Count; i++) 
            {
                if (userName == ListUser.Instance.ListAccountUser[i].UserName && password == ListUser.Instance.ListAccountUser[i].Password)
                {
                    Const.AccountType = ListUser.Instance.ListAccountUser[i].AccountType;
                    return true;
                }
            }

            return false;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string userName = txbUserName.Text;
            string password = txbPassword.Text;


            if(CheckLogin(userName, password))
            {
                FormMain f = new FormMain();
                f.Show();
                this.Hide();
                f.Logout += F_Logout;
            }
            else 
            {
                MessageBox.Show("ユーザーIDまたはパスワードが間違っています", "Error", MessageBoxButtons.OK);
                txbUserName.Focus();
                return;
            
            }
           
        }

        private void F_Logout(object sender, EventArgs e)
        {
            (sender as FormMain).isExit = false;
            (sender as FormMain).Close();
            this.Show();

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Formlogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            txbPassword.UseSystemPasswordChar = !ckbShowPassword.Checked;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txbPassword_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

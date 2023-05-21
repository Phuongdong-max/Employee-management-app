using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demoapp
{
    public class User
    {
        private string userName;
        private string password;
        private bool accountType;

        public string UserName { get => userName; set => userName = value; }
        public string Password { get => password; set => password = value; }
        public bool AccountType { get => accountType; set => accountType = value; }

        public User(string userName, string password, bool accountType)
        {
            this.UserName = userName;
            this.Password = password;
            this.AccountType = accountType;
        }

        public static bool AddUser(User user)
        {
            bool result = false;
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\Demoapp\\Demoapp\\csdl.mdf;Integrated Security=True";
            string sql = "INSERT INTO dbUser (UserName, Password, AccountType) VALUES (@UserName, @Password, @AccountType)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@UserName", user.UserName);
                command.Parameters.AddWithValue("@Password", user.Password);
                command.Parameters.AddWithValue("@AccountType", user.AccountType);
                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        result = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demoapp
{
    using System.Data.SqlClient;
    using System.Windows.Forms;

    public class ListUser
    {
        private static ListUser instance;

        private List<User> listAccountUser;

        public List<User> ListAccountUser { get => listAccountUser; set => listAccountUser = value; }

        public static ListUser Instance
        {
            get
            {
                if (instance == null)
                    instance = new ListUser();
                return instance;
            }
            set => instance = value;
        }

        private ListUser()
        {
            listAccountUser = new List<User>();

            // Thực hiện kết nối đến CSDL
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\Demoapp\\Demoapp\\csdl.mdf;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM dbUser", connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string userName = reader.GetString(0);
                    string password = reader.GetString(1);
                    bool accountType = reader.GetBoolean(2);

                    listAccountUser.Add(new User(userName, password, accountType));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kết nối CSDL không thành công. Lỗi: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }


}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Demoapp
{
    public class ListEmployee
    {

        public event EventHandler DataChanged;

        public void Add(Employee newEmployee)
        {
            // Thêm nhân viên mới vào danh sách
            listEmploy.Add(newEmployee);

            // Kích hoạt sự kiện DataChanged
            if (DataChanged != null)
            {
                DataChanged(this, EventArgs.Empty);
            }
        }

        private static ListEmployee instance;
        private List<Employee> listEmploy;
        private string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\Demoapp\\Demoapp\\csdl.mdf;Integrated Security=True";
        private SqlConnection connection;

        public List<Employee> ListEmploy { get => listEmploy; set => listEmploy = value; }

        public static ListEmployee Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ListEmployee();
                    instance.LoadData();
                }
                return instance;
            }
            set => instance = value;
        }

        private ListEmployee()
        {
            listEmploy = new List<Employee>();
            connection = new SqlConnection(connectionString);
        }

        public void LoadData()
        {
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM dbEmployee", connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Employee employee = new Employee();
                    employee.EmployeeCode = (int)reader["Id"];
                    employee.Name = reader["Name"].ToString();
                    employee.BirthDay = (DateTime)reader["BirthDay"];
                    employee.Sex = reader["Sex"].ToString();
                    employee.Address = reader["Address"].ToString();
                    employee.Phone = reader["Phone"].ToString();
                    employee.Education = reader["Education"].ToString();
                    employee.Expertise = reader["Expertise"].ToString();
                    employee.Mail = reader["Mail"].ToString();
                    listEmploy.Add(employee);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}

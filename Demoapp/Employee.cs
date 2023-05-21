using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Demoapp
{
    public class Employee
    {
        private string name;
        private DateTime birthDay;
        private string sex;
        private string address;
        private string phone;
        private string education;
        private string expertise;
        private string mail;

        private int employeeCode;

        public string Name { get => name; set => name = value; }
        public DateTime BirthDay { get => birthDay; set => birthDay = value; }
        public string Sex { get => sex; set => sex = value; }
        public int EmployeeCode { get => employeeCode; set => employeeCode = value; }
        public string Address { get => address; set => address = value; }
        public string Phone { get => phone; set => phone = value; }
        public string Education { get => education; set => education = value; }
        public string Expertise { get => expertise; set => expertise = value; }
        public string Mail { get => mail; set => mail = value; }

        // Thêm các thuộc tính kết nối CSDL vào đối tượng Employee
        private static string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\Demoapp\\Demoapp\\csdl.mdf;Integrated Security=True";
        private static SqlConnection connection = new SqlConnection(connectionString);

        public static List<Employee> GetAllEmployees()
        {
            List<Employee> employees = new List<Employee>();
            string sql = "SELECT * FROM dbEmployee";
            SqlCommand command = new SqlCommand(sql, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Employee emp = new Employee();
                emp.EmployeeCode = (int)reader["Id"];
                emp.Name = reader["Name"].ToString();
                emp.BirthDay = (DateTime)reader["BirthDay"];
                emp.Sex = reader["Sex"].ToString();
                emp.Address = reader["Address"].ToString();
                emp.Phone = reader["Phone"].ToString();
                emp.Education = reader["Education"].ToString();
                emp.Expertise = reader["Expertise"].ToString();
                emp.Mail = reader["Mail"].ToString();
                employees.Add(emp);
            }
            reader.Close();
            connection.Close();
            return employees;
        }

        public Employee(string name, DateTime birthDay, string sex, string address, string phone, string education, string expertise, string mail, int employeeeCode)
        {
            this.Name = name;
            this.BirthDay = birthDay;
            this.Sex = sex;
            this.Address = address;
            this.Phone = phone;
            this.Education = education;
            this.Expertise = expertise;
            this.Mail = mail;
            this.EmployeeCode = employeeeCode;
        }

        public Employee()
        {

        }
    }
}

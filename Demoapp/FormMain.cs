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
    public partial class FormMain : Form
    {
        public bool isExit = true;

        int index = -1;

        public event EventHandler Logout;

        public FormMain()
        {
            InitializeComponent();
        }

        
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

       

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        #region Method

        public void ExportFile(DataTable dataTable, string sheetName, string title)
        {
            //Tạo các đối tượng Excel

            Microsoft.Office.Interop.Excel.Application oExcel = new Microsoft.Office.Interop.Excel.Application();

            Microsoft.Office.Interop.Excel.Workbooks oBooks;

            Microsoft.Office.Interop.Excel.Sheets oSheets;

            Microsoft.Office.Interop.Excel.Workbook oBook;

            Microsoft.Office.Interop.Excel.Worksheet oSheet;

            //Tạo mới một Excel WorkBook 

            oExcel.Visible = true;

            oExcel.DisplayAlerts = false;

            oExcel.Application.SheetsInNewWorkbook = 1;

            oBooks = oExcel.Workbooks;

            oBook = (Microsoft.Office.Interop.Excel.Workbook)(oExcel.Workbooks.Add(Type.Missing));

            oSheets = oBook.Worksheets;

            oSheet = (Microsoft.Office.Interop.Excel.Worksheet)oSheets.get_Item(1);

            oSheet.Name = sheetName;

            // Tạo phần Tiêu đề
            Microsoft.Office.Interop.Excel.Range head = oSheet.get_Range("A1", "I1");

            head.MergeCells = true;

            head.Value2 = title;

            head.Font.Bold = true;

            head.Font.Name = "Times New Roman";

            head.Font.Size = "20";

            head.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            // Tạo tiêu đề cột 

            Microsoft.Office.Interop.Excel.Range cl1 = oSheet.get_Range("A3", "A3");

            cl1.Value2 = "ID";

            cl1.ColumnWidth = 12;

            Microsoft.Office.Interop.Excel.Range cl2 = oSheet.get_Range("B3", "B3");

            cl2.Value2 = "氏名";

            cl2.ColumnWidth = 25.0;

            Microsoft.Office.Interop.Excel.Range cl3 = oSheet.get_Range("C3", "C3");

            cl3.Value2 = "生年月日";
            cl3.ColumnWidth = 12.0;

            Microsoft.Office.Interop.Excel.Range cl4 = oSheet.get_Range("D3", "D3");

            cl4.Value2 = "性別";

            cl4.ColumnWidth = 10.5;

            Microsoft.Office.Interop.Excel.Range cl5 = oSheet.get_Range("E3", "E3");

            cl5.Value2 = "住所";

            cl5.ColumnWidth = 20.5;

            Microsoft.Office.Interop.Excel.Range cl6 = oSheet.get_Range("F3", "F3");

            cl6.Value2 = "資格";

            cl6.ColumnWidth = 18.5;

            Microsoft.Office.Interop.Excel.Range cl7 = oSheet.get_Range("G3", "G3");

            cl7.Value2 = "専門知識";

            cl7.ColumnWidth = 13.5;
            Microsoft.Office.Interop.Excel.Range cl8 = oSheet.get_Range("H3", "H3");

            cl8.Value2 = "電話番号";

            cl8.ColumnWidth = 13.5;
            Microsoft.Office.Interop.Excel.Range cl9 = oSheet.get_Range("I3", "I3");

            cl9.Value2 = "メール";

            cl9.ColumnWidth = 13.5;


            Microsoft.Office.Interop.Excel.Range rowHead = oSheet.get_Range("A3", "I3");

            rowHead.Font.Bold = true;

            // Kẻ viền

            rowHead.Borders.LineStyle = Microsoft.Office.Interop.Excel.Constants.xlSolid;

            // Thiết lập màu nền

            rowHead.Interior.ColorIndex = 6;

            rowHead.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            // Tạo mảng theo datatable

            object[,] arr = new object[dataTable.Rows.Count, dataTable.Columns.Count];

            //Chuyển dữ liệu từ DataTable vào mảng đối tượng

            for (int row = 0; row < dataTable.Rows.Count; row++)
            {
                DataRow dataRow = dataTable.Rows[row];

                for (int col = 0; col < dataTable.Columns.Count; col++)
                {
                    arr[row, col] = dataRow[col];
                }
            }

            //Thiết lập vùng điền dữ liệu

            int rowStart = 4;

            int columnStart = 1;

            int rowEnd = rowStart + dataTable.Rows.Count - 2;

            int columnEnd = dataTable.Columns.Count;

            // Ô bắt đầu điền dữ liệu

            Microsoft.Office.Interop.Excel.Range c1 = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowStart, columnStart];

            // Ô kết thúc điền dữ liệu

            Microsoft.Office.Interop.Excel.Range c2 = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowEnd, columnEnd];

            // Lấy về vùng điền dữ liệu

            Microsoft.Office.Interop.Excel.Range range = oSheet.get_Range(c1, c2);

            //Điền dữ liệu vào vùng đã thiết lập

            range.Value2 = arr;

            // Kẻ viền

            range.Borders.LineStyle = Microsoft.Office.Interop.Excel.Constants.xlSolid;

            // Căn giữa cột mã nhân viên

            //Microsoft.Office.Interop.Excel.Range c3 = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowEnd, columnStart];

            //Microsoft.Office.Interop.Excel.Range c4 = oSheet.get_Range(c1, c3);

            //Căn giữa cả bảng 
            oSheet.get_Range(c1, c2).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
        }



        void Decentralization()
        {
            if(Const.AccountType == false)
            {
                tsmiUser.Enabled = tsmiEmployee.Enabled = tsmiDepartment.Enabled = false;
            }
        }


        public void LoadListEmployee()
        {
            dtgvEmployee.Rows.Clear();

            // Kết nối CSDL
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\Demoapp\\Demoapp\\csdl.mdf;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Tạo truy vấn SQL để lấy thông tin nhân viên từ CSDL
                string sql = "SELECT * FROM dbEmployee";
                SqlCommand command = new SqlCommand(sql, connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Tạo đối tượng Employee từ dữ liệu lấy được từ CSDL
                        Employee emp = new Employee();
                        emp.EmployeeCode = Convert.ToInt32(reader["Id"]);
                        emp.Name = reader["Name"].ToString();
                        emp.BirthDay = Convert.ToDateTime(reader["BirthDay"]);
                        emp.Sex = reader["Sex"].ToString();
                        emp.Address = reader["Address"].ToString();
                        emp.Phone = reader["Phone"].ToString();
                        emp.Expertise = reader["Expertise"].ToString();
                        emp.Education = reader["Education"].ToString();
                        emp.Mail = reader["Mail"].ToString();

                        // Thêm nhân viên vào DataGridView
                        dtgvEmployee.Rows.Add(emp.EmployeeCode, emp.Name, emp.BirthDay.ToShortDateString(), emp.Sex, emp.Address, emp.Expertise, emp.Education, emp.Phone, emp.Mail);
                    }
                }
            }
        }




        #endregion

        #region Event

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(isExit)
            {
                if (MessageBox.Show("プログラムを終了しますか？", "Arlet", MessageBoxButtons.YesNo) != DialogResult.Yes)
                    e.Cancel = true;
            }
        }
        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(isExit)
              Application.Exit();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            btnShow.Enabled = btnAddNew.Enabled = btnEdit.Enabled = btnDelete.Enabled = false;
            Decentralization();
            //dtgvEmployee.DataSource = ListEmployee.Instance.ListEmploy;

            LoadListEmployee();


        }

        private void ログアウトToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logout(this, new EventArgs());
        }

        private void スタッフToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnShow.Enabled = btnAddNew.Enabled = btnEdit.Enabled = btnDelete.Enabled = true;
        }

        private void 管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(Const.AccountType == false)
            MessageBox.Show("You are not ADMIN", "Arlet");
            return;

        }

        private void tsmiUser_Click(object sender, EventArgs e)
        {
            FormUser f = new FormUser();
            f.ShowDialog();
        }

        private void dtgvEmployee_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            index = e.RowIndex;

            if (index < 0 || index >= ListEmployee.Instance.ListEmploy.Count)
                return;

            Const.NewEmploy = new Employee();

            Const.NewEmploy.Name = ListEmployee.Instance.ListEmploy[index].Name;
            Const.NewEmploy.BirthDay = ListEmployee.Instance.ListEmploy[index].BirthDay;
            Const.NewEmploy.Sex = ListEmployee.Instance.ListEmploy[index].Sex;            
            Const.NewEmploy.Address = ListEmployee.Instance.ListEmploy[index].Address;
            Const.NewEmploy.Expertise = ListEmployee.Instance.ListEmploy[index].Expertise;
            Const.NewEmploy.Education = ListEmployee.Instance.ListEmploy[index].Education;
            Const.NewEmploy.Phone = ListEmployee.Instance.ListEmploy[index].Phone;
            Const.NewEmploy.Mail = ListEmployee.Instance.ListEmploy[index].Mail;


            Const.NewEmploy.EmployeeCode = ListEmployee.Instance.ListEmploy[index].EmployeeCode;
        }


        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            if (index < 0 || index >= ListEmployee.Instance.ListEmploy.Count)
            {
                MessageBox.Show("レコードを選択してください");

                return;
            }

            FormShowInfoEmployee f = new FormShowInfoEmployee();
            f.ShowDialog();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            FormAddNewEmployee f = new FormAddNewEmployee();
            f.FormClosed += (s, args) => {
                if (f.DialogResult == DialogResult.OK)
                {
                    int index = dtgvEmployee.Rows.Count - 1; // Lấy index của hàng vừa thêm vào
                    if (index < 0 || index >= ListEmployee.Instance.ListEmploy.Count)
                    {
                        MessageBox.Show("レコードを選択してください");
                        return;
                    }
                    dtgvEmployee.Rows[index].Selected = true; // Chọn hàng vừa thêm vào
                    LoadListEmployee();
                }
            };
            f.ShowDialog();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (index < 0 || index >= ListEmployee.Instance.ListEmploy.Count)
            {
                MessageBox.Show("レコードを選択してください");
                return;
            }

            FormEditEmployee f = new FormEditEmployee();
            f.FormClosed += F_FormClosed1;
            f.ShowDialog();
        }

        private void F_FormClosed1(object sender, FormClosedEventArgs e)
        {
            ListEmployee.Instance.ListEmploy[index].Name = Const.NewEmploy.Name;
            ListEmployee.Instance.ListEmploy[index].BirthDay = Const.NewEmploy.BirthDay;
            ListEmployee.Instance.ListEmploy[index].Sex = Const.NewEmploy.Sex;
            ListEmployee.Instance.ListEmploy[index].Address = Const.NewEmploy.Address;
            ListEmployee.Instance.ListEmploy[index].Phone = Const.NewEmploy.Phone;
            ListEmployee.Instance.ListEmploy[index].Expertise = Const.NewEmploy.Expertise;
            ListEmployee.Instance.ListEmploy[index].Education = Const.NewEmploy.Education;
            ListEmployee.Instance.ListEmploy[index].Mail = Const.NewEmploy.Mail;
            ListEmployee.Instance.ListEmploy[index].EmployeeCode = Const.NewEmploy.EmployeeCode;

            LoadListEmployee(); // cập nhật lại dữ liệu trên DataGridView
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (index < 0 || index >= ListEmployee.Instance.ListEmploy.Count)
            {
                MessageBox.Show("レコードを選択してください");
                return;
            }

            if (MessageBox.Show("このレコードを削除してよろしいですか？", "確認", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                int id = ListEmployee.Instance.ListEmploy[index].EmployeeCode;

                // Thực hiện xóa bản ghi khỏi csdl
                string query = "DELETE FROM dbEmployee WHERE id = @id";
                using (SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\Demoapp\\Demoapp\\csdl.mdf;Integrated Security=True"))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }

                // Sau đó, cập nhật lại danh sách ListEmploy và DataGridView
                ListEmployee.Instance.ListEmploy.RemoveAt(index);
                LoadListEmployee(); // cập nhật lại dữ liệu trên DataGridView
            }
        }


        private void eXCELファイルにエクスポートToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable dataTable = new DataTable();

            DataColumn col1 = new DataColumn("ID");
            DataColumn col2 = new DataColumn("名前");
            DataColumn col3 = new DataColumn("生年月日");
            DataColumn col4 = new DataColumn("性別");
            DataColumn col5 = new DataColumn("住所");
            DataColumn col6 = new DataColumn("電話番号");
            DataColumn col7 = new DataColumn("資格");
            DataColumn col8 = new DataColumn("専門知識");
            DataColumn col9 = new DataColumn("メール");

            dataTable.Columns.Add(col1);
            dataTable.Columns.Add(col2);
            dataTable.Columns.Add(col3);
            dataTable.Columns.Add(col4);
            dataTable.Columns.Add(col5);
            dataTable.Columns.Add(col6);
            dataTable.Columns.Add(col7);
            dataTable.Columns.Add(col8);
            dataTable.Columns.Add(col9);

            foreach (DataGridViewRow dtgvRow in dtgvEmployee.Rows)
            {
                DataRow dtrow = dataTable.NewRow();

                dtrow[0] = dtgvRow.Cells[0].Value;
                dtrow[1] = dtgvRow.Cells[1].Value;
                dtrow[2] = dtgvRow.Cells[2].Value;
                dtrow[3] = dtgvRow.Cells[3].Value;
                dtrow[4] = dtgvRow.Cells[4].Value;
                dtrow[5] = dtgvRow.Cells[5].Value;
                dtrow[6] = dtgvRow.Cells[6].Value;
                dtrow[7] = dtgvRow.Cells[7].Value;
                dtrow[8] = dtgvRow.Cells[8].Value;

                dataTable.Rows.Add(dtrow);
            }

            ExportFile(dataTable, "リスト", "社員一覧");
        }

        #endregion



        private void dtgvEmployee_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void ヘルプToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("この機能は開発中です");

            return;
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("この機能は開発中です");

            return;
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("この機能は開発中です");

            return;
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            MessageBox.Show("この機能は開発中です");

            return;
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            MessageBox.Show("この機能は開発中です");

            return;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            MessageBox.Show("この機能は開発中です");

            return;
        }
    }
}
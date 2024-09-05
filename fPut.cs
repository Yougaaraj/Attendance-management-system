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

namespace Attendance
{
public partial class fPut : Form
{
public fPut(string data)
{
InitializeComponent();
label2.Text = data;
          

}

private void fPut_Load(object sender, EventArgs e)
{
void PopulateClassesComboBox(string FacultyId1)
{
// Assuming you have a connection string defined
string connectionString = " Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Attendance;Integrated Security=True";

// Assuming you have a table structure like Class (FacultyId NVARCHAR(50), ClassName NVARCHAR(255))
string query = "SELECT DISTINCT cName FROM Class WHERE cFaculty = @FacultyId";

using (SqlConnection connection = new SqlConnection(connectionString))
{
connection.Open();

using (SqlCommand command = new SqlCommand(query, connection))
{
command.Parameters.AddWithValue("@FacultyId", label2.Text);

using (SqlDataReader reader = command.ExecuteReader())
{
while (reader.Read())
{
string className1 = reader["cName"].ToString();
ComboBoxItem item = new ComboBoxItem { DisplayText = className1 };
comboBox1.Items.Add(item);
}
}
}
}
}
PopulateClassesComboBox(label2.Text);

}

private void button1_Click(object sender, EventArgs e)
{
DataTable FetchDataFromDatabase()
{
DataTable dataTable = new DataTable();
string connectionString = " Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Attendance;Integrated Security=True";
string Class = comboBox1.SelectedItem.ToString();
using (SqlConnection connection = new SqlConnection(connectionString))
{
string query = $"SELECT StudentId, StudentName FROM {Class}";

using (SqlCommand command = new SqlCommand(query, connection))
{
connection.Open();

using (SqlDataReader reader = command.ExecuteReader())
{
// Load the data into the DataTable
dataTable.Load(reader);
}
}
}

return dataTable;
}
void PopulateDataGridView()
{
DataTable dataTable = FetchDataFromDatabase();

foreach (DataRow row in dataTable.Rows)
{
int rowIndex = dataGridView1.Rows.Add();
dataGridView1.Rows[rowIndex].Cells["Column1"].Value = row["StudentID"];
dataGridView1.Rows[rowIndex].Cells["Column2"].Value = row["StudentName"];

// Add a CheckBox to the "Status" column and set it to checked
DataGridViewCheckBoxCell checkBoxCell = new DataGridViewCheckBoxCell();
checkBoxCell.Value = true; // Set it to checked
dataGridView1.Rows[rowIndex].Cells["Column3"] = checkBoxCell;
}
}

// Rename the columns if needed
/* dataGridView1.Columns[0].HeaderText = "Student ID";
dataGridView1.Columns[1].HeaderText = "Student Name";*/

PopulateDataGridView();
}

private void button2_Click(object sender, EventArgs e)
{
            




}

private void pictureBox5_Click(object sender, EventArgs e)
{
this.Close();
}

private void pictureBox4_Click(object sender, EventArgs e)
{
WindowState = FormWindowState.Maximized;
}

private void pictureBox3_Click(object sender, EventArgs e)
{
WindowState= FormWindowState.Minimized;
}

      
           
        


private void button2_Click_1(object sender, EventArgs e)
{
//string selectedTable = Attendance.comboBox1.SelectedItem.ToString();  // Assuming ComboBoxitem is the name of your ComboBox control.

string connectionString = " Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Attendance;Integrated Security=True";
// Call this method when you want to send data to the database, for example, on a button click

// Ensure the DataGridView has data
if (dataGridView1.Rows.Count > 0)
{
// Send data to the database
InsertDataIntoDatabase();
}
else
{
MessageBox.Show("No data in the DataGridView to insert.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
}
void InsertDataIntoDatabase()
{
foreach (DataGridViewRow row in dataGridView1.Rows)
{
// Assuming the columns are in the order of StudentId, StudentName, and Status
var studentId = row.Cells["Column1"].Value;
var studentName = (string)row.Cells["Column2"].Value;
var status = " ";

// Map the CheckBox state to status

//var status = (bool)row.Cells["Column3"].Value ? "Present" : "Absent";
if (row.Cells["Column3"] is DataGridViewCheckBoxCell checkBoxCell && checkBoxCell.Value != null && checkBoxCell.Value is bool)
                    
// Map the CheckBox state to status
status = (bool)checkBoxCell.Value ? "Present" : "Absent";

DateTime date = dateTimePicker1.Value;
var c = (comboBox1.SelectedItem).ToString();

// Check if studentId is null or DBNull.Value
if (studentId != null && studentId != DBNull.Value)
{
using (SqlConnection connection = new SqlConnection(connectionString))
{
// SQL query to insert data into the database
string query = "INSERT INTO AT (sId, sName, sStatus, sDate, sClass) VALUES (@StudentId, @StudentName, @Status, @dt, @class)";

using (SqlCommand command = new SqlCommand(query, connection))
{
// Add parameters to the SQL query
command.Parameters.AddWithValue("@StudentId", studentId);
command.Parameters.AddWithValue("@StudentName", studentName);
command.Parameters.AddWithValue("@Status", status);
command.Parameters.AddWithValue("@dt", date);
command.Parameters.AddWithValue("@class", c);

connection.Open();

// Execute the SQL query
command.ExecuteNonQuery();
}
}
}
else
{
// Handle the case where studentId is null or DBNull.Value (optional)
Console.WriteLine("StudentId is null or DBNull.Value for a row.");
}
}
MessageBox.Show("Attendance added");
}



}
}
}




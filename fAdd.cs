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
using Excel = Microsoft.Office.Interop.Excel;

namespace Attendance
{
    

public partial class fAdd : Form
{
private ComboBox combo;
public fAdd(string Data)
{
InitializeComponent();
label3.Text = Data;
}

private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
{

}

private void fAdd_Load(object sender, EventArgs e)
{
// Set up the scrollbar
/* vScrollBar1.Minimum = 0;
vScrollBar1.Maximum = 100;  // Adjust as needed
vScrollBar1.LargeChange = panel1.Height;

// Attach the event handler for scrollbar value changes
vScrollBar1.ValueChanged += VScrollBar1_ValueChanged;*/
void PopulateClassesComboBox(string facultyId1)
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
command.Parameters.AddWithValue("@FacultyId", label3.Text);

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
PopulateClassesComboBox(label3.Text);
}
/*  private void VScrollBar1_ValueChanged(object sender, EventArgs e)
{
// Adjust the position of the panel based on the scrollbar value
panel1.Top = -vScrollBar1.Value;
}*/

private void nButton1_Click(object sender, EventArgs e)
{
string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Attendance;Integrated Security=True";

// Get the faculty information (you may need to adapt this based on your application's logic)
string facultyId = label3.Text; // Implement the logic to get the current faculty's ID
string className = textBox1.Text; // Assuming you have a TextBox for entering class name

// Step 1: Insert an entry into the class table
using (SqlConnection connection = new SqlConnection(connectionString))
{
connection.Open();

string insertQuery = "INSERT INTO Class (cFaculty, cName) VALUES (@FacultyId, @ClassName)";

using (SqlCommand command = new SqlCommand(insertQuery, connection))
{
command.Parameters.AddWithValue("@FacultyId", facultyId);
command.Parameters.AddWithValue("@ClassName", className);

command.ExecuteNonQuery();
}
}

// Step 2: Create a new table using the given class name
using (SqlConnection connection = new SqlConnection(connectionString))
{
connection.Open();

// SQL data type NVARCHAR(20) for the Status column
string createTableQuery = $"CREATE TABLE {className} (StudentId VARCHAR(50), StudentName NVARCHAR(50) ,PRIMARY KEY(StudentId))";
try
{
using (SqlCommand command = new SqlCommand(createTableQuery, connection))
{
command.ExecuteNonQuery();
}
}
catch (Exception ex) { MessageBox.Show(ex.Message); }
}

MessageBox.Show("Class added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
void PopulateClassesComboBox(string facultyId1)
{
// Assuming you have a connection string defined
//string connectionString = "your_connection_string_here";

// Assuming you have a table structure like Class (FacultyId NVARCHAR(50), ClassName NVARCHAR(255))
string query = "SELECT DISTINCT cName FROM Class WHERE cFaculty = @FacultyId";

using (SqlConnection connection = new SqlConnection(connectionString))
{
connection.Open();

using (SqlCommand command = new SqlCommand(query, connection))
{
command.Parameters.AddWithValue("@FacultyId", facultyId);

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
comboBox1.Items.Clear();
PopulateClassesComboBox(facultyId);


// Assuming your DataGridView has columns named "Name" and "ID"



}

private void nButton2_Click(object sender, EventArgs e)
{
string selectedClassName = label3.Text; // Implement this method based on your UI

if (string.IsNullOrEmpty(selectedClassName))
{
MessageBox.Show("Please select a class.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
return;
}
try
{
foreach (DataGridViewRow row in dataGridView1.Rows)
{
string studentID = row.Cells["Column1"].Value?.ToString();
string studentName = row.Cells["Column2"].Value?.ToString();
// string status = row.Cells["Column4"].Value?.ToString();
//  DateTime date = dateTimePicker1.Value;
if (!string.IsNullOrEmpty(studentName) && !string.IsNullOrEmpty(studentID))
{
// Insert the student into the selected class table
InsertStudentIntoClass(selectedClassName, studentName, studentID);
// MessageBox.Show("Students added to the class successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
}
}
MessageBox.Show("Students added to the class successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

}
catch (Exception ex) { MessageBox.Show(ex.Message); }
void InsertStudentIntoClass(string className1, string studentName, string studentID)
{
// Assuming you have a connection string defined
string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Attendance;Integrated Security=True";
string tableName = comboBox1.SelectedItem?.ToString(); // Assuming comboBox1 contains table names

if (string.IsNullOrEmpty(tableName))
{
MessageBox.Show("Please select a class.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
return;
}

using (SqlConnection connection = new SqlConnection(connectionString))
{
connection.Open();

// Assuming you have a table structure like Class (ClassName NVARCHAR(255), StudentName NVARCHAR(255), StudentID NVARCHAR(255))
string insertQuery = $"INSERT INTO {tableName} (StudentName, StudentID) VALUES (@StudentName, @StudentID)";

using (SqlCommand command = new SqlCommand(insertQuery, connection))
{
command.Parameters.AddWithValue("@StudentName", studentName);
command.Parameters.AddWithValue("@StudentID", studentID);
// command.Parameters.AddWithValue("@Status", status);
// command.Parameters.AddWithValue("@Date", date);

command.ExecuteNonQuery();
}
}
}




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
WindowState = FormWindowState.Minimized;
}

private void nButton3_Click(object sender, EventArgs e)
{
void LoadExcelData()
{
// Show a file dialog to allow the user to select an Excel file
OpenFileDialog openFileDialog = new OpenFileDialog
{
Title = "Select Excel File",
Filter = "Excel Files|*.xls;*.xlsx|All Files|*.*",
Multiselect = false // Allow only one file selection
};

// Check if the user clicked OK in the file dialog
if (openFileDialog.ShowDialog() == DialogResult.OK)
{
// Get the selected file path
string excelFilePath = openFileDialog.FileName;

// Create an Excel application object
Excel.Application excelApp = new Excel.Application();

// Open the workbook
Excel.Workbook workbook = excelApp.Workbooks.Open(excelFilePath);

// Assume the data is in the first worksheet
Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Sheets[1];

// Get the used range of cells
Excel.Range usedRange = worksheet.UsedRange;

// Get the number of rows and columns in the worksheet
int rowCount = usedRange.Rows.Count;
int colCount = usedRange.Columns.Count;

// Iterate through DataGridView rows and populate cells
for (int row = 2; row <= rowCount; row++)
{
// Add a new row to the DataGridView
int rowIndex = dataGridView1.Rows.Add();

// Populate the DataGridView cells directly
dataGridView1.Rows[rowIndex].Cells["Column1"].Value = usedRange.Cells[row, 1].Value?.ToString();
dataGridView1.Rows[rowIndex].Cells["Column2"].Value = usedRange.Cells[row, 2].Value?.ToString();
}

// Close Excel objects
workbook.Close();
excelApp.Quit();
}
}

LoadExcelData();
}
public class ComboBoxItem
{
public string DisplayText { get; set; }
//public string AdditionalText { get; set; }

public override string ToString()
{
return $"{DisplayText} ";
}
}

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

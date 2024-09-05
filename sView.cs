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
public partial class sView : Form
{
public sView(string data)
{
InitializeComponent();
label2.Text = data;
}

private void sView_Load(object sender, EventArgs e)
{
void PopulateClassesComboBox()
{
// Assuming you have a connection string defined
string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Attendance;Integrated Security=True";

// Assuming you have a table structure like Class (FacultyId NVARCHAR(50), ClassName NVARCHAR(255))
string query = "SELECT DISTINCT cName FROM Class ";

using (SqlConnection connection = new SqlConnection(connectionString))
{
connection.Open();

using (SqlCommand command = new SqlCommand(query, connection))
{
// command.Parameters.AddWithValue("@FacultyId", label2.Text);

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
PopulateClassesComboBox();
}

private void button1_Click(object sender, EventArgs e)
{

// Get class name, student ID, and date from the form controls
string className = comboBox1.SelectedItem.ToString();
string studentId = label2.Text;
// DateTime selectedDate = dateTimePicker1.Value;

// Database connection string
string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Attendance;Integrated Security=True";

// SQL query to search records based on class name, student ID, and date
string query = "SELECT sId, sName, sStatus, sDate FROM AT " +
"WHERE sClass= @ClassName AND sId = @StudentId ";

using (SqlConnection connection = new SqlConnection(connectionString))
{
connection.Open();

using (SqlCommand command = new SqlCommand(query, connection))
{
// Add parameters to the SQL query
command.Parameters.AddWithValue("@ClassName", className);
command.Parameters.AddWithValue("@StudentId", studentId);
// command.Parameters.AddWithValue("@AttendanceDate", selectedDate);

// Create a DataTable to store the results
DataSet resultTable = new DataSet();

// Use SqlDataAdapter to fill the DataTable
using (SqlDataAdapter adapter = new SqlDataAdapter(command))
{
adapter.Fill(resultTable,"AT");
}

// Display the results in the DataGridView
dataGridView1.DataSource = resultTable.Tables["AT"];
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
}
}


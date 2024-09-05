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
public partial class fView : Form
{
public fView(string data)
{
InitializeComponent();
label2.Text = data; 
}

private void fView_Load(object sender, EventArgs e)
{
void PopulateClassesComboBox(string facultyId1)
{
// Assuming you have a connection string defined
string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Attendance;Integrated Security=True";

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

           

            
if (comboBox1.SelectedItem != null)
{
// Assuming your ComboBoxItem has a property named 'Value' representing the class
string className = ((ComboBoxItem)comboBox1.SelectedItem).ToString();
DateTime date = dateTimePicker1.Value;

string con = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Attendance;Integrated Security=True";
string query = "SELECT sId, sName, sStatus FROM AT WHERE sClass = @Class AND sDate = @Date";

using (SqlConnection connection = new SqlConnection(con))
{
connection.Open();

using (SqlCommand command = new SqlCommand(query, connection))
{
command.Parameters.AddWithValue("@Class", className);
command.Parameters.AddWithValue("@Date", date.ToString("yyyy-MM-dd")); // Adjust date format if necessary

SqlDataAdapter adapter = new SqlDataAdapter(command);
DataSet ds = new DataSet();
adapter.Fill(ds, "att");
Console.WriteLine($"Rows count: {ds.Tables["att"].Rows.Count}");

// Output the formatted date for debugging
Console.WriteLine(date.ToString("dd-MM-yyyy"));

dataGridView1.DataSource = ds.Tables["att"];
}
}
}
else
{
MessageBox.Show("Please select a class before retrieving data.");
}








}

private void pictureBox5_Click(object sender, EventArgs e)
{
this.Close();
}
}
public class ComboBoxItem
{
public string DisplayText { get; set; }

// You can add additional properties as needed

public override string ToString()
{
return DisplayText;
}
}

}

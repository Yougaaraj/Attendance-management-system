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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Attendance.items.Forms
{
public partial class Login :Form
{
public Login()
{
InitializeComponent();
}

private void label1_Click(object sender, EventArgs e)
{

}

private void label3_Click(object sender, EventArgs e)
{
forgetp form2 = new forgetp();
form2.Show();
}

private void Login_Load(object sender, EventArgs e)
{

}

private void pictureBoxShow_Click(object sender, EventArgs e)
{
pictureBoxShow.Visible = false;
textBox2.PasswordChar = '\0';
}

private void labelError_Click(object sender, EventArgs e)
{

}

private void pictureBox2_Click(object sender, EventArgs e)
{

}

private void panel1_Paint(object sender, PaintEventArgs e)
{

}

private void label4_Click(object sender, EventArgs e)
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
WindowState = FormWindowState.Minimized;
}

private void nButton1_Click(object sender, EventArgs e)
{
string enteredUsername = textBox1.Text;
string enteredPassword = textBox2.Text.ToString();

if (string.IsNullOrEmpty(enteredUsername) || string.IsNullOrEmpty(enteredPassword))
{
MessageBox.Show("Please enter both username and password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
return;
}
if (comboBox1.SelectedItem == null)
{
                
MessageBox.Show("Please select an option for the mode.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
}
const string ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Attendance;Integrated Security=True";
if (comboBox1.SelectedItem == "Student")
{
using (SqlConnection connection = new SqlConnection(ConnectionString))
{
try
{
connection.Open();

string query = "SELECT COUNT(*) FROM Student WHERE sName = @Username AND sPass = @Password";
using (SqlCommand command = new SqlCommand(query, connection))
{
command.Parameters.AddWithValue("@Username", enteredUsername);
command.Parameters.AddWithValue("@Password", enteredPassword);

int userCount = (int)command.ExecuteScalar();

if (userCount > 0)
{
MessageBox.Show("Login successful.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
// You can navigate to the next form or perform other actions here
}
else
{
MessageBox.Show("Invalid username or password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
return;
}
}
}
catch (Exception ex)
{
MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
}
}
}
if (comboBox1.SelectedItem.ToString()== "Faculty")
{
using (SqlConnection connection = new SqlConnection(ConnectionString))
{
try
{
connection.Open();

string query = "SELECT COUNT(*) FROM Faculty WHERE fName = @Username AND fPass = @Password";
using (SqlCommand command = new SqlCommand(query, connection))
{
command.Parameters.AddWithValue("@Username", enteredUsername);
command.Parameters.AddWithValue("@Password", enteredPassword);

int userCount = (int)command.ExecuteScalar();

if (userCount > 0)
{
MessageBox.Show("Login successful.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
// You can navigate to the next form or perform other actions here
}
else
{
MessageBox.Show("Invalid username or password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
}
}
}
catch (Exception ex)
{
MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
}
}
}
if (comboBox1.SelectedItem == "Faculty")
{
string username1 = textBox1.Text;
string password1 = textBox2.Text;
string result1 = GetUserId(username1, password1);
string GetUserId(string username, string password)
{
string userId = "" ; 

using (SqlConnection connection = new SqlConnection(ConnectionString))
{
connection.Open();

string query = "SELECT fId FROM Faculty WHERE fName = @Username AND fPass = @Password";

using (SqlCommand command = new SqlCommand(query, connection))
{
command.Parameters.AddWithValue("@Username", username);
command.Parameters.AddWithValue("@Password", password);

object result = command.ExecuteScalar();

if (result != null && result != DBNull.Value)
{
userId = Convert.ToString(result);
}
}
}

return userId;
}
fDash form2 = new fDash(result1);
form2.Show();
}
if (comboBox1.SelectedItem == "Student")
{
string username1 = textBox1.Text;
string password1 = textBox2.Text;
string result1 = GetUserId(username1, password1);
string GetUserId(string username, string password)
{
string userId = "" ; // Default value if user is not found

using (SqlConnection connection = new SqlConnection(ConnectionString))
{
connection.Open();

string query = "SELECT sId FROM Student WHERE sName = @Username AND sPass = @Password";

using (SqlCommand command = new SqlCommand(query, connection))
{
command.Parameters.AddWithValue("@Username", username);
command.Parameters.AddWithValue("@Password", password);

object result = command.ExecuteScalar();

if (result != null && result != DBNull.Value)
{
userId = result.ToString();
}
}
}

return userId;
}
sView form2 = new sView(result1);
form2.Show();
}





}

private void label5_Click(object sender, EventArgs e)
{
signup newsign = new signup();
newsign.Show();
}

private void pictureBoxHide_Click(object sender, EventArgs e)
{
pictureBoxShow.Visible = true;
textBox2.PasswordChar = '*';

}
}
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Drawing.Text;

namespace Attendance
{
public partial class signup : Form
{
public signup()
{
InitializeComponent();
}

private void nButton1_Click(object sender, EventArgs e)
{

if (string.IsNullOrWhiteSpace(textBox1.Text))
{
// Show an error message
MessageBox.Show("Please enter a value for Username.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


}
if (string.IsNullOrWhiteSpace(textBox2.Text))
{
// Show an error message
MessageBox.Show("Please enter a value for Password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
return;

}
if (string.IsNullOrWhiteSpace(textBox3.Text))
{
// Show an error message
MessageBox.Show("Please enter a value for Confirm password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
return;

}
if (string.IsNullOrWhiteSpace(textBox4.Text))
{
// Show an error message
MessageBox.Show("Please enter a value for id.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
return;


}
if (string.IsNullOrWhiteSpace(textBox5.Text))
{
// Show an error message
MessageBox.Show("Please enter a value for class.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
return;

}
if (string.IsNullOrWhiteSpace(textBox6.Text))
{
// Show an error message
MessageBox.Show("Please enter a value for Date of Birth.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
return;

}
if (comboBox1.SelectedItem == null)
{
// Display an error message
MessageBox.Show("Please select an option for the mode.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
return;

}
if (comboBox1.SelectedItem.ToString() == "Student")
{
const string ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Attendance;Integrated Security=True";
bool CheckRegistrationNumberExists(string registrationNumber)
{
using (SqlConnection connection = new SqlConnection(ConnectionString))
{
connection.Open();
string query = "SELECT COUNT(*) FROM Student WHERE sId = @RegistrationNumber";
using (SqlCommand command = new SqlCommand(query, connection))
{
command.Parameters.AddWithValue("@RegistrationNumber", registrationNumber);
int count = (int)command.ExecuteScalar();
return count > 0;
}
}
}

bool CheckCredentials(string username, string password)
{
using (SqlConnection connection = new SqlConnection(ConnectionString))
{
connection.Open();

string query = "SELECT COUNT(*) FROM Student WHERE sName = @Username AND sPass = @Password";
using (SqlCommand command = new SqlCommand(query, connection))
{
command.Parameters.AddWithValue("@Username", username);
command.Parameters.AddWithValue("@Password", password);

int count = (int)command.ExecuteScalar();

return count > 0;
}



}
}
if (!CheckRegistrationNumberExists(textBox4.Text))
{

if (!CheckCredentials(textBox1.Text, textBox2.Text))
{
if (textBox2.Text == textBox3.Text)
{
string username = textBox1.Text;
string password = textBox2.Text;
string id = textBox4.Text;
string userClass = textBox5.Text;
string dob = textBox6.Text;
using (SqlConnection connection = new SqlConnection(ConnectionString))
{
try
{
    connection.Open();

    string query = "INSERT INTO Student (sName, sPass, sID,sclass, sdOB) VALUES (@Username, @Password, @ID, @Class, @DOB)";
    using (SqlCommand command = new SqlCommand(query, connection))
    {
        command.Parameters.AddWithValue("@Username", username);
        command.Parameters.AddWithValue("@Password", password);
        command.Parameters.AddWithValue("@ID", id);
        command.Parameters.AddWithValue("@Class", userClass);
        command.Parameters.AddWithValue("@DOB", dob);

        int rowsAffected = command.ExecuteNonQuery();

        if (rowsAffected > 0)
        {
            MessageBox.Show("User registration successful.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);         

        }
        else
        {
            MessageBox.Show("Failed to register user.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
catch (Exception ex)
{
    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
}
}


}
else
{
MessageBox.Show("Password and Confirm password are different");
}

}
else
{
MessageBox.Show("Username and Password already exist");
}

}
else
{
MessageBox.Show("An account already exist with your id");
}

}
if (comboBox1.SelectedItem.ToString() == "Faculty")
{
const string ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Attendance;Integrated Security=True";
bool CheckRegistrationNumberExists(string registrationNumber)
{
using (SqlConnection connection = new SqlConnection(ConnectionString))
{
connection.Open();
string query = "SELECT COUNT(*) FROM Faculty WHERE fId = @RegistrationNumber";
using (SqlCommand command = new SqlCommand(query, connection))
{
command.Parameters.AddWithValue("@RegistrationNumber", registrationNumber);
int count = (int)command.ExecuteScalar();
return count > 0;
}
}
}

bool CheckCredentials(string username, string password)
{
using (SqlConnection connection = new SqlConnection(ConnectionString))
{
connection.Open();

string query = "SELECT COUNT(*) FROM Faculty WHERE fName = @Username AND fPass = @Password";
using (SqlCommand command = new SqlCommand(query, connection))
{
command.Parameters.AddWithValue("@Username", username);
command.Parameters.AddWithValue("@Password", password);

int count = (int)command.ExecuteScalar();

return count > 0;
}



}
}
if (!CheckRegistrationNumberExists(textBox4.Text))
{

if (!CheckCredentials(textBox1.Text, textBox2.Text))
{
if (textBox2.Text == textBox3.Text)
{
string username = textBox1.Text;
string password = textBox2.Text;
string id = textBox4.Text;
string userClass = textBox5.Text;
string dob = textBox6.Text;
using (SqlConnection connection = new SqlConnection(ConnectionString))
{
try
{
    connection.Open();

    string query = "INSERT INTO Faculty (fName, fPass, fID, fclass, fdOB) VALUES (@Username, @Password, @ID, @Class, @DOB)";                                   using (SqlCommand command = new SqlCommand(query, connection))
    {
        command.Parameters.AddWithValue("@Username", username);
        command.Parameters.AddWithValue("@Password", password);
        command.Parameters.AddWithValue("@ID", id);
        command.Parameters.AddWithValue("@Class", userClass);
        command.Parameters.AddWithValue("@DOB", dob);

        int rowsAffected = command.ExecuteNonQuery();

        if (rowsAffected > 0)
        {
            MessageBox.Show("User registration successful.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        else
        {
            MessageBox.Show("Failed to register user.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
catch (Exception ex)
{
    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
}
}


}
else
{
MessageBox.Show("Password and Confirm password are different");
}

}
else
{
MessageBox.Show("Username and Password already exist");
}

}
else
{
MessageBox.Show("An account already exist with your id");
}

}



}

      
private void pictureBox4_Click(object sender, EventArgs e)
{
WindowState = FormWindowState.Maximized;
}

private void pictureBox3_Click(object sender, EventArgs e)
{
WindowState = FormWindowState.Minimized;
}

private void pictureBox5_Click(object sender, EventArgs e)
{
this.Close();
}
}
}

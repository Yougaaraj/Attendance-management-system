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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Attendance
{
    public partial class forgetp : Form
    {
        public forgetp()
        {
            InitializeComponent();
        }

        private void nButton1_Click(object sender, EventArgs e)
        {
           string username = textBox2.Text;
            string id = textBox1.Text;
            string dob = textBox3.Text;// Format the date to match the database
             string newPassword = textBox4.Text;
            string mode = comboBox1.SelectedItem.ToString();
            string reg, db,name,pass;
            if(mode == "Student")
            {
                reg = "sId";
                db = "sDob";
                name = "sName";
                pass = "sPass";
            }
            else
            {
                reg = "sId";
                db = "sDob";
                name = "sName";
                pass = "sPass";

            }
            // Database connection string
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Attendance;Integrated Security=True";

            // SQL query to update the password
            string query = $"UPDATE {mode} SET {pass} = @NewPassword WHERE {name} = @Usernamee AND {reg} = @Id AND {db} = @Dob";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters to the SQL query
                    command.Parameters.AddWithValue("@Usernamee", username);
                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@Dob", dob);
                    command.Parameters.AddWithValue("@NewPassword", newPassword);
                    //command.Parameters.AddWithValue("@mode",mode);
                   
                    // Execute the update query
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Password updated successfully.");
                    }
                    else
                    {
                        MessageBox.Show("No matching record found. Please check your inputs.");
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
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Attendance
{
public partial class fDash : Form
{
string rec;
public fDash(string data)
{
InitializeComponent();
rec = data;
}

private void button1_Click(object sender, EventArgs e)
{
panel3.Controls.Clear();
fAdd form4 = new fAdd(rec);

// Set properties for the form
form4.TopLevel = false;
form4.FormBorderStyle = FormBorderStyle.None;
form4.Dock = DockStyle.Fill;

// Set the Parent property to the Panel
panel3.Controls.Add(form4);

// Show the form
form4.Show();

}

private void button2_Click(object sender, EventArgs e)
{
panel3.Controls.Clear();
fView form2 = new fView(rec);

// Set properties for the form
form2.TopLevel = false;
form2.FormBorderStyle = FormBorderStyle.None;
form2.Dock = DockStyle.Fill;

// Set the Parent property to the Panel
panel3.Controls.Add(form2);

// Show the form
form2.Show();

}

private void button3_Click(object sender, EventArgs e)
{
panel3.Controls.Clear();
fPut form3 = new fPut(rec);
form3.Parent = null; // Ensure it has no parent

panel3.Resize += (sender1, e1) =>
{
form3.Size = panel3.Size; // Adjust the form size when the panel is resized
};
// Set properties for the form
form3.TopLevel = false;
form3.FormBorderStyle = FormBorderStyle.None;
form3.Dock = DockStyle.Fill;

// Set the Parent property to the Panel
panel3.Controls.Add(form3);

// Show the form
form3.Show();

}

private void panel3_Paint(object sender, PaintEventArgs e)
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

private void fDash_Load(object sender, EventArgs e)
{

}
}
}

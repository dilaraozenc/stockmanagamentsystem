using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace StokManagamentSystem
{
    public partial class CustomerModuleForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Dilara\OneDrive\Belgeler\dbMS.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=False");
        SqlCommand cmd = new SqlCommand();
        private object lblCId;

        public CustomerModuleForm()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to save this customer?", "Saving Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cmd = new SqlCommand("INSERT INTO tbCustomer(cname, cphone) VALUES(@cname, @cphone)", con);
                    cmd.Parameters.AddWithValue("@cname", txtCName.Text);
                    cmd.Parameters.AddWithValue("@cphone", txtCPhone.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Record Saved Successfully!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void Clear()
        {
            txtCName.Clear();
            txtCPhone.Clear();
        }

        private void btnClear_Click_1(object sender, EventArgs e)
        {
            Clear();
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to update this customer?", "Update Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cmd = new SqlCommand("UPDATE tbCustomer SET cname=@cname, cphone=@cphone WHERE cid=@cid", con);
                    cmd.Parameters.AddWithValue("@cname", txtCName.Text);
                    cmd.Parameters.AddWithValue("@cphone", txtCPhone.Text);
                    cmd.Parameters.AddWithValue("@cid", lblCId); 
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Customer has been succesfully updated!");
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CustomerModuleForm_Load(object sender, EventArgs e)
        {

        }

        private void lblCld_Click(object sender, EventArgs e)
        {

        }
    }
}

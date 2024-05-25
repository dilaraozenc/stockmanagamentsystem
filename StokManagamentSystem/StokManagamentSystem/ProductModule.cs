using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace StokManagamentSystem
{
    public partial class ProductModule : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Dilara\OneDrive\Belgeler\dbMS.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=False");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;

        public ProductModule()
        {
            InitializeComponent();
            LoadCategory();
        }

        public void LoadCategory()
        {
            try
            {
                comboCat.Items.Clear();
                cmd = new SqlCommand("SELECT catname FROM tbCategory", con);
                con.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    comboCat.Items.Add(dr["catname"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading categories: " + ex.Message);
            }
            finally
            {
                dr.Close();
                con.Close();
            }
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to save this product?", "Saving Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // Eğer kullanıcı kaydedilmek istenirse
                    con.Open();
                    cmd = new SqlCommand("INSERT INTO tbProduct(pname, pqty, pprice, pdescription, pcategory) VALUES (@pname, @pqty, @pprice, @pdescription, @pcategory)", con);
                    cmd.Parameters.AddWithValue("@pname", txtPName.Text);
                    cmd.Parameters.AddWithValue("@pqty", Convert.ToInt16(txtPQty.Text));
                    cmd.Parameters.AddWithValue("@pprice", Convert.ToInt16(txtPPrice.Text));
                    cmd.Parameters.AddWithValue("@pdescription", txtPDes.Text);
                    cmd.Parameters.AddWithValue("@pcategory", comboCat.SelectedItem.ToString());
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Product Saved Successfully!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Clear()
        {
            txtPName.Clear();
            txtPQty.Clear();
            txtPPrice.Clear();
            txtPDes.Clear();
            comboCat.Text = "";
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
        }

        private void ProductModule_Load(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to update this product?", "Update Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cmd = new SqlCommand("UPDATE tbProduct SET pname=@pname, pqty=@pqty, pprice=@pprice, pdescription=@pdescription, pcategory=@pcategory WHERE pid LIKE '" + lblPid.Text + "'", con);
                    cmd.Parameters.AddWithValue("@pname", txtPName.Text);
                    cmd.Parameters.AddWithValue("@pqty", Convert.ToInt16(txtPQty.Text));
                    cmd.Parameters.AddWithValue("@pprice", Convert.ToInt16(txtPPrice.Text));
                    cmd.Parameters.AddWithValue("@pdescription", txtPDes.Text);
                    cmd.Parameters.AddWithValue("@pcategory", comboCat.SelectedItem.ToString());
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Product has been succesfully updated!");
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

          
            
            }
        }
    



using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace StokManagamentSystem
{
    public partial class OrderModuleForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Dilara\OneDrive\Belgeler\dbMS.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=False");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;
        int qty = 0;

        public OrderModuleForm()
        {
            InitializeComponent();
            LoadCustomer();
            LoadProduct();
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void LoadCustomer()
        {
            try
            {
                int i = 0;
                dgvCustomer.Rows.Clear();
                cmd = new SqlCommand("SELECT cid, cname FROM tbCustomer WHERE CONTACT(cid,cname) LIKE '%" + txtSearchCust.Text + "%'", con);
                con.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    i++;
                    dgvCustomer.Rows.Add(dr[0].ToString(), dr[1].ToString());
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading customers: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        public void LoadProduct()
        {
            try
            {
                int i = 0;
                dgvProduct.Rows.Clear();
                cmd = new SqlCommand("SELECT * FROM tbProduct WHERE CONTACT (pid,pname,pprice,pdescription,pcategory) LIKE '%" + txtSearchProd.Text + "%'", con);
                con.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    i++;
                    dgvProduct.Rows.Add(dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString());
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading products: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void txtSearchCust_TextChanged(object sender, EventArgs e)
        {
            LoadCustomer();
        }

        private void txtSearchProd_TextChanged(object sender, EventArgs e)
        {
            LoadProduct();
        }



        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt16(UDQty.Value) > qty)
                MessageBox.Show("Instock  quantity is not enough!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            UDQty.Value = UDQty.Value - 1;
            return;
        }

        private void dgvCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtCId.Text = dgvCustomer.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtCName.Text = dgvCustomer.Rows[e.RowIndex].Cells[1].Value.ToString();
        }

        private void dgvProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtPid.Text = dgvProduct.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtPName.Text = dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtPrice.Text = dgvProduct.Rows[e.RowIndex].Cells[2].Value.ToString();
            qty = Convert.ToUInt16(dgvProduct.Rows[e.RowIndex].Cells[3].Value.ToString());
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCId.Text == "")
                {
                    MessageBox.Show("Please select customer!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (txtPid.Text == "")
                {
                    MessageBox.Show("Please select product!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                con.Open();
                cmd = new SqlCommand("Insert Into tbOrder(odate,pid,cid,qty,price,total) Values(@odate,@pid,@cid,@qty,@price,@total)", con);
                cmd.Parameters.AddWithValue("@odate", dtOrder.Value);
                cmd.Parameters.AddWithValue("@pid", Convert.ToInt16(txtPid.Text));
                cmd.Parameters.AddWithValue("@cid", Convert.ToInt16(txtCId.Text));
                cmd.Parameters.AddWithValue("@qty", Convert.ToInt16(UDQty.Value));
                cmd.Parameters.AddWithValue("@price", Convert.ToInt16(txtPrice.Text));
                cmd.Parameters.AddWithValue("@total", Convert.ToInt16(txtTotal.Text));

                cmd.ExecuteNonQuery();


                cmd = new SqlCommand("UPDATE tbProduct SET pqty = (pqty - @pqty) WHERE pid = '" + txtPid.Text + "'", con);
                cmd.Parameters.AddWithValue("@pqty", Convert.ToInt16(UDQty.Value));
                cmd.ExecuteNonQuery();

                con.Close();
                MessageBox.Show("Order has been successfully inserted!");
                Clear();
                LoadProduct();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Clear()
        {
            txtCId.Clear();
            txtCName.Clear();
            txtPid.Clear();
            txtPName.Clear();
            txtPrice.Clear();
            UDQty.Value = 1;
            txtTotal.Clear();
            dtOrder.Value = DateTime.Now;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
           
           
        }
    }

}

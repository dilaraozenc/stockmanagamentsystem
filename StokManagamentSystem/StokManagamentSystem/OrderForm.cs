using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace StokManagamentSystem
{
    public partial class OrderForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Dilara\OneDrive\Belgeler\dbMS.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=False");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;

        public OrderForm()
        {
            InitializeComponent();
            LoadOrder();
        }

        public void LoadOrder()
        {
            try
            {
                int i = 0;
                dgvOrder.Rows.Clear();
                cmd = new SqlCommand("SELECT orderid,odate,O.pid,P.pname ,O.cid ,C.cname,qty,price,total  FROM tbOrder AS O  JOIN tbCustomer C ON O.cid=C.cid JOIN tbProduct AS P ON P.pid=P.pid", con);
                con.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    i++;
                    dgvOrder.Rows.Add(dr[0].ToString(), Convert.ToDateTime(dr[1]).ToString("dd/MM/yyyy"), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString());
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading orders: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void PictureBoxAdd_Click(object sender, EventArgs e)
        {
            OrderModuleForm moduleForm = new OrderModuleForm();
            moduleForm.ShowDialog();
            LoadOrder();
        }

        private void dgvOrder_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    string colName = dgvOrder.Columns[e.ColumnIndex].Name;

                    if (colName == "Delete")
                    {
                        if (MessageBox.Show("Are you sure you want to delete this order?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            con.Open();
                            SqlCommand cm = new SqlCommand("DELETE FROM tbOrder WHERE orderid LIKE @OrderID", con);
                            cm.Parameters.AddWithValue("@OrderID", dgvOrder.Rows[e.RowIndex].Cells[0].Value.ToString());
                            cm.ExecuteNonQuery();

                            cmd = new SqlCommand("UPDATE tbProduct SET pqty = (pqty - @pqty) WHERE pid = '" + dgvOrder.Rows[e.RowIndex].Cells[3].Value.ToString() + "'", con);
                            cmd.Parameters.AddWithValue("@pqty", Convert.ToInt16(dgvOrder.Rows[e.RowIndex].Cells[5].Value.ToString()));
                            cmd.ExecuteNonQuery();
                            cmd.ExecuteNonQuery();

                            con.Close();
                            MessageBox.Show("Record has been successfully deleted");
                            LoadOrder();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting order: " + ex.Message);
            }
        }
    }
}

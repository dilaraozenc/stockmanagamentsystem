using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StokManagamentSystem
{
    public partial class MainForm : Form
    {
        private Form activeForm = null;

        private void OpenChildForm(Form childForm)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelMain.Controls.Add(childForm);
            panelMain.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        public MainForm()
        {
            InitializeComponent();
        }

        private void pictureBoxUsers_Click(object sender, EventArgs e)
        {
            OpenChildForm(new UserForm1());

        }

        private void pictureBoxCustomer_Click(object sender, EventArgs e)
        {
            OpenChildForm(new CustomerForm());
        }

        private void pictureBoxCategories_Click(object sender, EventArgs e)
        {
            OpenChildForm(new CategoryForm());
               
        }

        private void pictureBoxProduct_Click(object sender, EventArgs e)
        {
            OpenChildForm(new ProductForm());
        }

        private void pictureBoxOrders_Click(object sender, EventArgs e)
        {
            OpenChildForm(new OrderForm());
        }
    }
}

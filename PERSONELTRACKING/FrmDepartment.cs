using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using DAL;
using DAL.DAO_DataAccessObject_;

namespace PERSONELTRACKING
{
    public partial class FrmDepartment : Form
    {
        public FrmDepartment()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!(txtDepartment.Text.Trim() == ""))
            {
                DEPARTMENT department = new DEPARTMENT();
                if (!isupdate)
                {
                    department.DepartmentName = txtDepartment.Text;
                    DepartmentBLL.AddDepartment(department);
                    MessageBox.Show("Department was added");
                    txtDepartment.Clear();
                }
                else
                {
                    DialogResult result = MessageBox.Show("Are you sure", "Warning", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        department.DepartmentName = txtDepartment.Text;
                        department.ID = detail.ID;
                        DepartmentBLL.UpdateDepartment(department);
                        MessageBox.Show("Department was Update");
                        this.Close();
                    }



                }
            }
            else
            {
                MessageBox.Show("Please Enter Department Name");
            }
        }
        public bool isupdate = false;
        public DEPARTMENT detail = new DEPARTMENT();
        private void FrmDepartment_Load(object sender, EventArgs e)
        {
            if (isupdate)
                txtDepartment.Text = detail.DepartmentName;
        }
    }
}

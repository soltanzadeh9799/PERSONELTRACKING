using BLL;
using DAL.DTO_DataTransferObject_;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PERSONELTRACKING
{
    public partial class FrmPermissionList : Form
    {
        public FrmPermissionList()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtUserNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.IsNumber(e);
        }

        private void txtDayAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.IsNumber(e);
        }
        PermissionDTO dto = new PermissionDTO();
        private bool combofull = false;
        void FillAllData()
        {
            dto = PermissionBLL.GetAll();
            if (!UserStatic.IsAdmin)
                dto.Permissions = dto.Permissions.Where(x => x.EmployeeID == UserStatic.EmployeeID).ToList();
            dataGridView1.DataSource = dto.Permissions;
            combofull = false;
            cmbDepartment.DataSource = dto.Departments;
            cmbDepartment.DisplayMember = "DepartmentName";
            cmbDepartment.ValueMember = "ID";
            cmbPosition.DataSource = dto.Positions;
            cmbPosition.DisplayMember = "PositionName";
            cmbPosition.ValueMember = "ID";
            cmbDepartment.SelectedIndex = -1;
            cmbPosition.SelectedIndex = -1;
            combofull = true;
            cmbState.DataSource = dto.States;
            cmbState.DisplayMember = "StateName";
            cmbState.ValueMember = "ID";
            cmbState.SelectedIndex = -1;
        }
        private void FrmPermissionList_Load(object sender, EventArgs e)
        {
             FillAllData();
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderText = "User No";
            dataGridView1.Columns[2].HeaderText = "Name";
            dataGridView1.Columns[3].HeaderText = "Surname";
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[8].HeaderText = "Start Date";
            dataGridView1.Columns[9].HeaderText = "End Date";
            dataGridView1.Columns[10].HeaderText = "Day Amount";
            dataGridView1.Columns[11].HeaderText = "State";
            dataGridView1.Columns[12].Visible = false;
            dataGridView1.Columns[13].Visible = false;
            if(!UserStatic.IsAdmin)
            {
                palAdmin.Visible = false;
                btnApprove.Visible = false;
                btnDisApprove.Visible = false;
                btnDelete.Visible = false;
                btnClose.Location = new Point(520, 26);



            }

        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            FrmPermission frm = new FrmPermission();
            this.Hide();
            frm.ShowDialog();
            this.Visible = true;
            FillAllData();
            CleanAllFilters();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (detail.PermissionID == 0)
                MessageBox.Show("Please select a permission from table");
            if(detail.State==PermissionStates.Approved || detail.State==PermissionStates.Disapproved)
                MessageBox.Show("You can not Update any approved and disapproved permission");
            else
            {
                FrmPermission frm = new FrmPermission();
                frm.isUpdate = true;
                frm.detail = detail;
                this.Hide();
                frm.ShowDialog();
                this.Visible = true;
                FillAllData();
                CleanAllFilters();

            }
            
            
        }

        private void btcSearch_Click(object sender, EventArgs e)
        {
            List<PermissionDetailDTO> list = dto.Permissions;
            if (txtUserNo.Text.Trim() != "")
                list = list.Where(x => x.UserNo == Convert.ToInt32(txtUserNo.Text.Trim())).ToList();
            if (txtName.Text.Trim() != "")
                list = list.Where(x => x.Name.Contains(txtName.Text)).ToList();
            if (txtSurname.Text.Trim() != "")
                list = list.Where(x => x.Name.Contains(txtSurname.Text)).ToList();
            if (cmbDepartment.SelectedIndex != -1)
                list = list.Where(x => x.DepartmentID == Convert.ToInt32(cmbDepartment.SelectedValue)).ToList();
            if (cmbPosition.SelectedIndex != -1)
                list = list.Where(x => x.PositionID == Convert.ToInt32(cmbPosition.SelectedValue)).ToList();
            if (rbStartDate.Checked)
                list=list.Where(x=>x.StartDate < Convert.ToDateTime(dpEnd.Value) &&
                    x.StartDate > Convert.ToDateTime(dpStart.Value)).ToList();
            else if (rbEndDate.Checked)
                list = list.Where(x => x.EndDate < Convert.ToDateTime(dpEnd.Value) &&
                    x.StartDate > Convert.ToDateTime(dpStart.Value)).ToList();
            if (cmbState.SelectedIndex != -1)
                list = list.Where(x => x.State == Convert.ToInt32(cmbState.SelectedValue)).ToList();
            if (txtDayAmount.Text.Trim()!="")
                list=list.Where(x=>x.PermissionDayAmount==Convert.ToInt32(txtDayAmount.Text.Trim())).ToList();


            dataGridView1.DataSource = list;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {

            CleanAllFilters();

        }

        private void CleanAllFilters()
        {
            txtUserNo.Clear();
            txtName.Clear();
            txtSurname.Clear();
            cmbDepartment.SelectedIndex = -1;
            cmbPosition.SelectedIndex = -1;
            combofull = false;
            cmbPosition.DataSource = dto.Positions;
            combofull = true;
            rbEndDate.Checked = false;
            rbStartDate.Checked = false;
            cmbState.SelectedIndex = -1;
            txtDayAmount.Clear();
            dataGridView1.DataSource = dto.Permissions;
        }
        PermissionDetailDTO detail = new PermissionDetailDTO();
        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            detail.UserNo = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[1].Value);
            detail.StartDate = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[8].Value);
            detail.EndDate = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[9].Value);
            detail.PermissionID= Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[14].Value);
            detail.State= Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[12].Value);
            detail.PermissionDayAmount= Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[10].Value);
            detail.Explanation= dataGridView1.Rows[e.RowIndex].Cells[13].Value.ToString();

        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            PermissionBLL.UpdatePermission(detail.PermissionID, PermissionStates.Approved);
            MessageBox.Show("Approved");
            FillAllData();
            CleanAllFilters();
        }

        private void btnDisApprove_Click(object sender, EventArgs e)
        {
            PermissionBLL.UpdatePermission(detail.PermissionID, PermissionStates.Disapproved);
            MessageBox.Show("DisApproved");
            FillAllData();
            CleanAllFilters();

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure Delete this Permission?","warning",MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                if(detail.State== PermissionStates.Approved || detail.State==PermissionStates.Disapproved)
                    MessageBox.Show("You cannot delete approved or disapproved Permission");
                else
                {
                    PermissionBLL.DeletePermission(detail.PermissionID);
                    MessageBox.Show("Permission was Deleted");
                    FillAllData();
                    CleanAllFilters();

                }
            }

        }

        private void ExportExcel_Click(object sender, EventArgs e)
        {
            ExportToExcel.ExportExcel(dataGridView1);
        }
    }
}

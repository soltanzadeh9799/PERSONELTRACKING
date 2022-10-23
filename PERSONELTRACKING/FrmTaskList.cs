using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL;
using BLL;
using DAL.DTO_DataTransferObject_;

namespace PERSONELTRACKING
{
    public partial class FrmTaskList : Form
    {
        public FrmTaskList()
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
        TaskDTO dto = new TaskDTO();
        private bool combofull = false;

        private void FillAllData()
        {
            dto = TaskBLL.GetAll();
            if (!UserStatic.IsAdmin)
                dto.Tasks = dto.Tasks.Where(x => x.EmployeeID == UserStatic.EmployeeID).ToList();
            dataGridView1.DataSource = dto.Tasks;
            cmbDepartment.DataSource = dto.Departments;
            cmbDepartment.DisplayMember = "DepartmentName";
            cmbDepartment.ValueMember = "ID";
            cmbPosition.DataSource = dto.Position;
            cmbPosition.DisplayMember = "PositionName";
            cmbPosition.ValueMember = "ID";
            cmbDepartment.SelectedIndex = -1;
            cmbPosition.SelectedIndex = -1;
            combofull = true;
            cmbTaskState.DataSource = dto.Taskstates;
            cmbTaskState.DisplayMember = "StateName";
            cmbTaskState.ValueMember = "ID";
            cmbTaskState.SelectedIndex = -1;
        }

        private void FrmTaskList_Load(object sender, EventArgs e)
        {
            FillAllData();

            dataGridView1.Columns[0].HeaderText = "Title";
            dataGridView1.Columns[1].HeaderText = "User No";
            dataGridView1.Columns[2].HeaderText = "Name";
            dataGridView1.Columns[3].HeaderText = "Surname";
            dataGridView1.Columns[4].HeaderText = "TaskstartDate";
            dataGridView1.Columns[5].HeaderText = "TaskDeliveryDate";
            dataGridView1.Columns[6].HeaderText = "Task State";
            dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[8].Visible = false;
            dataGridView1.Columns[9].Visible = false;
            dataGridView1.Columns[10].Visible = false;
            dataGridView1.Columns[11].Visible = false;
            dataGridView1.Columns[12].Visible = false;
            dataGridView1.Columns[13].Visible = false;
            dataGridView1.Columns[14].Visible = false;
            if (!UserStatic.IsAdmin)
            {
                btnNew.Visible = false;
                btnUpdate.Visible = false;
                btnDelete.Visible = false;
                btnClose.Location = new System.Drawing.Point(361, 25);
                btnApprove.Location = new System.Drawing.Point(247, 25);
                btnApprove.Text = "Delivery";
                pnlForAdmin.Hide();


            }



        }



        private void btnNew_Click(object sender, EventArgs e)
        {
            FrmTask frm = new FrmTask();
            this.Hide();
            frm.ShowDialog();
            this.Visible = true;
            FillAllData();
            CleanFilters();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (detail.TaskId == 0)
                MessageBox.Show("Please select a task on table");
            else
            {
                FrmTask frm = new FrmTask();
                frm.isupdate = true;
                frm.detail = detail;
                this.Hide();
                frm.ShowDialog();
                this.Visible = true;
                FillAllData();
                CleanFilters();
            }


        }

        private void cmbDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combofull)
                cmbPosition.DataSource = dto.Position.Where(x => x.DepartmentID == Convert.ToInt32(cmbDepartment.SelectedValue)).ToList();

        }

        private void btcSearch_Click(object sender, EventArgs e)
        {
            List<TaskDetailDTO> list = dto.Tasks;
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
                list = list.Where(x => x.TaskstartDate > Convert.ToDateTime(dpStart.Value) &&
                     x.TaskDeliveryDate < Convert.ToDateTime(dpEnd.Value)).ToList();
            if (rbDeliveryDate.Checked)
                list = list.Where(x => x.TaskDeliveryDate > Convert.ToDateTime(dpEnd.Value) &&
                     x.TaskDeliveryDate < Convert.ToDateTime(dpEnd.Value)).ToList();
            if (cmbTaskState.SelectedIndex != -1)
                list = list.Where(x => x.TaskStateID == Convert.ToInt32(cmbTaskState.SelectedValue)).ToList();


            dataGridView1.DataSource = list;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            CleanFilters();

        }

        private void CleanFilters()
        {
            txtUserNo.Clear();
            txtName.Clear();
            txtSurname.Clear();
            cmbDepartment.SelectedIndex = -1;
            cmbPosition.SelectedIndex = -1;
            combofull = false;
            cmbPosition.DataSource = dto.Position;
            combofull = true;
            rbDeliveryDate.Checked = false;
            rbStartDate.Checked = false;
            cmbTaskState.SelectedIndex = -1;
            dataGridView1.DataSource = dto.Tasks;
        }
        TaskDetailDTO detail = new TaskDetailDTO();
        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            detail.Name = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            detail.Surname = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            detail.Title = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            detail.Content = dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString();
            detail.UserNo = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[1].Value);
            detail.TaskStateID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[14].Value);
            detail.EmployeeID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[9].Value);
            detail.TaskId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[13].Value);
            detail.TaskstartDate = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[4].Value);
            detail.TaskDeliveryDate = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[5].Value);


        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure?", "Warning", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
                TaskBLL.DeleteTask(detail.TaskId);
            MessageBox.Show("Task Was Delete");
            FillAllData();
            CleanFilters();
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            if (UserStatic.IsAdmin && detail.TaskStateID == TaskStates.OnEmployee && UserStatic.EmployeeID != detail.EmployeeID)
                MessageBox.Show("Befor Approve a task emloyee have to delivery task");
            else if (UserStatic.IsAdmin && detail.TaskStateID == TaskStates.Approved)
                MessageBox.Show("this task is already approved");
            else if (!UserStatic.IsAdmin && detail.TaskStateID == TaskStates.Approved)
                MessageBox.Show("this task is already approved");
            else if (!UserStatic.IsAdmin && detail.TaskStateID == TaskStates.Delivered)
                MessageBox.Show("this task is already Deliverd");
            else
            {
                TaskBLL.ApproveTask(detail.TaskId, UserStatic.IsAdmin);
                MessageBox.Show("task was update");
                FillAllData();
                CleanFilters();
            }
        }

        private void ExportExcel_Click(object sender, EventArgs e)
        {
            ExportToExcel.ExportExcel(dataGridView1);
        }
    }
}

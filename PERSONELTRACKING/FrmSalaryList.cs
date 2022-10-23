using BLL;
using DAL;
using DAL.DAO_DataAccessObject_;
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
    public partial class FrmSalaryList : Form
    {
        public FrmSalaryList()
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

        private void txtSalary_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.IsNumber(e);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            FrmSalary frm = new FrmSalary();
            this.Hide();
            frm.ShowDialog();
            this.Visible = true;
            FillAllData();
            CleanAllFilters();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (detail.SalaryId == 0)
                MessageBox.Show("Please Select a salary from table");
            else
            {
                FrmSalary frm = new FrmSalary();
                frm.isupdate = true;
                frm.detail = detail;
                this.Hide();
                frm.ShowDialog();
                this.Visible = true;
                FillAllData();
                CleanAllFilters();

            }

        }
        salaryDTO dto = new salaryDTO();
        private bool combofull = false;

        private void FillAllData()
        {
            dto = SalaryBLL.GetAll();
            if (!UserStatic.IsAdmin)
                dto.Salaries = dto.Salaries.Where(x => x.EmployeeID == UserStatic.EmployeeID).ToList();

            dataGridView1.DataSource = dto.Salaries;
            cmbDepartment.DataSource = dto.Departments;
            cmbDepartment.DisplayMember = "DepartmentName";
            cmbDepartment.ValueMember = "ID";
            cmbPosition.DataSource = dto.Positions;
            cmbPosition.DisplayMember = "PositionName";
            cmbPosition.ValueMember = "ID";
            cmbDepartment.SelectedIndex = -1;
            cmbPosition.SelectedIndex = -1;
            combofull = true;
            cmbMonth.DataSource = dto.Months;
            cmbMonth.DisplayMember = "MonthName";
            cmbMonth.ValueMember = "ID";
            cmbMonth.SelectedIndex = -1;
        }
        SalaryDetailDTO detail = new SalaryDetailDTO();
        private void FrmSalaryList_Load(object sender, EventArgs e)
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
            dataGridView1.Columns[8].HeaderText = "Month";
            dataGridView1.Columns[9].HeaderText = "Year";
            dataGridView1.Columns[10].Visible = false;
            dataGridView1.Columns[11].HeaderText = "Salary";
            dataGridView1.Columns[12].Visible = false;
            dataGridView1.Columns[13].Visible = false;
            if (!UserStatic.IsAdmin)
            {
                btnUpdate.Hide();
                btnDelete.Hide();
                btnClose.Location = new Point(406, 14);
                btnNew.Location = new Point(292, 14);
                pnlForAdmin.Hide();

            }




        }



        private void cmbDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combofull)
                cmbPosition.DataSource = dto.Positions.Where(x => x.DepartmentID == Convert.ToInt32(cmbDepartment.SelectedValue)).ToList();

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
            cmbMonth.SelectedIndex = -1;
            rbMore.Checked = false;
            rbLess.Checked = false;
            rbEqual.Checked = false;
            txtYear.Clear();
            txtSalary.Clear();
            dataGridView1.DataSource = dto.Salaries;
        }

        private void btcSearch_Click(object sender, EventArgs e)
        {
            List<SalaryDetailDTO> list = dto.Salaries;
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
            if (txtYear.Text.Trim() != "")
                list = list.Where(x => x.SalaryYear == Convert.ToInt32(txtYear.Text.Trim())).ToList();
            if (cmbMonth.SelectedIndex != -1)
                list = list.Where(x => x.MonthID == Convert.ToInt32(cmbMonth.SelectedValue)).ToList();
            if (txtSalary.Text.Trim() != "")
            {
                if (rbMore.Checked)
                    list = list.Where(x => x.SalaryAmount > Convert.ToInt32(txtSalary.Text.Trim())).ToList();
                if (rbLess.Checked)
                    list = list.Where(x => x.SalaryAmount < Convert.ToInt32(txtSalary.Text.Trim())).ToList();
                if (rbEqual.Checked)
                    list = list.Where(x => x.SalaryAmount == Convert.ToInt32(txtSalary.Text.Trim())).ToList();

            }

            dataGridView1.DataSource = list;
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            detail.Name = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            detail.Surname = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            detail.UserNo = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[1].Value);
            detail.SalaryId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[12].Value);
            detail.EmployeeID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
            detail.SalaryYear = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[9].Value);
            detail.MonthID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[10].Value);
            detail.SalaryAmount = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[11].Value);
            detail.OldSalary = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[13].Value);





        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you Sure?", "Warning", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
                SalaryBLL.DeleteSalary(detail.SalaryId);

        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            ExportToExcel.ExportExcel(dataGridView1);
        }
    }
}

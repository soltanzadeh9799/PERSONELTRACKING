using BLL;
using DAL;
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
    public partial class FrmSalary : Form
    {
        public FrmSalary()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        salaryDTO dto = new salaryDTO();
        private bool combofull = false;
        public bool isupdate = false;
        public SalaryDetailDTO detail = new SalaryDetailDTO();

        private void FrmSalary_Load(object sender, EventArgs e)
        {
            dto = SalaryBLL.GetAll();
            if (!isupdate)
            {


                dataGridView1.DataSource = dto.Employees;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "User No";
                dataGridView1.Columns[2].Visible = false;
                dataGridView1.Columns[3].HeaderText = "Name";
                dataGridView1.Columns[4].HeaderText = "Surname";
                dataGridView1.Columns[5].HeaderText = "Department";
                dataGridView1.Columns[6].HeaderText = "Position";

                dataGridView1.Columns[7].Visible = false;
                dataGridView1.Columns[8].Visible = false;
                dataGridView1.Columns[9].HeaderText = "salary";
                dataGridView1.Columns[10].Visible = false;
                dataGridView1.Columns[11].Visible = false;
                dataGridView1.Columns[12].Visible = false;
                dataGridView1.Columns[13].Visible = false;



                cmbDepartment.DataSource = dto.Departments;
                cmbDepartment.DisplayMember = "DepartmentName";
                cmbDepartment.ValueMember = "ID";
                cmbPosition.DataSource = dto.Positions;
                cmbPosition.DisplayMember = "PositionName";
                cmbPosition.ValueMember = "ID";
                cmbDepartment.SelectedIndex = -1;
                cmbPosition.SelectedIndex = -1;
                combofull = true;
            }
            cmbMonth.DataSource = dto.Months;
            cmbMonth.DisplayMember = "MonthName";
            cmbMonth.ValueMember = "ID";
            cmbMonth.SelectedIndex = -1;
            if (isupdate)
            {
                panel1.Hide();
                txtName.Text = detail.Name;
                txtSurname.Text = detail.Surname;
                txtSalary.Text = detail.SalaryId.ToString();
                txtYear.Text = detail.SalaryYear.ToString();
                cmbMonth.SelectedIndex = detail.MonthID;
                txtUserNo.Text = UserStatic.UserNo.ToString();
            }
        }
        SALARY salary = new SALARY();
        int OldSalary = 0;
        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            txtUserNo.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtName.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtSurname.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            txtYear.Text = DateTime.Today.Year.ToString();
            txtSalary.Text = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
            salary.EmployeeID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
            OldSalary = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[9].Value);


        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtYear.Text.Trim() == "")
                MessageBox.Show("Please Fill The Year");
            if (txtSalary.Text.Trim() == "")
                MessageBox.Show("Please Fill The Salary");
            if (cmbMonth.SelectedIndex == -1)
                MessageBox.Show("Please select a month");
            if (txtUserNo.Text.Trim() == "")
                MessageBox.Show("Please select an Employee From Table");
            else
            {
                bool control = false;
                if (!isupdate)
                {
                    if (salary.EmployeeID == 0)
                        MessageBox.Show("Please select an Employee From Table");
                    else
                    {
                        salary.Year = Convert.ToInt32(txtYear.Text);
                        salary.MonthID = Convert.ToInt32(cmbMonth.SelectedValue);
                        salary.Amount = Convert.ToInt32(txtSalary.Text);
                        if(salary.Amount>OldSalary)
                            control = true;
                        SalaryBLL.AddSalary(salary,control);
                        MessageBox.Show("Salary was Added");
                        cmbMonth.SelectedIndex = -1;
                        salary=new SALARY();
                    }

                }
                else 
                {
                    DialogResult result = MessageBox.Show("Are you sure?","warning",MessageBoxButtons.YesNo);
                    if(result == DialogResult.Yes)
                    {
                        SALARY salary = new SALARY();
                        salary.ID = detail.SalaryId;
                        salary.EmployeeID=detail.EmployeeID;
                        salary.Year = Convert.ToInt32(txtYear.Text);
                        salary.MonthID = Convert.ToInt32(cmbMonth.SelectedValue);
                        salary.Amount=Convert.ToInt32(txtSalary.Text);
                        
                        if (salary.Amount>detail.OldSalary)
                            control= true;
                        SalaryBLL.UpdateSalary(salary, control);
                        MessageBox.Show("Salary was Updated");
                        this.Close();

                    }

                }

            }
        }
    }
}

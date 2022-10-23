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
using System.IO;

namespace PERSONELTRACKING
{
    public partial class FrmEmployee : Form
    {
        public FrmEmployee()
        {
            InitializeComponent();
        }

        private void txtUserNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.IsNumber(e);
        }

        private void txtSalary_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.IsNumber(e);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        EmployeeDTO dto = new EmployeeDTO();
        public EmployeeDetailDTO detail = new EmployeeDetailDTO();
        public bool isupdate = false;
        string imagepath = "";
        private void FrmEmployee_Load(object sender, EventArgs e)
        {
            dto = EmployeeBLL.GetAll();
            cmbPosition.DataSource = dto.Positions;
            cmbPosition.DisplayMember = "PositionName";
            cmbPosition.ValueMember = "ID";
            cmbPosition.SelectedIndex = -1;

            cmbDepartment.DataSource = dto.Departments;
            cmbDepartment.DisplayMember = "DepartmentName";
            cmbDepartment.ValueMember = "ID";
            cmbDepartment.SelectedIndex = -1;
            CmbFull = true;
            if (isupdate)
            {
                txtName.Text = detail.Name;
                txtSurname.Text = detail.Surname;
                txtPassword.Text = detail.Password;
                txtUserNo.Text = detail.UserNo.ToString();
                chAdmin.Checked = Convert.ToBoolean(detail.isAdmin);
                txtAddress.Text = detail.Address;
                dateTimePicker1.Value = Convert.ToDateTime(detail.Birthday);
                cmbDepartment.SelectedValue = detail.DepartmentID;
                cmbPosition.SelectedValue = detail.PositionID;
                txtSalary.Text = detail.Salary.ToString();
                imagepath = Application.StartupPath + "\\Image\\" + detail.ImagePath;
                txtImagePath.Text = imagepath;
                pictureBox1.ImageLocation = imagepath;
            }
            if(!UserStatic.IsAdmin)
            {
                txtUserNo.Enabled = false;
                chAdmin.Enabled = false;
                cmbDepartment.Enabled=false;
                cmbPosition.Enabled=false ;
                txtSalary.Enabled=false ;
            }

        }
        bool CmbFull = false;
        private void cmbDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CmbFull)
            {
                int departmentID = Convert.ToInt32(cmbDepartment.SelectedValue);
                cmbPosition.DataSource = dto.Positions.Where(x => x.DepartmentID == departmentID).ToList();

            }
        }
        string filename = "";
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Load(openFileDialog1.FileName);
                txtImagePath.Text = openFileDialog1.FileName;
                string unique = Guid.NewGuid().ToString();
                filename += unique + openFileDialog1.SafeFileName;

            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtUserNo.Text.Trim() == "")
                MessageBox.Show("UserNo is Empty");

            else if (txtPassword.Text.Trim() == "")
                MessageBox.Show("Password is Empty");
            else if (txtName.Text.Trim() == "")
                MessageBox.Show("Name is Empty");
            else if (txtSurname.Text.Trim() == "")
                MessageBox.Show("Surname is Empty");
            else if (txtSalary.Text.Trim() == "")
                MessageBox.Show("Salary is Empty");
            else if (cmbDepartment.SelectedIndex == -1)
                MessageBox.Show("Department is Empty");
            else if (cmbPosition.SelectedIndex == -1)
                MessageBox.Show("Position is Empty");
            else
            {
                EMPLOYEE employee = new EMPLOYEE();
                if (!isupdate)
                {
                    if (!EmployeeBLL.isUnique(Convert.ToInt32(txtUserNo.Text)))
                        MessageBox.Show("this UserNo used by another Emploee");
                    else
                    {
                        
                        employee.UserNo = Convert.ToInt32(txtUserNo.Text);
                        employee.Password = txtPassword.Text;
                        employee.Name = txtName.Text;
                        employee.Surname = txtSurname.Text;
                        employee.Address = txtAddress.Text;
                        employee.isAdmin = chAdmin.Checked;
                        employee.PositionID = Convert.ToInt32(cmbPosition.SelectedValue);
                        employee.DepartmentID = Convert.ToInt32(cmbDepartment.SelectedValue);
                        employee.Salary = Convert.ToInt32(txtSalary.Text);
                        employee.BirthDay = dateTimePicker1.Value;
                        employee.ImagePath = filename;
                        EmployeeBLL.AddEmployee(employee);
                        File.Copy(txtImagePath.Text, @"Image\\" + filename);
                        MessageBox.Show("employee was added");
                        txtUserNo.Clear();
                        txtPassword.Clear();
                        txtName.Clear();
                        txtSurname.Clear();
                        txtAddress.Clear();
                        chAdmin.Checked = false;
                        CmbFull = false;
                        cmbDepartment.SelectedIndex = -1;
                        cmbPosition.DataSource = dto.Positions;
                        cmbPosition.SelectedIndex = -1;
                        CmbFull = true;
                        txtSalary.Clear();
                        dateTimePicker1.Value = DateTime.Today;
                        txtImagePath.Clear();
                        pictureBox1.Image = null;
                    }
                }
                else
                {
                    DialogResult result = MessageBox.Show("Are you sure?", "Warning", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        if(imagepath!=txtImagePath.Text)
                        {
                            if (File.Exists(@"Image\\" + detail.ImagePath))
                                File.Delete(@"Image\\" + detail.ImagePath);

                            File.Copy(txtImagePath.Text, @"Image\\" + filename);
                            employee.ImagePath=filename;

                        }
                        else
                         employee.ImagePath=detail.ImagePath;
                        employee.ID = detail.EmployeeID;
                        employee.Name=txtName.Text;
                        employee.Surname=txtSurname.Text;
                        employee.Address=txtAddress.Text;
                        employee.isAdmin=chAdmin.Checked;
                        employee.BirthDay = dateTimePicker1.Value;
                        employee.Password=txtPassword.Text;
                        employee.Salary = Convert.ToInt32(txtSalary.Text);
                        employee.UserNo = Convert.ToInt32(txtUserNo.Text);
                        employee.DepartmentID = Convert.ToInt32(cmbDepartment.SelectedValue);
                        employee.PositionID = Convert.ToInt32(cmbPosition.SelectedValue);
                        EmployeeBLL.UpdateEmployee(employee);
                        MessageBox.Show("Employee was Update");
                        this.Close();


                    }
                }



            }





        }
        bool isunique = false;
        private void btnCheck_Click(object sender, EventArgs e)
        {
            if (txtUserNo.Text.Trim() == "")
                MessageBox.Show("UserNo is Empty");
            else
            {
                isunique = EmployeeBLL.isUnique(Convert.ToInt32(txtUserNo.Text));
                if (!isunique)
                    MessageBox.Show("this UserNo used by another Emploee");
                else
                    MessageBox.Show("this userno is unused");
            }

        }
    }
}

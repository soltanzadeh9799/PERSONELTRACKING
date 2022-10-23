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
using DAL.DTO_DataTransferObject_;

namespace PERSONELTRACKING
{
    public partial class FrmPermission : Form
    {
        public FrmPermission()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmPermission_Load(object sender, EventArgs e)
        {

            txtUserNo.Text=UserStatic.UserNo.ToString();  
            if(isUpdate)
            {
                dpStart.Value = detail.StartDate;
                dpEnd.Value = detail.EndDate;
                txtDayAmount.Text = detail.PermissionDayAmount.ToString();
                txtExplanation.Text=detail.Explanation.ToString();
                txtUserNo.Text= detail.UserNo.ToString();

            }
        }
        TimeSpan PermissionDay;
        public bool isUpdate = false;
        public PermissionDetailDTO detail=new PermissionDetailDTO();
        private void dpStart_ValueChanged(object sender, EventArgs e)
        {
            PermissionDay=dpEnd.Value.Date - dpStart.Value.Date;
            txtDayAmount.Text=PermissionDay.TotalDays.ToString();
        }

        private void dpEnd_ValueChanged(object sender, EventArgs e)
        {
            PermissionDay = dpEnd.Value.Date - dpStart.Value.Date;
            txtDayAmount.Text = PermissionDay.TotalDays.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtDayAmount.Text.Trim() == "")
                MessageBox.Show("Please Change End or Start Date");
            if (Convert.ToInt32(txtDayAmount.Text) <= 0)
                MessageBox.Show("PermissionDay must be bigger than 0");
            if (txtExplanation.Text.Trim() == "")
                MessageBox.Show("Explanation is Empty");
            else
            {
                PERMISSION permission = new PERMISSION();
                if (!isUpdate)
                {
                    
                    permission.EmployeeID = UserStatic.EmployeeID;
                    permission.PermissionStartDate = dpStart.Value.Date;
                    permission.PermissionEndDate = dpEnd.Value.Date;
                    permission.PermissionDay = Convert.ToInt32(txtDayAmount.Text);
                    permission.PermissionExplanation = txtExplanation.Text;
                    PermissionBLL.AddPermission(permission);
                    MessageBox.Show("Permission was Added");
                    dpEnd.Value = DateTime.Today;
                    dpStart.Value = DateTime.Today;
                    txtDayAmount.Clear();
                    txtExplanation.Clear();
                    permission = new PERMISSION();
                }
                else if(isUpdate)
                {
                    DialogResult result = MessageBox.Show("Are You Sure", "Warning", MessageBoxButtons.YesNo);
                    if(result== DialogResult.Yes)
                    {
                        permission.EmployeeID = UserStatic.EmployeeID;
                        permission.PermissionStartDate = dpStart.Value.Date;
                        permission.PermissionEndDate = dpEnd.Value.Date;
                        permission.PermissionDay = Convert.ToInt32(txtDayAmount.Text);
                        permission.PermissionExplanation = txtExplanation.Text;
                        PermissionBLL.UpdatePermission(permission);

                    }
                }
            }
        }
    }
}

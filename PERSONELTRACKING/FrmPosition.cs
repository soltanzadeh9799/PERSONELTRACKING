using DAL;
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
using DAL.DTO_DataTransferObject_;

namespace PERSONELTRACKING
{
    public partial class FrmPosition : Form
    {
        public FrmPosition()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        List<DEPARTMENT> departmentlist = new List<DEPARTMENT>();
        public PositionDTO detail = new PositionDTO();
        public bool isupdste = false;
        private void FrmPosition_Load(object sender, EventArgs e)
        {
            departmentlist = DepartmentBLL.GetDepartment();
            cmbDepartment.DataSource = departmentlist;
            cmbDepartment.DisplayMember = "DepartmentName";
            cmbDepartment.ValueMember = "ID";
            cmbDepartment.SelectedIndex = -1;
            if (isupdste)
            {
                txtPosition.Text = detail.PositionName;
                cmbDepartment.SelectedValue = detail.DepartmentID;
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtPosition.Text.Trim() == "")
                MessageBox.Show("Please Enter a Position");
            else if (cmbDepartment.SelectedIndex == -1)
                MessageBox.Show("Please select a Department");
            else
            {
                POSITION position = new POSITION();
                if (!isupdste)
                {

                    position.PositionName = txtPosition.Text;
                    position.DepartmentID = Convert.ToInt32(cmbDepartment.SelectedValue);
                    PositionBll.AddPosition(position);
                    MessageBox.Show("Position was Added");
                    txtPosition.Clear();
                    cmbDepartment.SelectedIndex = -1;
                }
                else
                {
                    position.ID = detail.ID;
                    position.PositionName=txtPosition.Text;
                    position.DepartmentID = Convert.ToInt32(cmbDepartment.SelectedValue);
                    bool control = false;
                    if(Convert.ToInt32(cmbDepartment.SelectedValue) !=detail.OldDepartmenID)
                        control = true;
                    PositionBll.UpdatePosition(position, control);
                    MessageBox.Show("Position was Updated");
                    this.Close();

                }



            }
        }
    }
}

using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DAO_DataAccessObject_;

namespace BLL
{
    public class DepartmentBLL
    {
        public static void AddDepartment(DEPARTMENT department)
        {
            DepartmentDAO.AddDepartment(department);
        }

        public static void DeleteDepartment(int iD)
        {
            DepartmentDAO.DeleteDepartment(iD);
        }

        public static List<DEPARTMENT> GetDepartment()
        {
            return DepartmentDAO.GetDepartment();
        }

        public static void UpdateDepartment(DEPARTMENT department)
        {
            DepartmentDAO.UpdateDepartment(department);
        }
    }
}

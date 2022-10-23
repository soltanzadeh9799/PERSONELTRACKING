using DAL.DTO_DataTransferObject_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.DAO_DataAccessObject_;


namespace BLL
{
    public class EmployeeBLL
    {
        public static void AddEmployee(EMPLOYEE employee)
        {
            EmployeeDAO.AddEmployee(employee);
        }

        public static void DeleteEmployee(int employeeID)
        {
            EmployeeDAO.DeleteEmployee(employeeID);
        }

        public static EmployeeDTO GetAll()
        {
            EmployeeDTO dto=new EmployeeDTO();
            dto.Departments = DepartmentDAO.GetDepartment();
            dto.Positions = PositionDAO.GetPositions();
            dto.Employees = EmployeeDAO.GetEmployee();
            return dto;
        }

        public static List<EMPLOYEE> GetEmployee(int v, string text)
        {
            return EmployeeDAO.GetEmployee(v, text);
        }

        public static bool isUnique(int v)
        {
            List<EMPLOYEE> list = EmployeeDAO.GetUsers(v);
            if(list.Count > 0)

                return false;
            else 
                return true;

        }

        public static void UpdateEmployee(EMPLOYEE employee)
        {
            EmployeeDAO.UpdateEmployee(employee);
        }
    }
}

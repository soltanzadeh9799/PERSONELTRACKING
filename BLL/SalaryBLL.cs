using DAL.DAO_DataAccessObject_;
using DAL.DTO_DataTransferObject_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DAO_DataAccessObject_;
using DAL.DTO_DataTransferObject_;
using DAL;

namespace BLL
{
    public class SalaryBLL
    {
        public static void AddSalary(SALARY salary,bool control)
        {
            SalaryDAO.AddSalary(salary);
            if (control)
                EmployeeDAO.UpdateEmployee(salary.EmployeeID, salary.Amount);

        }

        public static void DeleteSalary(int salaryId)
        {
            SalaryDAO.DeleteSalary(salaryId);
        }

        public static salaryDTO GetAll()
        {
            salaryDTO dto=new salaryDTO();

            dto.Employees = EmployeeDAO.GetEmployee();
            dto.Departments=DepartmentDAO.GetDepartment();
            dto.Positions = PositionDAO.GetPositions();
            dto.Months = SalaryDAO.GetMonth();
            dto.Salaries = SalaryDAO.GetSalaries();

            return dto;

        }

        public static void UpdateSalary(SALARY salary, bool control)
        {
            SalaryDAO.UpdateSalary(salary);
            if (control)
                EmployeeDAO.UpdateEmployee(salary.EmployeeID, salary.Amount);
        }
    }
}

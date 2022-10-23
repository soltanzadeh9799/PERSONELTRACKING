using DAL.DTO_DataTransferObject_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAO_DataAccessObject_
{
    public class EmployeeDAO : EmployeeContext
    {
        public static void AddEmployee(EMPLOYEE employee)
        {
            try
            {
                db.EMPLOYEEs.InsertOnSubmit(employee);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static void DeleteEmployee(int employeeID)
        {
            EMPLOYEE employee = db.EMPLOYEEs.First(x => x.ID == employeeID);
            db.EMPLOYEEs.DeleteOnSubmit(employee);
            db.SubmitChanges();

            //List<TASK> tasks = db.TASKs.Where(x => x.EmployeeID == employeeID).ToList();
            //db.TASKs.DeleteAllOnSubmit(tasks);
            //db.SubmitChanges();

            //List<SALARY> salary=db.SALARies.Where(x => x.EmployeeID == employeeID).ToList();
            //db.SALARies.DeleteAllOnSubmit(salary);
            //db.SubmitChanges();

            //List<PERMISSION> permission=db.PERMISSIONs.Where(x => x.EmployeeID == employeeID).ToList();
            //db.PERMISSIONs.DeleteAllOnSubmit(permission);
            //db.SubmitChanges();




        }

        public static List<EmployeeDetailDTO> GetEmployee()
        {
            List<EmployeeDetailDTO> employeelist = new List<EmployeeDetailDTO>();

            var list = (from e in db.EMPLOYEEs
                        join d in db.DEPARTMENTs on e.DepartmentID equals d.ID
                        join p in db.POSITIONs on e.PositionID equals p.ID
                        select new
                        {
                            UserNo = e.UserNo,
                            Name = e.Name,
                            Surname = e.Surname,
                            EmploeeID = e.ID,
                            Password = e.Password,
                            DepartmentName = d.DepartmentName,
                            PositionName = p.PositionName,
                            DepartmentID = e.DepartmentID,
                            PositionID = e.PositionID,
                            isAdmin = e.isAdmin,
                            Salary = e.Salary,
                            ImagePath = e.ImagePath,
                            Birthday = e.BirthDay,
                            Addres = e.Address
                        }).OrderBy(x => x.UserNo).ToList();

            foreach (var item in list)
            {
                EmployeeDetailDTO dto = new EmployeeDetailDTO();
                dto.Name = item.Name;
                dto.Surname = item.Surname;
                dto.UserNo = item.UserNo;
                dto.Password = item.Password;
                dto.EmployeeID = item.EmploeeID;
                dto.DepartmentID = item.DepartmentID;
                dto.DepartmentName = item.DepartmentName;
                dto.PositionID = item.PositionID;
                dto.PositionName = item.PositionName;
                dto.isAdmin = item.isAdmin;
                dto.Salary = item.Salary;
                dto.ImagePath = item.ImagePath;
                dto.Birthday = item.Birthday;
                dto.Address=item.Addres;
                employeelist.Add(dto);

            }


            return employeelist;
        }

        public static List<EMPLOYEE> GetEmployee(int v, string text)
        {
            try
            {
                List<EMPLOYEE> list = db.EMPLOYEEs.Where(x => x.UserNo == v && x.Password == text).ToList();
                return list;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static List<EMPLOYEE> GetUsers(int v)
        {
            return db.EMPLOYEEs.Where(x => x.UserNo == v).ToList();
        }

        public static void UpdateEmployee(int employeeID, int amount)
        {
            EMPLOYEE employee = db.EMPLOYEEs.First(x => x.ID == employeeID);
            employee.Salary = amount;
            db.SubmitChanges();
        }

        public static void UpdateEmployee(EMPLOYEE employee)
        {
            try
            {
                EMPLOYEE emp = db.EMPLOYEEs.First(x => x.ID == employee.ID);
                emp.UserNo = employee.UserNo;
                emp.Name=employee.Name;
                emp.Surname=employee.Surname;
                emp.Password=employee.Password;
                emp.Address=employee.Address;
                emp.BirthDay=employee.BirthDay;
                emp.Salary=employee.Salary;
                emp.DepartmentID = employee.DepartmentID;
                emp.PositionID = employee.PositionID;
                emp.isAdmin=employee.isAdmin;
                db.SubmitChanges();



            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static void UpdateEmployee(POSITION position)
        {
            List<EMPLOYEE> list=db.EMPLOYEEs.Where(x=>x.PositionID==position.ID).ToList();
            foreach (var item in list)
            {
                item.DepartmentID = position.DepartmentID;
            }
            db.SubmitChanges();
        }
    }
}

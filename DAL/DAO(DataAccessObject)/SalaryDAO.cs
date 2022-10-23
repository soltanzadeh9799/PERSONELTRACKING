using DAL.DTO_DataTransferObject_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAO_DataAccessObject_
{
    public class SalaryDAO : EmployeeContext
    {
        public static void AddSalary(SALARY salary)
        {
            try
            {
                db.SALARies.InsertOnSubmit(salary);
                db.SubmitChanges();
            }
            catch (Exception e)
            {

                throw e;
            }
            
        }

        public static void DeleteSalary(int salaryId)
        {
            SALARY s = db.SALARies.First(x => x.ID == salaryId);
            db.SALARies.DeleteOnSubmit(s);
            db.SubmitChanges();
        }

        public static List<MONTH> GetMonth()
        {
            return db.MONTHs.ToList();
        }

        public static List<SalaryDetailDTO> GetSalaries()
        {
            List<SalaryDetailDTO> salarylist = new List<SalaryDetailDTO>();

            var list = (from s in db.SALARies
                        join e in db.EMPLOYEEs on s.EmployeeID equals e.ID
                        join m in db.MONTHs on s.MonthID equals m.ID
                        select new
                        {
                            UserNo = e.UserNo,
                            Name = e.Name,
                            Surname = e.Surname,
                            EmploeeID = s.EmployeeID,
                            amount=s.MonthID,
                            year=s.Year,
                            monthName=m.MonthName,
                            monthID=s.MonthID,
                            salaryID=s.ID,
                            departmentID=e.DepartmentID,
                            positionID=e.PositionID

                        }).OrderBy(x => x.year).ToList();

            foreach (var item in list)
            {
                SalaryDetailDTO dto=new SalaryDetailDTO();
                dto.UserNo = item.UserNo;
                dto.Name = item.Name;
                dto.Surname = item.Surname;
                dto.EmployeeID = item.EmploeeID;
                dto.SalaryAmount = item.amount;
                dto.SalaryYear = item.year;
                dto.MonthName = item.monthName;
                dto.MonthID = item.monthID;
                dto.SalaryId = item.salaryID;
                dto.DepartmentID = item.departmentID;
                dto.PositionID = item.positionID;
                salarylist.Add(dto);


            }


            return salarylist;

        }

        public static void UpdateSalary(SALARY salary)
        {
            try
            {
                SALARY s = db.SALARies.First(x => x.ID == salary.ID);
                s.Year=salary.Year;
                s.MonthID = salary.MonthID;
                s.Amount = salary.Amount;
                db.SubmitChanges();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}

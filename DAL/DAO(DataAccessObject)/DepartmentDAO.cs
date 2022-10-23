﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAO_DataAccessObject_
{
    public class DepartmentDAO : EmployeeContext
    {
        public static void AddDepartment(DEPARTMENT department)
        {
            try
            {
                db.DEPARTMENTs.InsertOnSubmit(department);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
         
        }

        public static void DeleteDepartment(int iD)
        {
            DEPARTMENT d = db.DEPARTMENTs.First(x => x.ID == iD);
            db.DEPARTMENTs.DeleteOnSubmit(d);
            db.SubmitChanges();
        }

        public static List<DEPARTMENT> GetDepartment()
        {
            return db.DEPARTMENTs.ToList();
        }

        public static void UpdateDepartment(DEPARTMENT department)
        {
            try
            {
                DEPARTMENT dpt = db.DEPARTMENTs.First(x => x.ID == department.ID);
                dpt.DepartmentName = department.DepartmentName;
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}

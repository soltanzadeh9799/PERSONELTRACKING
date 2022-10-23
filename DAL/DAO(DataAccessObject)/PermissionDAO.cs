using DAL.DTO_DataTransferObject_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAO_DataAccessObject_
{
    public class PermissionDAO : EmployeeContext
    {
        public static void AddPermission(PERMISSION permission)
        {
			try
			{
				db.PERMISSIONs.InsertOnSubmit(permission);
				db.SubmitChanges();
			}
			catch ( Exception ex)
			{

				throw ex;
			}
        }

        public static void DeletePermission(int permissionID)
        {
			try
			{
				PERMISSION pr = db.PERMISSIONs.First(x => x.ID == permissionID);
				db.PERMISSIONs.DeleteOnSubmit(pr);
				db.SubmitChanges();
			}
			catch (Exception ex)
			{

				throw ex;
			}
        }

        public static List<PermissionDetailDTO> GetPermissions()
		{
			List<PermissionDetailDTO> Permissions = new List<PermissionDetailDTO>();

			var list = (from p in db.PERMISSIONs
						join s in db.PERMISSIONSTATEs on p.PermissionState equals s.ID
						join e in db.EMPLOYEEs on p.EmployeeID equals e.ID
						select new
						{
							UserNo = e.UserNo,
							Name = e.Name,
							Surname = e.Surname,
							StateName = s.StateName,
							StateID = s.ID,
							StartDate = p.PermissionStartDate,
							EndDate = p.PermissionEndDate,
							EmployeeID = p.EmployeeID,
							PermissionId = p.ID,
							Explanation = p.PermissionExplanation,
							Dayamount = p.PermissionDay,
							DepartmentID = e.DepartmentID,
							PositionID = e.PositionID

						}).OrderBy(x => x.StartDate).ToList();

			foreach (var item in list)
			{
				PermissionDetailDTO dto = new PermissionDetailDTO();
				dto.UserNo = item.UserNo;
				dto.Name = item.Name;
				dto.Surname = item.Surname;
				dto.StateName = item.StateName;
				dto.State=item.StateID;
				dto.StartDate = item.StartDate;
				dto.EndDate = item.EndDate;
				dto.EmployeeID = item.EmployeeID;
				dto.PermissionDayAmount = item.Dayamount;
				dto.DepartmentID = item.DepartmentID;
				dto.PositionID=item.PositionID;
				dto.Explanation = item.Explanation;
				dto.PermissionID = item.PermissionId;
				Permissions.Add(dto);

			}

			
			
			
			return Permissions;

		}

		public static List<PERMISSIONSTATE> GetStates()
		{
			return db.PERMISSIONSTATEs.ToList();
		}

		public static void UpdatePermission(PERMISSION permission)
		{
			try
			{
                PERMISSION pr = db.PERMISSIONs.First(x => x.ID == permission.ID);
                pr.PermissionStartDate = permission.PermissionStartDate;
                pr.PermissionEndDate = permission.PermissionEndDate;
                pr.PermissionExplanation = permission.PermissionExplanation;
                pr.PermissionDay = permission.PermissionDay;
                db.SubmitChanges();
            }
			catch (Exception ex )
			{

				throw ex;
			}
			
		}

		public static void UpdatePermission(int permissionID, int approved)
		{
			try
			{
				PERMISSION pr=db.PERMISSIONs.First(x => x.ID == permissionID);
				pr.PermissionState=approved;
				db.SubmitChanges();

			}
			catch (Exception ex)
			{

				throw ex;
			}
		}
	}
}

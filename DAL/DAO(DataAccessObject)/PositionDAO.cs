using DAL.DTO_DataTransferObject_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAO_DataAccessObject_
{
    public class PositionDAO : EmployeeContext
    {
        public static void AddPosition(POSITION position)
        {
			try
			{
				db.POSITIONs.InsertOnSubmit(position);
				db.SubmitChanges();
			}
			catch (Exception ex)
			{

				throw ex;
			}
        }

        public static void DeletePosition(int iD)
        {
            POSITION p = db.POSITIONs.First(x=>x.ID == iD);
			db.POSITIONs.DeleteOnSubmit(p);
			db.SubmitChanges();
        }

        public static List<PositionDTO> GetPositions()
		{
			try
			{
				var list = (from p in db.POSITIONs
						   join d in db.DEPARTMENTs on p.DepartmentID equals d.ID
						   select new
						   {
							   PositionID=p.ID,
							   DepartmentID=p.DepartmentID,
							   PositionName=p.PositionName,
							   DepartmentName=d.DepartmentName
						   }).OrderBy(x => x.PositionID).ToList();

				List<PositionDTO> positionlist = new List<PositionDTO>();

				foreach (var item in list)
				{
					PositionDTO dto=new PositionDTO();
					dto.DepartmentID=item.DepartmentID;
					dto.DepartmentName=item.DepartmentName;
					dto.ID = item.PositionID;
					dto.PositionName=item.PositionName;	
                    positionlist.Add(dto);
				}
				
				return positionlist;

			}
			catch (Exception ex)
			{

				throw ex;
			}
		}

        public static void UpdatePosition(POSITION position)
        {
			try
			{
				POSITION p = db.POSITIONs.First(x => x.ID == position.ID);
				p.PositionName = position.PositionName;
				p.DepartmentID = position.DepartmentID;
				db.SubmitChanges();
			}
			catch (Exception ex)
			{

				throw ex;
			}
        }
    }
}

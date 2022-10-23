using DAL;
using DAL.DAO_DataAccessObject_;
using DAL.DTO_DataTransferObject_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class PositionBll
    {
        public static void AddPosition(POSITION position)
        {
            PositionDAO.AddPosition(position);
        }

        public static void DeletePosition(int iD)
        {
            PositionDAO.DeletePosition(iD);
        }

        public static List<PositionDTO> GetPositions()
        {
            return PositionDAO.GetPositions();
        }

        public static void UpdatePosition(POSITION position, bool control)
        {
            PositionDAO.UpdatePosition(position);
            if (control)
                EmployeeDAO.UpdateEmployee(position);
        }
    }
}

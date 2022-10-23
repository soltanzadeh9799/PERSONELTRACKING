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
    public class PermissionBLL
    {
        public static void AddPermission(PERMISSION permission)
        {
            PermissionDAO.AddPermission(permission);
        }

        public static void DeletePermission(int permissionID)
        {
            PermissionDAO.DeletePermission(permissionID);
        }

        public static PermissionDTO GetAll()
        {
            PermissionDTO dto=new PermissionDTO();
            dto.Departments = DepartmentDAO.GetDepartment();
            dto.Positions = PositionDAO.GetPositions();
            dto.States = PermissionDAO.GetStates();
            dto.Permissions = PermissionDAO.GetPermissions();
            return dto;
        }

        public static void UpdatePermission(PERMISSION permission)
        {
            PermissionDAO.UpdatePermission(permission);
        }

        public static void UpdatePermission(int permissionID, int approved)
        {
            PermissionDAO.UpdatePermission(permissionID, approved);
        }
    }
}

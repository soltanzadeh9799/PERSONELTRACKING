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
    public class TaskBLL
    {
        public static void AddTask(TASK task)
        {
            TaskDAO.AddTask(task);
        }

        public static void ApproveTask(int taskId, bool isAdmin)
        {
            TaskDAO.ApproveTask(taskId, isAdmin);
        }

        public static void DeleteTask(int taskId)
        {
            TaskDAO.DeleteTask(taskId);
        }

        public static TaskDTO GetAll()
        {
            TaskDTO taskdto=new TaskDTO();
            taskdto.Departments = DepartmentDAO.GetDepartment();
            taskdto.Position = PositionDAO.GetPositions();
            taskdto.Employees = EmployeeDAO.GetEmployee();
            taskdto.Taskstates = TaskDAO.GetTaskStates();
            taskdto.Tasks = TaskDAO.GetTasks();
            return taskdto;

        }

        public static void UpdateTask(TASK task)
        {
            TaskDAO.UpdateTask(task);
        }
    }
}

using DAL.DTO_DataTransferObject_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAO_DataAccessObject_
{
    public class TaskDAO : EmployeeContext
    {
        public static void AddTask(TASK task)
        {
            db.TASKs.InsertOnSubmit(task);
            db.SubmitChanges();
        }

        public static void ApproveTask(int taskId, bool isAdmin)
        {
            try
            {
                TASK tsk = db.TASKs.First(x => x.ID == taskId);
                if(isAdmin)
                {
                    tsk.TaskState = TaskStates.Approved;
                }
                else
                {
                    tsk.TaskState = TaskStates.Delivered;
                    tsk.TaskDeliveryDate = DateTime.Today;
                }
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static void DeleteTask(int taskId)
        {
            TASK t = db.TASKs.First(x => x.ID == taskId);
            db.TASKs.DeleteOnSubmit(t);
            db.SubmitChanges();
        }

        public static List<TaskDetailDTO> GetTasks()
        {
            List<TaskDetailDTO> tasklist = new List<TaskDetailDTO>();
            var List = (from t in db.TASKs
                        join s in db.TASKSTATEs on t.TaskState equals s.ID
                        join e in db.EMPLOYEEs on t.EmployeeID equals e.ID
                        join d in db.DEPARTMENTs on e.DepartmentID equals d.ID
                        join p in db.POSITIONs on e.PositionID equals p.ID
                        select new
                        {
                            taskID = t.ID,
                            title = t.TaskTitle,
                            content = t.TaskContent,
                            startDate = t.TaskStartDate,
                            deliveryDate = t.TaskDeliveryDate,
                            taskStateName = s.StateName,
                            taskStateID = t.TaskState,
                            UserNo = e.UserNo,
                            Name = e.Name,
                            employeeID = t.EmployeeID,
                            Surname = e.Surname,
                            positionName = p.PositionName,
                            departmentName = d.DepartmentName,
                            positionID = e.PositionID,
                            DepartmentID = e.DepartmentID



                        }).OrderBy(x => x.startDate).ToList();
            foreach (var item in List)
            {
                TaskDetailDTO dto = new TaskDetailDTO();
                dto.TaskId = item.taskID;
                dto.Title = item.title;
                dto.Content = item.content;
                dto.TaskstartDate = item.startDate;
                dto.TaskDeliveryDate = item.deliveryDate;
                dto.TaskStateName = item.taskStateName;
                dto.TaskStateID = item.taskStateID;
                dto.UserNo = item.UserNo;
                dto.Name = item.Name;
                dto.Surname = item.Surname;
                dto.DepartmentName = item.departmentName;
                dto.DepartmentID = item.DepartmentID;
                dto.PositionName = item.positionName;
                dto.PositionID = item.positionID;
                dto.EmployeeID = item.employeeID;
                tasklist.Add(dto);
            }

            return tasklist;
        }

        public static List<TASKSTATE> GetTaskStates()
        {
            return db.TASKSTATEs.ToList();
        }

        public static void UpdateTask(TASK task)
        {
            try
            {
                TASK ts = db.TASKs.First(x => x.ID == task.ID);
                ts.TaskTitle = task.TaskTitle;
                ts.TaskContent = task.TaskContent;
                ts.TaskState = task.TaskState;
                ts.EmployeeID = task.EmployeeID;
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}

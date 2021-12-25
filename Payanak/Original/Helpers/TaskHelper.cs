using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Backend.ClientModels;

namespace Backend.Helpers
{
    public class TaskHelper
    {
        private static IDictionary<Guid, TaskResultModel> tasks = new Dictionary<Guid, TaskResultModel>();

        public static Guid AddTask()
        {
            var id = Guid.NewGuid();
            tasks.Add(id, new TaskResultModel());
            return id;
        }
        public static TaskResultModel GetResult(Guid id)
        {
            if (tasks.ContainsKey(id))
            {
                return tasks[id];
            }
            return null;
        }

        public static IDictionary<Guid, TaskResultModel> GetAllTasks()
        {
            return tasks;
        }

        public static bool SetResult(Guid id, TaskResultModel model)
        {
            if (tasks.ContainsKey(id))
            {
                tasks[id] = model;
                return true;
            }
            return false;
        }
        public static void RemoveTask(Guid id)
        {
            if (tasks.ContainsKey(id))
            {
                var task = tasks.First(a=> a.Key == id);
                tasks.Remove(task);
            }
        }
        
    }
}
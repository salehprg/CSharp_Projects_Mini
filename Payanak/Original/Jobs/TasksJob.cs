using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Backend.Helpers;
using Backend.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using Quartz;
using Microsoft.EntityFrameworkCore;

namespace Backend.Jobs
{
    [DisallowConcurrentExecution]
    public class TasksJob : IJob
    {

        public Task Execute(IJobExecutionContext context)
        {
            try
            {
                using (var _context = new SmsPanelDbContext())
                {
                    var tasks = TaskHelper.GetAllTasks();
                    foreach (var item in tasks)
                    {
                        if(item.Value.CreateDate.AddDays(1)<= DateTime.UtcNow)
                        {
                            _context.TaskInfo.Add(new TaskInfo
                        {
                            Guid = item.Key.ToString(),
                            Message = item.Value.Status[0].Message,
                            Status = (short)item.Value.Status[0].status,
                            Percent = 100,
                            UserId = item.Value.UserId
                        });
                        TaskHelper.RemoveTask(item.Key);
                        }
                    }
                    _context.SaveChanges();
                }
            }
            catch
            {
                return Task.CompletedTask;
            }
            return Task.CompletedTask;
        }
    }
}
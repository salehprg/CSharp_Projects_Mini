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
    public class ScheduleSmsJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                using (var _context = new SmsPanelDbContext())
                {
                    var time = DateTime.UtcNow.Date;
                    var ssd = _context.VwScheduleSms.Where(a => a.UpdatedDate.Value.AddYears(a.AddedYear.HasValue ? a.AddedYear.Value : 0)
                                                                                .AddMonths(a.AddedMonth.HasValue ? a.AddedMonth.Value : 0)
                                                                                .AddDays(a.AddedDay.HasValue ? a.AddedDay.Value : 0) == time).ToList();
                    foreach (var item in ssd)
                    {
                        var template = _context.PersonalTemplate.FirstOrDefault(a => a.Id == item.TemplateId);
                        var sendUser = _context.VwContact.FirstOrDefault(a => a.Id == item.OwnerId);
                        var guid = TaskHelper.AddTask();
                        var rectIds = await SmsService.sendSmsToNumbers(item.SendUsername,
                                                            item.SendPassword,
                                                            item.SendNumber,
                                                            new List<string> { item.MobilePhone.Replace("-", "") },
                                                            null,
                                                            template.Body,
                                                            "[ScheduleSMS]",
                                                            guid,
                                                            sendUser);
                        var ssdd = _context.ScheduleSmsDetail.FirstOrDefault(a => a.Id == item.Id);
                        ssdd.Counter++;
                        ssdd.UpdatedDate = time;
                        _context.ScheduleSmsDetail.Update(ssdd);
                        _context.SaveChanges();
                    }
                }
            }
            catch
            {
                return;
            }
            return;
        }

    }
}
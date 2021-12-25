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
    public class BusinessCardJob : IJob
    {

        public async Task Execute(IJobExecutionContext context)
        {
            using (var _context = new SmsPanelDbContext())
            {
                try
                {
                    long lastId = 0;
                    var uniqueKeys = new List<string>();
                    var setting = _context.Settings.FirstOrDefault();
                    if (setting == null)
                    {
                        _context.Settings.Add(new Settings
                        {
                            LastRecivedSmsId = 0,
                            SmsDiscount = 0,
                            SmsPrice = 0
                        });
                        _context.SaveChanges();
                    }

                    var soapClient = new ReceiveService.ReceiveSoapClient(ReceiveService.ReceiveSoapClient.EndpointConfiguration.ReceiveSoap);
                    var sendClient = new SendService.SendSoapClient(SendService.SendSoapClient.EndpointConfiguration.SendSoap);
                    var messages = new List<ReceiveService.MessagesBL>();
                    var numbers = _context.NumberInfo.ToList();
                    foreach (var no in numbers)
                    {
                        try
                        {

                            lastId = no.LastReceivedId.HasValue ? no.LastReceivedId.Value : 0;
                            messages = (await soapClient.GetMessagesAfterIDAsync(no.Username,
                                   no.Password,
                                   1,
                                   no.Number,
                                   100,
                                   (int)lastId)).ToList();
                            var currId = lastId;
                            foreach (var msg in messages)
                            {
                                currId = msg.MsgID + 1;
                                var body = msg.Body.Trim();
                                var sender = msg.Sender;
                                var receiver = msg.Receiver;
                                var fixedSender = SmsService.NumberListToString(SmsService.fixNumbers(new List<string> { sender }).ToArray());
                                var businessCard = _context.BusinessCard.FirstOrDefault(a => a.Key == body && receiver == no.Number);
                                _context.ReceiveInfo.Add(
                                   new ReceiveInfo()
                                   {
                                       Body = body,
                                       Sender = fixedSender,
                                       Receiver = receiver,
                                       Count = msg.Parts,
                                       Date = msg.SendDate,
                                       MsgId = msg.MsgID.ToString()
                                   }
                               );
                                _context.SaveChanges();
                                var user = _context.AccountInfo.FirstOrDefault(a => a.MobilePhone == fixedSender);
                                if (user == null)
                                {
                                    user = new AccountInfo
                                    {
                                        CreateDate = DateTime.UtcNow,
                                        LastLogin = DateTime.UtcNow,
                                        MobilePhone = fixedSender,
                                        Picture = "assets/img/portrait/avatars/avatar-08.png",
                                        Username = fixedSender,
                                        Password = CryptoService.MD5Hash(sender)
                                    };
                                    _context.AccountInfo.Add(user);
                                    _context.SaveChanges();
                                }
                                if (setting.FormKey.ToLower() == body.ToLower())
                                {
                                    var guid = TaskHelper.AddTask();
                                    var num = _context.NumberInfo.FirstOrDefault(a => a.Number == receiver);
                                    var owner = _context.VwContact.FirstOrDefault(a => a.Id == num.Owner);
                                    if (owner != null)
                                    {
                                        user.FormGuid = guid.ToString();
                                        user.FormDate = DateTime.UtcNow;
                                        _context.AccountInfo.Update(user);
                                        _context.SaveChanges();
                                        var rectIds = await SmsService.sendSmsToNumbers(no.Username,
                                                            no.Password,
                                                            no.Number,
                                                            new List<string> { fixedSender.Replace("-", "") },
                                                            null,
                                                            setting.FormMessage + guid,
                                                            "[RegisterForm]",
                                                            guid,
                                                            owner);
                                    }
                                }
                                if (businessCard == null)
                                    continue;
                                var groupId = businessCard.GroupId.HasValue ? businessCard.GroupId.Value : -1;
                                var group = _context.Group.FirstOrDefault(a => a.Id == groupId);
                                var template = _context.PersonalTemplate.FirstOrDefault(a => a.Id == businessCard.TemplateId.Value);

                                if (group != null && !_context.UserGroups.Any(a => a.GroupId == groupId && a.UserId == user.Id))
                                {
                                    _context.UserGroups.Add(new UserGroups
                                    {
                                        GroupId = groupId,
                                        UserId = user.Id
                                    });
                                    _context.SaveChanges();
                                }
                                var sendUser = _context.VwContact.FirstOrDefault(a => a.Id == businessCard.UserId);
                                if (template != null && sendUser != null)
                                {
                                    var guid = TaskHelper.AddTask();

                                    var rectIds = await SmsService.sendSmsToNumbers(no.Username,
                                                        no.Password,
                                                        no.Number,
                                                        new List<string> { fixedSender.Replace("-", "") },
                                                        null,
                                                        template.Body,
                                                        "[BusinessCard]",
                                                        guid,
                                                        sendUser);
                                }


                            }
                            no.LastReceivedId = currId;
                            _context.NumberInfo.Update(no);
                            _context.SaveChanges();
                        }
                        catch { }
                    }
                }
                catch(Exception ex)
                {
                    return;
                }

            }
            return;
        }
    }
}
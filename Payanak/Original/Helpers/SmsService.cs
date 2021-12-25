using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Backend.ClientModels;
using Backend.Models;
using Backend.Helpers.FarazSmsApi;

namespace Backend.Helpers
{
    public class SmsService
    {
        public static async Task<bool> HasFilteredWords(string username, string password, string body)
        {
            try
            {
                var res = new UsersService.UsersSoapClient(UsersService.UsersSoapClient.EndpointConfiguration.UsersSoap);
                var result = await res.HasFilterAsync(username, password, body);
                return result == 1;
            }
            catch
            {
                return false;
            }
        }
        public static async Task<string[]> sendSmsToNumbers(string username,
                                                        string password,
                                                        string sendNumber,
                                                        List<string> to,
                                                        List<long> groupIds,
                                                        string body,
                                                        string header,
                                                        Guid guid,
                                                        VwContact user)
        {

            try
            {             
                var allRectIds = new List<string>();
                using (var context = new SmsPanelDbContext())
                {
                    var taskRes = new TaskResultModel();
                    try
                    {
                        var setting = context.Settings.First();

                        taskRes = new TaskResultModel();
                        taskRes.Status.Add(new ResponseStatusModel(ResponseStatus.ok));
                        taskRes.Header = "فرآیند ارسال پیام به لیست شماره با عنوان \'" + header + "\'";
                        TaskHelper.SetResult(guid, taskRes);

                        var numbers = new List<string>();
                        var _Kind = 0;
                        string _groupIds = null;

                        if(groupIds != null)
                        {
                            _Kind = 1;
                            _groupIds = SmsService.NumberListToString(groupIds.Select(a => a.ToString()).ToArray());

                            var groupNumbers = context.Group.Where(a => groupIds.Contains(a.Id))
                                                    .Select(a => a.UserGroups.Select(b => b.User))
                                                    .ToList();

                            var userNums = new List<string>();
                            foreach (var item in groupNumbers)
                            {
                                foreach (var itm in item)
                                {
                                    userNums.Add(itm.MobilePhone);
                                }
                            }

                            numbers = fixNumbers(userNums);
                        }
                        else{
                            numbers = fixNumbers(to);
                        }

                        var hasFilterWord = await SmsService.HasFilteredWords(username,
                                                                        password,
                                                                        body);
                        if (hasFilterWord)
                        {
                            taskRes.Status.Clear();
                            taskRes.Status.Add(new ResponseStatusModel(ResponseStatus.filterWord));
                            taskRes.Percent = 100;
                            TaskHelper.SetResult(guid, taskRes);
                            context.SentInfo.Add(new SentInfo
                            {
                                Body = body,
                                Deliveries = null,
                                GroupIds = _groupIds,
                                Header = header,
                                Kind = _Kind, // ارسال به لیست شماره
                                Numbers = SmsService.NumberListToString(numbers.Select(a => a.Replace("-", "")).ToArray()),
                                RectIds = null,
                                SendNumber = sendNumber,
                                UserId = user.Id,
                                SentDate = DateTime.UtcNow,
                                Status = 1, // ارسال ناموفق کلمه فیلتر شده
                                CalculatedCount = 0,
                                Count = 0,
                                Price = 0
                            });
                            context.TaskInfo.Add(new TaskInfo
                            {
                                Guid = guid.ToString(),
                                Message = taskRes.Status[0].Message,
                                Status = (short)taskRes.Status[0].status,
                                Percent = 100,
                                UserId = user.Id
                            });
                            TaskHelper.RemoveTask(guid);
                            context.SaveChanges();
                            return null;
                        }

                        if (!user.Credit.HasValue)
                        {
                            taskRes.Status.Clear();
                            taskRes.Status.Add(new ResponseStatusModel(ResponseStatus.notEnoughCredit));
                            taskRes.Percent = 100;
                            TaskHelper.SetResult(guid, taskRes);
                            context.SentInfo.Add(new SentInfo
                            {
                                Body = body,
                                Deliveries = null,
                                GroupIds = _groupIds,
                                Header = header,
                                Kind = _Kind, // ارسال به لیست شماره
                                Numbers = SmsService.NumberListToString(numbers.Select(a => a.Replace("-", "")).ToArray()),
                                RectIds = null,
                                SendNumber = sendNumber,
                                UserId = user.Id,
                                SentDate = DateTime.UtcNow,
                                Status = 2,  // ارسال ناموفق اعتبار ناکافی
                                CalculatedCount = 0,
                                Count = 0,
                                Price = 0
                            });
                            context.TaskInfo.Add(new TaskInfo
                            {
                                Guid = guid.ToString(),
                                Message = taskRes.Status[0].Message,
                                Status = (short)taskRes.Status[0].status,
                                Percent = 100,
                                UserId = user.Id
                            });
                            TaskHelper.RemoveTask(guid);
                            context.SaveChanges();
                            return null;
                        }


                        var first = 0;
                        var count = numbers.Count;
                        var sumSms = SmsCounter(numbers.Count, body);
                        var userDiscount = user.Discount.HasValue ? 100 - user.Discount.Value : 100;
                        var SmsDiscount = setting.SmsDiscount.HasValue ? 100 - setting.SmsDiscount.Value : 100;
                        var sumPrice = sumSms * (int)Math.Ceiling((float)setting.SmsPrice.Value * (SmsDiscount / 100.0f) * (userDiscount / 100.0f));

                        if (sumPrice > user.Credit.Value)
                        {
                            taskRes.Status.Clear();
                            taskRes.Status.Add(new ResponseStatusModel(ResponseStatus.notEnoughCredit));
                            taskRes.Percent = 100;
                            TaskHelper.SetResult(guid, taskRes);
                            context.SentInfo.Add(new SentInfo
                            {
                                Body = body,
                                Deliveries = null,
                                GroupIds = _groupIds,
                                Header = header,
                                Kind = _Kind, // ارسال به لیست شماره
                                Numbers = SmsService.NumberListToString(numbers.Select(a => a.Replace("-", "")).ToArray()),
                                RectIds = null,
                                SendNumber = sendNumber,
                                UserId = user.Id,
                                SentDate = DateTime.UtcNow,
                                Status = 2,  // ارسال ناموفق اعتبار ناکافی
                                CalculatedCount = 0,
                                Count = 0,
                                Price = 0
                            });
                            context.TaskInfo.Add(new TaskInfo
                            {
                                Guid = guid.ToString(),
                                Message = taskRes.Status[0].Message,
                                Status = (short)taskRes.Status[0].status,
                                Percent = 100,
                                UserId = user.Id
                            });
                            TaskHelper.RemoveTask(guid);
                            context.SaveChanges();
                            return null;
                        }

                        if (numbers.Count > 100)
                            count = 100;

                        var userCredit = context.CreditInfo.FirstOrDefault(a => a.UserId == user.Id);

                        while (first < numbers.Count)
                        {

                            using (var _transaction = context.Database.BeginTransaction())
                            {
                                var nums = numbers.Skip(first)
                                                .Take(count)
                                                .Select(a => a.Replace("-", ""))
                                                .ToList();
                                                
                                var rectIds = FarazSmsApi.FarazSmsApi.SendSms(nums.ToArray() , body , sendNumber);

                                first += count;
                                taskRes.Percent = (int)(first / (numbers.Count * 1.0f) * 100);
                                TaskHelper.SetResult(guid, taskRes);
                                allRectIds.Add(rectIds);

                                var sumSmsInWhile = SmsCounter(nums.Count, body);
                                var sumPriceInWhile = sumSmsInWhile * (int)Math.Ceiling((float)setting.SmsPrice.Value * (SmsDiscount / 100.0f) * (userDiscount / 100.0f));
                                context.SentInfo.Add(new SentInfo
                                {
                                    Body = body,
                                    Deliveries = null,
                                    GroupIds = _groupIds,
                                    Header = header,
                                    Kind = _Kind, // ارسال به لیست شماره
                                    Numbers = SmsService.NumberListToString(nums.ToArray()),
                                    RectIds = rectIds,
                                    SendNumber = sendNumber,
                                    UserId = user.Id,
                                    SentDate = DateTime.UtcNow,
                                    Status = (short)0, // ارسال موفق
                                    CalculatedCount = sumSmsInWhile,
                                    Count = count,
                                    Price = sumPriceInWhile
                                });
                                userCredit.Credit -= sumPriceInWhile;
                                context.CreditInfo.Update(userCredit);
                                context.SaveChanges();
                                _transaction.Commit();
                            }
                        }
                        
                        taskRes.Percent = 100;
                        TaskHelper.SetResult(guid, taskRes);

                        context.TaskInfo.Add(new TaskInfo
                        {
                            Guid = guid.ToString(),
                            Message = taskRes.Status[0].Message,
                            Status = (short)taskRes.Status[0].status,
                            Percent = 100,
                            UserId = user.Id
                        });
                        TaskHelper.RemoveTask(guid);
                        context.SaveChanges();

                        return allRectIds.ToArray();

                    }
                    catch (Exception ex)
                    {
                        if (ex is TimeoutException)
                        {
                            taskRes.Status.Clear();
                            taskRes.Status.Add(new ResponseStatusModel(ResponseStatus.sendTimeout));
                            taskRes.Percent = 100;
                            context.TaskInfo.Add(new TaskInfo
                            {
                                Guid = guid.ToString(),
                                Message = taskRes.Status[0].Message,
                                Status = (short)taskRes.Status[0].status,
                                Percent = 100,
                                UserId = user.Id
                            });
                            TaskHelper.RemoveTask(guid);
                            context.SaveChanges();
                            return null;
                        }
                        taskRes.Status.Clear();
                        taskRes.Status.Add(new ResponseStatusModel(ResponseStatus.notEnoughCredit));
                        taskRes.Percent = 100;
                        context.TaskInfo.Add(new TaskInfo
                        {
                            Guid = guid.ToString(),
                            Message = taskRes.Status[0].Message,
                            Status = (short)taskRes.Status[0].status,
                            Percent = 100,
                            UserId = user.Id
                        });
                        TaskHelper.RemoveTask(guid);
                        context.SaveChanges();
                        return null;
                    }
                }
            }
            catch
            {
                return null;
            }
        }
        public static string NumberListToString(string[] nums)
        {
            return string.Join(';', nums);
        }


        public static List<string> fixNumbers(List<string> itms)
        {

            var newNumbers = new List<string>();
            foreach (var item in itms)
            {
                try
                {
                    var num = PhoneNumberService.GetCorrectNumber(item);
                    if (num != null)
                        newNumbers.Add(num);
                }
                catch { }
            }
            return newNumbers;
        }

        public static int SmsCounter(int NumsCount, string body)
        {
            var isPersian = IsPersian(body);
            if (isPersian)
            {
                if (body.Length <= 70)
                {
                    return NumsCount;
                }
                else
                {
                    return (1 + (int)Math.Ceiling((body.Length - 70) / 66.0f)) * NumsCount;
                }
            }
            else
            {
                if (body.Length <= 160)
                {
                    return NumsCount * 3;
                }
                else
                {
                    return (1 + (int)Math.Ceiling((body.Length - 160) / 152.0f)) * NumsCount  * 3;
                }
            }
        }

        public static bool IsPersian(string text)
        {
            return Regex.IsMatch(text, "[^\u0000-\u009F]");
        }

        public static string GetPanelNumber(string serial)
        {
            var datas = serial.Split("-");
            return datas[3];
        }

        public static string GetPanelVersion(string serial)
        {
            var datas = serial.Split("-");
            return datas[1];
        }

        public static DateTime GetPanelDate(string serial)
        {
            var datas = serial.Split("-");
            var date = datas[2].Split('.');
            var year = "20" + date[0];
            var month = date[1];
            return DateTime.Parse(month + "/1/" + year);
        }
    }
}
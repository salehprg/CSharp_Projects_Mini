using System.Collections.Generic;

namespace Backend.ClientModels
{
    //     public enum ResponseStatus{
    //         ok, // responce is ok,
    //         unauthorized, // user dont have the right to do the operation
    //         notFound, // request file or action not found
    //         badRequest, // invalid user request
    //         // Errors
    //         // register start from 10000
    //         usernameExist = 10001,

    //     }
    public static class ResponseStatus
    {
        public const int ok = 200;
        public const int unauthorized = 401;
        public const int notFound = 404;
        public const int badRequest = 500;

        // Errors
        // sign up start from 10000
        public const int usernameExist = 10001;
        public const int numberExist = 10002;
        public const int wrongNumber = 10003;
        public const int wrongUsernameOrPassword = 10004;
        // send sms start from 20000
        public const int filterWord = 20001;
        public const int notEnoughCredit = 20002;
        public const int noNumberOrGroup = 20003;
        public const int sendTimeout = 20004;

        // panels err start from 30000
        public const int wrongSerial = 30001;
        public const int noGroup = 30002;
        public const int panelNotExist = 30003;
        public const int panelVersionNotExist = 30004;

        // users err start from 40000
        public const int cantEdit = 40001;
        public const int cantDelete = 40002;
        public const int ticketCompelted = 40003;

        // business cards err from 50000
        public const int keyExist = 50001;

        // Schedule sms err from 60000
        public const int ssiCodeExist = 60001;
        public static string GetResponseText(int status)
        {
            switch (status)
            {
                case ok:
                    return "موفقیت آمیز";
                case unauthorized:
                    return "دسترسی غیر مجاز";
                case notFound:
                    return "موجود نیست";
                case badRequest:
                    return "خطا در درخواست";
                case usernameExist:
                    return "نام کاربری موجود می باشد.";
                case filterWord:
                    return "متن حاوی کلمه فیلتر شده می باشد.";
                case notEnoughCredit:
                    return "اعتبار شما کافی نمی باشد.لطفا حساب خود را مجددا شارژ نمایید.";
                case wrongSerial:
                    return "سریال وارد شده پنل صحیح نمی باشد.";
                case cantEdit:
                    return "آیتم قابل اصلاح نیست.";
                case cantDelete:
                    return "آیتم قابل حذف شدن نیست.";
                case numberExist:
                    return "شماره وارد شده موجود می باشد.";
                case wrongNumber:
                    return "شماره وارد شده صحیح نمی باشد.";
                case noGroup:
                    return "پنل انتخابی دارای گروه نمی باشد.";
                case keyExist:
                    return "کلید واژه وارد شده موجود می باشد.";
                case ssiCodeExist:
                    return "کلید واژه وارد شده موجود می باشد.";
                case panelNotExist:
                    return "دستگاهی با این سریال ثبت نشده است.";
                case panelVersionNotExist:
                    return "ورژن برای دستگاه با این سریال ثبت نشده است.";
                case ticketCompelted:
                    return "این تیکت بسته شده است.";
                case wrongUsernameOrPassword:
                    return "نام کاربری یا پسورد صحیح نمی باشد.";
                case noNumberOrGroup:
                    return "لطفا لیست شماره یا گروه ارسالی را وارد کنید.";
                case sendTimeout:
                    return "سرور ارسال پیام پاسخگو نمی باشد.لطفا بعدا تلاش کنید.";

            }
            return "";
        }
    }
}
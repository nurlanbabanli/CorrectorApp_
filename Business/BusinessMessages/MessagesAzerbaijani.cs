using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.BusinessMessages
{
    public class MessagesAzerbaijani
    {
        public static string DeviceParametersIsEmpty { get { return "Göndərilən qurğu parameterlərində məlumat tapılmadı. "; } }
        public static string DeviceParametersIsNull { get { return "Göndərilən qurğu parameterləri=Null"; } }
        public static string DeviceIdIsExists { get { return "Göndərilən İd-də qurğu mövcuddur."; } }
        public static string DeviceSerialNumberIsExists { get { return "Göndərilən seriya nömrəli qurğu mövcuddur."; } }
        public static string DeviceNameIsExists { get { return "Göndərilən adla qurğu mövcuddur."; } }
        public static string DeviceIpAddressIsExists { get { return "Göndərilən Ip adresində qurğu mövcuddur."; } }
        public static string DeviceAdded { get { return "Qurğu əlavə edildi."; } }
        public static string DeviceUpdated { get { return "Qurğuya dəyşikliklər edildi."; } }
        public static string UpdatedDeviceNameIsExists { get { return "Göndərilən adla başqa bir qurğu mövcuddur."; } }
        public static string UpdatedDeviceSerialNumberIsExists { get { return "Göndərilən seriya nömrəsi ilə başqa bir qurğu mövcuddur."; } }
        public static string UpdatedDeviceIpAddressIsExists { get { return "Göndərilən Ip adresi başqa bir qurğuda mövcuddur."; } }
        public static string DatabaseHourArchiveComonError { get { return "Saatlıq arxiv məlumatlarını database ə daxil edərkən xəta yarandı."; } }
        public static string DatabaseEventArchiveComonError { get { return "Hadisələr arxivi məlumatlarını database ə daxil edərkən xəta yarandı."; } }
        public static string DatabaseDailyArchiveComonError { get { return "Günlük arxivi məlumatlarını database ə daxil edərkən xəta yarandı."; } }
        public static string DatabaseMonthlyType1ArchiveComonError { get { return "Aylıq 1 arxivi məlumatlarını database ə daxil edərkən xəta yarandı."; } }
        public static string DatabaseMonthlyType2ArchiveComonError { get { return "Aylıq 2 arxivi məlumatlarını database ə daxil edərkən xəta yarandı."; } }
        public static string UserIdIsExists { get { return "Göndərilən user login adı mövcuddur. Başqasını seçin."; } }
        public static string UserUpdated { get { return "İstifadəçi üzərində dəyişikliklər edildi."; } }
        public static string UserNotFound { get { return "İstifadəçi mövcud deyil."; } }
        public static string PasswordError { get { return "İstifadəçi şifrəsi düzgün deyil."; } }
        public static string SuccessfullLogin { get { return "İstifadəçi sistemə daxil oldu."; } }
        public static string UserRegistered { get { return "Yeni istifadəçi sistemə əlavə edildi."; } }
        public static string UserIdIsNull { get { return "Login boş olmamalıdır."; } }
        public static string PassworIsNull { get { return "Şifrə boş olmamalıdır."; } }
        public static string AuthorizationDenied { get { return "Bu əməliyyat üçün icazəniz yoxdur."; } }
        public static string ActiveUserNotFound { get { return "Hazırda sistemdə aktiv istifadəçi mövcud deyil."; } }
        public static string UserRegistrationFaild { get { return "İstifadəçini əlavə etmək mümkün olmadı."; } }
        public static string UserAccessRegistrationFaild { get { return "İstifadəçini icazələrini əlavə etmək mümkün olmadı."; } }



        public const string HourlyArchiveManagerGetArchiveFromDevice= "saatlıq arxiv məlumatlarını qurğudan oxumaq üçün icazəniz yoxdur.";
        public const string HourlyArchiveManagerGetArchiveFromDatabase="saatlıq arxiv məlumatlarını arxivdən oxumaq üçün icazəniz yoxdur.";
        public const string EventArchiveManagerGetArchiveFromDevice = "hadisələr arxivi məlumatlarını qurğudan oxumaq üçün icazəniz yoxdur.";
        public const string EventArchiveManagerGetArchiveFromDatabase = "hadisələr arxivi məlumatlarını arxivdən oxumaq üçün icazəniz yoxdur.";
        public const string DailyArchiveManagerGetArchiveFromDevice = "günlük arxiv məlumatlarını qurğudan oxumaq üçün icazəniz yoxdur.";
        public const string DailyArchiveManagerGetArchiveFromDatabase = "günlük arxiv məlumatlarını arxivdən oxumaq üçün icazəniz yoxdur.";
        public const string MonthlyType1ArchiveManagerGetArchiveFromDevice = "aylıq həcm arxiv məlumatlarını qurğudan oxumaq üçün icazəniz yoxdur.";
        public const string MonthlyType1ArchiveManagerGetArchiveFromDatabase = "aylıq həcm arxiv məlumatlarını arxivdən oxumaq üçün icazəniz yoxdur.";
        public const string MonthlyType2ArchiveManagerGetArchiveFromDevice = "aylıq sərf arxiv məlumatlarını qurğudan oxumaq üçün icazəniz yoxdur.";
        public const string MonthlyType2ArchiveManagerGetArchiveFromDatabase = "aylıq sərf arxiv məlumatlarını arxivdən oxumaq üçün icazəniz yoxdur.";
        public const string CurrentParameterGetFromDevice = "Cari parameterin qurğudan oxunması üçün icazəniz yoxdur";
    }
}

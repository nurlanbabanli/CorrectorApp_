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
    }
}

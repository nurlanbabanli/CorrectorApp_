using Core.Tools;
using System;

namespace Core.ActionReports
{
    public class ProgressStatus
    {
        public MessageStatus StatusId { get; set; }
        public int Progress { get; set; }
        public DateTime MessageDateTime { get; set; }
        public string OrginalMessage { get; private set; }
        public string Message
        {
            get { return messageWithDateTime; }
            set
            {
                MessageDateTime = FormatDateTime.FormatDateTimeAsDateTime(DateTime.Now);
                messageWithDateTime = FormatDateTime.FormatDateTimeAsString(MessageDateTime) + " : " + value;
                OrginalMessage = value;
            }
        }
        private string messageWithDateTime;
    }


    public enum MessageStatus
    {
        All, Info, Error, Warning, Progress, MBWarning
    }
}

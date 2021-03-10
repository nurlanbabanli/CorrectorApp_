using Core.ActionReports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.FieldDeviceIdentifier
{
    public class UserInterfaceParameters
    {
        public Action<ProgressStatus> ProgressReportAction { get; set; }
    }
}

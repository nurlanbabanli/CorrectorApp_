using Core.ActionReports;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsAppTest
{
    public partial class ArchiveHandler : UserControl
    {
        public CorrectorMaster _correctorMaster;
        public Action<ProgressStatus> Action;
        public ArchiveHandler()
        {
            InitializeComponent();
            _correctorMaster = new CorrectorMaster();
            Action = ReportProgress;
        }
        public void Load(int id)
        {
            lblId.Text = id.ToString();
        }

        void ReportProgress(ProgressStatus progress)
        {

        }
    }
}

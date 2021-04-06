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
        public ArchiveHandler(int TestId)
        {
            InitializeComponent();
            _correctorMaster = new CorrectorMaster();
            _correctorMaster.Id = TestId;
            lblId.Text = _correctorMaster.Id.ToString();
            Action = ReportProgress;
            this.BackColor = Color.White;
        }


        void ReportProgress(ProgressStatus progress)
        {
            progressBar1.Value = progress.Progress;
            lblId.Text = _correctorMaster.Id.ToString() + "  " + progress.Progress.ToString();

            if (progress.StatusId==MessageStatus.Error)
            {
                this.BackColor = Color.Red;
                label1.Text = progress.Message;
            }
        }
    }
}

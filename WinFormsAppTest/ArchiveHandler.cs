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
        public ArchiveHandler()
        {
            InitializeComponent();
            _correctorMaster = new CorrectorMaster();
        }
        public void Load(int id)
        {
            lblId.Text = id.ToString();
        }
    }
}

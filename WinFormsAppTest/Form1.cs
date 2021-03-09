using Autofac;
using Business.Abstract;
using Business.DependencyResolvers.Autofac;
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
    public partial class Form1 : Form
    {
        IHourlyArchiveParameterService _hourArchiveParameterService;
        ArchiveHandler _archiveHandler;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int idCount = 0;
            List<CorrectorMaster> correctorMasters = new List<CorrectorMaster>();
            foreach (var item in archiveHandlerHolder.Controls)
            {
                if (item is ArchiveHandler)
                {
                    idCount++;
                    ArchiveHandler correctorArchiveHandler = (ArchiveHandler)item;
                    correctorArchiveHandler._correctorMaster.Id = idCount;
                    correctorArchiveHandler.Load(idCount);
                    correctorMasters.Add(correctorArchiveHandler._correctorMaster);
                }
            }

           GetHourArchiveFromDeviceAsync(correctorMasters, AutofacBusinessContainerBuilder.AutofacBusinessContainer.Resolve<IHourlyArchiveParameterService>());

        }


        private void GetHourArchiveFromDeviceAsync(List<CorrectorMaster> correctorMasters, IHourlyArchiveParameterService hourArchiveParameterService)
        {
            _hourArchiveParameterService = hourArchiveParameterService;
            _hourArchiveParameterService.GetHourArchivesFromDeviceAsync(correctorMasters);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 2; i++)
            {
                _archiveHandler = new ArchiveHandler();
                archiveHandlerHolder.Controls.Add(_archiveHandler);
            }
        }
    }
}

using Autofac;
using Business.Abstract;
using Business.DependencyResolvers.Autofac;
using Business.DeviceIdentifier;
using Core.Utilities.FieldDeviceIdentifier;
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

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int idCount = 0;
            List<DataTransmissionParameterHolder> dataTransmissionParameterHolder = new List<DataTransmissionParameterHolder>();
            foreach (var item in archiveHandlerHolder.Controls)
            {
                if (item is ArchiveHandler)
                {
                    idCount++;
                    ArchiveHandler correctorArchiveHandler = (ArchiveHandler)item;
                    correctorArchiveHandler._correctorMaster.Id = idCount;
                    correctorArchiveHandler.Load(idCount);

                    deviceParameters.Add(deviceParameter);
                }
            }

            _hourArchiveParameterService = AutofacBusinessContainerBuilder.AutofacBusinessContainer.Resolve<IHourlyArchiveParameterService>();
            GetHourArchiveFromDeviceAsync(deviceParameters, _hourArchiveParameterService);

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

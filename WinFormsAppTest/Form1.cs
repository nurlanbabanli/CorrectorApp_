using Autofac;
using Business.Abstract;
using Business.DependencyResolvers.Autofac;
using Business.Utilities;
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
            ConcurrentTaskLimiter.ParalelTasks = 2;

            List<DataTransmissionParameterHolder> dataTransmissionParameterHolder = new List<DataTransmissionParameterHolder>();
            foreach (var item in archiveHandlerHolder.Controls)
            {
                if (item is ArchiveHandler)
                {
                    ArchiveHandler correctorArchiveHandler = (ArchiveHandler)item;
                    dataTransmissionParameterHolder.Add(ParametersTransfer.CombineData(correctorArchiveHandler));          
                }
            }

            _hourArchiveParameterService = AutofacBusinessContainerBuilder.AutofacBusinessContainer.Resolve<IHourlyArchiveParameterService>();
            GetHourArchiveFromDeviceAsync(dataTransmissionParameterHolder, _hourArchiveParameterService);

        }


        private void GetHourArchiveFromDeviceAsync(List<DataTransmissionParameterHolder> DeviceParameters, IHourlyArchiveParameterService hourArchiveParameterService)
        {
            _hourArchiveParameterService = hourArchiveParameterService;
            _hourArchiveParameterService.GetHourArchivesFromDeviceAsync(DeviceParameters);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            ArchiveHandler _archiveHandler;
            for (int i = 0; i < 5; i++)
            {
                _archiveHandler = new ArchiveHandler(i);
                archiveHandlerHolder.Controls.Add(_archiveHandler);
            }
        }
    }
}

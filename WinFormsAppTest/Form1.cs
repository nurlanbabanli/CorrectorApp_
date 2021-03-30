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

            DataTransmissionParametersHolderList dataTransmissionParameterHolderList = new DataTransmissionParametersHolderList();
            foreach (var item in archiveHandlerHolder.Controls)
            {
                if (item is ArchiveHandler)
                {
                    ArchiveHandler correctorArchiveHandler = (ArchiveHandler)item;
                    dataTransmissionParameterHolderList.DataTransmissionParameterHolderList.Add(ParametersTransfer.CombineData(correctorArchiveHandler));        
                }
            }

            _hourArchiveParameterService = AutofacBusinessContainerBuilder.AutofacBusinessContainer.Resolve<IHourlyArchiveParameterService>();
            try
            {
                GetHourArchiveFromDeviceAsync(dataTransmissionParameterHolderList, _hourArchiveParameterService);
            }
            catch (Exception ex)
            {

                lblValidationException.Text = ex.Message;
            }

        }


        private void GetHourArchiveFromDeviceAsync(DataTransmissionParametersHolderList DeviceParameters, IHourlyArchiveParameterService hourArchiveParameterService)
        {

                _hourArchiveParameterService = hourArchiveParameterService;
                _hourArchiveParameterService.GetHourArchivesFromDeviceAsync(DeviceParameters);
   
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            ArchiveHandler _archiveHandler;
            for (int i = 1; i < 10; i++)
            {
                if (i==100)
                {
                    _archiveHandler = new ArchiveHandler(0); 
                }
                else
                {
                    _archiveHandler = new ArchiveHandler(i);
                }
                archiveHandlerHolder.Controls.Add(_archiveHandler);
            }
        }
    }
}

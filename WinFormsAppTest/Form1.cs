using Autofac;
using Business.Abstract;
using Business.Concrete;
using Business.DependencyResolvers.Autofac;
using Business.Events;
using Business.Utilities;
using Core.ActionReports;
using Core.Results.Abstract;
using Core.Utilities.FieldDeviceIdentifier;
using Entities.Concrete;
using Entities.Dtos;
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
        ICorrectorMasterService _correctorMasterService;
        Action<ProgressStatus> Action;
        IProgress<ProgressStatus> Progress;

        public Form1()
        {
            InitializeComponent();
            Action = ReportProgress;
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

        private void ReportProgress(ProgressStatus progress)
        {
            lblValidationException.Text = progress.Message;
        }
        private async void GetHourArchiveFromDeviceAsync(DataTransmissionParametersHolderList DeviceParameters, IHourlyArchiveParameterService hourArchiveParameterService)
        {
            try
            {
                Progress = new Progress<ProgressStatus>(Action);
                _hourArchiveParameterService = hourArchiveParameterService;
                IResult result = await _hourArchiveParameterService.GetArchivesFromDeviceAsync(DeviceParameters, Progress);
                if (result!=null && result.IsSuccess)
                {
                    lblValidationException.Text = "Main Succced";
                }
            }
            catch (Exception ex)
            {

                lblValidationException.Text = ex.Message;
            }
  
   
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            ArchiveHandler _archiveHandler;
            for (int i = 0; i < 5; i++)
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

        private void button2_Click(object sender, EventArgs e)
        {
            _correctorMasterService = AutofacBusinessContainerBuilder.AutofacBusinessContainer.Resolve<ICorrectorMasterService>();
            GetCorrectorMaster();
        }

        private void GetCorrectorMaster()
        {
            List<CorrectorMaster> correctorMasters = _correctorMasterService.GetAll().Data;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //INurlan nurlan = new Nurlan();
            //nurlan.MyTest(5);
        }

        private void button4_Click(object sender, EventArgs e)//resiter
        {
            Progress = new Progress<ProgressStatus>(Action);
            IWinFormAuthService winFormAuthService = AutofacBusinessContainerBuilder.AutofacBusinessContainer.Resolve<IWinFormAuthService>();
            var userForRegisterDto = new UserForRegisterDto
            {
                UserId = "Nurlan",
                Password = "123",
                FirstName = "Nurlan",
            };

            var userAccess = new UserAccess
            {
                UserId = "Nurlan2",
                AccessCode = "100"
            };
            List<UserAccess> Access = new List<UserAccess>();
            Access.Add(userAccess);

            IResult result = winFormAuthService.RegisterUser(userForRegisterDto,Progress);
            if (result!=null)
            {
                MessageBox.Show(result.Message);
            }
        }

        private void archiveHandlerHolder_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)//login
        {
            Progress = new Progress<ProgressStatus>(Action);
            IWinFormAuthService winFormAuthService = AutofacBusinessContainerBuilder.AutofacBusinessContainer.Resolve<IWinFormAuthService>();
            var userLoginDto = new UserForLoginDto
            {
                UserId = "Nurlan",
                Password = "123"
            };
           IDataResult<User> result= winFormAuthService.Login(userLoginDto, Progress);
            if (result==null)
            {
                MessageBox.Show("Login Error");
            }
            else if(!result.IsSuccess)
            {
                MessageBox.Show(result.Message);
            }
            else
            {
                MessageBox.Show(result.Message);
                label2.Text = result.Data.UserId + " loged";
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Progress = new Progress<ProgressStatus>(Action);
            IWinFormAuthService winFormAuthService = AutofacBusinessContainerBuilder.AutofacBusinessContainer.Resolve<IWinFormAuthService>();

            IResult result = winFormAuthService.Logout(Progress);
            if (result == null)
            {
                MessageBox.Show("Logotn Error");
            }
            else if (!result.IsSuccess)
            {
                MessageBox.Show(result.Message);
            }
            else
            {
                MessageBox.Show(result.Message);
                label2.Text ="User logedout";
            }
        }
    }
}

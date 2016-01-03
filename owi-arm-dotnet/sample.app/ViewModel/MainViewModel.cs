using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using owi_arm_dotnet;
using System;
using System.Windows.Input;

namespace sample.app.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private int gripperSliderValue;
        private int elbowSliderValue;
        private string logOutput;

        private IOwiCommand command = new OwiCommand();
        private IOwiArm arm = new OwiArm();

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            this.StopCommand = new RelayCommand(() => this.OnStopCommand());
            this.ConnectCommand = new RelayCommand(() => this.OnConnectCommand());
        }

        public override void Cleanup()
        {
            // TODO: why is this not being called by the MVVM light framework?
            base.Cleanup();
            this.arm.Disconnect();
        }

        public string LogOutput
        {
            get
            {
                return logOutput;
            }
            set
            {
                logOutput = value;
                this.RaisePropertyChanged(() => LogOutput);
            }
        }

        public int GripperSliderValue
        {
            get
            {
                return this.gripperSliderValue;
            }
            set
            {
                this.gripperSliderValue = value;
                if (this.gripperSliderValue == -1)
                    this.command.GripperClose();
                else if (this.gripperSliderValue == 1)
                    this.command.GripperOpen();
                else
                    this.command.GripperStop();

                this.RaisePropertyChanged(() => GripperSliderValue);
                this.SendCommandToRobotArm();
            }
        }

        public int ElbowSliderValue
        {
            get
            {
                return this.elbowSliderValue;
            }
            set
            {
                this.elbowSliderValue = value;
                if (this.elbowSliderValue == -1)
                    this.command.ElbowDown();
                else if (this.elbowSliderValue == 1)
                    this.command.ElbowUp();
                else
                    this.command.ElbowStop();

                this.RaisePropertyChanged(() => ElbowSliderValue);
                this.SendCommandToRobotArm();
            }
        }

        private void SendCommandToRobotArm()
        {
            try
            {
                this.arm.SendCommand(this.command);
            }
            catch (Exception e)
            {
                this.LogOutput += string.Format("Command failed: {0}{1}", e.Message, Environment.NewLine);
            }
        }

        public ICommand StopCommand { get; private set; }

        public ICommand ConnectCommand { get; set; }

        private void OnStopCommand()
        {
            this.gripperSliderValue = 0;
            this.RaisePropertyChanged(() => GripperSliderValue);

            this.elbowSliderValue = 0;
            this.RaisePropertyChanged(() => ElbowSliderValue);

            this.command.StopAllMovements();
            this.SendCommandToRobotArm();
        }

        private void OnConnectCommand()
        {
            try
            {
                this.arm.Connect();
            }
            catch (System.Exception e)
            {
                this.LogOutput += string.Format("Unable to connect to arm: {0}{1}", e.Message, Environment.NewLine);
            }
        }
    }
}
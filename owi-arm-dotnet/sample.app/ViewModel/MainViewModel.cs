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
        private int wristSliderValue;
        private int elbowSliderValue;
        private int shoulderSliderValue;
        private int baseSliderValue;
        private string logOutput;

        private IOwiCommand command = new OwiCommand();
        private IOwiArm arm = new OwiArm();

        private MovementCommander gripperCommander;
        private MovementCommander wristCommander;
        private MovementCommander elbowCommander;
        private MovementCommander shoulderCommander;
        private MovementCommander baseCommander;

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            this.ConnectCommand = new RelayCommand(() => this.OnConnectCommand());
            this.LedCommand = new RelayCommand<bool>(state => this.OnLedCommand(state));
            this.StopCommand = new RelayCommand(() => this.OnStopCommand());

            this.gripperCommander = new MovementCommander(c => c.GripperClose(), c => c.GripperStop(), c => c.GripperOpen());
            this.wristCommander = new MovementCommander(c => c.WristDown(), c => c.WristStop(), c => c.WristUp());
            this.elbowCommander = new MovementCommander(c => c.ElbowDown(), c => c.ElbowStop(), c => c.ElbowUp());
            this.shoulderCommander = new MovementCommander(c => c.ShoulderDown(), c => c.ShoulderStop(), c => c.ShoulderUp());
            this.baseCommander = new MovementCommander(c => c.BaseRotateCounterClockwise(), c => c.BaseRotateStop(), c => c.BaseRotateClockwise());
        }

        public override void Cleanup()
        {
            // TODO: why is this not being called by the MVVM light framework?
            base.Cleanup();
            this.arm.DisconnectAsync();
        }

        public bool IsArmConnected
        {
            get
            {
                return this.arm.IsConnected;
            }
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
                this.gripperCommander.ApplyMovement(value, this.command);
                this.SendCommandToRobotArm();

                this.gripperSliderValue = value;
                this.RaisePropertyChanged(() => GripperSliderValue);
            }
        }

        public int WristSliderValue
        {
            get
            {
                return this.wristSliderValue;
            }
            set
            {
                this.wristCommander.ApplyMovement(value, this.command);
                this.SendCommandToRobotArm();

                this.wristSliderValue = value;
                this.RaisePropertyChanged(() => WristSliderValue);
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
                this.elbowCommander.ApplyMovement(value, this.command);
                this.SendCommandToRobotArm();

                this.elbowSliderValue = value;
                this.RaisePropertyChanged(() => ElbowSliderValue);
            }
        }

        public int ShoulderSliderValue
        {
            get
            {
                return this.shoulderSliderValue;
            }
            set
            {
                this.shoulderCommander.ApplyMovement(value, this.command);
                this.SendCommandToRobotArm();

                this.shoulderSliderValue = value;
                this.RaisePropertyChanged(() => ShoulderSliderValue);
            }
        }

        public int BaseSliderValue
        {
            get
            {
                return this.baseSliderValue;
            }
            set
            {
                this.baseCommander.ApplyMovement(value, this.command);
                this.SendCommandToRobotArm();

                this.baseSliderValue = value;
                this.RaisePropertyChanged(() => BaseSliderValue);
            }
        }

        private async void SendCommandToRobotArm()
        {
            try
            {
                await this.arm.SendCommandAsync(this.command);
            }
            catch (Exception e)
            {
                this.LogOutput += string.Format("Command failed: {0}{1}", e.Message, Environment.NewLine);
            }
        }

        public ICommand ConnectCommand { get; private set; }

        public ICommand LedCommand { get; private set; }

        public ICommand StopCommand { get; private set; }

        private async void OnConnectCommand()
        {
            try
            {
                await this.arm.ConnectAsync();
                this.RaisePropertyChanged(() => IsArmConnected);
            }
            catch (System.Exception e)
            {
                this.LogOutput += string.Format("Unable to connect to arm: {0}{1}", e.Message, Environment.NewLine);
            }
        }

        private void OnLedCommand(bool isToggled)
        {
            if (isToggled)
                this.command.LedOn();
            else
                this.command.LedOff();

            this.SendCommandToRobotArm();
        }

        private void OnStopCommand()
        {
            this.gripperSliderValue = 0;
            this.RaisePropertyChanged(() => GripperSliderValue);

            this.wristSliderValue = 0;
            this.RaisePropertyChanged(() => WristSliderValue);

            this.elbowSliderValue = 0;
            this.RaisePropertyChanged(() => ElbowSliderValue);

            this.shoulderSliderValue = 0;
            this.RaisePropertyChanged(() => ShoulderSliderValue);

            this.baseSliderValue = 0;
            this.RaisePropertyChanged(() => BaseSliderValue);

            this.command.StopAllMovements();
            this.SendCommandToRobotArm();
        }
    }
}
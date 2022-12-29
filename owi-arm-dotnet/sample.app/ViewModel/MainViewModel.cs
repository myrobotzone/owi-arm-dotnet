using System;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using owi_arm_dotnet;
using owi_arm_dotnet_usb;

namespace sample.app.ViewModel;

public class MainViewModel : ViewModelBase
{
    private readonly MovementCommander baseCommander;
    private int baseSliderValue;
    private readonly MovementCommander elbowCommander;
    private int elbowSliderValue;

    private readonly MovementCommander gripperCommander;
    private int gripperSliderValue;
    private string logOutput;

    private readonly IOwiArm owiArm;
    private readonly IOwiCommand owiCommand;
    private readonly MovementCommander shoulderCommander;
    private int shoulderSliderValue;
    private readonly MovementCommander wristCommander;
    private int wristSliderValue;

    /// <summary>
    /// Initializes a new instance of the MainViewModel class.
    /// </summary>
    public MainViewModel()
    {
        var factory = new OwiFactory();
        owiArm = factory.CreateArm(new LibUsbOwiConnection());
        owiCommand = factory.CreateCommand();

        ConnectCommand = new RelayCommand(() => OnConnectCommand());
        LedCommand = new RelayCommand<bool>(state => OnLedCommand(state));
        StopCommand = new RelayCommand(() => OnStopCommand());

        gripperCommander = new MovementCommander(c => c.GripperClose(), c => c.GripperStop(), c => c.GripperOpen());
        wristCommander = new MovementCommander(c => c.WristDown(), c => c.WristStop(), c => c.WristUp());
        elbowCommander = new MovementCommander(c => c.ElbowDown(), c => c.ElbowStop(), c => c.ElbowUp());
        shoulderCommander = new MovementCommander(c => c.ShoulderDown(), c => c.ShoulderStop(), c => c.ShoulderUp());
        baseCommander = new MovementCommander(c => c.BaseRotateCounterClockwise(), c => c.BaseRotateStop(),
            c => c.BaseRotateClockwise());
    }

    public bool IsArmConnected => owiArm.IsConnected;

    public string LogOutput
    {
        get => logOutput;
        set
        {
            logOutput = value;
            RaisePropertyChanged(() => LogOutput);
        }
    }

    public int GripperSliderValue
    {
        get => gripperSliderValue;
        set
        {
            gripperCommander.ApplyMovement(value, owiCommand);
            SendCommandToRobotArm();

            gripperSliderValue = value;
            RaisePropertyChanged(() => GripperSliderValue);
        }
    }

    public int WristSliderValue
    {
        get => wristSliderValue;
        set
        {
            wristCommander.ApplyMovement(value, owiCommand);
            SendCommandToRobotArm();

            wristSliderValue = value;
            RaisePropertyChanged(() => WristSliderValue);
        }
    }

    public int ElbowSliderValue
    {
        get => elbowSliderValue;
        set
        {
            elbowCommander.ApplyMovement(value, owiCommand);
            SendCommandToRobotArm();

            elbowSliderValue = value;
            RaisePropertyChanged(() => ElbowSliderValue);
        }
    }

    public int ShoulderSliderValue
    {
        get => shoulderSliderValue;
        set
        {
            shoulderCommander.ApplyMovement(value, owiCommand);
            SendCommandToRobotArm();

            shoulderSliderValue = value;
            RaisePropertyChanged(() => ShoulderSliderValue);
        }
    }

    public int BaseSliderValue
    {
        get => baseSliderValue;
        set
        {
            baseCommander.ApplyMovement(value, owiCommand);
            SendCommandToRobotArm();

            baseSliderValue = value;
            RaisePropertyChanged(() => BaseSliderValue);
        }
    }

    public ICommand ConnectCommand { get; }

    public ICommand LedCommand { get; }

    public ICommand StopCommand { get; }

    public override void Cleanup()
    {
        base.Cleanup();
        if (IsArmConnected)
        {
            owiCommand.StopAllMovements().LedOff();
            SendCommandToRobotArm();
            owiArm.DisconnectAsync();
        }
    }

    private void SendCommandToRobotArm()
    {
        try
        {
            owiArm.SendCommandAsync(owiCommand);
        }
        catch (Exception e)
        {
            LogOutput += string.Format("Command failed: {0}{1}", e.Message, Environment.NewLine);
        }
    }

    private async void OnConnectCommand()
    {
        try
        {
            await owiArm.ConnectAsync();
            RaisePropertyChanged(() => IsArmConnected);
        }
        catch (Exception e)
        {
            LogOutput += $"Unable to connect to arm: {e.Message}{Environment.NewLine}";
        }
    }

    private void OnLedCommand(bool isToggled)
    {
        if (isToggled)
        {
            owiCommand.LedOn();
        }
        else
        {
            owiCommand.LedOff();
        }

        SendCommandToRobotArm();
    }

    private void OnStopCommand()
    {
        gripperSliderValue = 0;
        RaisePropertyChanged(() => GripperSliderValue);

        wristSliderValue = 0;
        RaisePropertyChanged(() => WristSliderValue);

        elbowSliderValue = 0;
        RaisePropertyChanged(() => ElbowSliderValue);

        shoulderSliderValue = 0;
        RaisePropertyChanged(() => ShoulderSliderValue);

        baseSliderValue = 0;
        RaisePropertyChanged(() => BaseSliderValue);

        owiCommand.StopAllMovements();
        SendCommandToRobotArm();
    }
}
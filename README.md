# owi-arm-dotnet

A .NET library that implements the [OWI-007 Robotic Arm parallel port protocol](http://notbrainsurgery.livejournal.com/38622.html). Tested on Windows 10 using [USB Controlled Robotic Arm Kit](http://www.maplin.co.uk/p/usb-controlled-robotic-arm-kit-a37jn)

## Installing the robotic arm USB driver, under Windows

Before operating the arm, ensure that you have the correct USB driver installed. Connect the arm to your computer using the supplied usb cable. Turn the arm on then install the WinUSB driver using [Zadig](http://zadig.akeo.ie/).

## Code snippet
The following code snippit demonstrates the API. It will at the same time and for 2 seconds, rotate the base clockwise, move the shoulder up and turn on the LED. After which all movment is stopped and the LED is turned off.

```csharp
IOwiArm arm = new OwiArm();
arm.Connect();

IOwiCommand command = new OwiCommand().BaseRotateClockwise().ShoulderUp().LedOn();

arm.SendCommand(command);
Thread.Sleep(2000);

arm.SendCommand(command.StopAllMovements().LedOff());

arm.Disconnect();
```

## Sample application

The sample application can be used to access all of the robotic arm features via a simple user interface.

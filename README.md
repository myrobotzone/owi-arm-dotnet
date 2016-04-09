# owi-arm-dotnet

A .NET library that implements the [OWI-007 Robotic Arm parallel port protocol](http://notbrainsurgery.livejournal.com/38622.html). Tested on Windows 10 using [USB Controlled Robotic Arm Kit](http://www.maplin.co.uk/p/usb-controlled-robotic-arm-kit-a37jn)

## Release 1 ##

### Installing the robotic arm USB driver, under Windows

Before operating the arm, ensure that you have the correct USB driver installed. Connect the arm to your computer using the supplied usb cable. Turn the arm on then install the WinUSB driver using [Zadig](http://zadig.akeo.ie/).

### Code snippet
The following code example demonstrates the API. It will at the same time and for 2 seconds, rotate the base clockwise, move the shoulder up and turn on the LED. After which all movement is stopped and the LED is turned off.

```csharp
IOwiArm arm = new OwiArm();
arm.Connect();

IOwiCommand command = new OwiCommand().BaseRotateClockwise().ShoulderUp().LedOn();

arm.SendCommand(command);
Thread.Sleep(2000);

arm.SendCommand(command.StopAllMovements().LedOff());

arm.Disconnect();
```

### Nuget Package
The library is available as a [nuget package](https://www.nuget.org/packages/owi-arm-dotnet/).

### Sample application

The sample application can be used to access all of the robotic arm features via a basic user interface.

## Release 1.5
I am in the process of migrating owi-arm-dotnet to a portalable class library that provides and asynchronous API. The latest code reflects these changes but I have not yet updated the documentation and nuget package. Note that this will be a breaking change to version 1 of the API.

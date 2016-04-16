# owi-arm-dotnet

A .NET library that implements the [OWI-007 Robotic Arm parallel port protocol](http://notbrainsurgery.livejournal.com/38622.html). Tested on Windows 10 using [USB Controlled Robotic Arm Kit](http://www.maplin.co.uk/p/usb-controlled-robotic-arm-kit-a37jn)

## Release 1.5 ##

### Installing the robotic arm USB driver, under Windows

Before operating the arm, ensure that you have the correct USB driver installed. Please follow the instructions documented at [www.myrobot.zone](https://www.myrobot.zone/blog/post/2016/04/04/Controlling-OWI-Arm-from-Windows-10.aspx)

### Code snippet
The following code example demonstrates the API. It will at the same time and for 2 seconds, rotate the base clockwise, move the shoulder up and turn on the LED. After which all movement is stopped and the LED is turned off.

```csharp
IOwiFactory factory = new OwiFactory();
IOwiArm arm = factory.CreateArm(new LibUsbOwiConnection());

await arm.ConnectAsync();

IOwiCommand command = factory.CreateCommand().BaseRotateClockwise().ShoulderUp().LedOn();

await arm.SendCommandAsync(command);

Thread.Sleep(2000);

await arm.SendCommandAsync(command.StopAllMovements().LedOff());

await arm.DisconnectAsync();
```

### Nuget Package(s)
The library is available on nuget - [owi-arm-dotnet](https://www.nuget.org/packages/owi-arm-dotnet/). 

For a Windows desktop usb implementation, refer to the sister package - [owi-arm-dotnet-usb](https://www.nuget.org/packages/owi-arm-dotnet-usb/).

### Sample application

The sample application can be used to access all of the robotic arm features via a basic user interface.

## Upgrading form Release 1.0 to 1.5

A number of breaking changes were made so please follow this guide if you are upgrading form 1.0 to 1.5.

1. Introduction of asynchronous API - Replace IOwiArm methods Connect(), SendCommand() and Disconnect() with ConnectAsync(), SendCommandAsync() and DisconnectAsync() respectively.
2. The implementation of IOwiUsbConnection was moved from lib-usb-dotnet to lib-usb-dotnet-usb, so add a reference to the latter project or use its respective nuget package.
3. Generally classes are now internal and only interfaces are public. To instantiate IOwiArm or IOwiCommand, use the OwiFactory (see code sample above).


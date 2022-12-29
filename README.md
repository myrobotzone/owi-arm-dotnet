# owi-arm-dotnet

A .NET 6 library that implements the [OWI-007 Robotic Arm parallel port protocol](http://notbrainsurgery.livejournal.com/38622.html). Tested on Windows 10 using [USB Controlled Robotic Arm Kit](http://www.maplin.co.uk/p/usb-controlled-robotic-arm-kit-a37jn)

## Installing the robotic arm USB driver, under Windows

Before operating the arm, ensure that you have the correct USB driver installed, as follows (based on [WinUSB (Winusb.sys) installation for developers](https://learn.microsoft.com/en-us/windows-hardware/drivers/usbcon/winusb-installation)):

1. Plug in the arm to the host system.
2. Open Device Manager and locate the device.
3. Select and hold (or right-click) the device and select **Update driver software...** from the context menu.
4. In the wizard, select **Browse my computer for driver software.**
5. Select **Let me pick from a list of device drivers on my computer.**
6. From the list of device classes, select **Universal Serial Bus devices.**
7. The wizard displays WinUsb Device. Select **ADB Device** to load the driver.

## Code snippet
The following code example demonstrates the API. It will rotate the base clockwise, move the shoulder up and turn on the LED. After 2 seconds all movement is stopped and the LED is turned off.

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

## Nuget Package(s)
The library is available on nuget - [owi-arm-dotnet](https://www.nuget.org/packages/owi-arm-dotnet/). 

For a Windows desktop usb implementation, refer to the sister package - [owi-arm-dotnet-usb](https://www.nuget.org/packages/owi-arm-dotnet-usb/).

## Sample application

The sample application can be used to access all of the robotic arm features via a basic user interface.


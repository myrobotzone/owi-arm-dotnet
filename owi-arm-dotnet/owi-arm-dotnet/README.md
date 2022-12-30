# owi-arm-dotnet

A .NET 6 library that implements the [OWI-007 Robotic Arm parallel port protocol](http://notbrainsurgery.livejournal.com/38622.html), enabling usb based control of the **OWI Robotic Arm Edge**

### Example usage

```csharp
IOwiFactory factory = new OwiFactory();
// LibUsbOwiConnection is provided by the owi-arm-dotnet-usb library, alternatively you can provide your own implementation
IOwiArm arm = factory.CreateArm(new LibUsbOwiConnection());

await arm.ConnectAsync();

IOwiCommand command = factory.CreateCommand().BaseRotateClockwise().ShoulderUp().LedOn();

await arm.SendCommandAsync(command);

Thread.Sleep(2000);

await arm.SendCommandAsync(command.StopAllMovements().LedOff());

await arm.DisconnectAsync();
```

### Documentation

See https://github.com/myrobotzone/owi-arm-dotnet


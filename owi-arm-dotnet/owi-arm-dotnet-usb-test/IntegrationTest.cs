using owi_arm_dotnet;
using owi_arm_dotnet_usb;

namespace owi_arm_dotnet_usb_test
{
    public class IntegrationTest
    {
        [Fact(Skip = "Requires the arm connected to the computer")]
        public async Task IntegrationTestThatRequiresArm()
        {
            var factory = new OwiFactory();

            var arm = factory.CreateArm(new LibUsbOwiConnection());

            await arm.ConnectAsync();

            var command = factory.CreateCommand().BaseRotateClockwise().ShoulderUp().LedOn();

            await arm.SendCommandAsync(command);

            Thread.Sleep(2000);

            await arm.SendCommandAsync(command.StopAllMovements().LedOff());

            await arm.DisconnectAsync();
        }  
    }
}

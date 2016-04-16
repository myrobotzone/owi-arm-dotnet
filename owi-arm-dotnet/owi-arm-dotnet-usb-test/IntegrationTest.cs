using Microsoft.VisualStudio.TestTools.UnitTesting;
using owi_arm_dotnet;
using owi_arm_dotnet_usb;
using System.Threading;
using System.Threading.Tasks;

namespace owi_arm_dotnet_usb_test
{
    /// <summary>
    /// These integration test require the arm connected to the computer.
    /// </summary>
    [TestClass]
    [Ignore]
    public class IntegrationTest
    {
        [TestMethod]
        public async Task IntegrationTestThatRequiresArm()
        {
            IOwiFactory factory = new OwiFactory();
            IOwiArm arm = factory.CreateArm(new LibUsbOwiConnection());

            await arm.ConnectAsync();

            IOwiCommand command = factory.CreateCommand().BaseRotateClockwise().ShoulderUp().LedOn();

            await arm.SendCommandAsync(command);

            Thread.Sleep(2000);

            await arm.SendCommandAsync(command.StopAllMovements().LedOff());

            await arm.DisconnectAsync();
        }  
    }
}

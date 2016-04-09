using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using owi_arm_dotnet;
using owi_arm_dotnet_usb;
using System.Threading;
using System.Threading.Tasks;

namespace owi_arm_dotnet_usb_test
{
    [TestClass]
    public class IntegrationTest
    {
        /// <summary>
        /// This integration test requires the arm connected to the computer.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        [Ignore]
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

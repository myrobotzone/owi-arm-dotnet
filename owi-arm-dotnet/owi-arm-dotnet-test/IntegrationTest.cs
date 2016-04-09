using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using owi_arm_dotnet;
using System.Threading;
using System.Threading.Tasks;

namespace owi_arm_dotnet_test
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
            var connection = new Mock<IOwiUsbConnection>().Object;
            IOwiArm arm = new OwiArm(connection);
            await arm.ConnectAsync();

            IOwiCommand command = new OwiCommand().BaseRotateClockwise().ShoulderUp().LedOn();

            await arm.SendCommandAsync(command);
            Thread.Sleep(2000);

            await arm.SendCommandAsync(command.StopAllMovements().LedOff());

            await arm.DisconnectAsync();
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using owi_arm_dotnet;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace owi_arm_dotnet_test
{
    [TestClass]
    public class OwiArmTest
    {
        [TestMethod]
        public async Task ConnectAsync_ConnectionIsNotOpen_CallsOpenOnUsbConnection()
        {
            var connectionMock = new Mock<IOwiUsbConnection>();
            connectionMock.SetupGet(mock => mock.IsOpen).Returns(false);

            var arm = new OwiArm(connectionMock.Object);
            await arm.ConnectAsync();

            connectionMock.Verify(mock => mock.OpenAsync(), Times.Once);
        }

        [TestMethod]
        public async Task ConnectAsync_ConnectionIsAlreadyOpen_DoesNotOpenConnectionAgain()
        {
            var connectionMock = new Mock<IOwiUsbConnection>();
            connectionMock.SetupGet(mock => mock.IsOpen).Returns(true);

            var arm = new OwiArm(connectionMock.Object);
            await arm.ConnectAsync();

            connectionMock.Verify(mock => mock.OpenAsync(), Times.Never);
        }

        [TestMethod]
        public async Task DisconnectAsync_ConnectionIsOpen_CallsCloseOnUsbConnection()
        {
            var connectionMock = new Mock<IOwiUsbConnection>();
            connectionMock.SetupGet(mock => mock.IsOpen).Returns(true);

            var arm = new OwiArm(connectionMock.Object);
            await arm.DisconnectAsync();

            connectionMock.Verify(mock => mock.CloseAsync(), Times.Once);
        }

        [TestMethod]
        public async Task DisconnectAsync_ConnectionIsNotOpen_ConnectionIsNotClosed()
        {
            var connectionMock = new Mock<IOwiUsbConnection>();
            connectionMock.SetupGet(mock => mock.IsOpen).Returns(false);

            var arm = new OwiArm(connectionMock.Object);
            await arm.DisconnectAsync();

            connectionMock.Verify(mock => mock.CloseAsync(), Times.Never);
        }

        [TestMethod]
        public void IsConnected_ReturnsTrueIfConnectionIsOpen()
        {
            var connectionMock = new Mock<IOwiUsbConnection>();
            connectionMock.SetupGet(mock => mock.IsOpen).Returns(true);

            var arm = new OwiArm(connectionMock.Object);

            Assert.IsTrue(arm.IsConnected);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task SendCommandAsync_ConnectionIsNotOpen_InvalidOperationExceptionOccurs()
        {
            var connectionMock = new Mock<IOwiUsbConnection>();
            connectionMock.SetupGet(mock => mock.IsOpen).Returns(false);

            var arm = new OwiArm(connectionMock.Object);
            await arm.SendCommandAsync(new OwiCommand());
        }

        [TestMethod]
        public async Task SendCommandAsync_ConnectionIsOpen_SendsCommand()
        {
            const byte expectedArmByte = 4;
            const byte expectedBaseOfArmByte = 6;
            const byte expectedLedByte = 2;

            var connectionMock = new Mock<IOwiUsbConnection>();
            connectionMock.SetupGet(mock => mock.IsOpen).Returns(true);

            var packetMock = new Mock<IOwiCommand>();
            packetMock.SetupGet(mock => mock.ArmByte).Returns(expectedArmByte);
            packetMock.SetupGet(mock => mock.BaseByte).Returns(expectedBaseOfArmByte);
            packetMock.SetupGet(mock => mock.LedByte).Returns(expectedLedByte);

            var arm = new OwiArm(connectionMock.Object);
            await arm.SendCommandAsync(packetMock.Object);

            connectionMock.Verify(mock => mock.SendAsync(expectedArmByte, expectedBaseOfArmByte, expectedLedByte), Times.Once);
        }

        [TestMethod]
        [Ignore]
        public async Task IntegrationTestThatRequiresAmr()
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

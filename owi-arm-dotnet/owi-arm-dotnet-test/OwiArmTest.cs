using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using owi_arm_dotnet;
using System;
using System.Threading;

namespace owi_arm_dotnet_test
{
    [TestClass]
    public class OwiArmTest
    {
        [TestMethod]
        public void Connect_ConnectionIsNotOpen_CallsOpenOnUsbConnection()
        {
            var connectionMock = new Mock<IOwiUsbConnection>();
            connectionMock.SetupGet(mock => mock.IsOpen).Returns(false);

            var arm = new OwiArm(connectionMock.Object);
            arm.Connect();

            connectionMock.Verify(mock => mock.Open(), Times.Once);
        }

        [TestMethod]
        public void Connect_ConnectionIsAlreadyOpen_DoesNotOpenConnectionAgain()
        {
            var connectionMock = new Mock<IOwiUsbConnection>();
            connectionMock.SetupGet(mock => mock.IsOpen).Returns(true);

            var arm = new OwiArm(connectionMock.Object);
            arm.Connect();

            connectionMock.Verify(mock => mock.Open(), Times.Never);
        }

        [TestMethod]
        public void Disconnect_ConnectionIsOpen_CallsCloseOnUsbConnection()
        {
            var connectionMock = new Mock<IOwiUsbConnection>();
            connectionMock.SetupGet(mock => mock.IsOpen).Returns(true);

            var arm = new OwiArm(connectionMock.Object);
            arm.Disconnect();

            connectionMock.Verify(mock => mock.Close(), Times.Once);
        }

        [TestMethod]
        public void Disconnect_ConnectionIsNotOpen_ConnectionIsNotClosed()
        {
            var connectionMock = new Mock<IOwiUsbConnection>();
            connectionMock.SetupGet(mock => mock.IsOpen).Returns(false);

            var arm = new OwiArm(connectionMock.Object);
            arm.Disconnect();

            connectionMock.Verify(mock => mock.Close(), Times.Never);
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
        public void SendCommand_ConnectionIsNotOpen_InvalidOperationExceptionOccurs()
        {
            var connectionMock = new Mock<IOwiUsbConnection>();
            connectionMock.SetupGet(mock => mock.IsOpen).Returns(false);

            var arm = new OwiArm(connectionMock.Object);
            arm.SendCommand(new OwiCommand());
        }

        [TestMethod]
        public void SendCommand_ConnectionIsOpen_SendsCommand()
        {
            const byte expectedArmByte = 4;
            const byte expectedBaseOfArmByte = 6;
            const byte expectedLedByte = 2;

            var connection = new Mock<IOwiUsbConnection>();
            connection.SetupGet(mock => mock.IsOpen).Returns(true);

            var packetMock = new Mock<IOwiCommand>();
            packetMock.SetupGet(mock => mock.ArmByte).Returns(expectedArmByte);
            packetMock.SetupGet(mock => mock.BaseByte).Returns(expectedBaseOfArmByte);
            packetMock.SetupGet(mock => mock.LedByte).Returns(expectedLedByte);

            var arm = new OwiArm(connection.Object);
            arm.SendCommand(packetMock.Object);

            connection.Verify(mock => mock.Send(expectedArmByte, expectedBaseOfArmByte, expectedLedByte), Times.Once);
        }

        [TestMethod]
        [Ignore]
        public void IntegrationTestThatRequiresAmr()
        {
            IOwiArm arm = new OwiArm();
            arm.Connect();


            IOwiCommand command = new OwiCommand().BaseRotateClockwise().ShoulderUp().LedOn();

            arm.SendCommand(command);
            Thread.Sleep(2000);

            arm.SendCommand(command.StopAllMovements().LedOff());

            arm.Disconnect();
        }
    }
}

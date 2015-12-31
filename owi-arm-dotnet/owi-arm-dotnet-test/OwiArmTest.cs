using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using owi_arm_dotnet;
using System;

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
        [ExpectedException(typeof(InvalidOperationException))]
        public void MoveArm_ConnectionIsNotOpen_InvalidOperationExceptionOccurs()
        {
            var connectionMock = new Mock<IOwiUsbConnection>();
            connectionMock.SetupGet(mock => mock.IsOpen).Returns(false);

            var arm = new OwiArm(connectionMock.Object);
            arm.MoveArm(new CommandPacket(ArmCommands.GripClose));
        }

        [TestMethod]
        public void MoveArm_SendsCommand()
        {
            var expectedArmCommand = ArmCommands.GripClose;
            var expectedBaseOfArmCommand = BaseOfArmCommands.RotateCounterClockwise;
            var expectedLedCommand = LedCommands.On;

            var connection = new Mock<IOwiUsbConnection>();
            connection.SetupGet(mock => mock.IsOpen).Returns(true);

            var packetMock = new Mock<ICommandPacket>();
            packetMock.SetupGet(mock => mock.ArmCommand).Returns(expectedArmCommand);
            packetMock.SetupGet(mock => mock.BaseOfArmCommand).Returns(expectedBaseOfArmCommand);
            packetMock.SetupGet(mock => mock.LedCommand).Returns(expectedLedCommand);

            var arm = new OwiArm(connection.Object);
            arm.MoveArm(packetMock.Object);

            connection.Verify(mock => mock.Send(expectedArmCommand.Value, expectedBaseOfArmCommand.Value, expectedLedCommand.Value), Times.Once);
        }
    }
}

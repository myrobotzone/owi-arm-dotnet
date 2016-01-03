﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            arm.MoveArm(new Command());
        }

        [TestMethod]
        public void MoveArm_SendsCommand()
        {
            const byte expectedArmByte = 4;
            const byte expectedBaseOfArmByte = 6;
            const byte expectedLedByte = 2;

            var connection = new Mock<IOwiUsbConnection>();
            connection.SetupGet(mock => mock.IsOpen).Returns(true);

            var packetMock = new Mock<ICommand>();
            packetMock.SetupGet(mock => mock.ArmByte).Returns(expectedArmByte);
            packetMock.SetupGet(mock => mock.BaseByte).Returns(expectedBaseOfArmByte);
            packetMock.SetupGet(mock => mock.LedByte).Returns(expectedLedByte);

            var arm = new OwiArm(connection.Object);
            arm.MoveArm(packetMock.Object);

            connection.Verify(mock => mock.Send(expectedArmByte, expectedBaseOfArmByte, expectedLedByte), Times.Once);
        }
    }
}

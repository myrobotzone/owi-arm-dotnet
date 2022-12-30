using FluentAssertions;
using Moq;
using owi_arm_dotnet;

namespace owi_arm_dotnet_test;

public class OwiArmTest
{
    [Fact]
    public async Task ConnectAsync_ConnectionIsNotOpen_CallsOpenOnUsbConnection()
    {
        var connectionMock = new Mock<IOwiUsbConnection>();
        connectionMock.SetupGet(mock => mock.IsOpen).Returns(false);

        var arm = new OwiArm(connectionMock.Object);
        await arm.ConnectAsync();

        connectionMock.Verify(mock => mock.OpenAsync(), Times.Once);
    }

    [Fact]
    public async Task ConnectAsync_ConnectionIsAlreadyOpen_DoesNotOpenConnectionAgain()
    {
        var connectionMock = new Mock<IOwiUsbConnection>();
        connectionMock.SetupGet(mock => mock.IsOpen).Returns(true);

        var arm = new OwiArm(connectionMock.Object);
        await arm.ConnectAsync();

        connectionMock.Verify(mock => mock.OpenAsync(), Times.Never);
    }

    [Fact]
    public async Task DisconnectAsync_ConnectionIsOpen_CallsCloseOnUsbConnection()
    {
        var connectionMock = new Mock<IOwiUsbConnection>();
        connectionMock.SetupGet(mock => mock.IsOpen).Returns(true);

        var arm = new OwiArm(connectionMock.Object);
        await arm.DisconnectAsync();

        connectionMock.Verify(mock => mock.CloseAsync(), Times.Once);
    }

    [Fact]
    public async Task DisconnectAsync_ConnectionIsNotOpen_ConnectionIsNotClosed()
    {
        var connectionMock = new Mock<IOwiUsbConnection>();
        connectionMock.SetupGet(mock => mock.IsOpen).Returns(false);

        var arm = new OwiArm(connectionMock.Object);
        await arm.DisconnectAsync();

        connectionMock.Verify(mock => mock.CloseAsync(), Times.Never);
    }

    [Fact]
    public void IsConnected_ReturnsTrueIfConnectionIsOpen()
    {
        var connectionMock = new Mock<IOwiUsbConnection>();
        connectionMock.SetupGet(mock => mock.IsOpen).Returns(true);

        var arm = new OwiArm(connectionMock.Object);

        arm.IsConnected.Should().BeTrue();
    }

    [Fact]
    public async Task SendCommandAsync_ConnectionIsNotOpen_InvalidOperationExceptionOccurs()
    {
        var connectionMock = new Mock<IOwiUsbConnection>();
        connectionMock.SetupGet(mock => mock.IsOpen).Returns(false);

        var arm = new OwiArm(connectionMock.Object);
        await arm.Invoking(a => a.SendCommandAsync(new OwiCommand())).Should()
            .ThrowAsync<InvalidOperationException>();
    }

    [Fact]
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

        connectionMock.Verify(mock => mock.SendAsync(expectedArmByte, expectedBaseOfArmByte, expectedLedByte),
            Times.Once);
    }
}
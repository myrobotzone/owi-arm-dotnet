using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using owi_arm_dotnet;

namespace owi_arm_dotnet_test
{
    [TestClass]
    public class CommandPacketTest
    {
        [TestMethod]
        public void ctor_ArmCommandIsGripOpen_OthersAreStopped()
        {
            var packet = new CommandPacket(ArmCommands.GripOpen);

            Assert.AreEqual(ArmCommands.GripOpen, packet.ArmCommand);
            Assert.AreEqual(BaseOfArmCommands.Stop, packet.BaseOfArmCommand);
            Assert.AreEqual(LedCommands.Off, packet.LedCommand);
        }

        [TestMethod]
        public void ctor_BaseOfArmCommandIsRotateClockwise_OthersAreStopped()
        {
            var packet = new CommandPacket(BaseOfArmCommands.RotateClockwise);

            Assert.AreEqual(ArmCommands.Stop, packet.ArmCommand);
            Assert.AreEqual(BaseOfArmCommands.RotateClockwise, packet.BaseOfArmCommand);
            Assert.AreEqual(LedCommands.Off, packet.LedCommand);
        }

        [TestMethod]
        public void ctor_LedCommandIsOn_OthersAreStopped()
        {
            var packet = new CommandPacket(LedCommands.On);

            Assert.AreEqual(ArmCommands.Stop, packet.ArmCommand);
            Assert.AreEqual(BaseOfArmCommands.Stop, packet.BaseOfArmCommand);
            Assert.AreEqual(LedCommands.On, packet.LedCommand);
        }
    }
}

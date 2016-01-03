using Microsoft.VisualStudio.TestTools.UnitTesting;
using owi_arm_dotnet;

namespace owi_arm_dotnet_test
{
    [TestClass]
    public class OwiCommandTest
    {
        [TestMethod]
        public void LedOn_LedWasOff_LedBitIsSet()
        {
            var command = new OwiCommand();

            command.LedOn();

            Assert.IsTrue(IsBitSet(command.LedByte, 0));
        }


        [TestMethod]
        public void LedOff_LedWasOn_LedBitIsNotSet()
        {
            var command = new OwiCommand();

            command.LedOn();
            command.LedOff();

            Assert.IsFalse(IsBitSet(command.LedByte, 0));
        }

        [TestMethod]
        public void ElbowUp_ElbowWasStoped_ElbowUpBitIsSet()
        {
            var command = new OwiCommand();

            command.ElbowUp();

            Assert.IsTrue(IsBitSet(command.ArmByte, 4));
        }

        [TestMethod]
        public void ElbowDown_ElbowWasMovingUp_ElbowDownBitIsSet()
        {
            var command = new OwiCommand();

            command.ElbowUp();
            command.ElbowDown();
            
            Assert.IsFalse(IsBitSet(command.ArmByte, 4));
            Assert.IsTrue(IsBitSet(command.ArmByte, 5));
        }

        [TestMethod]
        public void ElbowStop_ElbowWasDown_ElbowBitsAreReset()
        {
            var command = new OwiCommand();

            command.ElbowDown();
            command.ElbowStop();

            Assert.IsFalse(IsBitSet(command.ArmByte, 4));
            Assert.IsFalse(IsBitSet(command.ArmByte, 5));
        }

        [TestMethod]
        public void StopAllMovements_LedIsOneAndArmIsMoving_LedBitIsOnButMovementsBitsAreReset()
        {
            var command = new OwiCommand();

            command.LedOn();
            command.ElbowUp();
            command.StopAllMovements();

            Assert.AreNotEqual(0, command.LedByte);
            Assert.AreEqual(0, command.ArmByte);
            Assert.AreEqual(0, command.BaseByte);
        }

        bool IsBitSet(byte b, int pos)
        {
            return (b & (1 << pos)) != 0;
        }
    }
}

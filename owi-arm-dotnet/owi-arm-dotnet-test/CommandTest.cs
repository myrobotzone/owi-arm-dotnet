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

            command.LedOn().LedOff();

            Assert.IsFalse(IsBitSet(command.LedByte, 0));
        }

        [TestMethod]
        public void GripperClose_GriperIsStopped_GripperCloseBitIsSet()
        {
            var command = new OwiCommand();

            command.GripperStop().GripperClose();

            Assert.IsTrue(IsBitSet(command.ArmByte, 0));
        }

        [TestMethod]
        public void GripperOpen_GriperIsClosing_GripperOpenBitIsSet()
        {
            var command = new OwiCommand();

            command.GripperClose().GripperOpen();

            Assert.IsFalse(IsBitSet(command.ArmByte, 0));
            Assert.IsTrue(IsBitSet(command.ArmByte, 1));
        }

        [TestMethod]
        public void WristUp_WristIsStopped_WristUpBitIsSet()
        {
            var command = new OwiCommand();

            command.WristUp();

            Assert.IsTrue(IsBitSet(command.ArmByte, 2));
        }

        [TestMethod]
        public void WristDown_WristWasMovingUp_WristDownBitIsSet()
        {
            var command = new OwiCommand();

            command.WristUp().WristDown();

            Assert.IsFalse(IsBitSet(command.ArmByte, 2));
            Assert.IsTrue(IsBitSet(command.ArmByte, 3));
        }

        public void WristStop_WristWasMovingUp_WristDownBitIsSet()
        {
            var command = new OwiCommand();

            command.WristUp().WristStop();

            Assert.IsFalse(IsBitSet(command.ArmByte, 2));
            Assert.IsFalse(IsBitSet(command.ArmByte, 3));
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

            command.ElbowUp().ElbowDown();

            Assert.IsFalse(IsBitSet(command.ArmByte, 4));
            Assert.IsTrue(IsBitSet(command.ArmByte, 5));
        }

        [TestMethod]
        public void ElbowStop_ElbowWasDown_ElbowBitsAreReset()
        {
            var command = new OwiCommand();

            command.ElbowDown().ElbowStop();

            Assert.IsFalse(IsBitSet(command.ArmByte, 4));
            Assert.IsFalse(IsBitSet(command.ArmByte, 5));
        }

        [TestMethod]
        public void ShoulderUp_ShoulderWasStoped_ShoulderUpBitIsSet()
        {
            var command = new OwiCommand();

            command.ShoulderUp();

            Assert.IsTrue(IsBitSet(command.ArmByte, 6));
        }

        [TestMethod]
        public void ShoulderDown_ShoulderWasMovingUp_ShoulderDownBitIsSet()
        {
            var command = new OwiCommand();

            command.ShoulderUp().ShoulderDown();

            Assert.IsFalse(IsBitSet(command.ArmByte, 6));
            Assert.IsTrue(IsBitSet(command.ArmByte, 7));
        }

        [TestMethod]
        public void ShoulderStop_ShoulderWasDown_ShoulderBitsAreReset()
        {
            var command = new OwiCommand();

            command.ShoulderDown().ShoulderStop();

            Assert.IsFalse(IsBitSet(command.ArmByte, 6));
            Assert.IsFalse(IsBitSet(command.ArmByte, 7));
        }

        [TestMethod]
        public void BaseRotateClockwise_BaseRotateWasStoped_BaseRotateClockwiseBitIsSet()
        {
            var command = new OwiCommand();

            command.BaseRotateClockwise();

            Assert.IsTrue(IsBitSet(command.BaseByte, 1));
        }

        [TestMethod]
        public void BaseRotateCounterClockwise_BaseRotateWasMovingClockwise_BaseRotateCounterClockwiseBitIsSet()
        {
            var command = new OwiCommand();

            command.BaseRotateClockwise().BaseRotateCounterClockwise();

            Assert.IsFalse(IsBitSet(command.BaseByte, 1));
            Assert.IsTrue(IsBitSet(command.BaseByte, 0));
        }

        [TestMethod]
        public void BaseRotateStop_BaseRotateWasClockwise_BaseRotateBitsAreReset()
        {
            var command = new OwiCommand();

            command.BaseRotateClockwise().BaseRotateStop();

            Assert.IsFalse(IsBitSet(command.BaseByte, 0));
            Assert.IsFalse(IsBitSet(command.BaseByte, 1));
        }

        [TestMethod]
        public void StopAllMovements_LedIsOneAndArmIsMoving_LedBitIsOnButMovementsBitsAreReset()
        {
            var command = new OwiCommand();

            command.LedOn().ElbowUp().StopAllMovements();

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

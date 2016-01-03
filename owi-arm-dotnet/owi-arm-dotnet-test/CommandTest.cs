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
            var command = new Command();

            command.LedOn();

            Assert.IsTrue(IsBitSet(command.LedByte, 0));
        }


        [TestMethod]
        public void LedOff_LedWasOn_LedBitIsNotSet()
        {
            var command = new Command();

            command.LedOn();
            command.LedOff();

            Assert.IsFalse(IsBitSet(command.LedByte, 0));
        }

        [TestMethod]
        public void ElbowUp_ElbowWasStoped_ElbowUpBitIsSet()
        {
            var command = new Command();

            command.ElbowUp();

            Assert.IsTrue(IsBitSet(command.ElbowByte, 4));
        }

        [TestMethod]
        public void ElbowDown_ElbowWasMovingUp_ElbowDownBitIsSet()
        {
            var command = new Command();

            command.ElbowUp();
            command.ElbowDown();
            
            Assert.IsFalse(IsBitSet(command.ElbowByte, 4));
            Assert.IsTrue(IsBitSet(command.ElbowByte, 5));
        }

        [TestMethod]
        public void ElbowStop_ElbowWasDown_ElbowBitsAreReset()
        {
            var command = new Command();

            command.ElbowDown();
            command.ElbowStop();

            Assert.IsFalse(IsBitSet(command.ElbowByte, 4));
            Assert.IsFalse(IsBitSet(command.ElbowByte, 5));
        }

        bool IsBitSet(byte b, int pos)
        {
            return (b & (1 << pos)) != 0;
        }
    }
}

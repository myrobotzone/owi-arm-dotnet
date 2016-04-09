namespace owi_arm_dotnet
{
    /// <inheritdoc />
    public class OwiArmFactory : IOwiArmFactory
    {
        /// <inheritdoc />
        public IOwiArm Create(IOwiUsbConnection owiUsbConnection)
        {
            return new OwiArm(owiUsbConnection);
        }
    }
}

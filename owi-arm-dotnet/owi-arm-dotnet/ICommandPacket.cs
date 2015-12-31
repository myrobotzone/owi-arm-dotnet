namespace owi_arm_dotnet
{
    public interface ICommandPacket
    {
        ArmCommand ArmCommand { get; }

        BaseOfArmCommand BaseOfArmCommand { get; }

        LedCommand LedCommand { get; }
    }
}

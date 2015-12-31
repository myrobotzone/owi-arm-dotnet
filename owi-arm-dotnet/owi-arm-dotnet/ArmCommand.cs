using System;

namespace owi_arm_dotnet
{
    public class ArmCommand : ICommand
    {
        internal ArmCommand(Byte value)
        {
            this.Value = value;
        }

        public Byte Value { get; private set; }

        public static ArmCommand operator +(ArmCommand a, ArmCommand b)
        {
            return new ArmCommand((Byte)(a.Value + b.Value));
        }
    }
}

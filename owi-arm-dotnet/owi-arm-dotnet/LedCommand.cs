using System;

namespace owi_arm_dotnet
{
    public class LedCommand : ICommand
    {
        internal LedCommand(Byte value)
        {
            this.Value = value;
        }

        public Byte Value { get; private set; }
    }
}

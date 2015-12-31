namespace owi_arm_dotnet
{
    public static class LedCommands
    {
        static LedCommands()
        {
            On = new LedCommand(0);
            Off = new LedCommand(1);
        }

        public static LedCommand On { get; private set; }

        public static LedCommand Off { get; private set; }
    }
}

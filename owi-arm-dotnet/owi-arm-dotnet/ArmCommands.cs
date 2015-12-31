namespace owi_arm_dotnet
{
    // Based on protocol as documented at http://notbrainsurgery.livejournal.com/38622.html
    public static class ArmCommands
    {
        static ArmCommands()
        {
            Stop = new ArmCommand(0);
            GripClose = new ArmCommand(1);
            GripOpen = new ArmCommand(2);
        }

        public static ArmCommand Stop { get; private set; }

        public static ArmCommand GripClose { get; private set; }

        public static ArmCommand GripOpen { get; private set; }
    }
}

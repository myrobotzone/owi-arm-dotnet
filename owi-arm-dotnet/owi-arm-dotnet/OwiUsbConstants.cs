namespace owi_arm_dotnet
{
    public static class OwiUsbConstants
    {
        public static uint Vid
        {
            get
            {
                return 0x1267;
            }
        }

        public static uint Pid
        {
            get
            {
                return 0x0000;
            }
        }

        public static byte Requst
        {
            get
            {
                return 6;
            }
        }

        public static short Value
        {
            get
            {
                return 0x100;
            }
        }
    }
}

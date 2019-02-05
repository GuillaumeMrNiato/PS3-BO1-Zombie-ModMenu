namespace BO2NoHost
{
    using System;

    internal class Buttons
    {
        public static bool DpadDown()
        {
            return Convert.ToBoolean(PS3.Extension.ReadInt32(0xf0a91f));
        }

        public static bool DpadL()
        {
            return Convert.ToBoolean(PS3.Extension.ReadInt32(0xf0a933));
        }

        public static bool DpadR()
        {
            return Convert.ToBoolean(PS3.Extension.ReadInt32(0xf0a93f));
        }

        public static bool DpadUp()
        {
            return Convert.ToBoolean(PS3.Extension.ReadInt32(0xf0a90f));
        }

        public static bool R3()
        {
            return Convert.ToBoolean(PS3.Extension.ReadInt32(0xf0a8df));
        }

        public static bool Square()
        {
            return Convert.ToBoolean(PS3.Extension.ReadInt32(0xf0a803));
        }
    }
}


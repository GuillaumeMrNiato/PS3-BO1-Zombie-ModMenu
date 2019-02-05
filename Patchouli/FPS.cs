namespace BO2NoHost
{
    using Patchouli;
    using System;

    internal class FPS
    {
        public static uint Text = 0x8E3290;

        public static void CloseMenu()
        {
            Form1.SetText("\0");
        }

        public static void DestroyText()
        {
            byte[] destroy = new byte[0x15];
            PS3.SetMemory(Text, destroy);
        }

        public static void Enable(bool enabled)
        {
            if (enabled)
            {
                PS3.SetMemory(0x37fec, new byte[] { 0x40 });
                PS3.SetMemory(0x397a22, new byte[] { 1, 8 });
                PS3.SetMemory(0x397a2a, new byte[2]);
                PS3.SetMemory(0x397400, new byte[] { 0x41, 0xb0, 0, 0, 0x43, 100, 0, 0, 0x42, 0x10 });
            }
            else
            {
                PS3.SetMemory(0x3979fc, new byte[] { 0x41 });
                PS3.SetMemory(0x397a22, new byte[] { 1, 8 });
                PS3.SetMemory(0x397a2a, new byte[2]);
                PS3.SetMemory(0x397400, new byte[] { 0x41, 0xb0, 0, 0, 0x43, 100, 0, 0, 0x42, 0x10 });
            }
        }
    }
}


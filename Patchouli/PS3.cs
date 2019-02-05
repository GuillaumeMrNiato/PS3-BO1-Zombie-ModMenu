namespace BO2NoHost
{
    using PS3Lib;
    using System;
    using System.Linq;
    using System.Text;

    internal class PS3
    {
        private static PS3API DEX = new PS3API(SelectAPI.TargetManager);

        public static void ChangeAPI(SelectAPI API)
        {
            DEX.ChangeAPI(API);
        }

        public static void Connect()
        {
            DEX.ConnectTarget(0);
            DEX.AttachProcess();
        }

        public static SelectAPI GetCurrentAPI()
        {
            return DEX.GetCurrentAPI();
        }

        public static byte[] GetMemory(uint offset, int length)
        {
            byte[] array = new byte[length];
            DEX.GetMemory(offset, array);
            return array;
        }

        public static byte[] GetMemoryL(uint address, int length)
        {
            byte[] buffer = new byte[length];
            DEX.GetMemory(address, buffer);
            return buffer;
        }

        public static void GetMemoryR(uint Address, ref byte[] Bytes)
        {
            DEX.GetMemory(Address, Bytes);
        }

        public static void Reconnect()
        {
            DEX.ConnectTarget(0);
        }

        public static void SetMemory(uint Address, byte[] Bytes)
        {
            DEX.SetMemory(Address, Bytes);
        }

        private class Conversions
        {
            public static byte[] RandomizeRGBA()
            {
                byte[] RGBA = new byte[4];
                Random randomize = new Random();
                RGBA[0] = BitConverter.GetBytes(randomize.Next(0, 0xff))[0];
                RGBA[1] = BitConverter.GetBytes(randomize.Next(0, 0xff))[0];
                RGBA[2] = BitConverter.GetBytes(randomize.Next(0, 0xff))[0];
                RGBA[3] = BitConverter.GetBytes(randomize.Next(0, 0xff))[0];
                return RGBA;
            }

            public static byte[] ReverseBytes(byte[] input)
            {
                Array.Reverse(input);
                return input;
            }
        }

        public class Extension
        {
            private static SelectAPI CurrentAPI;

            private static byte[] GetBytes(uint offset, int length, SelectAPI API)
            {
                byte[] bytes = new byte[length];
                if (API == SelectAPI.ControlConsole)
                {
                    CurrentAPI = PS3.GetCurrentAPI();
                    return PS3.DEX.GetBytes(offset, length);
                }
                if (API == SelectAPI.TargetManager)
                {
                    CurrentAPI = PS3.GetCurrentAPI();
                    bytes = PS3.DEX.GetBytes(offset, length);
                }
                return bytes;
            }

            private static void GetMem(uint offset, byte[] buffer, SelectAPI API)
            {
                if (API == SelectAPI.ControlConsole)
                {
                    PS3.GetMemoryR(offset, ref buffer);
                }
                else if (API == SelectAPI.TargetManager)
                {
                    PS3.GetMemoryR(offset, ref buffer);
                }
            }

            public static bool ReadBool(uint offset)
            {
                byte[] buffer = new byte[1];
                GetMem(offset, buffer, CurrentAPI);
                return (buffer[0] != 0);
            }

            public static byte ReadByte(uint offset)
            {
                return GetBytes(offset, 1, CurrentAPI)[0];
            }

            public static byte[] ReadBytes(uint offset, int length)
            {
                return GetBytes(offset, length, CurrentAPI);
            }

            public static float ReadFloat(uint offset)
            {
                byte[] array = GetBytes(offset, 4, CurrentAPI);
                Array.Reverse(array, 0, 4);
                return BitConverter.ToSingle(array, 0);
            }

            public static short ReadInt16(uint offset)
            {
                byte[] array = GetBytes(offset, 2, CurrentAPI);
                Array.Reverse(array, 0, 2);
                return BitConverter.ToInt16(array, 0);
            }

            public static int ReadInt32(uint offset)
            {
                byte[] array = GetBytes(offset, 4, CurrentAPI);
                Array.Reverse(array, 0, 4);
                return BitConverter.ToInt32(array, 0);
            }

            public static long ReadInt64(uint offset)
            {
                byte[] array = GetBytes(offset, 8, CurrentAPI);
                Array.Reverse(array, 0, 8);
                return BitConverter.ToInt64(array, 0);
            }

            public static sbyte ReadSByte(uint offset)
            {
                byte[] buffer = new byte[1];
                GetMem(offset, buffer, CurrentAPI);
                return (sbyte) buffer[0];
            }

            public static string ReadString(uint offset)
            {
                int length = 40;
                int num2 = 0;
                string source = "";
                do
                {
                    byte[] bytes = ReadBytes(offset + ((uint) num2), length);
                    source = source + Encoding.UTF8.GetString(bytes);
                    num2 += length;
                }
                while (!source.Contains<char>('\0'));
                int index = source.IndexOf('\0');
                string str2 = source.Substring(0, index);
                source = string.Empty;
                return str2;
            }

            public static ushort ReadUInt16(uint offset)
            {
                byte[] array = GetBytes(offset, 2, CurrentAPI);
                Array.Reverse(array, 0, 2);
                return BitConverter.ToUInt16(array, 0);
            }

            public static uint ReadUInt32(uint offset)
            {
                byte[] array = GetBytes(offset, 4, CurrentAPI);
                Array.Reverse(array, 0, 4);
                return BitConverter.ToUInt32(array, 0);
            }

            public static ulong ReadUInt64(uint offset)
            {
                byte[] array = GetBytes(offset, 8, CurrentAPI);
                Array.Reverse(array, 0, 8);
                return BitConverter.ToUInt64(array, 0);
            }

            public static byte[] ReverseArray(float float_0)
            {
                byte[] bytes = BitConverter.GetBytes(float_0);
                Array.Reverse(bytes);
                return bytes;
            }

            public static byte[] ReverseBytes(byte[] inArray)
            {
                Array.Reverse(inArray);
                return inArray;
            }

            private static void SetMem(uint Address, byte[] buffer, SelectAPI API)
            {
                PS3.DEX.SetMemory(Address, buffer);
            }

            public static byte[] ToHexFloat(float Axis)
            {
                byte[] bytes = BitConverter.GetBytes(Axis);
                Array.Reverse(bytes);
                return bytes;
            }

            public static byte[] uintBytes(uint input)
            {
                byte[] data = BitConverter.GetBytes(input);
                Array.Reverse(data);
                return data;
            }

            public static void WriteBool(uint offset, bool input)
            {
                byte[] buffer = new byte[] { input ? ((byte) 1) : ((byte) 0) };
                SetMem(offset, buffer, CurrentAPI);
            }

            public static void WriteByte(uint offset, byte input)
            {
                byte[] buffer = new byte[] { input };
                SetMem(offset, buffer, CurrentAPI);
            }

            public static void WriteBytes(uint offset, byte[] input)
            {
                byte[] buffer = input;
                SetMem(offset, buffer, CurrentAPI);
            }

            public static void WriteFloat(uint offset, float input)
            {
                byte[] array = new byte[4];
                BitConverter.GetBytes(input).CopyTo(array, 0);
                Array.Reverse(array, 0, 4);
                SetMem(offset, array, CurrentAPI);
            }

            public static void WriteInt16(uint offset, short input)
            {
                byte[] array = new byte[2];
                BitConverter.GetBytes(input).CopyTo(array, 0);
                Array.Reverse(array, 0, 2);
                SetMem(offset, array, CurrentAPI);
            }

            public static void WriteInt32(uint offset, int input)
            {
                byte[] array = new byte[4];
                BitConverter.GetBytes(input).CopyTo(array, 0);
                Array.Reverse(array, 0, 4);
                SetMem(offset, array, CurrentAPI);
            }

            public static void WriteInt64(uint offset, long input)
            {
                byte[] array = new byte[8];
                BitConverter.GetBytes(input).CopyTo(array, 0);
                Array.Reverse(array, 0, 8);
                SetMem(offset, array, CurrentAPI);
            }

            public static void WriteSByte(uint offset, sbyte input)
            {
                byte[] buffer = new byte[] { (byte) input };
                SetMem(offset, buffer, CurrentAPI);
            }

            public static void WriteSingle(uint address, float[] input)
            {
                int length = input.Length;
                byte[] array = new byte[length * 4];
                for (int i = 0; i < length; i++)
                {
                    ReverseBytes(BitConverter.GetBytes(input[i])).CopyTo(array, (int) (i * 4));
                }
                PS3.SetMemory(address, array);
            }

            public static void WriteSingle(uint address, float input)
            {
                byte[] array = new byte[4];
                BitConverter.GetBytes(input).CopyTo(array, 0);
                Array.Reverse(array, 0, 4);
                PS3.SetMemory(address, array);
            }

            public static void WriteString(uint offset, string input)
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);
                Array.Resize<byte>(ref bytes, bytes.Length + 1);
                SetMem(offset, bytes, CurrentAPI);
            }

            public static void WriteUInt16(uint offset, ushort input)
            {
                byte[] array = new byte[2];
                BitConverter.GetBytes(input).CopyTo(array, 0);
                Array.Reverse(array, 0, 2);
                SetMem(offset, array, CurrentAPI);
            }

            public static void WriteUInt32(uint offset, uint input)
            {
                byte[] array = new byte[4];
                BitConverter.GetBytes(input).CopyTo(array, 0);
                Array.Reverse(array, 0, 4);
                SetMem(offset, array, CurrentAPI);
            }

            public static void WriteUInt64(uint offset, ulong input)
            {
                byte[] array = new byte[8];
                BitConverter.GetBytes(input).CopyTo(array, 0);
                Array.Reverse(array, 0, 8);
                SetMem(offset, array, CurrentAPI);
            }
        }
    }
}


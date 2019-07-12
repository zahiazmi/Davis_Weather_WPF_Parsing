namespace WulungGCSLibrary
{
#if DEBUG
    public static class ValueTypeExtensions
    {
        public static string GetBinaryString(this bool value)
        {
            return value ? "1" : "0";
        }
        public static string GetBinaryString(this byte value, bool reverse = true)
        {
            string retVal = "0b";
            UInt8T arbVal = value;
            if (reverse)
            {
                for (int i = 0; i < UInt8T.BitSize; i++)
                {
                    retVal += arbVal[7 - i] ? "1" : "0";
                }
            }
            else
            {
                for (int i = 0; i < UInt8T.BitSize; i++)
                {
                    retVal += arbVal[i] ? "1" : "0";
                }
            }
            return retVal;
        }
        public static string GetBinaryString(this sbyte value, bool reverse = true)
        {
            string retVal = "0b";
            Int8T arbVal = value;
            if (reverse)
            {
                for (int i = 0; i < Int8T.BitSize; i++)
                {
                    retVal += arbVal[7 - i] ? "1" : "0";
                }
            }
            else
            {
                for (int i = 0; i < Int8T.BitSize; i++)
                {
                    retVal += arbVal[i] ? "1" : "0";
                }
            }
            return retVal;
        }
        public static string GetBinaryString(this ushort value, bool reverse = true)
        {
            string retVal = "0b";
            UInt16T arbVal = value;
            if (reverse)
            {
                for (int i = 0; i < UInt16T.BitSize; i++)
                {
                    retVal += arbVal[UInt16T.Size - 1 - (i / UInt8T.BitSize)][UInt8T.BitSize - 1 - (i % UInt8T.BitSize)] ? "1" : "0";
                }
            }
            else
            {
                for (int i = 0; i < UInt16T.BitSize; i++)
                {
                    retVal += arbVal[i / UInt8T.BitSize][i % UInt8T.BitSize] ? "1" : "0";
                }
            }
            return retVal;
        }
        public static string GetBinaryString(this short value, bool reverse = true)
        {
            string retVal = "0b";
            Int16T arbVal = value;
            if (reverse)
            {
                for (int i = 0; i < Int16T.BitSize; i++)
                {
                    retVal += arbVal[Int16T.Size - 1 - (i / UInt8T.BitSize)][UInt8T.BitSize - 1 - (i % UInt8T.BitSize)] ? "1" : "0";
                }
            }
            else
            {
                for (int i = 0; i < Int16T.BitSize; i++)
                {
                    retVal += arbVal[i / UInt8T.BitSize][i % UInt8T.BitSize] ? "1" : "0";
                }
            }
            return retVal;
        }
        public static string GetBinaryString(this uint value, bool reverse = true)
        {
            string retVal = "0b";
            UInt32T arbVal = value;
            if (reverse)
            {
                for (int i = 0; i < UInt32T.BitSize; i++)
                {
                    retVal += arbVal[UInt32T.Size - 1 - (i / UInt8T.BitSize)][UInt8T.BitSize - 1 - (i % UInt8T.BitSize)] ? "1" : "0";
                }
            }
            else
            {
                for (int i = 0; i < UInt32T.BitSize; i++)
                {
                    retVal += arbVal[i / UInt8T.BitSize][i % UInt8T.BitSize] ? "1" : "0";
                }
            }
            return retVal;
        }
        public static string GetBinaryString(this int value, bool reverse = true)
        {
            string retVal = "0b";
            Int32T arbVal = value;
            if (reverse)
            {
                for (int i = 0; i < Int32T.BitSize; i++)
                {
                    retVal += arbVal[Int32T.Size - 1 - (i / UInt8T.BitSize)][UInt8T.BitSize - 1 - (i % UInt8T.BitSize)] ? "1" : "0";
                }
            }
            else
            {
                for (int i = 0; i < Int32T.BitSize; i++)
                {
                    retVal += arbVal[i / UInt8T.BitSize][i % UInt8T.BitSize] ? "1" : "0";
                }
            }
            return retVal;
        }
        public static string GetBinaryString(this ulong value, bool reverse = true)
        {
            string retVal = "0b";
            UInt64T arbVal = value;
            if (reverse)
            {
                for (int i = 0; i < UInt64T.BitSize; i++)
                {
                    retVal += arbVal[UInt64T.Size - 1 - (i / UInt8T.BitSize)][UInt8T.BitSize - 1 - (i % UInt8T.BitSize)] ? "1" : "0";
                }
            }
            else
            {
                for (int i = 0; i < UInt64T.BitSize; i++)
                {
                    retVal += arbVal[i / UInt8T.BitSize][i % UInt8T.BitSize] ? "1" : "0";
                }
            }
            return retVal;
        }
        public static string GetBinaryString(this long value, bool reverse = true)
        {
            string retVal = "0b";
            Int64T arbVal = value;
            if (reverse)
            {
                for (int i = 0; i < Int64T.BitSize; i++)
                {
                    retVal += arbVal[Int64T.Size - 1 - (i / UInt8T.BitSize)][UInt8T.BitSize - 1 - (i % UInt8T.BitSize)] ? "1" : "0";
                }
            }
            else
            {
                for (int i = 0; i < Int64T.BitSize; i++)
                {
                    retVal += arbVal[i / UInt8T.BitSize][i % UInt8T.BitSize] ? "1" : "0";
                }
            }
            return retVal;
        }
        public static string GetBinaryString(this float value, bool reverse = true)
        {
            string retVal = "0b";
            Real32T arbVal = value;
            if (reverse)
            {
                for (int i = 0; i < Real32T.BitSize; i++)
                {
                    retVal += arbVal[Real32T.Size - 1 - (i / UInt8T.BitSize)][UInt8T.BitSize - 1 - (i % UInt8T.BitSize)] ? "1" : "0";
                }
            }
            else
            {
                for (int i = 0; i < Real32T.BitSize; i++)
                {
                    retVal += arbVal[i / UInt8T.BitSize][i % UInt8T.BitSize] ? "1" : "0";
                }
            }
            return retVal;
        }
        public static string GetBinaryString(this double value, bool reverse = true)
        {
            string retVal = "0b";
            Real64T arbVal = value;
            if (reverse)
            {
                for (int i = 0; i < Real64T.BitSize; i++)
                {
                    retVal += arbVal[Real64T.Size - 1 - (i / UInt8T.BitSize)][UInt8T.BitSize - 1 - (i % UInt8T.BitSize)] ? "1" : "0";
                }
            }
            else
            {
                for (int i = 0; i < Real64T.BitSize; i++)
                {
                    retVal += arbVal[i / UInt8T.BitSize][i % UInt8T.BitSize] ? "1" : "0";
                }
            }
            return retVal;
        }

        public static string GetHexString(this byte value)
        {
            return $"0x{value.ToString("X2")}";
        }
        public static string GetHexString(this sbyte value)
        {
            return $"0x{value.ToString("X2")}";
        }
        public static string GetHexString(this ushort value, bool reverse = true)
        {
            string retVal = "0x";
            UInt16T arbVal = value;
            if (reverse)
            {
                for (int i = 0; i < UInt16T.Size; i++)
                {
                    retVal += ((byte)arbVal[UInt16T.Size - 1 - i]).ToString("X2");
                }
            }
            else
            {
                for (int i = 0; i < UInt16T.Size; i++)
                {
                    retVal += ((byte)arbVal[i]).ToString("X2");
                }
            }
            return retVal;
        }
        public static string GetHexString(this short value, bool reverse = true)
        {
            string retVal = "0x";
            Int16T arbVal = value;
            if (reverse)
            {
                for (int i = 0; i < Int16T.Size; i++)
                {
                    retVal += ((byte)arbVal[Int16T.Size - 1 - i]).ToString("X2");
                }
            }
            else
            {
                for (int i = 0; i < Int16T.Size; i++)
                {
                    retVal += ((byte)arbVal[i]).ToString("X2");
                }
            }
            return retVal;
        }
        public static string GetHexString(this uint value, bool reverse = true)
        {
            string retVal = "0x";
            UInt32T arbVal = value;
            if (reverse)
            {
                for (int i = 0; i < UInt32T.Size; i++)
                {
                    retVal += ((byte)arbVal[UInt32T.Size - 1 - i]).ToString("X2");
                }
            }
            else
            {
                for (int i = 0; i < UInt32T.Size; i++)
                {
                    retVal += ((byte)arbVal[i]).ToString("X2");
                }
            }
            return retVal;
        }
        public static string GetHexString(this int value, bool reverse = true)
        {
            string retVal = "0x";
            Int32T arbVal = value;
            if (reverse)
            {
                for (int i = 0; i < Int32T.Size; i++)
                {
                    retVal += ((byte)arbVal[Int32T.Size - 1 - i]).ToString("X2");
                }
            }
            else
            {
                for (int i = 0; i < Int32T.Size; i++)
                {
                    retVal += ((byte)arbVal[i]).ToString("X2");
                }
            }
            return retVal;
        }
        public static string GetHexString(this ulong value, bool reverse = true)
        {
            string retVal = "0x";
            UInt64T arbVal = value;
            if (reverse)
            {
                for (int i = 0; i < UInt64T.Size; i++)
                {
                    retVal += ((byte)arbVal[UInt64T.Size - 1 - i]).ToString("X2");
                }
            }
            else
            {
                for (int i = 0; i < UInt64T.Size; i++)
                {
                    retVal += ((byte)arbVal[i]).ToString("X2");
                }
            }
            return retVal;
        }
        public static string GetHexString(this long value, bool reverse = true)
        {
            string retVal = "0x";
            Int64T arbVal = value;
            if (reverse)
            {
                for (int i = 0; i < Int64T.Size; i++)
                {
                    retVal += ((byte)arbVal[Int64T.Size - 1 - i]).ToString("X2");
                }
            }
            else
            {
                for (int i = 0; i < Int64T.Size; i++)
                {
                    retVal += ((byte)arbVal[i]).ToString("X2");
                }
            }
            return retVal;
        }
        public static string GetHexString(this float value, bool reverse = true)
        {
            string retVal = "0x";
            Real32T arbVal = value;
            if (reverse)
            {
                for (int i = 0; i < Real32T.Size; i++)
                {
                    retVal += ((byte)arbVal[Real32T.Size - 1 - i]).ToString("X2");
                }
            }
            else
            {
                for (int i = 0; i < Real32T.Size; i++)
                {
                    retVal += ((byte)arbVal[i]).ToString("X2");
                }
            }
            return retVal;
        }
        public static string GetHexString(this double value, bool reverse = true)
        {
            string retVal = "0x";
            Real64T arbVal = value;
            if (reverse)
            {
                for (int i = 0; i < Real64T.Size; i++)
                {
                    retVal += ((byte)arbVal[Real64T.Size - 1 - i]).ToString("X2");
                }
            }
            else
            {
                for (int i = 0; i < Real64T.Size; i++)
                {
                    retVal += ((byte)arbVal[i]).ToString("X2");
                }
            }
            return retVal;
        }
    }
#endif
}
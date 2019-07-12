using System;
using System.Runtime.InteropServices;

namespace WulungGCSLibrary
{
    [StructLayout(LayoutKind.Explicit, Size = 1, Pack = 1)]
    public struct UInt8T
    {
        public const int Size = 1;
        public const int BitSize = 8;

        public bool this[int index]
        {
            get
            {
#if DEBUG
                if (index < 0 || index > (BitSize - 1))
                {
                    throw new ArgumentOutOfRangeException(nameof(index), index, $"Parameter \"index\" should be between 0 and {(BitSize - 1)}.");
                }
#endif
                return (_value & (1 << index)) != 0;
            }
            set
            {
#if DEBUG
                if (index < 0 || index > (BitSize - 1))
                {
                    throw new ArgumentOutOfRangeException(nameof(index), index, $"Parameter \"index\" should be between 0 and {(BitSize - 1)}.");
                }
#endif
                if (value)
                {
                    _value |= (byte)(1 << index);
                }
                else
                {
                    _value &= (byte)(~(1 << index));
                }
            }
        }

        private UInt8T(byte value)
        {
            _value = value;
        }

        [FieldOffset(0)]
        private byte _value;

        public void BitArrayCopyTo(ref bool[] destination, int startIndex = 0, int count = BitSize)
        {
            for (int i = 0; i < count; i++)
            {
                destination[i + startIndex] = this[i];
            }
        }

        public static implicit operator byte(UInt8T value)
        {
            return value._value;
        }
        public static implicit operator UInt8T(byte value)
        {
            return new UInt8T(value);
        }
    }

    [StructLayout(LayoutKind.Explicit, Size = 1, Pack = 1)]
    public struct Int8T
    {
        public const int Size = 1;
        public const int BitSize = 8;

        public bool this[int index]
        {
            get
            {
#if DEBUG
                if (index < 0 || index > (BitSize - 1))
                {
                    throw new ArgumentOutOfRangeException(nameof(index), index, $"Parameter \"index\" should be between 0 and {(BitSize - 1)}.");
                }
#endif
                return (_value & (1 << index)) != 0;
            }
            set
            {
#if DEBUG
                if (index < 0 || index > (BitSize - 1))
                {
                    throw new ArgumentOutOfRangeException(nameof(index), index, $"Parameter \"index\" should be between 0 and {(BitSize - 1)}.");
                }
#endif
                if (value)
                {
                    _value |= (sbyte)(1 << index);
                }
                else
                {
                    _value &= (sbyte)(~(1 << index));
                }
            }
        }

        private Int8T(sbyte value)
        {
            _value = value;
        }

        [FieldOffset(0)]
        private sbyte _value;

        public void BitArrayCopyTo(ref bool[] destination, int startIndex = 0, int count = BitSize)
        {
            for (int i = 0; i < count; i++)
            {
                destination[i + startIndex] = this[i];
            }
        }

        public static implicit operator sbyte(Int8T value)
        {
            return value._value;
        }
        public static implicit operator Int8T(sbyte value)
        {
            return new Int8T(value);
        }
    }

    [StructLayout(LayoutKind.Explicit, Size = 2, Pack = 1)]
    public struct UInt16T
    {
        public const int Size = 2;
        public const int BitSize = 16;

        public UInt8T this[int index]
        {
            get
            {
#if DEBUG
                if (index < 0 || index > (Size - 1))
                {
                    throw new ArgumentOutOfRangeException(nameof(index), index, $"Parameter \"index\" should be between 0 and {(Size - 1)}.");
                }
#endif
                switch (index)
                {
                    case 0:
                        return _byte0;
                    case 1:
                        return _byte1;
                    default:
                        return 0;
                }
            }
            set
            {
#if DEBUG
                if (index < 0 || index > (Size - 1))
                {
                    throw new ArgumentOutOfRangeException(nameof(index), index, $"Parameter \"index\" should be between 0 and {(Size - 1)}.");
                }
#endif
                switch (index)
                {
                    case 0:
                        _byte0 = value;
                        return;
                    case 1:
                        _byte1 = value;
                        return;
                    default:
                        break;
                }
            }
        }

        private UInt16T(ushort value)
        {
            _byte0 = _byte1 = 0;
            _value = value;
        }

        [FieldOffset(0)]
        private ushort _value;

        [FieldOffset(0)]
        private UInt8T _byte0;
        [FieldOffset(1)]
        private UInt8T _byte1;

        public void BitArrayCopyTo(ref bool[] destination, int startIndex = 0, int count = BitSize)
        {
            for (int i = 0; i < count; i++)
            {
                destination[i + startIndex] = this[i / UInt8T.BitSize][i % UInt8T.BitSize];
            }
        }
        public void ByteArrayCopyTo(ref byte[] destination, int startIndex = 0, int count = Size)
        {
            for (int i = 0; i < count; i++)
            {
                destination[i + startIndex] = this[i];
            }
        }

        public static implicit operator ushort(UInt16T value)
        {
            return value._value;
        }
        public static implicit operator UInt16T(ushort value)
        {
            return new UInt16T(value);
        }
    }

    [StructLayout(LayoutKind.Explicit, Size = 2, Pack = 1)]
    public struct Int16T
    {
        public const int Size = 2;
        public const int BitSize = 16;

        public UInt8T this[int index]
        {
            get
            {
#if DEBUG
                if (index < 0 || index > (Size - 1))
                {
                    throw new ArgumentOutOfRangeException(nameof(index), index, $"Parameter \"index\" should be between 0 and {(Size - 1)}.");
                }
#endif
                switch (index)
                {
                    case 0:
                        return _byte0;
                    case 1:
                        return _byte1;
                    default:
                        return 0;
                }
            }
            set
            {
#if DEBUG
                if (index < 0 || index > (Size - 1))
                {
                    throw new ArgumentOutOfRangeException(nameof(index), index, $"Parameter \"index\" should be between 0 and {(Size - 1)}.");
                }
#endif
                switch (index)
                {
                    case 0:
                        _byte0 = value;
                        return;
                    case 1:
                        _byte1 = value;
                        return;
                    default:
                        break;
                }
            }
        }

        private Int16T(short value)
        {
            _byte0 = _byte1 = 0;
            _value = value;
        }

        [FieldOffset(0)]
        private short _value;

        [FieldOffset(0)]
        private UInt8T _byte0;
        [FieldOffset(1)]
        private UInt8T _byte1;

        public void BitArrayCopyTo(ref bool[] destination, int startIndex = 0, int count = BitSize)
        {
            for (int i = 0; i < count; i++)
            {
                destination[i + startIndex] = this[i / UInt8T.BitSize][i % UInt8T.BitSize];
            }
        }
        public void ByteArrayCopyTo(ref byte[] destination, int startIndex = 0, int count = Size)
        {
            for (int i = 0; i < count; i++)
            {
                destination[i + startIndex] = this[i];
            }
        }

        public static implicit operator short(Int16T value)
        {
            return value._value;
        }
        public static implicit operator Int16T(short value)
        {
            return new Int16T(value);
        }
    }

    [StructLayout(LayoutKind.Explicit, Size = 4, Pack = 1)]
    public struct UInt32T
    {
        public const int Size = 4;
        public const int BitSize = 32;

        public UInt8T this[int index]
        {
            get
            {
#if DEBUG
                if (index < 0 || index > (Size - 1))
                {
                    throw new ArgumentOutOfRangeException(nameof(index), index, $"Parameter \"index\" should be between 0 and {(Size - 1)}.");
                }
#endif
                switch (index)
                {
                    case 0:
                        return _byte0;
                    case 1:
                        return _byte1;
                    case 2:
                        return _byte2;
                    case 3:
                        return _byte3;
                    default:
                        return 0;
                }
            }
            set
            {
#if DEBUG
                if (index < 0 || index > (Size - 1))
                {
                    throw new ArgumentOutOfRangeException(nameof(index), index, $"Parameter \"index\" should be between 0 and {(Size - 1)}.");
                }
#endif
                switch (index)
                {
                    case 0:
                        _byte0 = value;
                        return;
                    case 1:
                        _byte1 = value;
                        return;
                    case 2:
                        _byte2 = value;
                        return;
                    case 3:
                        _byte3 = value;
                        return;
                    default:
                        break;
                }
            }
        }

        private UInt32T(uint value)
        {
            _byte0 = _byte1 = _byte2 = _byte3 = 0;
            _value = value;
        }

        [FieldOffset(0)]
        private uint _value;

        [FieldOffset(0)]
        private UInt8T _byte0;
        [FieldOffset(1)]
        private UInt8T _byte1;
        [FieldOffset(2)]
        private UInt8T _byte2;
        [FieldOffset(3)]
        private UInt8T _byte3;

        public void BitArrayCopyTo(ref bool[] destination, int startIndex = 0, int count = BitSize)
        {
            for (int i = 0; i < count; i++)
            {
                destination[i + startIndex] = this[i / UInt8T.BitSize][i % UInt8T.BitSize];
            }
        }
        public void ByteArrayCopyTo(ref byte[] destination, int startIndex = 0, int count = Size)
        {
            for (int i = 0; i < count; i++)
            {
                destination[i + startIndex] = this[i];
            }
        }

        public static implicit operator uint(UInt32T value)
        {
            return value._value;
        }
        public static implicit operator UInt32T(uint value)
        {
            return new UInt32T(value);
        }
    }

    [StructLayout(LayoutKind.Explicit, Size = 4, Pack = 1)]
    public struct Int32T
    {
        public const int Size = 4;
        public const int BitSize = 32;

        public UInt8T this[int index]
        {
            get
            {
#if DEBUG
                if (index < 0 || index > (Size - 1))
                {
                    throw new ArgumentOutOfRangeException(nameof(index), index, $"Parameter \"index\" should be between 0 and {(Size - 1)}.");
                }
#endif
                switch (index)
                {
                    case 0:
                        return _byte0;
                    case 1:
                        return _byte1;
                    case 2:
                        return _byte2;
                    case 3:
                        return _byte3;
                    default:
                        return 0;
                }
            }
            set
            {
#if DEBUG
                if (index < 0 || index > (Size - 1))
                {
                    throw new ArgumentOutOfRangeException(nameof(index), index, $"Parameter \"index\" should be between 0 and {(Size - 1)}.");
                }
#endif
                switch (index)
                {
                    case 0:
                        _byte0 = value;
                        return;
                    case 1:
                        _byte1 = value;
                        return;
                    case 2:
                        _byte2 = value;
                        return;
                    case 3:
                        _byte3 = value;
                        return;
                    default:
                        break;
                }
            }
        }

        private Int32T(int value)
        {
            _byte0 = _byte1 = _byte2 = _byte3 = 0;
            _value = value;
        }

        [FieldOffset(0)]
        private int _value;

        [FieldOffset(0)]
        private UInt8T _byte0;
        [FieldOffset(1)]
        private UInt8T _byte1;
        [FieldOffset(2)]
        private UInt8T _byte2;
        [FieldOffset(3)]
        private UInt8T _byte3;

        public void BitArrayCopyTo(ref bool[] destination, int startIndex = 0, int count = BitSize)
        {
            for (int i = 0; i < count; i++)
            {
                destination[i + startIndex] = this[i / UInt8T.BitSize][i % UInt8T.BitSize];
            }
        }
        public void ByteArrayCopyTo(ref byte[] destination, int startIndex = 0, int count = Size)
        {
            for (int i = 0; i < count; i++)
            {
                destination[i + startIndex] = this[i];
            }
        }

        public static implicit operator int(Int32T value)
        {
            return value._value;
        }
        public static implicit operator Int32T(int value)
        {
            return new Int32T(value);
        }
    }

    [StructLayout(LayoutKind.Explicit, Size = 8, Pack = 1)]
    public struct UInt64T
    {
        public const int Size = 8;
        public const int BitSize = 64;

        public UInt8T this[int index]
        {
            get
            {
#if DEBUG
                if (index < 0 || index > (Size - 1))
                {
                    throw new ArgumentOutOfRangeException(nameof(index), index, $"Parameter \"index\" should be between 0 and {(Size - 1)}.");
                }
#endif
                switch (index)
                {
                    case 0:
                        return _byte0;
                    case 1:
                        return _byte1;
                    case 2:
                        return _byte2;
                    case 3:
                        return _byte3;
                    case 4:
                        return _byte4;
                    case 5:
                        return _byte5;
                    case 6:
                        return _byte6;
                    case 7:
                        return _byte7;
                    default:
                        return 0;
                }
            }
            set
            {
#if DEBUG
                if (index < 0 || index > (Size - 1))
                {
                    throw new ArgumentOutOfRangeException(nameof(index), index, $"Parameter \"index\" should be between 0 and {(Size - 1)}.");
                }
#endif
                switch (index)
                {
                    case 0:
                        _byte0 = value;
                        return;
                    case 1:
                        _byte1 = value;
                        return;
                    case 2:
                        _byte2 = value;
                        return;
                    case 3:
                        _byte3 = value;
                        return;
                    case 4:
                        _byte4 = value;
                        return;
                    case 5:
                        _byte5 = value;
                        return;
                    case 6:
                        _byte6 = value;
                        return;
                    case 7:
                        _byte7 = value;
                        return;
                    default:
                        break;
                }
            }
        }

        private UInt64T(ulong value)
        {
            _byte0 = _byte1 = _byte2 = _byte3 = _byte4 = _byte5 = _byte6 = _byte7 = 0;
            _value = value;
        }

        [FieldOffset(0)]
        private ulong _value;

        [FieldOffset(0)]
        private UInt8T _byte0;
        [FieldOffset(1)]
        private UInt8T _byte1;
        [FieldOffset(2)]
        private UInt8T _byte2;
        [FieldOffset(3)]
        private UInt8T _byte3;
        [FieldOffset(4)]
        private UInt8T _byte4;
        [FieldOffset(5)]
        private UInt8T _byte5;
        [FieldOffset(6)]
        private UInt8T _byte6;
        [FieldOffset(7)]
        private UInt8T _byte7;

        public void BitArrayCopyTo(ref bool[] destination, int startIndex = 0, int count = BitSize)
        {
            for (int i = 0; i < count; i++)
            {
                destination[i + startIndex] = this[i / UInt8T.BitSize][i % UInt8T.BitSize];
            }
        }
        public void ByteArrayCopyTo(ref byte[] destination, int startIndex = 0, int count = Size)
        {
            for (int i = 0; i < count; i++)
            {
                destination[i + startIndex] = this[i];
            }
        }

        public static implicit operator ulong(UInt64T value)
        {
            return value._value;
        }
        public static implicit operator UInt64T(ulong value)
        {
            return new UInt64T(value);
        }
    }

    [StructLayout(LayoutKind.Explicit, Size = 8, Pack = 1)]
    public struct Int64T
    {
        public const int Size = 8;
        public const int BitSize = 64;

        public UInt8T this[int index]
        {
            get
            {
#if DEBUG
                if (index < 0 || index > (Size - 1))
                {
                    throw new ArgumentOutOfRangeException(nameof(index), index, $"Parameter \"index\" should be between 0 and {(Size - 1)}.");
                }
#endif
                switch (index)
                {
                    case 0:
                        return _byte0;
                    case 1:
                        return _byte1;
                    case 2:
                        return _byte2;
                    case 3:
                        return _byte3;
                    case 4:
                        return _byte4;
                    case 5:
                        return _byte5;
                    case 6:
                        return _byte6;
                    case 7:
                        return _byte7;
                    default:
                        return 0;
                }
            }
            set
            {
#if DEBUG
                if (index < 0 || index > (Size - 1))
                {
                    throw new ArgumentOutOfRangeException(nameof(index), index, $"Parameter \"index\" should be between 0 and {(Size - 1)}.");
                }
#endif
                switch (index)
                {
                    case 0:
                        _byte0 = value;
                        return;
                    case 1:
                        _byte1 = value;
                        return;
                    case 2:
                        _byte2 = value;
                        return;
                    case 3:
                        _byte3 = value;
                        return;
                    case 4:
                        _byte4 = value;
                        return;
                    case 5:
                        _byte5 = value;
                        return;
                    case 6:
                        _byte6 = value;
                        return;
                    case 7:
                        _byte7 = value;
                        return;
                    default:
                        break;
                }
            }
        }

        private Int64T(long value)
        {
            _byte0 = _byte1 = _byte2 = _byte3 = _byte4 = _byte5 = _byte6 = _byte7 = 0;
            _value = value;
        }

        [FieldOffset(0)]
        private long _value;

        [FieldOffset(0)]
        private UInt8T _byte0;
        [FieldOffset(1)]
        private UInt8T _byte1;
        [FieldOffset(2)]
        private UInt8T _byte2;
        [FieldOffset(3)]
        private UInt8T _byte3;
        [FieldOffset(4)]
        private UInt8T _byte4;
        [FieldOffset(5)]
        private UInt8T _byte5;
        [FieldOffset(6)]
        private UInt8T _byte6;
        [FieldOffset(7)]
        private UInt8T _byte7;

        public void BitArrayCopyTo(ref bool[] destination, int startIndex = 0, int count = BitSize)
        {
            for (int i = 0; i < count; i++)
            {
                destination[i + startIndex] = this[i / UInt8T.BitSize][i % UInt8T.BitSize];
            }
        }
        public void ByteArrayCopyTo(ref byte[] destination, int startIndex = 0, int count = Size)
        {
            for (int i = 0; i < count; i++)
            {
                destination[i + startIndex] = this[i];
            }
        }

        public static implicit operator long(Int64T value)
        {
            return value._value;
        }
        public static implicit operator Int64T(long value)
        {
            return new Int64T(value);
        }
    }

    [StructLayout(LayoutKind.Explicit, Size = 4, Pack = 1)]
    public struct Real32T
    {
        public const int Size = 4;
        public const int BitSize = 32;

        public UInt8T this[int index]
        {
            get
            {
#if DEBUG
                if (index < 0 || index > (Size - 1))
                {
                    throw new ArgumentOutOfRangeException(nameof(index), index, $"Parameter \"index\" should be between 0 and {(Size - 1)}.");
                }
#endif
                switch (index)
                {
                    case 0:
                        return _byte0;
                    case 1:
                        return _byte1;
                    case 2:
                        return _byte2;
                    case 3:
                        return _byte3;
                    default:
                        return 0;
                }
            }
            set
            {
#if DEBUG
                if (index < 0 || index > (Size - 1))
                {
                    throw new ArgumentOutOfRangeException(nameof(index), index, $"Parameter \"index\" should be between 0 and {(Size - 1)}.");
                }
#endif
                switch (index)
                {
                    case 0:
                        _byte0 = value;
                        return;
                    case 1:
                        _byte1 = value;
                        return;
                    case 2:
                        _byte2 = value;
                        return;
                    case 3:
                        _byte3 = value;
                        return;
                    default:
                        break;
                }
            }
        }

        private Real32T(float value)
        {
            _byte0 = _byte1 = _byte2 = _byte3 = 0;
            _value = value;
        }

        [FieldOffset(0)]
        private float _value;

        [FieldOffset(0)]
        private UInt8T _byte0;
        [FieldOffset(1)]
        private UInt8T _byte1;
        [FieldOffset(2)]
        private UInt8T _byte2;
        [FieldOffset(3)]
        private UInt8T _byte3;

        public void BitArrayCopyTo(ref bool[] destination, int startIndex = 0, int count = BitSize)
        {
            for (int i = 0; i < count; i++)
            {
                destination[i + startIndex] = this[i / UInt8T.BitSize][i % UInt8T.BitSize];
            }
        }
        public void ByteArrayCopyTo(ref byte[] destination, int startIndex = 0, int count = Size)
        {
            for (int i = 0; i < count; i++)
            {
                destination[i + startIndex] = this[i];
            }
        }

        public static implicit operator float(Real32T value)
        {
            return value._value;
        }
        public static implicit operator Real32T(float value)
        {
            return new Real32T(value);
        }
    }

    [StructLayout(LayoutKind.Explicit, Size = 8, Pack = 1)]
    public struct Real64T
    {
        public const int Size = 8;
        public const int BitSize = 64;

        public UInt8T this[int index]
        {
            get
            {
#if DEBUG
                if (index < 0 || index > (Size - 1))
                {
                    throw new ArgumentOutOfRangeException(nameof(index), index, $"Parameter \"index\" should be between 0 and {(Size - 1)}.");
                }
#endif
                switch (index)
                {
                    case 0:
                        return _byte0;
                    case 1:
                        return _byte1;
                    case 2:
                        return _byte2;
                    case 3:
                        return _byte3;
                    case 4:
                        return _byte4;
                    case 5:
                        return _byte5;
                    case 6:
                        return _byte6;
                    case 7:
                        return _byte7;
                    default:
                        return 0;
                }
            }
            set
            {
#if DEBUG
                if (index < 0 || index > (Size - 1))
                {
                    throw new ArgumentOutOfRangeException(nameof(index), index, $"Parameter \"index\" should be between 0 and {(Size - 1)}.");
                }
#endif
                switch (index)
                {
                    case 0:
                        _byte0 = value;
                        return;
                    case 1:
                        _byte1 = value;
                        return;
                    case 2:
                        _byte2 = value;
                        return;
                    case 3:
                        _byte3 = value;
                        return;
                    case 4:
                        _byte4 = value;
                        return;
                    case 5:
                        _byte5 = value;
                        return;
                    case 6:
                        _byte6 = value;
                        return;
                    case 7:
                        _byte7 = value;
                        return;
                    default:
                        break;
                }
            }
        }

        private Real64T(double value)
        {
            _byte0 = _byte1 = _byte2 = _byte3 = _byte4 = _byte5 = _byte6 = _byte7 = 0;
            _value = value;
        }

        [FieldOffset(0)]
        private double _value;

        [FieldOffset(0)]
        private UInt8T _byte0;
        [FieldOffset(1)]
        private UInt8T _byte1;
        [FieldOffset(2)]
        private UInt8T _byte2;
        [FieldOffset(3)]
        private UInt8T _byte3;
        [FieldOffset(4)]
        private UInt8T _byte4;
        [FieldOffset(5)]
        private UInt8T _byte5;
        [FieldOffset(6)]
        private UInt8T _byte6;
        [FieldOffset(7)]
        private UInt8T _byte7;

        public void BitArrayCopyTo(ref bool[] destination, int startIndex = 0, int count = BitSize)
        {
            for (int i = 0; i < count; i++)
            {
                destination[i + startIndex] = this[i / UInt8T.BitSize][i % UInt8T.BitSize];
            }
        }
        public void ByteArrayCopyTo(ref byte[] destination, int startIndex = 0, int count = Size)
        {
            for (int i = 0; i < count; i++)
            {
                destination[i + startIndex] = this[i];
            }
        }

        public static implicit operator double(Real64T value)
        {
            return value._value;
        }
        public static implicit operator Real64T(double value)
        {
            return new Real64T(value);
        }
    }
}
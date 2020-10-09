using System;
namespace DataBinding.PropertyProviders
{

    //========================================bool========================================

    public class BooleanBooleanProvider : PropertyBindingProvider<bool, bool>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToBoolean(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToBoolean(targetGetter()));
        }
    }

    public class CharBooleanProvider : PropertyBindingProvider<char, bool>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToBoolean(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToChar(targetGetter()));
        }
    }

    public class SByteBooleanProvider : PropertyBindingProvider<sbyte, bool>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToBoolean(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToSByte(targetGetter()));
        }
    }

    public class ByteBooleanProvider : PropertyBindingProvider<byte, bool>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToBoolean(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToByte(targetGetter()));
        }
    }

    public class Int16BooleanProvider : PropertyBindingProvider<short, bool>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToBoolean(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToInt16(targetGetter()));
        }
    }

    public class UInt16BooleanProvider : PropertyBindingProvider<ushort, bool>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToBoolean(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToUInt16(targetGetter()));
        }
    }

    public class Int32BooleanProvider : PropertyBindingProvider<int, bool>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToBoolean(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToInt32(targetGetter()));
        }
    }

    public class UInt32BooleanProvider : PropertyBindingProvider<uint, bool>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToBoolean(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToUInt32(targetGetter()));
        }
    }

    public class Int64BooleanProvider : PropertyBindingProvider<long, bool>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToBoolean(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToInt64(targetGetter()));
        }
    }

    public class UInt64BooleanProvider : PropertyBindingProvider<ulong, bool>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToBoolean(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToUInt64(targetGetter()));
        }
    }

    public class SingleBooleanProvider : PropertyBindingProvider<float, bool>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToBoolean(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToSingle(targetGetter()));
        }
    }

    public class DoubleBooleanProvider : PropertyBindingProvider<double, bool>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToBoolean(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToDouble(targetGetter()));
        }
    }

    public class DecimalBooleanProvider : PropertyBindingProvider<Decimal, bool>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToBoolean(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToDecimal(targetGetter()));
        }
    }

    public class DateTimeBooleanProvider : PropertyBindingProvider<DateTime, bool>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToBoolean(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToDateTime(targetGetter()));
        }
    }

    public class StringBooleanProvider : PropertyBindingProvider<string, bool>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToBoolean(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToString(targetGetter()));
        }
    }

    //========================================char========================================

    public class BooleanCharProvider : PropertyBindingProvider<bool, char>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToChar(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToBoolean(targetGetter()));
        }
    }

    public class CharCharProvider : PropertyBindingProvider<char, char>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToChar(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToChar(targetGetter()));
        }
    }

    public class SByteCharProvider : PropertyBindingProvider<sbyte, char>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToChar(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToSByte(targetGetter()));
        }
    }

    public class ByteCharProvider : PropertyBindingProvider<byte, char>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToChar(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToByte(targetGetter()));
        }
    }

    public class Int16CharProvider : PropertyBindingProvider<short, char>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToChar(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToInt16(targetGetter()));
        }
    }

    public class UInt16CharProvider : PropertyBindingProvider<ushort, char>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToChar(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToUInt16(targetGetter()));
        }
    }

    public class Int32CharProvider : PropertyBindingProvider<int, char>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToChar(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToInt32(targetGetter()));
        }
    }

    public class UInt32CharProvider : PropertyBindingProvider<uint, char>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToChar(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToUInt32(targetGetter()));
        }
    }

    public class Int64CharProvider : PropertyBindingProvider<long, char>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToChar(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToInt64(targetGetter()));
        }
    }

    public class UInt64CharProvider : PropertyBindingProvider<ulong, char>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToChar(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToUInt64(targetGetter()));
        }
    }

    public class SingleCharProvider : PropertyBindingProvider<float, char>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToChar(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToSingle(targetGetter()));
        }
    }

    public class DoubleCharProvider : PropertyBindingProvider<double, char>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToChar(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToDouble(targetGetter()));
        }
    }

    public class DecimalCharProvider : PropertyBindingProvider<Decimal, char>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToChar(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToDecimal(targetGetter()));
        }
    }

    public class DateTimeCharProvider : PropertyBindingProvider<DateTime, char>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToChar(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToDateTime(targetGetter()));
        }
    }

    public class StringCharProvider : PropertyBindingProvider<string, char>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToChar(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToString(targetGetter()));
        }
    }

    //========================================sbyte========================================

    public class BooleanSByteProvider : PropertyBindingProvider<bool, sbyte>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToSByte(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToBoolean(targetGetter()));
        }
    }

    public class CharSByteProvider : PropertyBindingProvider<char, sbyte>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToSByte(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToChar(targetGetter()));
        }
    }

    public class SByteSByteProvider : PropertyBindingProvider<sbyte, sbyte>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToSByte(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToSByte(targetGetter()));
        }
    }

    public class ByteSByteProvider : PropertyBindingProvider<byte, sbyte>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToSByte(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToByte(targetGetter()));
        }
    }

    public class Int16SByteProvider : PropertyBindingProvider<short, sbyte>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToSByte(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToInt16(targetGetter()));
        }
    }

    public class UInt16SByteProvider : PropertyBindingProvider<ushort, sbyte>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToSByte(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToUInt16(targetGetter()));
        }
    }

    public class Int32SByteProvider : PropertyBindingProvider<int, sbyte>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToSByte(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToInt32(targetGetter()));
        }
    }

    public class UInt32SByteProvider : PropertyBindingProvider<uint, sbyte>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToSByte(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToUInt32(targetGetter()));
        }
    }

    public class Int64SByteProvider : PropertyBindingProvider<long, sbyte>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToSByte(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToInt64(targetGetter()));
        }
    }

    public class UInt64SByteProvider : PropertyBindingProvider<ulong, sbyte>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToSByte(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToUInt64(targetGetter()));
        }
    }

    public class SingleSByteProvider : PropertyBindingProvider<float, sbyte>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToSByte(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToSingle(targetGetter()));
        }
    }

    public class DoubleSByteProvider : PropertyBindingProvider<double, sbyte>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToSByte(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToDouble(targetGetter()));
        }
    }

    public class DecimalSByteProvider : PropertyBindingProvider<Decimal, sbyte>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToSByte(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToDecimal(targetGetter()));
        }
    }

    public class DateTimeSByteProvider : PropertyBindingProvider<DateTime, sbyte>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToSByte(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToDateTime(targetGetter()));
        }
    }

    public class StringSByteProvider : PropertyBindingProvider<string, sbyte>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToSByte(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToString(targetGetter()));
        }
    }

    //========================================byte========================================

    public class BooleanByteProvider : PropertyBindingProvider<bool, byte>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToByte(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToBoolean(targetGetter()));
        }
    }

    public class CharByteProvider : PropertyBindingProvider<char, byte>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToByte(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToChar(targetGetter()));
        }
    }

    public class SByteByteProvider : PropertyBindingProvider<sbyte, byte>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToByte(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToSByte(targetGetter()));
        }
    }

    public class ByteByteProvider : PropertyBindingProvider<byte, byte>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToByte(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToByte(targetGetter()));
        }
    }

    public class Int16ByteProvider : PropertyBindingProvider<short, byte>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToByte(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToInt16(targetGetter()));
        }
    }

    public class UInt16ByteProvider : PropertyBindingProvider<ushort, byte>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToByte(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToUInt16(targetGetter()));
        }
    }

    public class Int32ByteProvider : PropertyBindingProvider<int, byte>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToByte(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToInt32(targetGetter()));
        }
    }

    public class UInt32ByteProvider : PropertyBindingProvider<uint, byte>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToByte(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToUInt32(targetGetter()));
        }
    }

    public class Int64ByteProvider : PropertyBindingProvider<long, byte>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToByte(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToInt64(targetGetter()));
        }
    }

    public class UInt64ByteProvider : PropertyBindingProvider<ulong, byte>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToByte(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToUInt64(targetGetter()));
        }
    }

    public class SingleByteProvider : PropertyBindingProvider<float, byte>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToByte(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToSingle(targetGetter()));
        }
    }

    public class DoubleByteProvider : PropertyBindingProvider<double, byte>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToByte(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToDouble(targetGetter()));
        }
    }

    public class DecimalByteProvider : PropertyBindingProvider<Decimal, byte>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToByte(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToDecimal(targetGetter()));
        }
    }

    public class DateTimeByteProvider : PropertyBindingProvider<DateTime, byte>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToByte(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToDateTime(targetGetter()));
        }
    }

    public class StringByteProvider : PropertyBindingProvider<string, byte>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToByte(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToString(targetGetter()));
        }
    }

    //========================================short========================================

    public class BooleanInt16Provider : PropertyBindingProvider<bool, short>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToInt16(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToBoolean(targetGetter()));
        }
    }

    public class CharInt16Provider : PropertyBindingProvider<char, short>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToInt16(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToChar(targetGetter()));
        }
    }

    public class SByteInt16Provider : PropertyBindingProvider<sbyte, short>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToInt16(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToSByte(targetGetter()));
        }
    }

    public class ByteInt16Provider : PropertyBindingProvider<byte, short>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToInt16(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToByte(targetGetter()));
        }
    }

    public class Int16Int16Provider : PropertyBindingProvider<short, short>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToInt16(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToInt16(targetGetter()));
        }
    }

    public class UInt16Int16Provider : PropertyBindingProvider<ushort, short>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToInt16(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToUInt16(targetGetter()));
        }
    }

    public class Int32Int16Provider : PropertyBindingProvider<int, short>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToInt16(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToInt32(targetGetter()));
        }
    }

    public class UInt32Int16Provider : PropertyBindingProvider<uint, short>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToInt16(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToUInt32(targetGetter()));
        }
    }

    public class Int64Int16Provider : PropertyBindingProvider<long, short>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToInt16(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToInt64(targetGetter()));
        }
    }

    public class UInt64Int16Provider : PropertyBindingProvider<ulong, short>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToInt16(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToUInt64(targetGetter()));
        }
    }

    public class SingleInt16Provider : PropertyBindingProvider<float, short>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToInt16(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToSingle(targetGetter()));
        }
    }

    public class DoubleInt16Provider : PropertyBindingProvider<double, short>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToInt16(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToDouble(targetGetter()));
        }
    }

    public class DecimalInt16Provider : PropertyBindingProvider<Decimal, short>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToInt16(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToDecimal(targetGetter()));
        }
    }

    public class DateTimeInt16Provider : PropertyBindingProvider<DateTime, short>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToInt16(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToDateTime(targetGetter()));
        }
    }

    public class StringInt16Provider : PropertyBindingProvider<string, short>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToInt16(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToString(targetGetter()));
        }
    }

    //========================================ushort========================================

    public class BooleanUInt16Provider : PropertyBindingProvider<bool, ushort>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToUInt16(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToBoolean(targetGetter()));
        }
    }

    public class CharUInt16Provider : PropertyBindingProvider<char, ushort>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToUInt16(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToChar(targetGetter()));
        }
    }

    public class SByteUInt16Provider : PropertyBindingProvider<sbyte, ushort>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToUInt16(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToSByte(targetGetter()));
        }
    }

    public class ByteUInt16Provider : PropertyBindingProvider<byte, ushort>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToUInt16(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToByte(targetGetter()));
        }
    }

    public class Int16UInt16Provider : PropertyBindingProvider<short, ushort>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToUInt16(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToInt16(targetGetter()));
        }
    }

    public class UInt16UInt16Provider : PropertyBindingProvider<ushort, ushort>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToUInt16(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToUInt16(targetGetter()));
        }
    }

    public class Int32UInt16Provider : PropertyBindingProvider<int, ushort>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToUInt16(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToInt32(targetGetter()));
        }
    }

    public class UInt32UInt16Provider : PropertyBindingProvider<uint, ushort>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToUInt16(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToUInt32(targetGetter()));
        }
    }

    public class Int64UInt16Provider : PropertyBindingProvider<long, ushort>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToUInt16(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToInt64(targetGetter()));
        }
    }

    public class UInt64UInt16Provider : PropertyBindingProvider<ulong, ushort>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToUInt16(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToUInt64(targetGetter()));
        }
    }

    public class SingleUInt16Provider : PropertyBindingProvider<float, ushort>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToUInt16(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToSingle(targetGetter()));
        }
    }

    public class DoubleUInt16Provider : PropertyBindingProvider<double, ushort>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToUInt16(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToDouble(targetGetter()));
        }
    }

    public class DecimalUInt16Provider : PropertyBindingProvider<Decimal, ushort>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToUInt16(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToDecimal(targetGetter()));
        }
    }

    public class DateTimeUInt16Provider : PropertyBindingProvider<DateTime, ushort>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToUInt16(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToDateTime(targetGetter()));
        }
    }

    public class StringUInt16Provider : PropertyBindingProvider<string, ushort>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToUInt16(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToString(targetGetter()));
        }
    }

    //========================================int========================================

    public class BooleanInt32Provider : PropertyBindingProvider<bool, int>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToInt32(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToBoolean(targetGetter()));
        }
    }

    public class CharInt32Provider : PropertyBindingProvider<char, int>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToInt32(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToChar(targetGetter()));
        }
    }

    public class SByteInt32Provider : PropertyBindingProvider<sbyte, int>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToInt32(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToSByte(targetGetter()));
        }
    }

    public class ByteInt32Provider : PropertyBindingProvider<byte, int>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToInt32(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToByte(targetGetter()));
        }
    }

    public class Int16Int32Provider : PropertyBindingProvider<short, int>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToInt32(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToInt16(targetGetter()));
        }
    }

    public class UInt16Int32Provider : PropertyBindingProvider<ushort, int>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToInt32(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToUInt16(targetGetter()));
        }
    }

    public class Int32Int32Provider : PropertyBindingProvider<int, int>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToInt32(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToInt32(targetGetter()));
        }
    }

    public class UInt32Int32Provider : PropertyBindingProvider<uint, int>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToInt32(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToUInt32(targetGetter()));
        }
    }

    public class Int64Int32Provider : PropertyBindingProvider<long, int>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToInt32(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToInt64(targetGetter()));
        }
    }

    public class UInt64Int32Provider : PropertyBindingProvider<ulong, int>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToInt32(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToUInt64(targetGetter()));
        }
    }

    public class SingleInt32Provider : PropertyBindingProvider<float, int>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToInt32(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToSingle(targetGetter()));
        }
    }

    public class DoubleInt32Provider : PropertyBindingProvider<double, int>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToInt32(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToDouble(targetGetter()));
        }
    }

    public class DecimalInt32Provider : PropertyBindingProvider<Decimal, int>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToInt32(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToDecimal(targetGetter()));
        }
    }

    public class DateTimeInt32Provider : PropertyBindingProvider<DateTime, int>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToInt32(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToDateTime(targetGetter()));
        }
    }

    public class StringInt32Provider : PropertyBindingProvider<string, int>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToInt32(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToString(targetGetter()));
        }
    }

    //========================================uint========================================

    public class BooleanUInt32Provider : PropertyBindingProvider<bool, uint>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToUInt32(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToBoolean(targetGetter()));
        }
    }

    public class CharUInt32Provider : PropertyBindingProvider<char, uint>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToUInt32(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToChar(targetGetter()));
        }
    }

    public class SByteUInt32Provider : PropertyBindingProvider<sbyte, uint>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToUInt32(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToSByte(targetGetter()));
        }
    }

    public class ByteUInt32Provider : PropertyBindingProvider<byte, uint>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToUInt32(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToByte(targetGetter()));
        }
    }

    public class Int16UInt32Provider : PropertyBindingProvider<short, uint>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToUInt32(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToInt16(targetGetter()));
        }
    }

    public class UInt16UInt32Provider : PropertyBindingProvider<ushort, uint>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToUInt32(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToUInt16(targetGetter()));
        }
    }

    public class Int32UInt32Provider : PropertyBindingProvider<int, uint>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToUInt32(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToInt32(targetGetter()));
        }
    }

    public class UInt32UInt32Provider : PropertyBindingProvider<uint, uint>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToUInt32(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToUInt32(targetGetter()));
        }
    }

    public class Int64UInt32Provider : PropertyBindingProvider<long, uint>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToUInt32(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToInt64(targetGetter()));
        }
    }

    public class UInt64UInt32Provider : PropertyBindingProvider<ulong, uint>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToUInt32(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToUInt64(targetGetter()));
        }
    }

    public class SingleUInt32Provider : PropertyBindingProvider<float, uint>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToUInt32(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToSingle(targetGetter()));
        }
    }

    public class DoubleUInt32Provider : PropertyBindingProvider<double, uint>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToUInt32(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToDouble(targetGetter()));
        }
    }

    public class DecimalUInt32Provider : PropertyBindingProvider<Decimal, uint>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToUInt32(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToDecimal(targetGetter()));
        }
    }

    public class DateTimeUInt32Provider : PropertyBindingProvider<DateTime, uint>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToUInt32(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToDateTime(targetGetter()));
        }
    }

    public class StringUInt32Provider : PropertyBindingProvider<string, uint>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToUInt32(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToString(targetGetter()));
        }
    }

    //========================================long========================================

    public class BooleanInt64Provider : PropertyBindingProvider<bool, long>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToInt64(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToBoolean(targetGetter()));
        }
    }

    public class CharInt64Provider : PropertyBindingProvider<char, long>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToInt64(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToChar(targetGetter()));
        }
    }

    public class SByteInt64Provider : PropertyBindingProvider<sbyte, long>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToInt64(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToSByte(targetGetter()));
        }
    }

    public class ByteInt64Provider : PropertyBindingProvider<byte, long>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToInt64(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToByte(targetGetter()));
        }
    }

    public class Int16Int64Provider : PropertyBindingProvider<short, long>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToInt64(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToInt16(targetGetter()));
        }
    }

    public class UInt16Int64Provider : PropertyBindingProvider<ushort, long>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToInt64(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToUInt16(targetGetter()));
        }
    }

    public class Int32Int64Provider : PropertyBindingProvider<int, long>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToInt64(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToInt32(targetGetter()));
        }
    }

    public class UInt32Int64Provider : PropertyBindingProvider<uint, long>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToInt64(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToUInt32(targetGetter()));
        }
    }

    public class Int64Int64Provider : PropertyBindingProvider<long, long>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToInt64(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToInt64(targetGetter()));
        }
    }

    public class UInt64Int64Provider : PropertyBindingProvider<ulong, long>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToInt64(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToUInt64(targetGetter()));
        }
    }

    public class SingleInt64Provider : PropertyBindingProvider<float, long>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToInt64(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToSingle(targetGetter()));
        }
    }

    public class DoubleInt64Provider : PropertyBindingProvider<double, long>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToInt64(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToDouble(targetGetter()));
        }
    }

    public class DecimalInt64Provider : PropertyBindingProvider<Decimal, long>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToInt64(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToDecimal(targetGetter()));
        }
    }

    public class DateTimeInt64Provider : PropertyBindingProvider<DateTime, long>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToInt64(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToDateTime(targetGetter()));
        }
    }

    public class StringInt64Provider : PropertyBindingProvider<string, long>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToInt64(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToString(targetGetter()));
        }
    }

    //========================================ulong========================================

    public class BooleanUInt64Provider : PropertyBindingProvider<bool, ulong>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToUInt64(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToBoolean(targetGetter()));
        }
    }

    public class CharUInt64Provider : PropertyBindingProvider<char, ulong>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToUInt64(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToChar(targetGetter()));
        }
    }

    public class SByteUInt64Provider : PropertyBindingProvider<sbyte, ulong>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToUInt64(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToSByte(targetGetter()));
        }
    }

    public class ByteUInt64Provider : PropertyBindingProvider<byte, ulong>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToUInt64(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToByte(targetGetter()));
        }
    }

    public class Int16UInt64Provider : PropertyBindingProvider<short, ulong>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToUInt64(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToInt16(targetGetter()));
        }
    }

    public class UInt16UInt64Provider : PropertyBindingProvider<ushort, ulong>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToUInt64(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToUInt16(targetGetter()));
        }
    }

    public class Int32UInt64Provider : PropertyBindingProvider<int, ulong>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToUInt64(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToInt32(targetGetter()));
        }
    }

    public class UInt32UInt64Provider : PropertyBindingProvider<uint, ulong>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToUInt64(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToUInt32(targetGetter()));
        }
    }

    public class Int64UInt64Provider : PropertyBindingProvider<long, ulong>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToUInt64(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToInt64(targetGetter()));
        }
    }

    public class UInt64UInt64Provider : PropertyBindingProvider<ulong, ulong>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToUInt64(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToUInt64(targetGetter()));
        }
    }

    public class SingleUInt64Provider : PropertyBindingProvider<float, ulong>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToUInt64(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToSingle(targetGetter()));
        }
    }

    public class DoubleUInt64Provider : PropertyBindingProvider<double, ulong>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToUInt64(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToDouble(targetGetter()));
        }
    }

    public class DecimalUInt64Provider : PropertyBindingProvider<Decimal, ulong>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToUInt64(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToDecimal(targetGetter()));
        }
    }

    public class DateTimeUInt64Provider : PropertyBindingProvider<DateTime, ulong>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToUInt64(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToDateTime(targetGetter()));
        }
    }

    public class StringUInt64Provider : PropertyBindingProvider<string, ulong>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToUInt64(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToString(targetGetter()));
        }
    }

    //========================================float========================================

    public class BooleanSingleProvider : PropertyBindingProvider<bool, float>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToSingle(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToBoolean(targetGetter()));
        }
    }

    public class CharSingleProvider : PropertyBindingProvider<char, float>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToSingle(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToChar(targetGetter()));
        }
    }

    public class SByteSingleProvider : PropertyBindingProvider<sbyte, float>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToSingle(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToSByte(targetGetter()));
        }
    }

    public class ByteSingleProvider : PropertyBindingProvider<byte, float>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToSingle(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToByte(targetGetter()));
        }
    }

    public class Int16SingleProvider : PropertyBindingProvider<short, float>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToSingle(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToInt16(targetGetter()));
        }
    }

    public class UInt16SingleProvider : PropertyBindingProvider<ushort, float>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToSingle(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToUInt16(targetGetter()));
        }
    }

    public class Int32SingleProvider : PropertyBindingProvider<int, float>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToSingle(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToInt32(targetGetter()));
        }
    }

    public class UInt32SingleProvider : PropertyBindingProvider<uint, float>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToSingle(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToUInt32(targetGetter()));
        }
    }

    public class Int64SingleProvider : PropertyBindingProvider<long, float>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToSingle(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToInt64(targetGetter()));
        }
    }

    public class UInt64SingleProvider : PropertyBindingProvider<ulong, float>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToSingle(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToUInt64(targetGetter()));
        }
    }

    public class SingleSingleProvider : PropertyBindingProvider<float, float>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToSingle(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToSingle(targetGetter()));
        }
    }

    public class DoubleSingleProvider : PropertyBindingProvider<double, float>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToSingle(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToDouble(targetGetter()));
        }
    }

    public class DecimalSingleProvider : PropertyBindingProvider<Decimal, float>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToSingle(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToDecimal(targetGetter()));
        }
    }

    public class DateTimeSingleProvider : PropertyBindingProvider<DateTime, float>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToSingle(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToDateTime(targetGetter()));
        }
    }

    public class StringSingleProvider : PropertyBindingProvider<string, float>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToSingle(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToString(targetGetter()));
        }
    }

    //========================================double========================================

    public class BooleanDoubleProvider : PropertyBindingProvider<bool, double>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToDouble(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToBoolean(targetGetter()));
        }
    }

    public class CharDoubleProvider : PropertyBindingProvider<char, double>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToDouble(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToChar(targetGetter()));
        }
    }

    public class SByteDoubleProvider : PropertyBindingProvider<sbyte, double>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToDouble(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToSByte(targetGetter()));
        }
    }

    public class ByteDoubleProvider : PropertyBindingProvider<byte, double>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToDouble(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToByte(targetGetter()));
        }
    }

    public class Int16DoubleProvider : PropertyBindingProvider<short, double>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToDouble(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToInt16(targetGetter()));
        }
    }

    public class UInt16DoubleProvider : PropertyBindingProvider<ushort, double>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToDouble(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToUInt16(targetGetter()));
        }
    }

    public class Int32DoubleProvider : PropertyBindingProvider<int, double>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToDouble(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToInt32(targetGetter()));
        }
    }

    public class UInt32DoubleProvider : PropertyBindingProvider<uint, double>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToDouble(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToUInt32(targetGetter()));
        }
    }

    public class Int64DoubleProvider : PropertyBindingProvider<long, double>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToDouble(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToInt64(targetGetter()));
        }
    }

    public class UInt64DoubleProvider : PropertyBindingProvider<ulong, double>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToDouble(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToUInt64(targetGetter()));
        }
    }

    public class SingleDoubleProvider : PropertyBindingProvider<float, double>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToDouble(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToSingle(targetGetter()));
        }
    }

    public class DoubleDoubleProvider : PropertyBindingProvider<double, double>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToDouble(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToDouble(targetGetter()));
        }
    }

    public class DecimalDoubleProvider : PropertyBindingProvider<Decimal, double>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToDouble(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToDecimal(targetGetter()));
        }
    }

    public class DateTimeDoubleProvider : PropertyBindingProvider<DateTime, double>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToDouble(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToDateTime(targetGetter()));
        }
    }

    public class StringDoubleProvider : PropertyBindingProvider<string, double>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToDouble(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToString(targetGetter()));
        }
    }

    //========================================Decimal========================================

    public class BooleanDecimalProvider : PropertyBindingProvider<bool, Decimal>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToDecimal(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToBoolean(targetGetter()));
        }
    }

    public class CharDecimalProvider : PropertyBindingProvider<char, Decimal>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToDecimal(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToChar(targetGetter()));
        }
    }

    public class SByteDecimalProvider : PropertyBindingProvider<sbyte, Decimal>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToDecimal(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToSByte(targetGetter()));
        }
    }

    public class ByteDecimalProvider : PropertyBindingProvider<byte, Decimal>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToDecimal(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToByte(targetGetter()));
        }
    }

    public class Int16DecimalProvider : PropertyBindingProvider<short, Decimal>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToDecimal(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToInt16(targetGetter()));
        }
    }

    public class UInt16DecimalProvider : PropertyBindingProvider<ushort, Decimal>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToDecimal(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToUInt16(targetGetter()));
        }
    }

    public class Int32DecimalProvider : PropertyBindingProvider<int, Decimal>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToDecimal(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToInt32(targetGetter()));
        }
    }

    public class UInt32DecimalProvider : PropertyBindingProvider<uint, Decimal>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToDecimal(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToUInt32(targetGetter()));
        }
    }

    public class Int64DecimalProvider : PropertyBindingProvider<long, Decimal>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToDecimal(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToInt64(targetGetter()));
        }
    }

    public class UInt64DecimalProvider : PropertyBindingProvider<ulong, Decimal>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToDecimal(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToUInt64(targetGetter()));
        }
    }

    public class SingleDecimalProvider : PropertyBindingProvider<float, Decimal>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToDecimal(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToSingle(targetGetter()));
        }
    }

    public class DoubleDecimalProvider : PropertyBindingProvider<double, Decimal>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToDecimal(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToDouble(targetGetter()));
        }
    }

    public class DecimalDecimalProvider : PropertyBindingProvider<Decimal, Decimal>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToDecimal(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToDecimal(targetGetter()));
        }
    }

    public class DateTimeDecimalProvider : PropertyBindingProvider<DateTime, Decimal>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToDecimal(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToDateTime(targetGetter()));
        }
    }

    public class StringDecimalProvider : PropertyBindingProvider<string, Decimal>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToDecimal(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToString(targetGetter()));
        }
    }

    //========================================DateTime========================================

    public class BooleanDateTimeProvider : PropertyBindingProvider<bool, DateTime>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToDateTime(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToBoolean(targetGetter()));
        }
    }

    public class CharDateTimeProvider : PropertyBindingProvider<char, DateTime>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToDateTime(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToChar(targetGetter()));
        }
    }

    public class SByteDateTimeProvider : PropertyBindingProvider<sbyte, DateTime>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToDateTime(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToSByte(targetGetter()));
        }
    }

    public class ByteDateTimeProvider : PropertyBindingProvider<byte, DateTime>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToDateTime(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToByte(targetGetter()));
        }
    }

    public class Int16DateTimeProvider : PropertyBindingProvider<short, DateTime>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToDateTime(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToInt16(targetGetter()));
        }
    }

    public class UInt16DateTimeProvider : PropertyBindingProvider<ushort, DateTime>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToDateTime(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToUInt16(targetGetter()));
        }
    }

    public class Int32DateTimeProvider : PropertyBindingProvider<int, DateTime>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToDateTime(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToInt32(targetGetter()));
        }
    }

    public class UInt32DateTimeProvider : PropertyBindingProvider<uint, DateTime>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToDateTime(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToUInt32(targetGetter()));
        }
    }

    public class Int64DateTimeProvider : PropertyBindingProvider<long, DateTime>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToDateTime(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToInt64(targetGetter()));
        }
    }

    public class UInt64DateTimeProvider : PropertyBindingProvider<ulong, DateTime>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToDateTime(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToUInt64(targetGetter()));
        }
    }

    public class SingleDateTimeProvider : PropertyBindingProvider<float, DateTime>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToDateTime(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToSingle(targetGetter()));
        }
    }

    public class DoubleDateTimeProvider : PropertyBindingProvider<double, DateTime>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToDateTime(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToDouble(targetGetter()));
        }
    }

    public class DecimalDateTimeProvider : PropertyBindingProvider<Decimal, DateTime>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToDateTime(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToDecimal(targetGetter()));
        }
    }

    public class DateTimeDateTimeProvider : PropertyBindingProvider<DateTime, DateTime>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToDateTime(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToDateTime(targetGetter()));
        }
    }

    public class StringDateTimeProvider : PropertyBindingProvider<string, DateTime>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToDateTime(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToString(targetGetter()));
        }
    }

    //========================================string========================================

    public class BooleanStringProvider : PropertyBindingProvider<bool, string>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToString(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToBoolean(targetGetter()));
        }
    }

    public class CharStringProvider : PropertyBindingProvider<char, string>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToString(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToChar(targetGetter()));
        }
    }

    public class SByteStringProvider : PropertyBindingProvider<sbyte, string>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToString(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToSByte(targetGetter()));
        }
    }

    public class ByteStringProvider : PropertyBindingProvider<byte, string>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToString(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToByte(targetGetter()));
        }
    }

    public class Int16StringProvider : PropertyBindingProvider<short, string>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToString(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToInt16(targetGetter()));
        }
    }

    public class UInt16StringProvider : PropertyBindingProvider<ushort, string>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToString(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToUInt16(targetGetter()));
        }
    }

    public class Int32StringProvider : PropertyBindingProvider<int, string>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToString(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToInt32(targetGetter()));
        }
    }

    public class UInt32StringProvider : PropertyBindingProvider<uint, string>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToString(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToUInt32(targetGetter()));
        }
    }

    public class Int64StringProvider : PropertyBindingProvider<long, string>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToString(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToInt64(targetGetter()));
        }
    }

    public class UInt64StringProvider : PropertyBindingProvider<ulong, string>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToString(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToUInt64(targetGetter()));
        }
    }

    public class SingleStringProvider : PropertyBindingProvider<float, string>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToString(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToSingle(targetGetter()));
        }
    }

    public class DoubleStringProvider : PropertyBindingProvider<double, string>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToString(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToDouble(targetGetter()));
        }
    }

    public class DecimalStringProvider : PropertyBindingProvider<Decimal, string>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToString(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToDecimal(targetGetter()));
        }
    }

    public class DateTimeStringProvider : PropertyBindingProvider<DateTime, string>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToString(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToDateTime(targetGetter()));
        }
    }

    public class StringStringProvider : PropertyBindingProvider<string, string>
    {
        public override void SyncTarget()
        {
            targetSetter(Convert.ToString(sourceGetter()));
        }

        public override void SyncSource()
        {
            sourceSetter(Convert.ToString(targetGetter()));
        }
    }
}
using System;

public static class TelemetryBuffer
{
    public static byte[] ToBuffer(long reading)
    {
        int size;
        bool signed;

        if (reading >= 0 && reading <= ushort.MaxValue)
        {
            size = 2; signed = false;
        }
        else if (reading >= short.MinValue && reading <= -1)
        {
            size = 2; signed = true;
        }
        else if (reading >= 65536 && reading <= int.MaxValue)
        {
            size = 4; signed = true;
        }
        else if (reading >= int.MinValue && reading <= -32769)
        {
            size = 4; signed = true;
        }
        else if (reading >= 0 && reading <= uint.MaxValue)
        {
            size = 4; signed = false;
        }
        else if (reading >= (long)uint.MaxValue + 1 && reading <= long.MaxValue)
        {
            size = 8; signed = true;
        }
        else if (reading >= long.MinValue && reading <= (long)int.MinValue - 1)
        {
            size = 8; signed = true;
        }
        else
        {
            throw new ArgumentOutOfRangeException(nameof(reading));
        }


        byte[] buffer = new byte[9];
        buffer[0] = (byte)(signed ? 256 - size : size);

        byte[] valueBytes = size switch
        {
            2 => signed ? BitConverter.GetBytes((short)reading) : BitConverter.GetBytes((ushort)reading),
            4 => signed ? BitConverter.GetBytes((int)reading) : BitConverter.GetBytes((uint)reading),
            8 => BitConverter.GetBytes(reading),
            _ => throw new InvalidOperationException()
        };

        Array.Copy(valueBytes, 0, buffer, 1, size);
        return buffer;
    }

    public static long FromBuffer(byte[] buffer)
    {
        if (buffer == null || buffer.Length != 9)
            return 0;

        int prefix = buffer[0];
        bool signed;
        int size;

        if (prefix == 2 || prefix == 4) // unsigned
        {
            signed = false; size = prefix;
        }
        else if (prefix == 8) // ulong ไม่ถูกใช้ในโจทย์
        {
            return 0;
        }
        else if (prefix == 254 || prefix == 252 || prefix == 248) // signed
        {
            signed = true; size = 256 - prefix;
        }
        else
        {
            return 0;
        }

        byte[] valueBytes = new byte[size];
        Array.Copy(buffer, 1, valueBytes, 0, size);

        return signed switch
        {
            true => size switch
            {
                2 => BitConverter.ToInt16(valueBytes, 0),
                4 => BitConverter.ToInt32(valueBytes, 0),
                8 => BitConverter.ToInt64(valueBytes, 0),
                _ => 0
            },
            false => size switch
            {
                2 => BitConverter.ToUInt16(valueBytes, 0),
                4 => BitConverter.ToUInt32(valueBytes, 0),
                _ => 0
            }
        };
    }
}

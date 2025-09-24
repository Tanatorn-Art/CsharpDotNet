using System.Text.RegularExpressions;

public static class IntergalacticTransmission
{
    public static byte[] GetTransmitSequence(byte[] message)
    {
        string stream = "";
        foreach (byte data in message)
            stream += Convert.ToString(data, 2).PadLeft(8, '0');
        while (stream.Length % 7 != 0)
            stream += "0";
        List<byte> result = new List<byte>();
        for (int i = 0; i < stream.Length; i += 7)
        {
            string binary = stream.Substring(i, 7);
            byte data = Convert.ToByte(binary, 2);
            int parity = Regex.Matches(binary, "1").Count % 2;
            result.Add((byte) (data << 1 | parity));
        }
        return result.ToArray();
    }

    public static byte[] DecodeSequence(byte[] receivedSeq)
    {
        string stream = "";
        foreach (byte data in receivedSeq)
        {
            string binary = Convert.ToString(data, 2).PadLeft(8, '0');
            if (Regex.Matches(binary, "1").Count % 2 == 1)
                throw new ArgumentException();
            stream += binary.Substring(0, 7);
        }
        while (stream.Length % 8 != 0)
        {
            if (stream[stream.Length - 1] != '0')
                throw new ArgumentException();
            stream = stream.Substring(0, stream.Length - 1);
        }
        List<byte> result = new List<byte>();
        for (int i = 0; i < stream.Length; i += 8)
            result.Add(Convert.ToByte(stream.Substring(i, 8), 2));
        return result.ToArray();
    }
}
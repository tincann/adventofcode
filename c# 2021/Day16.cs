using System.Buffers;

namespace c__2021;

public class Day16
{
    
    public void Part1(IEnumerable<string> lines)
    {
        var bitStream = HexToBitStream(lines.First());
        var reader = new CharBasedBitReader(bitStream);
        var packet = ParsePacket(reader);
        
        var sum = 0L;
        var q = new Queue<Packet>();
        q.Enqueue(packet);
        while (q.TryDequeue(out var p))
        {
            sum += p.Version;
            var subPackets = p.Value switch
            {
                PacketValue.Operator op => op.SubPackets,
                _ => ArraySegment<Packet>.Empty,
            };
            foreach (var subPacket in subPackets)
            {
                q.Enqueue(subPacket);
            }
        }

        Console.WriteLine(sum);
    }
    
    public void Part2(IEnumerable<string> lines)
    {
        var bitStream = HexToBitStream(lines.First());
        var reader = new CharBasedBitReader(bitStream);
        var packet = ParsePacket(reader);

        var result = Evaluate(packet);

        Console.WriteLine(result);
    }
    
    private static Packet ParsePacket(IBitReader reader)
    {
        var version = reader.ReadUShort(3);
        var typeId = reader.ReadUShort(3);
        
        PacketValue result = typeId switch
        {
            4 => ReadLiteral(reader),
            
            0 => ReadOperator(reader, (a, b) => a + b),
            1 => ReadOperator(reader, (a, b) => a * b),
            2 => ReadOperator(reader, Math.Min),
            3 => ReadOperator(reader, Math.Max),
            5 => ReadOperator(reader, (a, b) => a > b ? 1u : 0),
            6 => ReadOperator(reader, (a, b) => a < b ? 1u : 0),
            7 => ReadOperator(reader, (a, b) => a == b ? 1u : 0),
            _ => throw new NotSupportedException()
        };

        return new Packet(version, typeId, result);
    }

    private static PacketValue.Operator ReadOperator(IBitReader reader, Func<ulong, ulong, ulong> operation)
    {
        var lengthTypeId = reader.ReadBool();
        if (lengthTypeId)
        {
            var subPacketCount = reader.ReadUShort(11);
            var subPackets = new List<Packet>(subPacketCount);
            while (subPacketCount-- > 0)
            {
                subPackets.Add(ParsePacket(reader));
            }

            return new PacketValue.Operator(lengthTypeId, subPackets, operation);
        }
        else
        {
            var bitCount = reader.ReadUShort(15);
            var subReader = reader.Fork(bitCount);
            var subPackets = new List<Packet>();

            do
            {
                subPackets.Add(ParsePacket(subReader));
            } while (subReader.CanRead);

            reader.Skip(bitCount);
            
            return new PacketValue.Operator(lengthTypeId, subPackets, operation);
        }
    }

    private static PacketValue.Literal ReadLiteral(IBitReader reader)
    {
        var keepReading = true;
        
        var result = 0ul;
        while(keepReading)
        {
            var group = reader.ReadUInt(5);
            keepReading = group >> 4 == 1;
            result <<= 4;
            result |= group & 0x0F;
        }
        return new PacketValue.Literal(result);
    }

    private ulong Evaluate(Packet packet)
    {
        return packet.Value switch
        {
            PacketValue.Operator op => op.SubPackets.Select(Evaluate).Aggregate(op.Operation),
            PacketValue.Literal lit => lit.Value,
            _ => throw new NotSupportedException()
        };
    }

    interface IBitReader
    {
        bool CanRead { get; }
        void Skip(int bitCount);
        IBitReader Fork(int bitCount);
        uint ReadUInt(int bitCount);
        bool ReadBool();
        ushort ReadUShort(int bitCount);
    }
    class CharBasedBitReader : IBitReader
    {
        private readonly ReadOnlyMemory<char> _bitStream;
        private int _currentIndex;
        public CharBasedBitReader(ReadOnlyMemory<char> bitStream)
        {
            _bitStream = bitStream;
        }

        public bool CanRead => _currentIndex < _bitStream.Length - 1;

        public void Skip(int bitCount)
        {
            _currentIndex += bitCount;
        }

        public IBitReader Fork(int bitCount)
        {
            var memory = Slice(_currentIndex, bitCount);
            return new CharBasedBitReader(memory);
        }

        public uint ReadUInt(int bitCount)
        {
            var bits = Slice(_currentIndex, bitCount).Span;
            _currentIndex += bitCount;
            return Convert.ToUInt32(bits.ToString(), 2);
        }

        public bool ReadBool()
        {
            var bits = Slice(_currentIndex, 1).Span;
            _currentIndex++;
            return bits[0] switch { '1' => true, '0' => false, _ => throw new InvalidOperationException() };
        }

        public ushort ReadUShort(int bitCount)
        {
            var bits = Slice(_currentIndex, bitCount);
            _currentIndex += bitCount;
            return Convert.ToUInt16(bits.ToString(), 2);
        }

        private ReadOnlyMemory<char> Slice(int offset, int bitCount)
        {
            return _bitStream[offset..(offset + bitCount)];
        }
    }

    record Packet(ushort Version, ushort TypeId, PacketValue Value);

    abstract record PacketValue
    {
        public record Literal(ulong Value) : PacketValue;

        public record Operator(bool LengthTypeId, IReadOnlyList<Packet> SubPackets, Func<ulong, ulong, ulong> Operation) : PacketValue;
    }

    private static ReadOnlyMemory<char> HexToBitStream(string hexString)
    {
        var memory = MemoryPool<char>.Shared.Rent(hexString.Length * 4).Memory;
        var span = memory.Span;
        for (var i = 0; i < hexString.Length; i++)
        {
            var c = ToBits(hexString[i]);
            span[i * 4] = c[0];
            span[i * 4 + 1] = c[1];
            span[i * 4 + 2] = c[2];
            span[i * 4 + 3] = c[3];
        }

        return memory;
    }

    private static string ToBits(char hexChar) => hexChar switch
    {
        '0' => "0000",
        '1' => "0001",
        '2' => "0010",
        '3' => "0011",
        '4' => "0100",
        '5' => "0101",
        '6' => "0110",
        '7' => "0111",
        '8' => "1000",
        '9' => "1001",
        'A' => "1010",
        'B' => "1011",
        'C' => "1100",
        'D' => "1101",
        'E' => "1110",
        'F' => "1111",
        _ => throw new InvalidOperationException(),
    };
}

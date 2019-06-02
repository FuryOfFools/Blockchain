using System;

namespace BC
{
    public interface IBlock
    {
        byte[] Data { get; }
        byte[] Hash { get; set; }
        int Nonce { get; set; }
        byte[] PrevHash { get; set; }
        DateTime TimeStamp { get; }

        string ToString();
    }

    [Serializable]
    public class Block : IBlock
    {
        public byte[] Data { get; }
        public byte[] Hash { get; set; }
        public int Nonce { get; set; }
        public byte[] PrevHash { get; set; }
        public DateTime TimeStamp { get; }

        public Block(byte[] data)
        {
            Data = data ?? throw new ArgumentNullException(nameof(data));
            Nonce = 0;
            PrevHash = new byte[] { 0x00 };
            TimeStamp = DateTime.UtcNow;
        }

        public override string ToString()
        {
            return $"Hash: {BitConverter.ToString(Hash).Replace("-", "")}\n PrevHash: " +
                $"{BitConverter.ToString(PrevHash).Replace("-", "")}\n  Nonce: {Nonce}\n TimeStamp: {TimeStamp}\n";
        }
    }
}

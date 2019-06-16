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
}

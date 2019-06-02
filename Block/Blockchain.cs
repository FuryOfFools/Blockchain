using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace BC
{
    public static class BlockchainExtension
    {
        //генерация хеша
        public static byte[] GenerateHash(this IBlock block)
        {
            using (SHA512 sha = new SHA512Managed())
            using (MemoryStream st = new MemoryStream())
            using (BinaryWriter bw = new BinaryWriter(st))
            {
                bw.Write(block.Data);
                bw.Write(block.Nonce);
                bw.Write(block.TimeStamp.ToBinary());
                bw.Write(block.PrevHash);
                var starr = st.ToArray();
                return sha.ComputeHash(starr);
            }
        }

        //майнинг хеша
        public static byte[] MineHash(this IBlock block, byte[] difficulty)
        {
            if (difficulty == null)
                throw new ArgumentNullException(nameof(difficulty));

            byte[] hash = new byte[0];
            while(!hash.Take(2).SequenceEqual(difficulty))
            {
                block.Nonce++;
                hash = block.GenerateHash();
            }
            return hash;
        }

        //валидный блок
        public static bool IsValid(this IBlock block)
        {
            var bk = block.GenerateHash();
            return block.Hash.SequenceEqual(bk);
        }
        //валидный предыдущий блок
        public static bool IsValidPrevBlock(this IBlock block, IBlock prevBlock)
        {
            if (prevBlock == null)
                throw new ArgumentNullException(nameof(prevBlock));
            var prev = prevBlock.GenerateHash();
            return prevBlock.IsValid() && block.PrevHash.SequenceEqual(prev);
        }
        //валидный блокчейн
        public static bool IsValid(this IEnumerable<IBlock> items)
        {
            var enmerable = items.ToList();
            return enmerable.Zip(enmerable.Skip(1), Tuple.Create).All(block => 
                block.Item2.IsValid() && block.Item2.IsValidPrevBlock(block.Item1));
        }
    }

    [Serializable]
    public class Blockchain : IEnumerable<IBlock>
    {
        private List<IBlock> _items = new List<IBlock>();

        //список блоков
        public List<IBlock> Items
        {
            get => _items;
            set => _items = value;
        }

        //длина цепочки
        public int Count => Items.Count;

        //получить блок по индексу
        public IBlock this[int index]
        {
            get => Items[index];
            private set => Items[index] = value;
        }
        //сложность
        public byte[] Difficulty { get; }

        //конструктор из файла
        public Blockchain(string fileName)
        {
            Blockchain chain;
            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream fs = new FileStream(fileName, FileMode.Open))
            {
                chain = (Blockchain)bf.Deserialize(fs);
            }
            Items = chain.Items;
            Difficulty = chain.Difficulty;
        }

        // конструктор со сложностью и блоком генезиса
        public Blockchain(byte[] difficulty, IBlock genesis)
        {
            Difficulty = difficulty;
            genesis.Hash = genesis.MineHash(difficulty);
            Items.Add(genesis);
        }

        // добавить новый блок
        public void Add(IBlock item)
        {
            if(Items.LastOrDefault() != null)
            {
                item.PrevHash = Items.LastOrDefault()?.Hash;
            }
            item.Hash = item.MineHash(Difficulty);
            Items.Add(item);
        }

        public IEnumerator<IBlock> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Items.GetEnumerator();
        }
    }
}

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using BC;

namespace Interactions
{
    public static class LocalInteractions
    {
        //сохранить файл локально
        public static void SaveLocal(this Blockchain chain, string fileName)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream fs = new FileStream(fileName, FileMode.Create))
            {
                bf.Serialize(fs, chain);
            }
        }

        // существует ли файл
        public static bool FileExists(string fileName)
        {
            if (File.Exists(fileName))
                return true;
            return false;
        }

        // загрузить блокчейн
        public static Blockchain LoadBlockchain(string fileName)
        {
            Blockchain chain;

            //есть ли в локальном хранилище
            if (FileExists(fileName))
                chain = new Blockchain(fileName);
            //создаем с нуля
            else
            {
                IBlock genesis = new Block(new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00 });
                byte[] difficulty = new byte[] { 0x00, 0x00 };
                chain = new Blockchain(difficulty, genesis);
            }
            return chain;
        }

        // добавить данные в блокчейн
        public static void AddData(this Blockchain chain, byte[] Data)
        {
            IBlock block = new Block(Data);
            chain.Add(block);
        }

        // считывает байты из файла
        public static byte[] ReadBytesFromFile(string fileName)
        {
            if(FileExists(fileName))
                return File.ReadAllBytes(fileName);
            return null;
        }
    }
}

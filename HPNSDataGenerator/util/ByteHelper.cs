using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace HPNSDataGenerator.util
{
    public class ByteHelper
    {
        public static byte[] ObjectToByteArray(Object obj)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        public static Object ByteArrayToObject(byte[] arrBytes)
        {
            using (var memStream = new MemoryStream())
            {
                var binForm = new BinaryFormatter();
                memStream.Write(arrBytes, 0, arrBytes.Length);
                memStream.Seek(0, SeekOrigin.Begin);
                var obj = binForm.Deserialize(memStream);
                return obj;
            }
        }

        public static byte[] AddByteToArray(byte[] bArray, byte newByte)
        {
            byte[] newArray = new byte[bArray.Length + 1];
            bArray.CopyTo(newArray, 1);
            newArray[0] = newByte;
            return newArray;
        }

        public static byte[] GetByteInt(dynamic intValue)
        {
            byte[] intBytes = BitConverter.GetBytes(intValue);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(intBytes);
            return intBytes;
        }

        public static byte[] GetByteString(string value)
        {
            return Encoding.ASCII.GetBytes(value);
        }

        public static byte[] Combine(byte[] first, byte[] second)
        {
            byte[] ret = new byte[first.Length + second.Length];
            Buffer.BlockCopy(first, 0, ret, 0, first.Length);
            Buffer.BlockCopy(second, 0, ret, first.Length, second.Length);
            return ret;
        }
        public static byte[] Combine(params byte[][] arrays)
        {
            byte[] ret = new byte[arrays.Sum(x => x.Length)];
            int offset = 0;
            foreach (byte[] data in arrays)
            {
                Buffer.BlockCopy(data, 0, ret, offset, data.Length);
                offset += data.Length;
            }
            return ret;
        }

        public static byte[] GetByteRejectCode(string rejectCode)
        {
            byte[] rejectByte = Encoding.ASCII.GetBytes(rejectCode);
            var newArray = new byte[3];

            var startAt = newArray.Length - rejectByte.Length;
            Buffer.BlockCopy(rejectByte, 0, newArray, startAt, rejectByte.Length);

            return newArray;
        }

        public static long GetLongLE(byte[] buffer, int startIndex, int count)
        {
            long result = 0;
            long multiplier = 1;
            for (int i = 0; i < count; i++)
            {
                result += buffer[startIndex + i] * multiplier;
                multiplier *= 256;
            }
            return result;
        }

        public static long GetLongBE(byte[] buffer, int startIndex, int count)
        {
            Array.Reverse(buffer);
            long result = 0;
            long multiplier = 1;
            for (int i = 0; i < count; i++)
            {
                result += buffer[startIndex + i] * multiplier;
                multiplier *= 256;
            }
            return result;
        }

        public static long GetTickTimeStamp()
        {
            //return DateTime.Now.Ticks;
            DateTime currentTime = DateTime.UtcNow;
            return ((DateTimeOffset)currentTime).ToUnixTimeSeconds();
        }

        public static byte[] GetByteLastTransaction(byte[] trans)
        {
            byte[] newArray = new byte[trans.Length - 1];
            Buffer.BlockCopy(trans, 0, newArray, 0, newArray.Length);

            return newArray;
        }

        public static List<string> GetCVSBIN()
        {
            var lstBin = new List<string>();
            string folderPath = @"SampleFiles\cvs_bin.txt";
            string[] lines = File.ReadAllLines(folderPath);

            foreach (string line in lines)
                lstBin.Add(line);

            return lstBin;
        }

        public static List<string> GetRejectCode()
        {
            var lstRejectCode = new List<string>();
            string folderPath = @"SampleFiles\reject_code.txt";
            string[] lines = File.ReadAllLines(folderPath);

            foreach (string line in lines)
                lstRejectCode.Add(line);

            return lstRejectCode;
        }
        public static List<string> GetTransCode()
        {
            var lstTransCode = new List<string>();
            string folderPath = @"SampleFiles\transaction_code.txt";
            string[] lines = File.ReadAllLines(folderPath);

            foreach (string line in lines)
                lstTransCode.Add(line);

            return lstTransCode;
        }
        public static byte[] GetByteTimeStamp()
        {
            var temp = BitConverter.GetBytes(GetTickTimeStamp()); //remove last two bytes
            byte[] newArray = new byte[temp.Length - 2];
            Buffer.BlockCopy(temp, 0, newArray, 0, newArray.Length);

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(newArray);
                return newArray;
            }
            else
                return newArray;
            //unix byte Int32 ==> 12345345 Big Endian
            //11111111 11111111 11111111 00000000
            //255 * 256*256*256

            //window byte Int32 ==> 12345345
            //00000000 11111111 11111111 11111111 ==> Little Endian
            //BitConverter.ToSingle(bytes, 0);
        }

        public static string GetIPAddr()
        {
            String strHostName = string.Empty;
            // Getting Ip address of local machine...
            // First get the host name of local machine.
            strHostName = Dns.GetHostName();
            Console.WriteLine("Local Machine's Host Name: " + strHostName);
            // Then using host name, get the IP address list..
            IPHostEntry ipEntry = Dns.GetHostEntry(strHostName);
            IPAddress[] addr = ipEntry.AddressList;

            string rightIP = "";
            for (int i = 0; i < addr.Length; i++)
            {
                if (addr[i].ToString().StartsWith("192.168") || addr[i].ToString().StartsWith("10.24"))
                    rightIP = addr[i].ToString();
            }
            return rightIP;
        }
        //arrProp = 
        //BitConverter.ToInt64(arrProp, 0)

        //long milliseconds = GetLongLE(timestamp, 0, 6);
    }
}

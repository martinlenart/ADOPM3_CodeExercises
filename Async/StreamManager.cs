using System;
using System.Text;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;

namespace Async
{
	public static class StreamManager
	{

        public static Task<string> WriteCompressBytesAsync(string path, byte[] array)
        {
            return Task.Run(() => WriteCompressBytes(path, array));
        }

        public static string WriteCompressBytes(string path, byte[] array)
        {
            using (Stream s = File.Create(path))
            using (Stream ds = new GZipStream(s, CompressionMode.Compress))
            using (BinaryWriter w = new BinaryWriter(ds))
            {
                w.Write(array);
                return path;
            }
        }

        public static Task<byte[]> ReadCompressBytesAsync(string path, int NrOfBytes)
        {
            return Task.Run(() => ReadCompressBytes(path, NrOfBytes));
        }

        public static byte[] ReadCompressBytes(string path, int NrOfBytes)
        {
            using (Stream s = File.OpenRead(path))
            using (Stream ds = new GZipStream(s, CompressionMode.Decompress))
            using (BinaryReader r = new BinaryReader(ds))
            {
                byte[] imageRead = r.ReadBytes(NrOfBytes);
                return imageRead;
            }
        }


        public static Task<byte[]> ReadCompressBytesAsync(string path)
        {
            return Task.Run(() => ReadCompressBytes(path));
        }
        public static byte[] ReadCompressBytes(string path)
        {
            const int arrayPageSize = 32768;

            byte[] buffer = new byte[arrayPageSize];
            int bytesInBuffer = 0;

            using (Stream s = File.OpenRead(path))
            using (Stream ds = new GZipStream(s, CompressionMode.Decompress))
            using (BinaryReader r = new BinaryReader(ds))
            {
                int bytesRead;
                while ((bytesRead = r.Read(buffer, bytesInBuffer, buffer.Length - bytesInBuffer)) > 0)
                {
                    //bytesRead number of bytes read into buffer
                    bytesInBuffer += bytesRead;

                    //Check if buffer needs to be expanded
                    if (bytesInBuffer == buffer.Length)
                    {
                        int nextByte = r.ReadByte();

                        //if nothing more to read, terminate and return buffer
                        if (nextByte == -1)
                        {
                            return buffer;
                        }

                        //resize and copy the buffer
                        byte[] newBuffer = new byte[buffer.Length + arrayPageSize];
                        Array.Copy(buffer, newBuffer, buffer.Length);

                        newBuffer[bytesInBuffer] = (byte)nextByte;
                        buffer = newBuffer;
                        bytesInBuffer++;
                    }
                }
            
                //No more bytes to read, adjust the bufferSize
                byte[] retBuffer = new byte[bytesInBuffer];
                Array.Copy(buffer, retBuffer, bytesInBuffer);
                return retBuffer;
            }
        }
    }
}


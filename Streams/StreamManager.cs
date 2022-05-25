using System;
using System.Text;
using System.IO;
using System.IO.Compression;


namespace Streams
{
    public static class StreamManager
    {
        public static long StreamToFile(byte[] buffer, string path)
        {
            //Uncompressed stream to Memory
            using (Stream s = File.Create(path))
            using (BinaryWriter w = new BinaryWriter(s))
            {
                w.Write(buffer);
                return s.Length;
            }
        }

        public static byte[] StreamFromFile(int nrOfBytes, string path)
        {
            //Uncompressed stream from Memory
            using (Stream s = File.OpenRead(path))
            using (BinaryReader r = new BinaryReader(s))
            {
                byte[] buffer = r.ReadBytes(nrOfBytes);
                return buffer;
            }
        }

        public static byte[] StreamFromFile(string path)
        {
            const int arrayPageSize = 100_000; //default size and chunks of increase

            byte[] buffer = new byte[arrayPageSize];
            int bytesInBuffer = 0;

            using (Stream s = File.OpenRead(path))
            using (BinaryReader r = new BinaryReader(s))
            {
                int bytesRead;
                while ((bytesRead = r.Read(buffer, bytesInBuffer, buffer.Length - bytesInBuffer)) > 0)
                {
                    //bytesRead number of bytes read into buffer
                    bytesInBuffer += bytesRead;

                    //Check buffer boundry to see if buffer needs to be expanded
                    if (bytesInBuffer == buffer.Length)
                    {
                        int nextByte;
                        try
                        {
                            nextByte = r.ReadByte();
                        }
                        catch (EndOfStreamException)
                        {
                            //Nothing more to read, terminate and return buffer
                            return buffer;
                        }

                        //resize and copy the buffer
                        byte[] newBuffer = new byte[buffer.Length + arrayPageSize];
                        Array.Copy(buffer, newBuffer, buffer.Length);

                        //Dont forget the byte read to test EndOfStream
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


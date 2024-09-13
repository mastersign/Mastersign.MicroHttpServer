using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Mastersign.MicroHttpServer.IO
{
    internal class LineReader
    {
        private readonly Stream _stream;
        private readonly Encoding _encoding;
        private readonly byte[] _buffer;
        private int _dataOffset = 0;
        private int _dataLength = 0;
        private int _scanPosition = 0;
        private long _readBytes = 0;
        private long _consumedBytes = 0;
        private long _parsedChars = 0;
        private long _parsedLines = 0;

        public LineReader(Stream stream, Encoding encoding, int size)
        {
            _stream = stream;
            _encoding = encoding;
            _buffer = new byte[size];
        }

        public long TotalReadBytes => _readBytes;
        public long TotalConsumedBytes => _consumedBytes;
        public long TotalParsedChars => _parsedChars;
        public long TotalParsedLines => _parsedLines;

        private void Realign()
        {
            if (_dataOffset > 0)
            {
                if (_dataLength > 0)
                {
                    Array.Copy(_buffer, _dataOffset, _buffer, 0, _dataLength);
                }
                _scanPosition = Math.Max(0, _scanPosition - _dataOffset);
                _dataOffset = 0;
            }
        }

        private int DataEnd() => _dataOffset + _dataLength;

        private bool IsFull => DataEnd() >= _buffer.Length;

        private bool ReadFromStream()
        {
            var dataEnd = DataEnd();
            var count = _buffer.Length - dataEnd;
            if (count <= 0) throw new InvalidOperationException("Buffer is full");
            var read = _stream.Read(_buffer, dataEnd, count);
            _dataLength += read;
            _readBytes += read;
            return read > 0;
        }

        private string ParseLine()
        {
            var dataEnd = DataEnd();
            if (_scanPosition >= dataEnd) return null;
            var hadCR = false;
            for (int i = _scanPosition; i < dataEnd; i++)
            {
                var c = _buffer[i];
                if (c == '\r')
                {
                    hadCR = true;
                    continue;
                }
                else if (hadCR && c == '\n')
                {
                    var l = i - _dataOffset - 1;
                    _consumedBytes += l + 2;
                    var line = l > 0 ? _encoding.GetString(_buffer, _dataOffset, l) : string.Empty;
                    _parsedChars += line.Length + 2;
                    _parsedLines++;

                    _dataOffset = i + 1;
                    _dataLength = dataEnd - _dataOffset;
                    _scanPosition = i + 1;
                    Realign();
                    Debug.Assert(_dataOffset == 0, "Expected data to be aligned at the start of the buffer");
                    Debug.Assert(_scanPosition == 0, "Expected scan position to be at the start of the buffer");
                    return line;
                }
                else
                {
                    hadCR = false;
                }
            }
            _scanPosition = dataEnd - 1;
            return null;
        }

        public string ReadNextLine()
        {
            var line = ParseLine();
            if (line != null) return line;
            do
            {
                if (IsFull) return null;
                ReadFromStream();
            } while ((line = ParseLine()) == null);
            return line;
        }

        public byte[] GetRemainingData()
        {
            var data = new byte[_dataLength];
            Array.Copy(_buffer, _dataOffset, data, 0, _dataLength);
            return data;
        }
    }
}
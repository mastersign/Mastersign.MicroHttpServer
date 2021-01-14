using System.IO;

namespace Mastersign.MicroHttpServer
{
    internal class LimitedStream : Stream
    {
        private const string _exceptionMessageFormat = "The stream has exceeded the {0} limit specified.";
        private readonly Stream _child;
        private readonly long _offset;
        private long _readLimit;
        private long _writeLimit;

        public override bool CanRead => _child.CanRead;

        public override bool CanSeek => _child.CanSeek;

        public override bool CanWrite => _child.CanWrite;

        public override long Length => _child.Length - _offset;

        public override long Position
        {
            get => _child.Position - _offset;
            set => _child.Position = value + _offset;
        }

        public override int ReadTimeout
        {
            get => _child.ReadTimeout;
            set => _child.ReadTimeout = value;
        }

        public override int WriteTimeout
        {
            get => _child.WriteTimeout;
            set => _child.WriteTimeout = value;
        }

        public LimitedStream(Stream child, long readLimit = -1, long writeLimit = -1)
        {
            _child = child;
            _offset = child.CanSeek ? child.Position : 0L;
            _readLimit = readLimit;
            _writeLimit = writeLimit;
        }

        public override void Flush()
        {
            _child.Flush();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            long p = _child.Position;
            long targetPosition;
            switch (origin)
            {
                case SeekOrigin.Begin:
                    targetPosition = _offset + offset;
                    break;
                case SeekOrigin.Current:
                    targetPosition = p + offset;
                    break;
                case SeekOrigin.End:
                    targetPosition = _child.Length - offset;
                    break;
                default:
                    targetPosition = p;
                    break;
            }
            if (targetPosition < _offset)
            {
                throw new IOException("Seeking position before the beginning of the stream.");
            }
            return _child.Seek(targetPosition, SeekOrigin.Begin);
        }

        public override void SetLength(long value)
        {
            _child.SetLength(value + _offset);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            var retVal = _child.Read(buffer, offset, count);

            AssertReadLimit(retVal);

            return retVal;
        }

        private void AssertReadLimit(int coefficient)
        {
            if (_readLimit == -1) return;

            _readLimit -= coefficient;

            if (_readLimit < 0) throw new IOException(string.Format(_exceptionMessageFormat, "read"));
        }

        private void AssertWriteLimit(int coefficient)
        {
            if (_writeLimit == -1) return;

            _writeLimit -= coefficient;

            if (_writeLimit < 0) throw new IOException(string.Format(_exceptionMessageFormat, "write"));
        }

        public override int ReadByte()
        {
            var retVal = _child.ReadByte();

            AssertReadLimit(1);

            return retVal;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            _child.Write(buffer, offset, count);

            AssertWriteLimit(count);
        }

        public override void WriteByte(byte value)
        {
            _child.WriteByte(value);

            AssertWriteLimit(1);
        }
    }
}
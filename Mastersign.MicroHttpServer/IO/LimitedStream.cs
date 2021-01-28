using System;
using System.IO;

namespace Mastersign.MicroHttpServer
{
    internal class LimitedStream : Stream
    {
        private const string _exceptionMessageFormat = "The stream has exceeded the {0} limit specified.";
        private readonly Stream _child;
        private long _readLimit;
        private long _writeLimit;

        public override bool CanRead => _child.CanRead;

        public override bool CanSeek => false;

        public override bool CanWrite => _child.CanWrite;

        public override long Length => throw new NotSupportedException();

        public override long Position
        {
            get => throw new NotSupportedException();
            set => throw new NotSupportedException();
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
            _readLimit = readLimit;
            _writeLimit = writeLimit;
        }

        public override void Flush()
        {
            _child.Flush();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException();
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException();
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
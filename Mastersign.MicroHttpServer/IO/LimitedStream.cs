using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Mastersign.MicroHttpServer
{
    internal class LimitedStream : Stream
    {
        private const string _exceptionMessageFormat = "The stream has exceeded the {0} limit specified.";
        private readonly Stream _child;
        private long _readCapacity;
        private long _writeCapacity;

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
            _readCapacity = readLimit;
            _writeCapacity = writeLimit;
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
            var bytesRead = _child.Read(buffer, offset, count);
            AssertReadLimit(bytesRead);
            return bytesRead;
        }

        public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
            => _child.BeginRead(buffer, offset, count, callback, state);

        public override int EndRead(IAsyncResult asyncResult)
        {
            var bytesRead = _child.EndRead(asyncResult);
            AssertReadLimit(bytesRead);
            return bytesRead;
        }

        public override async Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            var bytesRead = await _child.ReadAsync(buffer, offset, count, cancellationToken);
            AssertReadLimit(bytesRead);
            return bytesRead;
        }

        public override int ReadByte()
        {
            var v = _child.ReadByte();
            AssertReadLimit(1);
            return v;
        }

        private void AssertReadLimit(int consumed)
        {
            if (_readCapacity == -1) return;
            _readCapacity -= consumed;
            if (_readCapacity < 0) throw new IOException(string.Format(_exceptionMessageFormat, "read"));
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            _child.Write(buffer, offset, count);
            AssertWriteLimit(count);
        }

        public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
        {
            AssertWriteLimit(count);
            return _child.BeginWrite(buffer, offset, count, callback, state);
        }

        public override void EndWrite(IAsyncResult asyncResult)
            => _child.EndWrite(asyncResult);

        public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            AssertWriteLimit(count);
            return _child.WriteAsync(buffer, offset, count, cancellationToken);
        }

        public override void WriteByte(byte value)
        {
            _child.WriteByte(value);
            AssertWriteLimit(1);
        }

        private void AssertWriteLimit(int consumed)
        {
            if (_writeCapacity == -1) return;
            _writeCapacity -= consumed;
            if (_writeCapacity < 0) throw new IOException(string.Format(_exceptionMessageFormat, "write"));
        }
    }
}
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Mastersign.MicroHttpServer
{
    internal class ContentStream : Stream
    {
        private const string _exceptionMessageFormat = "The stream has exceeded the {0} limit specified.";
        private readonly Stream _child;
        private readonly long _length;
        private long _position = 0L;

        public override bool CanRead => _child.CanRead;

        public override bool CanSeek => false;

        public override bool CanWrite => false;

        public override long Length => _length;

        public override long Position
        {
            get => _position;
            set => throw new NotSupportedException();
        }

        public override int ReadTimeout
        {
            get => _child.ReadTimeout;
            set => _child.ReadTimeout = value;
        }

        public override int WriteTimeout
        {
            get => throw new NotSupportedException();
            set => throw new NotSupportedException();
        }

        public ContentStream(Stream child, long length)
        {
            _child = child;
            _length = length;
        }

        public override void Flush()
            => throw new NotSupportedException();

        public override long Seek(long offset, SeekOrigin origin)
            => throw new NotSupportedException();

        public override void SetLength(long value)
            => throw new NotSupportedException();

        public override int Read(byte[] buffer, int offset, int count)
        {
            count = Math.Max(count, (int)(_length - _position));
            var bytesRead = _child.Read(buffer, offset, count);
            _position += bytesRead;
            return bytesRead;
        }

        public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
        {
            count = Math.Max(count, (int)(_length - _position));
            return _child.BeginRead(buffer, offset, count, callback, state);
        }

        public override int EndRead(IAsyncResult asyncResult)
        {
            var bytesRead = _child.EndRead(asyncResult);
            _position += bytesRead;
            return bytesRead;
        }

        public override async Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            count = Math.Max(count, (int)(_length - _position));
            var bytesRead = await _child.ReadAsync(buffer, offset, count, cancellationToken);
            _position += bytesRead;
            return bytesRead;
        }

        public override int ReadByte()
        {
            if (_position >= _length) return -1;
            var v = _child.ReadByte();
            if (v >= 0) _position++;
            return v;
        }

        public override void Write(byte[] buffer, int offset, int count)
            => throw new NotSupportedException();

        public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
            => throw new NotSupportedException();

        public override void EndWrite(IAsyncResult asyncResult)
            => throw new NotSupportedException();

        public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
            => throw new NotSupportedException();

        public override void WriteByte(byte value)
            => throw new NotSupportedException();
    }
}
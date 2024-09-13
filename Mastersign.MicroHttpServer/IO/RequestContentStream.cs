using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Mastersign.MicroHttpServer
{
    internal class RequestContentStream : Stream
    {
        private readonly Stream _child;
        private readonly long _length;
        private long _position = 0L;

        public override bool CanRead => _child.CanRead;

        public override bool CanSeek => false;

        public override bool CanWrite => false;

        public override bool CanTimeout => _child.CanTimeout;

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

        public RequestContentStream(Stream child, long length)
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
            var limitedCount = Math.Max(0, Math.Min(count, (int)(_length - _position)));
            Debug.WriteLine("Sync Read Attempt: requested={0}, limited={1}", count, limitedCount);
            if (limitedCount <= 0) return 0;
            var bytesRead = _child.Read(buffer, offset, limitedCount);
            Debug.WriteLine("Sync Read Result:  requested={0}, limited={1}, read={2}", count, limitedCount, bytesRead);
            _position += bytesRead;
            return bytesRead;
        }

        private class LimitedEmptyReadASyncResult : IAsyncResult
        {
            public object AsyncState { get; set; }

            public WaitHandle AsyncWaitHandle { get; set; }

            public bool CompletedSynchronously => true;

            public bool IsCompleted => true;
        }

        public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
        {
            var limitedCount = Math.Max(0, Math.Min(count, (int)(_length - _position)));
            Debug.WriteLine("Old Async Read Attempt: requested={0}, limited={1}", count, limitedCount);
            if (limitedCount <= 0)
            {
                return new LimitedEmptyReadASyncResult() { AsyncState = state };
            }
            else
            {
                return _child.BeginRead(buffer, offset, limitedCount, callback, state);
            }
        }

        public override int EndRead(IAsyncResult asyncResult)
        {
            if (asyncResult is LimitedEmptyReadASyncResult)
            {
                return 0;
            }
            var bytesRead = _child.EndRead(asyncResult);
            Debug.WriteLine("Old Async Read Result: read={0}", bytesRead);
            _position += bytesRead;
            return bytesRead;
        }

        public override async Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            var limitedCount = Math.Max(0, Math.Min(count, (int)(_length - _position)));
            Debug.WriteLine("Async Read Attempt: requested={0}, limited={1}", count, limitedCount);
            if (limitedCount <= 0) return 0;
            var bytesRead = await _child.ReadAsync(buffer, offset, limitedCount, cancellationToken);
            Debug.WriteLine("Async Read Result: requested={0}, limited={1}, read={2}", count, limitedCount, bytesRead);
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
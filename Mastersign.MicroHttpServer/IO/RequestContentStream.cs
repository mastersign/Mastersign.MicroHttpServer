using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

namespace Mastersign.MicroHttpServer
{
    internal class RequestContentStream : Stream
    {
        private byte[] _prefix;
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

        public RequestContentStream(Stream child, byte[] prefix, long length)
        {
            _prefix = prefix;
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
            int bytesRead;
            if (limitedCount == 0)
            {
                bytesRead = 0;
            }
            else if (_prefix != null && _position < _prefix.LongLength)
            {
                bytesRead = Math.Min(_prefix.Length - (int)_position, limitedCount);
                Debug.WriteLine("Sync Read Prefix: prefix={0}", _prefix.Length);
                Array.Copy(_prefix, (int)_position, buffer, offset, bytesRead);
                _position += bytesRead;
                if (_position >= _prefix.LongLength) _prefix = null;
            }
            else
            {
                bytesRead = _child.Read(buffer, offset, limitedCount);
                _position += bytesRead;
            }
            Debug.WriteLine("Sync Read Result:  requested={0}, limited={1}, read={2}", count, limitedCount, bytesRead);
            return bytesRead;
        }

        private class CompletedAsyncResult : IAsyncResult
        {
            public object AsyncState { get; set; }

            public WaitHandle AsyncWaitHandle { get; set; }

            public bool CompletedSynchronously => true;

            public bool IsCompleted => true;
        }

        private class PrefixReadAsyncResult : CompletedAsyncResult
        {
            public int ReadBytes { get; set; }
        }

        public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
        {
            var limitedCount = Math.Max(0, Math.Min(count, (int)(_length - _position)));
            Debug.WriteLine("Old Async Read Attempt: requested={0}, limited={1}", count, limitedCount);
            if (limitedCount <= 0)
            {
                return new CompletedAsyncResult() { AsyncState = state };
            }
            if (_prefix != null && _position < _prefix.LongLength)
            {
                var bytesRead = Math.Min(_prefix.Length - (int)_position, limitedCount);
                Debug.WriteLine("Old Async Read Prefix: prefix={0}", _prefix.Length);
                Array.Copy(_prefix, (int)_position, buffer, offset, bytesRead);
                return new PrefixReadAsyncResult() { AsyncState = state, ReadBytes = bytesRead };
            }
            return _child.BeginRead(buffer, offset, limitedCount, callback, state);
        }

        public override int EndRead(IAsyncResult asyncResult)
        {
            int bytesRead;
            if (asyncResult is PrefixReadAsyncResult)
            {
                bytesRead = ((PrefixReadAsyncResult)asyncResult).ReadBytes;
                _position += bytesRead;
                if (_position >= _prefix.LongLength) _prefix = null;
            }
            else if (asyncResult is CompletedAsyncResult)
            {
                // limited count was 0
                bytesRead = 0;
            }
            else
            {
                bytesRead = _child.EndRead(asyncResult);
                _position += bytesRead;
            }
            Debug.WriteLine("Old Async Read Result: read={0}", bytesRead);
            return bytesRead;
        }

        public override async Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            var limitedCount = Math.Max(0, Math.Min(count, (int)(_length - _position)));
            Debug.WriteLine("Async Read Attempt: requested={0}, limited={1}", count, limitedCount);
            int bytesRead;
            if (limitedCount == 0)
            { 
                bytesRead = 0;
            }
            else if (_prefix != null && _position < _prefix.LongLength)
            {
                bytesRead = Math.Min(_prefix.Length - (int)_position, limitedCount);
                Debug.WriteLine("Async Read Prefix: prefix={0}", _prefix.Length);
                Array.Copy(_prefix, (int)_position, buffer, offset, bytesRead);
                _position += bytesRead;
                if (_position >= _prefix.LongLength) _prefix = null;
            }
            else
            {
                bytesRead = await _child.ReadAsync(buffer, offset, limitedCount, cancellationToken);
                _position += bytesRead;
            }
            Debug.WriteLine("Async Read Result: requested={0}, limited={1}, read={2}", count, limitedCount, bytesRead);
            return bytesRead;
        }

        public override int ReadByte()
        {
            if (_position >= _length) return -1;
            int v;
            if (_prefix != null && _position < _prefix.LongLength)
            {
                v = _prefix[(int)_position];
                _position++;
                if (_position >= _prefix.LongLength) _prefix = null;
            }
            else
            {
                v = _child.ReadByte();
                if (v >= 0) _position++;
            }
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
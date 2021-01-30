using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Mastersign.MicroHttpServer
{
    internal class PreloadBufferStream : Stream
    {
        private readonly Stream _child;
        private byte[] _preloadBuffer;
        private int _preloadOffset;

        private long _readCapacity;

        public override bool CanRead => _child.CanRead;

        public override bool CanSeek => false;

        public override bool CanWrite => false;

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

        public PreloadBufferStream(Stream child, byte[] preloadBuffer, int preloadOffset, long readLimit = -1)
        {
            _child = child;
            _preloadBuffer = preloadBuffer;
            _preloadOffset = preloadOffset;
            _readCapacity = readLimit;
        }

        public override void Flush() => _child.Flush();

        public override long Seek(long offset, SeekOrigin origin)
            => throw new NotSupportedException();

        public override void SetLength(long value)
            => throw new NotSupportedException();

        public override int Read(byte[] buffer, int offset, int count)
        {
            int bytesRead;
            if (_preloadBuffer != null)
            {
                bytesRead = Math.Min(count, _preloadBuffer.Length - _preloadOffset);
                Array.Copy(_preloadBuffer, _preloadOffset, buffer, offset, bytesRead);
                NotifyReadPreloadBytes(bytesRead);
            }
            else
            {
                bytesRead = _child.Read(buffer, offset, count);
            }
            AssertReadLimit(bytesRead);
            return bytesRead;
        }

        public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
        {
            if (_preloadBuffer != null)
            {
                var bytesRead = Math.Min(count, _preloadBuffer.Length - _preloadOffset);
                Array.Copy(_preloadBuffer, _preloadOffset, buffer, offset, bytesRead);
                NotifyReadPreloadBytes(bytesRead);
                var asyncResult = new AsyncResult(bytesRead, state);
                callback(asyncResult);
                return asyncResult;
            }
            else
            {
                return _child.BeginRead(buffer, offset, count, callback, state);
            }
        }

        public override int EndRead(IAsyncResult asyncResult)
        {
            int bytesRead;
            if (asyncResult is AsyncResult ar)
            {
                bytesRead = ar.BytesRead;
            } 
            else
            {
                bytesRead = _child.EndRead(asyncResult);
            }
            AssertReadLimit(bytesRead);
            return bytesRead;
        }

        public override async Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            int bytesRead;
            if (_preloadBuffer != null)
            {
                bytesRead = Math.Min(count, _preloadBuffer.Length - _preloadOffset);
                Array.Copy(_preloadBuffer, _preloadOffset, buffer, offset, bytesRead);
                NotifyReadPreloadBytes(bytesRead);
            }
            else
            {
                bytesRead = await _child.ReadAsync(buffer, offset, count, cancellationToken);
            }
            AssertReadLimit(bytesRead);
            return bytesRead;
        }

        public override int ReadByte()
        {
            int v;
            if (_preloadBuffer != null)
            {
                v = _preloadBuffer[_preloadOffset];
                NotifyReadPreloadBytes(1);
            }
            else
            {
                v = _child.ReadByte();
            }
            AssertReadLimit(1);
            return v;
        }

        private void NotifyReadPreloadBytes(int bytesRead)
        {
            _preloadOffset += bytesRead;
            if (_preloadOffset >= _preloadBuffer.Length)
            {
                _preloadBuffer = null;
            }
        }

        private void AssertReadLimit(int consumed)
        {
            if (_readCapacity == -1) return;
            _readCapacity -= consumed;
            if (_readCapacity < 0) throw new IOException("Read limit exceeded.");
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

        private class AsyncResult : IAsyncResult
        {
            private static readonly WaitHandle CompletedWaitHandle = new ManualResetEvent(true);

            public int BytesRead { get; }

            public object AsyncState { get; }

            public WaitHandle AsyncWaitHandle => CompletedWaitHandle;

            public bool CompletedSynchronously => true;

            public bool IsCompleted => true;

            public AsyncResult(int bytesRead, object asyncState)
            {
                BytesRead = bytesRead;
                AsyncState = asyncState;
            }
        }
    }
}
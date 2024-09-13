using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Mastersign.MicroHttpServer
{
    internal class NotFlushingStream : Stream
    {
        private readonly Stream _child;

        public override bool CanRead => _child.CanRead;

        public override bool CanSeek => _child.CanSeek;

        public override bool CanWrite => _child.CanWrite;

        public override bool CanTimeout => _child.CanTimeout;

        public override long Length => _child.Length;

        public override long Position
        {
            get => _child.Position;
            set => _child.Position = value;
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

        public NotFlushingStream(Stream child)
        {
            _child = child;
        }

        public void ExplicitFlush() => _child.Flush();

        public Task ExplicitFlushAsync() => _child.FlushAsync();

        public override void Flush()
        {
            // _child.Flush();
        }

        public override long Seek(long offset, SeekOrigin origin) 
            => _child.Seek(offset, origin);

        public override void SetLength(long value)
            => _child.SetLength(value);

        public override int Read(byte[] buffer, int offset, int count)
            => _child.Read(buffer, offset, count);

        public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
            => _child.BeginRead(buffer, offset, count, callback, state);

        public override int EndRead(IAsyncResult asyncResult)
            => _child.EndRead(asyncResult);

        public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
            => _child.ReadAsync(buffer, offset, count, cancellationToken);

        public override int ReadByte() 
            => _child.ReadByte();

        public override void Write(byte[] buffer, int offset, int count) 
            => _child.Write(buffer, offset, count);

        public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
            => _child.BeginWrite(buffer, offset, count, callback, state);

        public override void EndWrite(IAsyncResult asyncResult)
            => _child.EndWrite(asyncResult);

        public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
            => _child.WriteAsync(buffer, offset, count, cancellationToken);

        public override void WriteByte(byte value) 
            => _child.WriteByte(value);
    }
}
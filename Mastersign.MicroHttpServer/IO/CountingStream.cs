using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Mastersign.MicroHttpServer
{
    internal class CountingStream : Stream
    {
        private readonly Stream _child;
        private long _read = 0L;
        private long _written = 0L;

        public CountingStream(Stream child)
        {
            _child = child;
        }

        public long ReadBytes => _read;
        public long WrittenBytes => _written;

        public override bool CanRead => _child.CanRead;

        public override bool CanSeek => false;

        public override bool CanWrite => _child.CanWrite;

        public override bool CanTimeout => _child.CanTimeout;

        public override long Length => _child.Length;

        public override long Position { 
            get => _child.Position;
            set => throw new NotSupportedException(); 
        }

        public override void Flush() 
            => _child.Flush();

        public override Task FlushAsync(CancellationToken cancellationToken)
            => _child.FlushAsync(cancellationToken);

        public override int Read(byte[] buffer, int offset, int count)
        {
            var read = _child.Read(buffer, offset, count);
            _read += read;
            return read;
        }

        public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
            => throw new NotSupportedException();

        public override async Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            var read = await _child.ReadAsync(buffer, offset, count, cancellationToken);
            _read += read;
            return read;
        }


        public override long Seek(long offset, SeekOrigin origin)
            => throw new NotSupportedException();

        public override void SetLength(long value)
            => throw new NotSupportedException();

        public override void Write(byte[] buffer, int offset, int count)
        {
            _child.Write(buffer, offset, count);
            _written += count;
        }

        public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
            => throw new NotSupportedException();

        public override async Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            await _child.WriteAsync(buffer, offset, count, cancellationToken);
            _written += count;
        }

        public override void Close() => _child.Close();
    }
}

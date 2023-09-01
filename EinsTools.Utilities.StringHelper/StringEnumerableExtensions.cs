using System.Data;

namespace EinsTools.Utilities.StringHelper;

/// <summary>
/// Extensions for IEnumerable of string
/// </summary>
public static class StringEnumerableExtensions {
    /// <summary>
    /// Returns the common prefix of all strings in the enumerable. If the enumerable is empty, an
    /// empty string is returned.
    /// </summary>
    /// <param name="values">Input enumerable</param>
    /// <returns>Prefix, all strings have in common</returns>
    public static string CommonPrefix(this IEnumerable<string> values) {
        using var enumerator = values.GetEnumerator();
        if (!enumerator.MoveNext()) return "";
        var prefix = enumerator.Current;
        while (enumerator.MoveNext() && prefix is { Length: > 0 }) {
            var value = enumerator.Current;
            if (value == null) continue;
            for (var k = 0; k < prefix.Length; k++) {
                if (k < value.Length && prefix[k] == value[k]) continue;
                prefix = prefix.Substring(0, k);
                break;
            }
        }
        return prefix ?? "";
    }

    /// <summary>
    /// Returns the common suffix of all strings in the enumerable. If the enumerable is empty, an
    /// empty string is returned.
    /// </summary>
    /// <param name="values">Input enumerable</param>
    /// <returns>Suffix all strings have in common</returns>
    public static string CommonSuffix(this IEnumerable<string> values) {
        return values.Select(it => it.Reverse()).CommonPrefix().Reverse();
    }

    /// <summary>
    /// Converts an enumerable of strings to a stream. The stream contains all strings concatenated with the
    /// specified separator. The default separator is a line feed. The default encoding is UTF-8.
    /// </summary>
    /// <param name="values">Input enumerable</param>
    /// <param name="separator">The string that should separate the entries. The default in "\n". </param>
    /// <param name="encoding">The encoding to use for the strings. The default is UTF-8</param>
    public static Stream ToStream(this IEnumerable<string> values,
        string separator = "\n",
        Encoding? encoding = null) {
        return new SeqStream(values, encoding ?? Encoding.UTF8, separator);
    }

    private class SeqStream : Stream
    {
        private readonly Encoding _encoding;
        private readonly string _separator;
        private readonly IEnumerator<string> _enumerator;
        private byte[]? _current;
        private int _currentPos;
        private bool _isFirst = true;

        public SeqStream(IEnumerable<string> values, Encoding encoding, string separator)
        {
            _encoding = encoding;
            _separator = separator;
            _enumerator = values.GetEnumerator();
        }

        
        public override void Flush()
        {
            throw new ReadOnlyException();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            var total = 0;
            while (true)
            {
                if (_current == null)
                {
                    while (true)
                    {
                        if (!_enumerator.MoveNext()) return total;
                        var c = _enumerator.Current;
                        if (c == null) continue;
                        if (_isFirst)
                        {
                            _isFirst = false;
                        }
                        else
                        {
                            c = _separator + c;
                        }
                        _current = _encoding.GetBytes(c);
                        _currentPos = 0;
                        break;
                    }
                }
                var read = Math.Min(count, _current.Length - _currentPos);
                Array.Copy(_current, _currentPos, buffer, offset, read);
                _currentPos += read;
                Position += read;
                offset += read;
                count -= read;
                total += read;
                if (_currentPos == _current.Length) _current = null;
                if (read == count) return total;

            }

        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new ReadOnlyException();
        }

        public override void SetLength(long value)
        {
            throw new ReadOnlyException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new ReadOnlyException();
        }

        public override bool CanRead => true;
        public override bool CanSeek => false;
        public override bool CanWrite => false;
        public override long Length => -1;
        public override long Position { get; set; }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing) _enumerator.Dispose();
        }
    }
}
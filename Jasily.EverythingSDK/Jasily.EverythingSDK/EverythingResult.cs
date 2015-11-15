using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Jasily.EverythingSDK
{
    internal sealed class EverythingResult : IEnumerable<string>, IDisposable
    {
        private const int MaxFileNameLength = 65536;

        private bool isDisposed;

        public IEverythingSearchParameters Parameters { get; private set; }

        internal EverythingResult(IEverythingSearchParameters parameters)
        {
            this.Parameters = parameters;
        }

        public IEnumerator<string> GetEnumerator()
        {
            var sb = new StringBuilder(MaxFileNameLength);
            for (var i = 0; i < this.GetValue(EverythingAPI.Everything_GetNumResults); i++)
            {
                if (this.isDisposed) throw new ObjectDisposedException("readly begin another search.");
                sb.Clear();
                EverythingAPI.Everything_GetResultFullPathNameW(i, sb, MaxFileNameLength);
                yield return sb.ToString();
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        public void Dispose()
        {
            this.isDisposed = true;
        }

        private T GetValue<T>(Func<T> func)
        {
            if (this.isDisposed) throw new ObjectDisposedException("readly begin another search.");
            return func();
        }

        public int TotalFileCount => this.GetValue(EverythingAPI.Everything_GetTotFileResults);

        public int TotalFolderCount => this.GetValue(EverythingAPI.Everything_GetTotFolderResults);

        public int TotalCount => this.GetValue(EverythingAPI.Everything_GetTotResults);
    }
}
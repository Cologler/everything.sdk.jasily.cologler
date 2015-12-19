using System;
using System.Collections.Generic;
using System.Threading;

namespace Jasily.EverythingSDK
{
    public sealed class EverythingSearch
    {
        private static EverythingResult lastResult;

        public EverythingSearchParameters Parameters { get; private set; }
            = new EverythingSearchParameters();

        public EverythingResult Search(string keyword, int maxCount, int offset = 0)
        {
            if (keyword == null) throw new ArgumentNullException(nameof(keyword));

            // disponse
            var last = Interlocked.Exchange(ref lastResult, null);
            last?.Dispose();

            // reset
            EverythingAPI.Everything_Reset();

            // apply
            var param = this.Parameters.Apply();
            EverythingAPI.Everything_SetSearchW(keyword);
            EverythingAPI.Everything_SetMax(maxCount);
            EverythingAPI.Everything_SetOffset(offset);
            EverythingAPI.Everything_QueryW(true);

            return (lastResult = new EverythingResult(param));
        }

        public IEnumerable<EverythingResult> SearchAll(string keyword, int pageSize)
        {
            var offset = 0;
            EverythingResult ret;
            do
            {
                ret = this.Search(keyword, pageSize, offset);
                yield return ret;
                offset += ret.ResultCount;
            } while (ret.TotalCount > offset + pageSize);
        }
    }
}

namespace Jasily.EverythingSDK
{
    public sealed class EverythingSearchParameters : IEverythingSearchParameters
    {
        public bool IsMatchPath { get; set; }

        public bool IsMatchCase { get; set; }

        public bool IsMatchWholeWord { get; set; }

        public bool IsRegex { get; set; }

        internal IEverythingSearchParameters Apply()
        {
            var clone = new EverythingSearchReadonlyParameters(this);
            EverythingAPI.Everything_SetMatchPath(clone.IsMatchPath);
            EverythingAPI.Everything_SetMatchCase(clone.IsMatchCase);
            EverythingAPI.Everything_SetMatchWholeWord(clone.IsMatchWholeWord);
            EverythingAPI.Everything_SetRegex(clone.IsRegex);
            return clone;
        }

        private sealed class EverythingSearchReadonlyParameters : IEverythingSearchParameters
        {
            public EverythingSearchReadonlyParameters(EverythingSearchParameters parameters)
            {
                this.IsMatchPath = parameters.IsMatchPath;
                this.IsMatchCase = parameters.IsMatchCase;
                this.IsMatchWholeWord = parameters.IsMatchWholeWord;
                this.IsRegex = parameters.IsRegex;
            }

            public bool IsMatchPath { get; }

            public bool IsMatchCase { get; }

            public bool IsMatchWholeWord { get; }

            public bool IsRegex { get; }
        }
    }
}
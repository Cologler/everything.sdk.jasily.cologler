namespace Jasily.EverythingSDK
{
    public interface IEverythingSearchParameters
    {
        bool IsMatchPath { get; }

        bool IsMatchCase { get; }

        bool IsMatchWholeWord { get; }

        bool IsRegex { get; }
    }
}
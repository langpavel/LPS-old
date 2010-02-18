using System;

namespace LPS
{
    public interface ILookupInfo
    {
        string LookupTable { get; }
        string[] LookupColumns { get; }
		string FkListReplaceFormat { get; }
    }
}

using System;

namespace LPS
{
	[Obsolete("Use IDBColumn")]
	public interface IColumnInfo : ILookupInfo, ICloneable
	{
		string Name { get; }
		string Caption { get; }
		bool Visible { get; }
		bool Editable { get; }
		bool Required { get; }
		bool Unique { get; }
		int Width { get; }
		int MaxLength { get; }
		string Default { get; }
		string FkReferenceTable { get; }
		string FkComboReplaceColumns { get; }
		string DisplayFormat { get; }
		string Description { get; }
		bool IsForeignKey { get; }
   	}
}
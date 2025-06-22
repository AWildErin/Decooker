namespace Decooker.Objects.Structs;

/// <summary>
/// C# implementation of FName
/// </summary>
/// @todo	Copy over NameHash from UnName.cpp, probably allows us to easily do string based name caching
///			Figure out how epic does the NAME_ stuff. They're an enum but it doesn't really work fine in here. Seems to be related to ^
///			See UnName.cpp:418, it creates an FName from a TCHAR and will test again the name hash
///			This also means we can likely cut down on string comparisons which isn't a good idea
/// 
public class FName
{
	private FNameEntry entry;

	public int Number { get; private set; }
	public int Index => entry.Index;
	public string Name => entry.Name;
	public string Text => Number > 0 ? $"{entry.Name}_{Number}" : entry.Name;

	public FName( FNameEntry Entry, int Number = 0 )
	{
		entry = Entry;
		this.Number = Number;
	}

	public FName( int Index, int Number = 0 )
	{
		var entry = new FNameEntry
		{
			Index = Index
		};

		this.entry = entry;
		this.Number = Number;
	}

	// @todo Add mode so we can do a find or add. Right now it just adds it without any real index
	// so also relies on the hash map
	public FName( string Name )
	{
		entry = new FNameEntry
		{
			Name = Name,
			Index = 0
		};

		Number = -1;
	}

	public override string ToString()
	{
		return Text;
	}

	public bool IsNone()
	{
		return Name.Equals( "None", StringComparison.OrdinalIgnoreCase );
	}

	// Comparison functions

	public static bool operator ==( FName lhs, FName rhs )
	{
		return lhs.entry.Index == rhs.entry.Index && lhs.Number == rhs.Number;
	}

	public static bool operator !=( FName lhs, FName rhs )
	{
		return !(lhs == rhs);
	}

	public bool StartsWith( string Text )
	{
		return Name.StartsWith( Text, StringComparison.OrdinalIgnoreCase );
	}

	public bool EndsWith( string Text )
	{
		return Name.EndsWith( Text, StringComparison.OrdinalIgnoreCase );
	}

	public bool Contains( string Text )
	{
		return Name.Contains( Text, StringComparison.OrdinalIgnoreCase );
	}

	public bool Equals( string Text )
	{
		return Name.Equals( Text, StringComparison.OrdinalIgnoreCase );
	}
}

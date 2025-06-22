using Decooker.Deserialization;

namespace Decooker.Objects.Structs;

public class FNameEntry : IDeserializable
{
	/// <summary>
	/// EObjectFlags, does not exist on console files
	/// </summary>
	public ulong Flags { get; internal set; } = 0;

	public string Name { get; internal set; } = string.Empty;

	public int Index { get; internal set; } = 0;

	public bool IsUnicode { get; internal set; } = false;


	public void Deserialize( UnrealReader Reader )
	{
		int stringLen = Reader.ReadInt32();

		// We're an unicode string
		if ( stringLen < 0 )
		{
			IsUnicode = true;
			throw new NotImplementedException();
		}
		// Else we're an ansi string
		else
		{
			Name = Reader.ReadNullTerminatedString();
		}

		Flags = Reader.ReadUInt64();
	}
}

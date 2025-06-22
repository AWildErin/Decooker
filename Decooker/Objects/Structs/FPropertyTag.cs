using Decooker.Deserialization;
using Decooker.Objects.Enums;
using System.Diagnostics;

namespace Decooker.Objects.Structs;

public class FPropertyTag
{
	public FName Type { get; private set; } = DefaultNames.NAME_None;
	public FName Name { get; private set; } = DefaultNames.NAME_None;
	public int Size { get; private set; }
	//public int SizeOffset { get; private set; }

	// Basic Values
	public int ArrayIndex { get; private set; }
	public FName StructName { get; private set; } = DefaultNames.NAME_None;
	public FName EnumName { get; private set; } = DefaultNames.NAME_None;
	public byte BoolValue;

	// Uses a different version just so we can properly exit the function
	public bool Deserialize( UnrealReader Reader )
	{
		Name = Reader.ReadName();
		if ( Name.IsNone() )
		{
			return false;
		}

		Type = Reader.ReadName();
		Size = Reader.ReadInt32();
		ArrayIndex = Reader.ReadInt32();

		if ( Type.Equals( "StructProperty" ) )
		{
			StructName = Reader.ReadName();
		}
		else if ( Type.Equals( "BoolProperty" ) )
		{
			if ( Reader.Version < (int)EUnrealEngineObjectVersion.VER_PROPERTYTAG_BOOL_OPTIMIZATION )
			{
				int value = Reader.ReadInt32();
				BoolValue = (byte)value;
			}
			else
			{
				// Check to see if this ReadByte is correct on byte swapped
				throw new NotImplementedException();
			}
		}
		else if ( Type.Equals( "ByteProperty" ) && Reader.Version >= (int)EUnrealEngineObjectVersion.VER_BYTEPROP_SERIALIZE_ENUM )
		{
			EnumName = Reader.ReadName();
		}

		return true;
	}
}

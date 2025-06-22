using Decooker.Deserialization;
using Decooker.Objects.Enums;

namespace Decooker.Objects.Properties;
public class UStruct : UField
{
	public UStruct? SuperStruct { get; private set; }
	public UField? Children { get; private set; }

	// Private for now, we don't deal with script stuff but nice to keep serialized
	private int scriptBytecodeSize;
	private int scriptStorageSize;

	public override void Deserialize( UnrealReader Reader )
	{
		base.Deserialize( Reader );

		if ( Reader.Version >= (int)EUnrealEngineObjectVersion.VER_MOVED_SUPERFIELD_TO_USTRUCT )
		{
			SuperStruct = Reader.ReadObject<UStruct>();
		}

		// @TODO Handle bIsCookedForConsole to get script text on non console cooked packages

		Children = Reader.ReadObject<UField>();

		// @TODO Same as above, but for cpp text, line and textpos

		scriptBytecodeSize = Reader.ReadInt32();
		if ( Reader.Version >= (int)EUnrealEngineObjectVersion.VER_USTRUCT_SERIALIZE_ONDISK_SCRIPTSIZE )
		{
			scriptStorageSize = Reader.ReadInt32();
		}
	}
}

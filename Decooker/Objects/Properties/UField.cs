using Decooker.Deserialization;
using Decooker.Objects.Enums;

namespace Decooker.Objects.Properties;

public class UField : UObject
{
	// Only valid for versions < 756
	public UField? SuperField { get; private set; }
	public UField? Next { get; private set; }

	public override void Deserialize( UnrealReader Reader )
	{
		base.Deserialize( Reader );

		if ( Reader.Version < (int)EUnrealEngineObjectVersion.VER_MOVED_SUPERFIELD_TO_USTRUCT )
		{
			SuperField = Reader.ReadObject<UField>();
			// @todo Cast to struct here and set the superstruct
		}

		Next = Reader.ReadObject<UField>();
	}
}

using Decooker.Deserialization;
using Decooker.Objects.Enums;
using Decooker.Objects.Properties;
using Decooker.Objects.Structs;
namespace Decooker.Objects;

public class UClass : UStruct
{
	public EClassFlags ClassFlags { get; private set; }
	public UClass? ClassWithin { get; private set; }
	public FName ClassConfigName { get; private set; } = DefaultNames.NAME_None;

	public override void Deserialize( UnrealReader Reader )
	{
		base.Deserialize( Reader );

		ClassFlags = (EClassFlags)Reader.ReadUInt32();
		ClassWithin = Reader.ReadObject<UClass>();
		ClassConfigName = Reader.ReadName();


		// Handle the #if !CONSOLE block
	}
}

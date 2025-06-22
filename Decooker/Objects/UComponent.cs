using Decooker.Deserialization;
using Decooker.Objects.Enums;
using Decooker.Objects.Structs;

namespace Decooker.Objects;

public class UComponent : UObject
{
	public UClass? TemplateOwnerClass { get; private set; }
	public FName TemplateName { get; private set; } = DefaultNames.NAME_None;

	public void PreDeserialize( UnrealReader Reader )
	{
		TemplateOwnerClass = Reader.ReadObject<UClass>();

		if ( EnumerateOuter().Any( obj => obj.ObjectExport != null && obj.ObjectExport.ObjectFlags.HasFlag( EObjectFlags.RF_ClassDefaultObject ) ) )
		{
			TemplateName = Reader.ReadName();
		}
	}
}

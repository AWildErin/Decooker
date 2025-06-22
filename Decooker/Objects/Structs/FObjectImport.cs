using Decooker.Deserialization;

namespace Decooker.Objects.Structs;

public class FObjectImport : FObjectResource, IDeserializable
{
	public FName ClassPackage { get; private set; } = DefaultNames.NAME_None;
	public FName ClassName { get; private set; } = DefaultNames.NAME_None;
	public int SourceIndex { get; private set; }

	public UObject? Object { get; internal set; }
	public int PackageIndex { get; internal set; }

	//public FObjectImport( int SourceIndex )
	//{
	//	this.SourceIndex = SourceIndex;
	//}

	public void Deserialize( UnrealReader Reader )
	{
		ClassPackage = Reader.ReadName();
		ClassName = Reader.ReadName();
		OuterIndex = Reader.ReadInt32();
		ObjectName = Reader.ReadName();
	}
}

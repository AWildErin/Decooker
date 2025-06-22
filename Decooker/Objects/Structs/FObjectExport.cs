using Decooker.Deserialization;
using Decooker.Objects.Enums;

namespace Decooker.Objects.Structs;

public class FObjectExport : FObjectResource, IDeserializable
{
	public int ClassIndex { get; private set; }
	public int SuperIndex { get; private set; }
	public int ArchetypeIndex { get; private set; }
	public EObjectFlags ObjectFlags { get; private set; }
	public int SerialSize { get; private set; }
	public int SerialOffset { get; private set; }
	public EExportFlags ExportFlags { get; private set; }
	public List<int> GenerationNetObjectCount { get; private set; } = new();
	public FGuid Guid { get; private set; } = new();
	public EPackageFlags PackageFlags { get; private set; }

	public UObject? Object { get; internal set; }
	public int PackageIndex { get; internal set; }

	public void Deserialize( UnrealReader Reader )
	{
		long startPos = Reader.BaseStream.Position;

		ClassIndex = Reader.ReadInt32();
		SuperIndex = Reader.ReadInt32();
		OuterIndex = Reader.ReadInt32();
		ObjectName = Reader.ReadName();
		ArchetypeIndex = Reader.ReadInt32();
		ObjectFlags = (EObjectFlags)Reader.ReadUInt64();
		SerialSize = Reader.ReadInt32();
		SerialOffset = Reader.ReadInt32();

		if ( Reader.Version < (int)EUnrealEngineObjectVersion.VER_REMOVED_COMPONENT_MAP )
		{
			throw new NotImplementedException();
		}

		ExportFlags = (EExportFlags)Reader.ReadUInt32();

		int arrayCount = Reader.ReadInt32();
		for ( int i = 0; i < arrayCount; i++ )
		{
			GenerationNetObjectCount.Add( Reader.ReadInt32() );
		}

		Guid = Reader.ReadGuid();
		PackageFlags = (EPackageFlags)Reader.ReadUInt32();
	}
}

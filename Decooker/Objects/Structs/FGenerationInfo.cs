using Decooker.Deserialization;

namespace Decooker.Objects.Structs;

public class FGenerationInfo : IDeserializable
{
	public int ExportCount { get; private set; }
	public int NameCount { get; private set; }
	public int NetObjectCount { get; private set; }

	public void Deserialize( UnrealReader Reader )
	{
		ExportCount = Reader.ReadInt32();
		NameCount = Reader.ReadInt32();
		NetObjectCount = Reader.ReadInt32();
	}
}

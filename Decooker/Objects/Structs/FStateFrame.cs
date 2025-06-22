using Decooker.Deserialization;
using Decooker.Objects.Enums;
using Decooker.Objects.Properties;

namespace Decooker.Objects.Structs;

public class FStateFrame : IDeserializable
{
	public class FPushedState : IDeserializable
	{
		// @TODO Needs to be UState
		public UStruct? State { get; private set; }
		public UStruct? Node { get; private set; }
		public byte Code { get; private set; }

		public void Deserialize( UnrealReader Reader )
		{
			State = Reader.ReadObject<UStruct>();
			Node = Reader.ReadObject<UStruct>();
		}
	}

	public UStruct? Node { get; private set; }
	// @TODO Needs to be UState
	public UStruct? StateNode { get; private set; }
	public ulong ProbeMask { get; private set; }
	public ushort LatentAction { get; private set; }

	public List<FPushedState> StateStack { get; private set; } = new();

	public void Deserialize( UnrealReader Reader )
	{
		Node = Reader.ReadObject<UStruct>();
		StateNode = Reader.ReadObject<UStruct>();

		if ( Reader.Version < (int)EUnrealEngineObjectVersion.VER_REDUCED_PROBEMASK_REMOVED_IGNOREMASK )
		{
			Reader.ReadUInt64();
		}
		else
		{
			ProbeMask = Reader.ReadUInt64();
		}

		if ( Reader.Version < (int)EUnrealEngineObjectVersion.VER_REDUCED_STATEFRAME_LATENTACTION_SIZE )
		{
			Reader.ReadInt32();
		}
		else
		{
			LatentAction = Reader.ReadUInt16();
		}

		int count = Reader.ReadInt32();
		for ( int i = 0; i < count; i++ )
		{
			FPushedState state = new();
			state.Deserialize( Reader );
			StateStack.Add( state );
		}

		if ( Node != null )
		{
			// OFFSet
			Reader.ReadInt32();
		}
	}
}

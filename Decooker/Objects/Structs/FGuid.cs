using Decooker.Attributes;
using Decooker.Deserialization;
using Decooker.Objects.Properties;

namespace Decooker.Objects.Structs;

/// <summary>
/// Specific Unreal Engine Guid implementation
/// </summary>
/// @todo Doesn't seem to read correctly?
[UnrealStruct( "Guid", true )]
public class FGuid : IUnrealAtomicStruct
{
	public uint A { get; private set; }
	public uint B { get; private set; }
	public uint C { get; private set; }
	public uint D { get; private set; }

	public FGuid( uint A, uint B, uint C, uint D )
	{
		this.A = A;
		this.B = B;
		this.C = C;
		this.D = D;
	}

	public FGuid()
	{
		A = 0;
		B = 0;
		C = 0;
		D = 0;
	}

	public override string ToString()
	{
		return $"{A:X8}{B:X8}{C:X8}{D:X8}";
	}

	public void ReadStruct( UnrealReader Reader, UStructProperty Struct )
	{
		var aProperty = new UIntProperty( "A", Reader.ReadInt32() );
		var bProperty = new UIntProperty( "B", Reader.ReadInt32() );
		var cProperty = new UIntProperty( "C", Reader.ReadInt32() );
		var dProperty = new UIntProperty( "D", Reader.ReadInt32() );

		Struct.Properties.Add( aProperty );
		Struct.Properties.Add( bProperty );
		Struct.Properties.Add( cProperty );
		Struct.Properties.Add( dProperty );
	}
}

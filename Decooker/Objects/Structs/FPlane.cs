using Decooker.Attributes;
using Decooker.Deserialization;
using Decooker.Objects.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decooker.Objects.Structs;

[UnrealStruct( "FPlane", true )]
public class FPlane : IUnrealAtomicStruct, IDeserializable
{
	public float X { get; private set; }
	public float Y { get; private set; }
	public float Z { get; private set; }
	public float W { get; private set; }

	public void ReadStruct( UnrealReader Reader, UStructProperty Struct )
	{
		var xProperty = new UFloatProperty( "X", Reader.ReadSingle() );
		var yProperty = new UFloatProperty( "Y", Reader.ReadSingle() );
		var zProperty = new UFloatProperty( "Z", Reader.ReadSingle() );
		var wProperty = new UFloatProperty( "W", Reader.ReadSingle() );

		Struct.Properties.Add( xProperty );
		Struct.Properties.Add( yProperty );
		Struct.Properties.Add( zProperty );
		Struct.Properties.Add( wProperty );
	}

	public void Deserialize( UnrealReader Reader )
	{
		X = Reader.ReadSingle();
		Y = Reader.ReadSingle();
		Z = Reader.ReadSingle();
		W = Reader.ReadSingle();
	}
}

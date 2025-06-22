using Decooker.Attributes;
using Decooker.Deserialization;
using Decooker.Objects.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decooker.Objects.Structs;

[UnrealStruct( "Vector4", true )]
public class FVector4 : IUnrealAtomicStruct
{
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
}

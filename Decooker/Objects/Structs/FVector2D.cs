using Decooker.Attributes;
using Decooker.Deserialization;
using Decooker.Objects.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decooker.Objects.Structs;

[UnrealStruct( "Vector2D", true )]
public struct FVector2D : IUnrealAtomicStruct
{
	public void ReadStruct( UnrealReader Reader, UStructProperty Struct )
	{
		var xProperty = new UFloatProperty( "X", Reader.ReadSingle() );
		var yProperty = new UFloatProperty( "Y", Reader.ReadSingle() );

		Struct.Properties.Add( xProperty );
		Struct.Properties.Add( yProperty );
	}
}

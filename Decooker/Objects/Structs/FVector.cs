using Decooker.Attributes;
using Decooker.Deserialization;
using Decooker.Objects.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decooker.Objects.Structs;

/// <summary>
/// Used mainly to handle deserializing Scale3D., 
/// </summary>
[UnrealStruct( "Scale3D", true )]
[UnrealStruct( "Vector", true )]
public class FVector : IUnrealAtomicStruct
{
	public void ReadStruct( UnrealReader Reader, UStructProperty Struct )
	{
		var xProperty = new UFloatProperty( "X", Reader.ReadSingle() );
		var yProperty = new UFloatProperty( "Y", Reader.ReadSingle() );
		var zProperty = new UFloatProperty( "Z", Reader.ReadSingle() );

		Struct.Properties.Add( xProperty );
		Struct.Properties.Add( yProperty );
		Struct.Properties.Add( zProperty );
	}
}

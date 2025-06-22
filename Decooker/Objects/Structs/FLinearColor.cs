using Decooker.Attributes;
using Decooker.Deserialization;
using Decooker.Objects.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decooker.Objects.Structs;

[UnrealStruct("LinearColor", true)]
public class FLinearColor : IUnrealAtomicStruct
{
	public void ReadStruct( UnrealReader Reader, UStructProperty Struct )
	{
		var rProperty = new UFloatProperty( "R", Reader.ReadSingle() );
		var gProperty = new UFloatProperty( "G", Reader.ReadSingle() );
		var bProperty = new UFloatProperty( "B", Reader.ReadSingle() );
		var aProperty = new UFloatProperty( "A", Reader.ReadSingle() );

		Struct.Properties.Add( rProperty );
		Struct.Properties.Add( gProperty );
		Struct.Properties.Add( bProperty );
		Struct.Properties.Add( aProperty );
	}
}

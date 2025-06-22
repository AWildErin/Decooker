using Decooker.Attributes;
using Decooker.Deserialization;
using Decooker.Objects.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decooker.Objects.Structs;

[UnrealStruct("Color", true)]
public class FColor : IUnrealAtomicStruct
{
	public void ReadStruct( UnrealReader Reader, UStructProperty Struct )
	{
		var rProperty = new UByteProperty( "R", Reader.ReadByte() );
		var gProperty = new UByteProperty( "G", Reader.ReadByte() );
		var bProperty = new UByteProperty( "B", Reader.ReadByte() );
		var aProperty = new UByteProperty( "A", Reader.ReadByte() );

		Struct.Properties.Add( rProperty );
		Struct.Properties.Add( gProperty );
		Struct.Properties.Add( bProperty );
		Struct.Properties.Add( aProperty );
	}
}

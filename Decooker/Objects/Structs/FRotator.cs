using Decooker.Attributes;
using Decooker.Deserialization;
using Decooker.Objects.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decooker.Objects.Structs;

[UnrealStruct( "Rotator", true )]
public class FRotator : IUnrealAtomicStruct
{
	public void ReadStruct( UnrealReader Reader, UStructProperty Struct )
	{
		var pitchProperty = new UIntProperty( "Pitch", Reader.ReadInt32() );
		var yawProperty = new UIntProperty( "Yaw", Reader.ReadInt32() );
		var rollProperty = new UIntProperty( "Roll", Reader.ReadInt32() );

		Struct.Properties.Add( pitchProperty );
		Struct.Properties.Add( yawProperty );
		Struct.Properties.Add( rollProperty );
	}
}

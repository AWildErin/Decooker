using Decooker.Attributes;
using Decooker.Deserialization;
using Decooker.Objects.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decooker.Objects.Structs;

[UnrealStruct( "ActorReference", true )]
public class FActorReference : IUnrealAtomicStruct
{
	public void ReadStruct( UnrealReader Reader, UStructProperty Struct )
	{
		var actorProperty = new UObjectProperty( "Actor", Reader.ReadObject() );

		var guidProperty = new UStructProperty( "Guid" );
		var guid = Reader.ReadGuid();
		guidProperty.Properties.Add( new UFloatProperty( "A", guid.A ) );
		guidProperty.Properties.Add( new UFloatProperty( "B", guid.B ) );
		guidProperty.Properties.Add( new UFloatProperty( "C", guid.C ) );
		guidProperty.Properties.Add( new UFloatProperty( "D", guid.D ) );

		Struct.Properties.Add( actorProperty );
		Struct.Properties.Add( guidProperty );
	}
}

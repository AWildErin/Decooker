using Decooker.Attributes;
using Decooker.Deserialization;
using Decooker.Objects.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decooker.Objects.Structs;

[UnrealStruct("Box", true)]
public class FBox : IUnrealAtomicStruct
{
	public void ReadStruct( UnrealReader Reader, UStructProperty Struct )
	{
		var vectorOne = new UStructProperty();
		var vectorOneType = new FVector();
		vectorOneType.ReadStruct( Reader, Struct );


		var vectorTwo = new UStructProperty();
		var vectorTwoType = new FVector();
		vectorTwoType.ReadStruct( Reader, Struct );

		vectorOne.Name = new FName( "Min" );
		vectorTwo.Name = new FName( "Max" );

		Struct.Properties.Add( vectorOne );
		Struct.Properties.Add( vectorTwo );

		var isValid = new UByteProperty("IsValid", Reader.ReadByte());
		Struct.Properties.Add( isValid );
	}
}

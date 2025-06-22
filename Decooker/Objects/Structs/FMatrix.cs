using Decooker.Attributes;
using Decooker.Deserialization;
using Decooker.Objects.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decooker.Objects.Structs;

[UnrealStruct( "Matrix", true )]
public class FMatrix : IUnrealAtomicStruct, IDeserializable
{

	public float[,] M { get; private set; } = new float[4, 4];

	// UProperty
	public void ReadStruct( UnrealReader Reader, UStructProperty Struct )
	{
		var plane = new FPlane();

		var xProperty = new UStructProperty( "XPlane" );
		var yProperty = new UStructProperty( "YPlane" );
		var zProperty = new UStructProperty( "ZPlane" );
		var wProperty = new UStructProperty( "WPlane" );

		plane.ReadStruct( Reader, xProperty );
		plane.ReadStruct( Reader, yProperty );
		plane.ReadStruct( Reader, zProperty );
		plane.ReadStruct( Reader, wProperty );

		Struct.Properties.Add( xProperty );
		Struct.Properties.Add( yProperty );
		Struct.Properties.Add( zProperty );
		Struct.Properties.Add( wProperty );
	}

	// Native
	public void Deserialize( UnrealReader Reader )
	{
		for ( int i = 0; i < 4; i++ )
		{
			for ( int j = 0; j < 4; j++ )
			{
				M[i, j] = Reader.ReadSingle();
			}
		}
	}
}

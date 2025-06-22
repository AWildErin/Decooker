using Decooker.Attributes;
using Decooker.Deserialization;
using Decooker.Extensions;
using Decooker.Objects.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decooker.Objects.Properties;

[UnrealClass( "FloatProperty" )]
public class UFloatProperty : UProperty
{
	public float Value { get; private set; }

	public UFloatProperty() { }

	public UFloatProperty( float Value )
	{
		this.Value = Value;
	}

	public UFloatProperty( string Name, float Value )
		: base( Name )
	{
		this.Value = Value;
	}

	internal override void DeserializeItem( UnrealReader Reader )
	{
		Value = Reader.ReadSingle();
	}

	public override string GetValueAsString()
	{
		return Value.ToString();
	}
}

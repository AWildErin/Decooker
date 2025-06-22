using Decooker.Attributes;
using Decooker.Deserialization;
using Decooker.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decooker.Objects.Properties;

[UnrealClass( "IntProperty" )]
public class UIntProperty : UProperty
{
	public int Value { get; private set; }

	public UIntProperty() { }

	public UIntProperty( int Value )
	{
		this.Value = Value;
	}

	public UIntProperty( string Name, int Value )
		: base( Name )
	{
		this.Value = Value;
	}

	internal override void DeserializeItem( UnrealReader Reader )
	{
		Value = Reader.ReadInt32();
	}

	public override string GetValueAsString()
	{
		return Value.ToString();
	}
}

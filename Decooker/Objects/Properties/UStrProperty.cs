using Decooker.Attributes;
using Decooker.Deserialization;
using Decooker.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decooker.Objects.Properties;

[UnrealClass( "StrProperty" )]
public class UStrProperty : UProperty
{
	public string Value { get; private set; } = string.Empty;

	public UStrProperty() { }

	public UStrProperty( string Value )
	{
		this.Value = Value;
	}

	public UStrProperty( string Name, string Value )
		: base( Name )
	{
		this.Value = Value;
	}

	internal override void DeserializeItem( UnrealReader Reader )
	{
		Value = Reader.ReadString();
	}

	public override string GetValueAsString()
	{
		return Value.ToString();
	}
}

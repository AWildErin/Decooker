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

[UnrealClass("ByteProperty")]
public class UByteProperty : UProperty
{
	public FName? EnumName { get; private set; }
	public byte Value { get; private set; }

	public UByteProperty() { }

	public UByteProperty( byte Value )
	{
		this.Value = Value;
	}

	public UByteProperty( string Name, byte Value )
		: base( Name )
	{
		this.Value = Value;
	}

	internal override void DeserializeItem( UnrealReader Reader )
	{
		// @todo bUseBinarySerialization, we use this for FColor too which isn't ideal at all

		if ( Tag?.Size != 1 )
		{
			EnumName = Reader.ReadName();
		}
		else
		{
			Value = Reader.ReadByte();
		}
	}

	public override string GetValueAsString()
	{
		return EnumName is null ? Value.ToString() : EnumName.ToString();
	}
}

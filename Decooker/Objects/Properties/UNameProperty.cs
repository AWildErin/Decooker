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

[UnrealClass( "NameProperty" )]
public class UNameProperty : UProperty
{
	public FName Value { get; private set; } = DefaultNames.NAME_None;

	internal override void DeserializeItem( UnrealReader Reader )
	{
		Value = Reader.ReadName();
	}

	public override string GetValueAsString()
	{
		return Value.ToString();
	}
}

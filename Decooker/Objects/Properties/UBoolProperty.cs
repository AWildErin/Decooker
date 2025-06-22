using Decooker.Attributes;
using Decooker.Deserialization;
using Decooker.Extensions;
using Decooker.Objects.Enums;
using System.Text;

namespace Decooker.Objects.Properties;

[UnrealClass( "BoolProperty" )]
public class UBoolProperty : UProperty
{
	public bool Value { get; private set; }

	public UBoolProperty() { }

	public UBoolProperty( bool Value )
	{
		this.Value = Value;
	}

	public UBoolProperty( string Name, bool Value )
		: base( Name )
	{
		this.Value = Value;
	}

	internal override void DeserializeItem( UnrealReader Reader )
	{
		if ( Tag?.Size == 0 )
		{
			return;
		}

		Value = Reader.ReadByte() > 0;
	}

	public override string GetValueAsString()
	{
		return Value.ToString();
	}
}

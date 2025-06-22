using Decooker.Attributes;
using Decooker.Deserialization;
using Decooker.Extensions;
using System.Text;

namespace Decooker.Objects.Properties;

[UnrealClass( "ObjectProperty" )]
public class UObjectProperty : UProperty
{
	public UObject? Value { get; private set; }

	public UObjectProperty() { }

	public UObjectProperty( UObject Value )
	{
		this.Value = Value;
	}

	public UObjectProperty( string Name, UObject? Value )
		: base( Name )
	{
		this.Value = Value;
	}

	internal override void DeserializeItem( UnrealReader Reader )
	{
		Value = Reader.ReadObject();
	}

	public override string GetValueAsString()
	{
		return Value is not null ? Value.GetReferencePath() : "None";
	}
}

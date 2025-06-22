using Decooker.Deserialization;
using Decooker.Extensions;
using Decooker.Objects.Structs;
using System.Diagnostics;
using System.Text;

namespace Decooker.Objects.Properties;

[DebuggerDisplay("{GetDebugString()}")]
public class UProperty : UField
{
	public FPropertyTag? Tag { get; internal set; }
	public UObject? OwnerObject { get; internal set; }

	public UProperty()
	{

	}

	public UProperty(string Name)
	{
		this.Name = new( Name );
	}

	public override void Deserialize( UnrealReader Reader )
	{
		base.Deserialize( Reader );
	}

	internal virtual void DeserializeItem( UnrealReader Reader )
	{
		throw new NotImplementedException();
	}

	public virtual string GetValueAsString()
	{
		throw new NotImplementedException();
	}

	public virtual string GetDebugString()
	{
		return $"{Tag?.Name} : {GetValueAsString()}";
	}
}

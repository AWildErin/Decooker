using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decooker.Attributes;

[AttributeUsage( AttributeTargets.Property, Inherited = false, AllowMultiple = false )]
public sealed class UnrealPropertyAttribute : Attribute
{
	public string PropertyName { get; private set; }

	public UnrealPropertyAttribute( string PropertyName )
	{
		this.PropertyName = PropertyName;
	}
}

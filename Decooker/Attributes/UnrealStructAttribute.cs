using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decooker.Attributes;

/// <summary>
/// Defines a struct type used by UStructProperty
/// </summary>
[AttributeUsage( AttributeTargets.Class | AttributeTargets.Struct, Inherited = false, AllowMultiple = true )]
public sealed class UnrealStructAttribute : Attribute
{
	public string StructName { get; private set; }
	public bool IsAtomic { get; private set; }

	public UnrealStructAttribute( string StructName, bool IsAtomic = false )
	{
		this.StructName = StructName;
		this.IsAtomic = IsAtomic;
	}
}

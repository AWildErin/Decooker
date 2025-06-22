namespace Decooker.Attributes;

[System.AttributeUsage( AttributeTargets.Class, Inherited = false )]
public sealed class UnrealClassAttribute : Attribute
{
	public string ClassName { get; private set; }

	// This is a positional argument
	public UnrealClassAttribute( string ClassName )
	{
		this.ClassName = ClassName;
	}
}

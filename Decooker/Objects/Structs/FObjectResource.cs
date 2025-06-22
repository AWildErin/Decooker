namespace Decooker.Objects.Structs;

public class FObjectResource
{
	public FName ObjectName { get; protected set; } = new( 0, 0 );
	public int OuterIndex { get; protected set; }
}

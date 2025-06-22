namespace Decooker.Exceptions.Name;
internal class BadNameIndexException : Exception
{
	public BadNameIndexException()
	{
	}

	public BadNameIndexException( string? message ) : base( message )
	{
	}

	public BadNameIndexException( string? message, Exception? innerException ) : base( message, innerException )
	{
	}
}

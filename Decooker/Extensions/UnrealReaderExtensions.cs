using Decooker.Deserialization;
using Decooker.Objects;
using Decooker.Objects.Structs;

namespace Decooker.Extensions;

/// <summary>
/// Handles reading TArray and TMap types
/// </summary>
/// @todo Handle IDeserializable in ReadValue
public static class UnrealReaderExtensions
{
	public static object ReadValue( this UnrealReader Reader, Type ObjectType )
	{
		object value = ObjectType switch
		{
			Type t when t == typeof( float ) => Reader.ReadSingle(),
			Type t when t == typeof( byte ) => Reader.ReadByte(),
			Type t when t == typeof( sbyte ) => Reader.ReadSByte(),
			Type t when t == typeof( ushort ) => Reader.ReadUInt16(),
			Type t when t == typeof( short ) => Reader.ReadInt16(),
			Type t when t == typeof( uint ) => Reader.ReadUInt32(),
			Type t when t == typeof( int ) => Reader.ReadInt32(),
			Type t when t == typeof( ulong ) => Reader.ReadUInt64(),
			Type t when t == typeof( long ) => Reader.ReadInt64(),

			Type t when t == typeof( FName ) => Reader.ReadName(),
			Type t when t == typeof( FGuid ) => Reader.ReadGuid(),

			// @TODO Don't do new here!
			Type t when typeof( UObject ).IsAssignableFrom( t ) => Reader.ReadObject() ?? new(),

			_ => throw new NotSupportedException(),
		};

		return value;
	}

	public static T ReadValue<T>( this UnrealReader Reader )
	{
		var obj = Reader.ReadValue( typeof( T ) );
		return (T)obj;
	}

	public static void ReadMap<T1, T2>( this UnrealReader Reader, out Dictionary<T1, T2> Dict )
		where T1 : notnull
	{
		Dict = new();

		int count = Reader.ReadInt32();

		for ( int i = 0; i < count; i++ )
		{
			T1 key = Reader.ReadValue<T1>();
			T2 value = Reader.ReadValue<T2>();
			Dict[key] = value;
		}
	}
}

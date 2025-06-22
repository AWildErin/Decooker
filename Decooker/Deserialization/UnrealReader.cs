using Decooker.Core;
using Decooker.Objects;
using Decooker.Objects.Structs;
using System.Buffers.Binary;
using System.Text;

namespace Decooker.Deserialization;


/// <summary>
/// Binary reader with endian support
/// </summary>
/// @todo Ref support for readers?
public class UnrealReader : BinaryReader
{
	// Properties
	public bool IsByteSwapping { get; set; } = false;

	public int Version { get; set; }
	public int LicenseeVersion { get; set; }

	// @todo rework this, we need it for stuff like names but 
	// we probably should construct UnrealPackage with a path ctor, then
	// have that construct UnrealReader
	public UnrealPackage? Package { get; set; }

	public UnrealReader( Stream input ) : base( input )
	{
	}

	public UnrealReader( Stream input, Encoding encoding ) : base( input, encoding )
	{
	}

	public UnrealReader( Stream input, Encoding encoding, bool leaveOpen ) : base( input, encoding, leaveOpen )
	{
	}

	//
	// Unreal Typess
	//

	public FName ReadName()
	{
		int index = ReadInt32();
		// @todo Only in >= 343, need to add those to EUnrealEngineObjectVersions
		int number = ReadInt32();

		if ( Package?.Names.ElementAtOrDefault( index ) == null )
		{
			throw new Exception( $"Bad name index {index}/{Package?.Names.Count()}" );
		}

		return new FName( Package.Names[index], number );
	}

	public FGuid ReadGuid()
	{
		uint a = ReadUInt32();
		uint b = ReadUInt32();
		uint c = ReadUInt32();
		uint d = ReadUInt32();

		return new FGuid( a, b, c, d );
	}

	public T? ReadObject<T>()
		where T : UObject, new()
	{
		int index = ReadInt32();
		return Package?.IndexToObject<T>( index );
	}

	public UObject? ReadObject()
	{
		int index = ReadInt32();
		return Package?.IndexToObject<UObject>( index );
	}

	//
	// C# types
	//


	public override unsafe float ReadSingle()
	{
		Span<byte> buffer = stackalloc byte[4];
		int read = BaseStream.Read( buffer );

		return IsByteSwapping
			? BinaryPrimitives.ReadSingleBigEndian( buffer )
			: BinaryPrimitives.ReadSingleLittleEndian( buffer );
	}


	public override short ReadInt16()
	{
		Span<byte> buffer = stackalloc byte[2];
		int read = BaseStream.Read( buffer );

		return IsByteSwapping
			? BinaryPrimitives.ReadInt16BigEndian( buffer )
			: BinaryPrimitives.ReadInt16LittleEndian( buffer );
	}

	public override ushort ReadUInt16()
	{
		Span<byte> buffer = stackalloc byte[2];
		int read = BaseStream.Read( buffer );

		return IsByteSwapping
			? BinaryPrimitives.ReadUInt16BigEndian( buffer )
			: BinaryPrimitives.ReadUInt16LittleEndian( buffer );
	}

	public override int ReadInt32()
	{
		Span<byte> buffer = stackalloc byte[4];
		int read = BaseStream.Read( buffer );

		return IsByteSwapping
			? BinaryPrimitives.ReadInt32BigEndian( buffer )
			: BinaryPrimitives.ReadInt32LittleEndian( buffer );
	}

	public override uint ReadUInt32()
	{
		Span<byte> buffer = stackalloc byte[4];
		int read = BaseStream.Read( buffer );

		return IsByteSwapping
			? BinaryPrimitives.ReadUInt32BigEndian( buffer )
			: BinaryPrimitives.ReadUInt32LittleEndian( buffer );
	}

	public override long ReadInt64()
	{
		Span<byte> buffer = stackalloc byte[8];
		int read = BaseStream.Read( buffer );

		return IsByteSwapping
			? BinaryPrimitives.ReadInt64BigEndian( buffer )
			: BinaryPrimitives.ReadInt64LittleEndian( buffer );
	}

	public override ulong ReadUInt64()
	{
		Span<byte> buffer = stackalloc byte[8];
		int read = BaseStream.Read( buffer );

		return IsByteSwapping
			? BinaryPrimitives.ReadUInt64BigEndian( buffer )
			: BinaryPrimitives.ReadUInt64LittleEndian( buffer );
	}

	public override string ReadString()
	{
		int length = ReadInt32();

		// Ansi
		if ( length >= 0 )
		{
			byte[] buffer = new byte[length];
			for ( int i = 0; i < length; i++ )
			{
				buffer[i] = ReadByte();
			}

			return buffer[length - 1] == '\0'
				? Encoding.ASCII.GetString( buffer, 0, buffer.Length - 1 )
				: Encoding.ASCII.GetString( buffer, 0, buffer.Length );
		}
		// Unicode
		else
		{
			length = (-length * 2);

			byte[] buffer = new byte[length];
			for (int i =0; i < length; i++ )
			{
				buffer[i] = ReadByte();
			}

			// @todo I'm not sure if the -2 is correct here, but it works
			return buffer[length - 2] == '\0'
				? Encoding.Unicode.GetString( buffer, 0, buffer.Length - 2 )
				: Encoding.Unicode.GetString( buffer, 0, buffer.Length );
		}
	}

	public string ReadNullTerminatedString()
	{
		var bytes = new List<byte>();

		byte c = ReadByte();
		while ( c != '\0' )
		{
			bytes.Add( c );
			c = ReadByte();
		}

		return Encoding.ASCII.GetString( bytes.ToArray() );
	}
}

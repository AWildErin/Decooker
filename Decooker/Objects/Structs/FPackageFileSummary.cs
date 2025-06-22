using Decooker.Deserialization;
using Decooker.Objects.Enums;

namespace Decooker.Objects.Structs;

public class FPackageFileSummary : IDeserializable
{
	public const uint PACKAGE_FILE_TAG = 0x9E2A83C1;
	public const uint PACKGE_FILE_TAG_SWAPPED = 0xC1832A9E;

	public uint Tag { get; private set; }
	// lower 16 = engine ver
	// upper 16 = licensee ver
	public int FileVersion { get; private set; }
	public int TotalHeaderSize { get; private set; }
	public EPackageFlags PackageFlags { get; private set; }
	public string FolderName { get; private set; } = string.Empty;
	public int NameCount { get; private set; }
	public int NameOffset { get; private set; }
	public int ExportCount { get; private set; }
	public int ExportOffset { get; private set; }
	public int ImportCount { get; private set; }
	public int ImportOffset { get; private set; }
	public int DependsOffset { get; private set; }
	public int ImportExportGuidsOffset { get; private set; }
	public int ImportGuidsCount { get; private set; }
	public int ExportGuidsCount { get; private set; }
	public int ThumbnailTableOffset { get; private set; }
	public FGuid Guid { get; private set; } = new();
	public List<FGenerationInfo> Generations { get; private set; } = new();
	public int EngineVersion { get; private set; }
	public int CookedContentVersion { get; private set; }
	public ECompressionFlags CompressionFlags { get; private set; }

	// These have yet to be added because we don't use them

	// public ulong PackageSource { get; private set; }#
	// TArray<FCompressedChunk> CompressedChunks;
	// TArray<FString>	AdditionalPackagesToCook;
	// FTextureAllocations	TextureAllocations;


	public void Deserialize( UnrealReader Reader )
	{
		Tag = Reader.ReadUInt32();

		if ( Tag == PACKAGE_FILE_TAG || Tag == PACKGE_FILE_TAG_SWAPPED )
		{
			if ( Tag == PACKGE_FILE_TAG_SWAPPED )
			{
				Reader.IsByteSwapping = true;
			}
		}
		else
		{
			throw new Exception( $"Could not load Unreal packagee. Tag was {Tag}." );
		}

		FileVersion = Reader.ReadInt32();

		Reader.Version = GetFileVersion();
		Reader.LicenseeVersion = GetFileLicenseeVersion();

		TotalHeaderSize = Reader.ReadInt32();
		FolderName = Reader.ReadString();
		PackageFlags = (EPackageFlags)Reader.ReadUInt32();

		NameCount = Reader.ReadInt32();
		NameOffset = Reader.ReadInt32();
		ExportCount = Reader.ReadInt32();
		ExportOffset = Reader.ReadInt32();
		ImportCount = Reader.ReadInt32();
		ImportOffset = Reader.ReadInt32();

		DependsOffset = Reader.ReadInt32();

		if ( Reader.Version >= (int)EUnrealEngineObjectVersion.VER_ADDED_CROSSLEVEL_REFERENCES )
		{
			ImportExportGuidsOffset = Reader.ReadInt32();
			ImportGuidsCount = Reader.ReadInt32();
			ExportGuidsCount = Reader.ReadInt32();
		}
		else
		{
			ImportExportGuidsOffset = -1;
		}

		if ( Reader.Version >= (int)EUnrealEngineObjectVersion.VER_ASSET_THUMBNAILS_IN_PACKAGES )
		{
			ThumbnailTableOffset = Reader.ReadInt32();
		}

		Guid = Reader.ReadGuid();

		int generationCount = Reader.ReadInt32();
		for ( int i = 0; i < generationCount; i++ )
		{
			FGenerationInfo generationInfo = new();
			generationInfo.Deserialize( Reader );
			Generations.Add( generationInfo );
		}

		EngineVersion = Reader.ReadInt32();
		CookedContentVersion = Reader.ReadInt32();
		CompressionFlags = (ECompressionFlags)Reader.ReadUInt32();

		//Reader.BaseStream.Seek(NameOffset, SeekOrigin.Begin);

		//int NameMapIndex = 0;
		//while(NameMapIndex < NameCount)
		//{
		//	FNameEntry Entry = new FNameEntry();
		//	Entry.Deserialize(Reader);
		//	NameMapIndex++;
		//}
	}

	public int GetFileVersion()
	{
		return FileVersion & 0xffff;
	}

	public int GetFileLicenseeVersion()
	{
		return (FileVersion >> 16) & 0xffff;
	}

	public void DumpSummary()
	{
		Console.WriteLine( "Package file summary dump:" );
		Console.WriteLine( $"	FileVersion: {FileVersion}" );
		Console.WriteLine( $"		FileVersion (Engine): {GetFileVersion()}" );
		Console.WriteLine( $"		FileVersion (Licensee): {GetFileLicenseeVersion()}" );
		Console.WriteLine( $"	TotalHeaderSize: {TotalHeaderSize}" );
		Console.WriteLine( $"	PackageFlags: {PackageFlags}" );
		Console.WriteLine( $"	NameCount: {NameCount}" );
		Console.WriteLine( $"	NameOffset: {NameOffset}" );
		Console.WriteLine( $"	ExportCount: {ExportCount}" );
		Console.WriteLine( $"	ExportOffset: {ExportOffset}" );
		Console.WriteLine( $"	ImportCount: {ImportCount}" );
		Console.WriteLine( $"	ImportOffset: {ImportOffset}" );
		Console.WriteLine( $"	EngineVersion: {EngineVersion}" );
		Console.WriteLine( $"	CookedContentVersion: {CookedContentVersion}" );
		Console.WriteLine( $"	CompressionFlags: {CompressionFlags}" );
	}
}

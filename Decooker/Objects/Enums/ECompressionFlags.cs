namespace Decooker.Objects.Enums;

[Flags]
public enum ECompressionFlags : uint
{
	/** No compression																*/
	COMPRESS_None = 0x00,
	/** Compress with ZLIB															*/
	COMPRESS_ZLIB = 0x01,
	/** Compress with LZO															*/
	COMPRESS_LZO = 0x02,
	/** Compress with LZX															*/
	COMPRESS_LZX = 0x04,
	/** Prefer compression that compresses smaller (ONLY VALID FOR COMPRESSION)		*/
	COMPRESS_BiasMemory = 0x10,
	/** Prefer compression that compresses faster (ONLY VALID FOR COMPRESSION)		*/
	COMPRESS_BiasSpeed = 0x20,
	/** If this flag is present, decompression will not happen on the SPUs.			*/
	COMPRESS_ForcePPUDecompressZLib = 0x80
}

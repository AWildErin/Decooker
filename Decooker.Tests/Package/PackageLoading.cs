using Decooker.Objects.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decooker.Tests.Package;

internal class PackageLoading : TestBase
{
	[Test]
	public void LoadCompressedPackage_575()
	{
		string path = $"{GetTestDirectory()}/Packages/SP_Outpost_11_Compressed.xxx";
		UnrealReader stream = GetReaderForPackage( path );

		UnrealPackage package = new( path );
		package.Deserialize( stream );

		Assert.That( package.Summary.GetFileVersion(), Is.EqualTo( 575 ) );
		Assert.That( package.Summary.GetFileLicenseeVersion(), Is.EqualTo( 0 ) );
		Assert.That( package.Summary.EngineVersion, Is.EqualTo( 4638 ) );
		Assert.That( package.Summary.CompressionFlags, Is.EqualTo( ECompressionFlags.COMPRESS_LZX ) );
	}

	[Test]
	public void LoadUncompressedPackage_575()
	{
		string path = $"{GetTestDirectory()}/Packages/SP_Outpost_11_Decompressed.xxx";
		UnrealReader stream = GetReaderForPackage( path );

		UnrealPackage package = new( path );
		package.Deserialize( stream );

		Assert.That( package.Summary.GetFileVersion(), Is.EqualTo( 575 ) );
		Assert.That( package.Summary.GetFileLicenseeVersion(), Is.EqualTo( 0 ) );
		Assert.That( package.Summary.EngineVersion, Is.EqualTo( 4638 ) );
		Assert.That( package.Summary.CompressionFlags, Is.EqualTo( ECompressionFlags.COMPRESS_None ) );
	}
}

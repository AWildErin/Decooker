using System.IO;
using System.Text;

namespace Decooker.Tests;

internal abstract class TestBase
{
	public UnrealReader GetReaderForPackage( string packagePath )
	{
		// @todo Leaks the stream after we're done
		// probably store it in a IDisposable list and dispose on teardown
		var stream = new FileStream( packagePath, FileMode.Open, FileAccess.Read );
		var reader = new UnrealReader( stream, Encoding.UTF8, false );

		return reader;
	}

	public static string GetTestDirectory()
	{
		string exeDir = Directory.GetCurrentDirectory();
		return Path.Combine( exeDir, "../../../../TestResources" );
	}
}

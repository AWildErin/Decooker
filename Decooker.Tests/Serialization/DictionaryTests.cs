using Decooker.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decooker.Tests.Serialization;

internal class DictionaryTests : TestBase
{
	[Test]
	public void Dictionary_IntInt()
	{
		using var stream = new MemoryStream();
		using var writer = new BinaryWriter( stream );

		writer.Write( 500 );
		for ( int i = 0; i < 500; i++ )
		{
			writer.Write( i );
			writer.Write( i + 1 );
		}

		stream.Seek( 0, SeekOrigin.Begin );
		UnrealReader reader = new( stream );

		Dictionary<int, int> dict;
		reader.ReadMap( out dict );

		Assert.That( dict.Count, Is.EqualTo( 500 ) );
		Assert.That( dict[0], Is.EqualTo( 1 ) );
	}
}

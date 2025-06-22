using Decooker.Objects;
using Decooker.Objects.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decooker;
public static class UnrealTextHelper
{
	public static string GetIndentString( int IndentLevel )
	{
		return new string('\t', IndentLevel);
	}

	public static string GetObjectDeliminator(UObject Object)
	{
		// @todo this is not the best way to handle it, but should be fine for our usecase for now
		// but really does need to be revisited.
		if ( Object.ObjectImport is not null && !Object.ObjectImport.ClassName.Equals( "Package" ) )
		{
			return ":";
		}

		return ".";
	}
}

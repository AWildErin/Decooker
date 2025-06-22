using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decooker.Extensions;
public static class StringBuilderExtensions
{
	public static StringBuilder AppendIndentedLine( this StringBuilder StringBuilder, int Indent, string Text )
	{
		return StringBuilder.AppendLine( $"{UnrealTextHelper.GetIndentString( Indent )}{Text}" );
	}

	public static StringBuilder AppendIndented( this StringBuilder StringBuilder, int Indent, string Text )
	{
		return StringBuilder.Append( $"{UnrealTextHelper.GetIndentString( Indent )}{Text}" );
	}
}

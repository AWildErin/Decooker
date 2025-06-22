using Decooker.Attributes;
using Decooker.Deserialization;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Decooker.Extensions;

namespace Decooker.Objects.Properties;

[UnrealClass( "ArrayProperty" )]
public class UArrayProperty : UProperty
{
	public static Dictionary<Type, Dictionary<string, Type>> MappedTypes = new()
	{
	};

	public int Count { get; private set; }
	public List<UProperty> Data { get; private set; } = new();

	public override void Deserialize( UnrealReader Reader )
	{
		base.Deserialize( Reader );
	}

	internal override void DeserializeItem( UnrealReader Reader )
	{
		Count = Reader.ReadInt32();

		if ( Tag is null || OwnerObject is null )
		{
			throw new InvalidOperationException();
		}

		if ( MappedTypes.ContainsKey( OwnerObject.GetType() ) )
		{
			var data = MappedTypes[OwnerObject.GetType()];
			if ( data is not null && data.ContainsKey( Tag.Name.Name ) )
			{
				Type? classType = data[Tag.Name.Name];
				for ( int i = 0; i < Count; i++ )
				{
					if ( !classType.IsAssignableTo( typeof( UProperty ) ) )
					{
						throw new InvalidOperationException( $"Type {classType} is not derrived from UProperty!" );
					}

					var prop = (UProperty?)Activator.CreateInstance( classType );
					prop.OwnerObject = this;
					prop.DeserializeItem( Reader );
					Data.Add( prop );
				}
			}
			else
			{
				Reader.BaseStream.Seek( Tag.Size - 4, SeekOrigin.Current );
			}
		}
		else
		{
			Reader.BaseStream.Seek( Tag.Size - 4, SeekOrigin.Current );
		}
	}

	public override string GetValueAsString()
	{
		return $"{Count} elements";
	}
}

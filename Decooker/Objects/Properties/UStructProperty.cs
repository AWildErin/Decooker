using Decooker.Attributes;
using Decooker.Deserialization;
using Decooker.Extensions;
using Decooker.Objects.Structs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Decooker.Objects.Properties;

/// <summary>
/// 
/// </summary>
/// @todo This will crash with atomic structs, unfortunately not much you can do without implementing all the atomic types.
[UnrealClass( "StructProperty" )]
public class UStructProperty : UProperty
{
	public Type? ValueType { get; internal set; }
	public object? Value { get; internal set; }

	public UStructProperty() { }

	public UStructProperty( string Name )
		: base( Name )
	{

	}


	internal override void DeserializeItem( UnrealReader Reader )
	{
		var list = new List<FPropertyTag>();

		// Check for an atomic struct type
		Type[] assemblyTypes = Assembly.GetExecutingAssembly().GetTypes();
		Type? structType = assemblyTypes.FirstOrDefault( t => t.GetCustomAttributes<UnrealStructAttribute>().Any( x => x.StructName == Tag?.StructName.Name && x.IsAtomic ) );
		if ( structType != null )
		{
			IUnrealAtomicStruct? structDeserializer = (IUnrealAtomicStruct?)Activator.CreateInstance( structType );
			if ( structDeserializer == null )
			{
				throw new NotSupportedException( $"{structType} did not implement IUnrealStruct!" );
			}
			structDeserializer?.ReadStruct( Reader, this );
			return;
		}

		while ( true )
		{
			var tag = new FPropertyTag();
			if ( !tag.Deserialize( Reader ) )
			{
				break;
			}

			PropertyTags.Add( tag );

			// @todo I want to use GetClassType here but unreal's naming makes it a bit harder
			var classType = assemblyTypes.FirstOrDefault( t => t.GetCustomAttribute<UnrealClassAttribute>()?.ClassName == tag.Type.Name );

			if ( classType == null )
			{
				//throw new NotImplementedException( "Couldn't find property class for type." );
				Reader.BaseStream.Seek( tag.Size, SeekOrigin.Current );
				continue;
			}

			UProperty? prop = (UProperty?)Activator.CreateInstance( classType );
			if ( prop == null )
			{
				throw new Exception( $"Failed to create property of type {tag.Type}!" );
			}


			prop.Name = tag.Name;
			prop.Tag = tag;
			prop.OwnerObject = this;
			prop.DeserializeItem( Reader );
			Properties.Add( prop );
		}
	}

	public override string GetValueAsString()
	{
		StringBuilder sb = new StringBuilder();
		foreach ( var prop in Properties )
		{
			FName name = prop.Tag is not null ? prop.Tag.Name : prop.Name;
			sb.Append( $"{name}={prop.GetValueAsString()}," );
		}

		return sb.ToString();
	}
}

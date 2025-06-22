using Decooker.Attributes;
using Decooker.Core;
using Decooker.Deserialization;
using Decooker.Extensions;
using Decooker.Objects.Enums;
using Decooker.Objects.Properties;
using Decooker.Objects.Structs;
using System.Dynamic;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Decooker.Objects;

public class UObject
{
	public FName Name { get; internal set; } = DefaultNames.NAME_None;
	public EObjectFlags Flags { get; internal set; }
	public UnrealPackage? Package { get; internal set; }
	public int PackageIndex { get; internal set; }

	public UClass? Class { get; internal set; }
	public UObject? Outer { get; internal set; }

	public FObjectImport? ObjectImport { get; internal set; }
	public FObjectExport? ObjectExport { get; internal set; }

	// Native properties
	public FStateFrame? StateFrame { get; internal set; }
	public int NetIndex { get; private set; }
	public List<UProperty> Properties { get; private set; } = new();

	// All properties set for this object, they may not be serialized.
	public List<FPropertyTag> PropertyTags { get; private set; } = new();

	public IEnumerable<UObject> EnumerateOuter()
	{
		for ( var outer = Outer; outer != null; outer = outer.Outer )
		{
			yield return outer;
		}
	}

	public string GetReferencePath()
	{
		string outerPath = EnumerateOuter().Aggregate( string.Empty, ( current, outer ) => $"{outer.Name}{UnrealTextHelper.GetObjectDeliminator( outer )}{current}" );
		if ( Class is null )
		{
			return $"{outerPath}{Name}";
		}
		else
		{
			return $"{Class.Name}'{outerPath}{Name}'";
		}
	}

	#region Deserialization

	public void BeginLoad( UnrealReader Reader, bool bSkipObjectNativeSerialization = true )
	{
		if ( ObjectImport != null || ObjectExport == null )
		{
			throw new InvalidOperationException( "Cannot load imported object!" );
		}

		Reader.BaseStream.Seek( ObjectExport.SerialOffset, SeekOrigin.Begin );

		Deserialize( Reader );
	}

	public virtual void Deserialize( UnrealReader Reader )
	{
		if ( Flags.HasFlag( EObjectFlags.RF_HasStack ) )
		{
			StateFrame = new();
			StateFrame.Deserialize( Reader );
		}

		// For some reason, components have data before anything else
		if ( this is UComponent component )
		{
			component.PreDeserialize( Reader );
		}

		NetIndex = Reader.ReadInt32();

		if ( ObjectExport?.ClassIndex == 0 )
		{
			return;
		}

		DeserializeProperties( Reader );
	}

	protected void DeserializeProperties( UnrealReader Reader )
	{
		while ( true )
		{
			FPropertyTag tag = new();
			if ( !tag.Deserialize( Reader ) )
			{
				break;
			}

			PropertyTags.Add( tag );

			// @todo I want to use GetClassType here but unreal's stupid naming makes it a bit harder
			var types = Assembly.GetExecutingAssembly().GetTypes();
			var type = types.FirstOrDefault( t => t.GetCustomAttribute<UnrealClassAttribute>()?.ClassName == tag.Type.Name );

			if ( type == null )
			{
				//throw new NotImplementedException( "Couldn't find property class for type." );
				Reader.BaseStream.Seek( tag.Size, SeekOrigin.Current );
				continue;
			}

			UProperty? prop = (UProperty?)Activator.CreateInstance( type );
			if ( prop == null )
			{
				throw new Exception( $"Failed to create property of type {tag.Type}!" );
			}

			prop.OwnerObject = this;
			prop.Tag = tag;
			prop.Name = tag.Name;
			prop.DeserializeItem( Reader );
			Properties.Add( prop );
		}
	}

	#endregion

	#region Debugging

	public void DumpToConsole()
	{
		Console.WriteLine( $"Object {Name}" );
		Console.WriteLine( $"	Class: {Class?.Name}" );
		Console.WriteLine( $"	Outer: {Outer?.Name}" );
		Console.WriteLine( $"	Flags: {Flags}" );
		Console.WriteLine( $"	PackageIndex: {PackageIndex}" );
		Console.WriteLine( $"	Deserialized Properties: {Properties.Count}" );
		foreach ( var prop in Properties )
		{
			FName name = prop.Tag is not null ? prop.Tag.Name : prop.Name;
			Console.WriteLine( $"		{name} ({prop.Tag?.Type}) : {prop.GetValueAsString()}" );
		}
		Console.WriteLine( $"	All Properties: {PropertyTags.Count}" );
		foreach ( var prop in PropertyTags )
		{
			Console.WriteLine( $"		{prop.Name} : {prop.Type}" );
		}
	}

	#endregion

	public override string ToString()
	{
		if ( Class is null )
		{
			return Name.Name;
		}

		return $"{Class.Name}'{Name.Name}'";
	}
}

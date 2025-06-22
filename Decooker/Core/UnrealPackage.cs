using Decooker.Deserialization;
using Decooker.Objects;
using Decooker.Objects.Enums;
using Decooker.Objects.Properties;
using Decooker.Objects.Structs;

namespace Decooker.Core;

/// <summary>
/// The main owner of UPackage and subsequent UObjects
/// This fulfills the role of ULinkerLoad
/// </summary>
public class UnrealPackage
{
	public FPackageFileSummary Summary { get; private set; } = new();

	public List<FNameEntry> Names { get; private set; } = new();
	public List<FObjectExport> Exports { get; private set; } = new();
	public List<FObjectImport> Imports { get; private set; } = new();

	// Collection of all objects in the package, including imports.
	public List<UObject> Objects { get; private set; } = new();

	public UnrealPackage( string FilePath )
	{

	}

	public UnrealPackage( FileStream fileStream )
	{

	}

	public void Deserialize( UnrealReader Reader )
	{
		Reader.Package = this;

		if ( !DeserializePackageFileSummary( Reader ) )
		{
			throw new Exception( "Failed to deserialize package summary!" );
		}

		// @todo Warning out
		if ( (Summary.CompressionFlags & ~ECompressionFlags.COMPRESS_None) != 0 )
		{
			return;
		}

		if ( !DeserializeNameMap( Reader ) )
		{
			throw new Exception( "Failed to deserialize name map!" );
		}

		if ( !DeserializeImportMap( Reader ) )
		{
			throw new Exception( "Failed to deserialize import map!" );
		}

		if ( !DeserializeExportMap( Reader ) )
		{
			throw new Exception( "Failed to deserialize export map!" );
		}
	}

	private bool DeserializePackageFileSummary( UnrealReader Reader )
	{
		Summary.Deserialize( Reader );

		return true;
	}

	private bool DeserializeNameMap( UnrealReader Reader )
	{
		if ( Summary.NameCount < 0 )
		{
			return true;
		}

		Reader.BaseStream.Seek( Summary.NameOffset, SeekOrigin.Begin );

		for ( int i = 0; i < Summary.NameCount; i++ )
		{
			var entry = new FNameEntry
			{
				Index = i
			};
			entry.Deserialize( Reader );
			Names.Add( entry );
		}

		return true;
	}

	private bool DeserializeImportMap( UnrealReader Reader )
	{
		if ( Summary.NameCount <= 0 )
		{
			return true;
		}

		Reader.BaseStream.Seek( Summary.ImportOffset, SeekOrigin.Begin );

		for ( int i = 0; i < Summary.ImportCount; i++ )
		{
			FObjectImport import = new();
			import.PackageIndex = i;
			import.Deserialize( Reader );
			Imports.Add( import );
		}

		return true;
	}

	private bool DeserializeExportMap( UnrealReader Reader )
	{
		if ( Summary.ExportCount <= 0 )
		{
			return true;
		}

		Reader.BaseStream.Seek( Summary.ExportOffset, SeekOrigin.Begin );

		for ( int i = 0; i < Summary.ExportCount; i++ )
		{
			FObjectExport export = new();
			export.PackageIndex = i;
			export.Deserialize( Reader );
			Exports.Add( export );
		}

		return true;
	}

	//public bool LoadAllObjects()
	//{
	//	foreach ( var export in Exports )
	//	{
	//		var obj = CreateExport()
	//	}
	//	return true;
	//}

	public UObject? IndexToObject( int Index )
	{
		if ( Index < 0 )
		{
			FObjectImport entry = Imports[-Index - 1];
			return entry.Object ?? CreateImport( entry );
		}
		else if ( Index > 0 )
		{
			FObjectExport entry = Exports[Index - 1];
			return entry.Object ?? CreateExport( entry );
		}

		return null;
	}

	public T? IndexToObject<T>( int Index )
		where T : UObject, new()
	{
		if ( Index < 0 )
		{
			FObjectImport entry = Imports[-Index - 1];
			return entry.Object as T ?? CreateImport<T>( entry );
		}
		else if ( Index > 0 )
		{
			FObjectExport entry = Exports[Index - 1];
			return entry.Object as T ?? CreateExport<T>( entry );
		}

		return null;
	}

	private T? CreateImport<T>( FObjectImport Import )
		where T : UObject, new()
	{
		if ( Import.Object != null )
		{
			return Import.Object as T;
		}

		var obj = new T
		{
			Name = Import.ObjectName,
			Package = this,
			PackageIndex = -Import.PackageIndex - 1,
			Class = null,
			Outer = IndexToObject<UObject>( Import.OuterIndex ),
			ObjectImport = Import
		};

		Import.Object = obj;

		Objects.Add( obj );

		return obj;
	}

	private UObject? CreateImport( FObjectImport Import )
	{
		if ( Import.Object != null )
		{
			return Import.Object;
		}

		var obj = new UObject
		{
			Name = Import.ObjectName,
			Package = this,
			PackageIndex = -Import.PackageIndex - 1,
			Class = null,
			Outer = IndexToObject<UObject>( Import.OuterIndex ),
			ObjectImport = Import
		};

		Import.Object = obj;

		Objects.Add( obj );

		return obj;
	}

	private T? CreateExport<T>( FObjectExport Export )
		where T : UObject, new()
	{
		if ( Export.Object != null )
		{
			return Export.Object as T;
		}

		// @todo assign to struct class
		var superObject = IndexToObject<UStruct>( Export.SuperIndex );
		var outerObject = IndexToObject<UObject>( Export.OuterIndex );
		var classObject = IndexToObject<UClass>( Export.ClassIndex );

		Type type = typeof( T );
		if ( classObject != null )
		{
			type = UnrealReflectionHelper.GetClassType<T>( classObject.Name );

			bool isComponent = classObject.Name.EndsWith( "component" ) || classObject.Name.StartsWith( "distribution" ) || classObject.Name.StartsWith( "UIComp" );
			if ( type is null && isComponent )
			{
				type = typeof( UComponent );
			}
		}

		// @todo USE THE METHOD BELOW

		var obj = (UObject?)Activator.CreateInstance( type );
		if ( obj == null )
		{
			return null;
		}

		obj.Name = Export.ObjectName;
		obj.Flags = Export.ObjectFlags;
		obj.Package = this;
		obj.PackageIndex = Export.PackageIndex - 1;
		obj.Class = classObject;
		obj.Outer = outerObject;
		obj.ObjectExport = Export;

		Export.Object = obj;

		Objects.Add( obj );

		return (T)obj;
	}

	private UObject? CreateExport( FObjectExport Export )
	{
		if ( Export.Object != null )
		{
			return Export.Object;
		}

		// @todo assign to struct class
		var superObject = IndexToObject<UStruct>( Export.SuperIndex );
		var outerObject = IndexToObject<UObject>( Export.OuterIndex );
		var classObject = IndexToObject<UClass>( Export.ClassIndex );

		// @todo	Use an attribute to look for C# classes with the same name, if it doesn't exist then use a static
		//			list to figure out what base native class it'll just use raw uobject if that doesn't exist.

		var obj = (UObject?)Activator.CreateInstance( classObject != null ? UnrealReflectionHelper.GetClassType( classObject.Name ) : typeof( UObject ) );
		if ( obj == null )
		{
			return null;
		}

		obj.Name = Export.ObjectName;
		obj.Flags = Export.ObjectFlags;
		obj.Package = this;
		obj.PackageIndex = Export.PackageIndex - 1;
		obj.Class = classObject;
		obj.Outer = outerObject;
		obj.ObjectExport = Export;

		Export.Object = obj;

		Objects.Add( obj );

		return obj;
	}

	public void DumpToConsole()
	{
		Summary.DumpSummary();

		Console.WriteLine( "Names dump:" );
		foreach ( var entry in Names )
		{
			Console.WriteLine( $"	{entry.Name} ({entry.Flags})" );
		}

		Console.WriteLine( "Imports dump:" );
		foreach ( var entry in Imports )
		{
			Console.WriteLine( $"	{entry.ObjectName} ({entry.OuterIndex})" );
		}

		Console.WriteLine( "Exports dump:" );
		foreach ( var entry in Exports )
		{
			Console.WriteLine( $"	{entry.ObjectName} ({entry.ClassIndex}, {entry.SuperIndex}, {entry.OuterIndex}. OFFSET {entry.SerialOffset})" );
		}
	}
}

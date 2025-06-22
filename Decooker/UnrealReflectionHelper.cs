using Decooker.Attributes;
using Decooker.Objects;
using Decooker.Objects.Structs;
using System.Reflection;

namespace Decooker;

public static class UnrealReflectionHelper
{
	public static Type GetClassType( FName ClassName )
	{
		// @todo can this be done better, we should allow custom assesmbly support too
		var types = Assembly.GetExecutingAssembly().GetTypes();
		var matchingType = types.FirstOrDefault( t => t.GetCustomAttribute<UnrealClassAttribute>()?.ClassName == ClassName.Name );
		if (matchingType != null)
		{
			return matchingType;
		}

		// @todo Need a better way for this, UE serializes some component stuff before net index so we *have* to do this
		bool isComponent = ClassName.EndsWith( "component" ) || ClassName.StartsWith( "distribution" ) || ClassName.StartsWith( "UIComp" );
		if ( isComponent )
		{
			return typeof( UComponent );
		}

		if ( matchingType == null )
		{
			return typeof( UObject );
		}

		return matchingType;
	}

	public static Type GetClassType<T>( FName ClassName )
	{
		// @todo can this be done better, we should allow custom assesmbly support too
		var types = Assembly.GetExecutingAssembly().GetTypes();
		var matchingType = types.FirstOrDefault( t => t.GetCustomAttribute<UnrealClassAttribute>()?.ClassName == ClassName.Name );
		if ( matchingType != null )
		{
			return matchingType;
		}

		// @todo Need a better way for this, UE serializes some component stuff before net index so we *have* to do this
		bool isComponent = ClassName.EndsWith( "component" ) || ClassName.StartsWith( "distribution" ) || ClassName.StartsWith( "UIComp" );
		if ( isComponent )
		{
			return typeof( UComponent );
		}

		if ( matchingType == null )
		{
			return typeof( T );
		}

		return matchingType;
	}

	public static PropertyInfo? GetProperty( Type Type, FName PropertyName )
	{
		return Type
			.GetProperties( BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic )
			.FirstOrDefault( p =>
			{
				var attr = p.GetCustomAttribute<UnrealPropertyAttribute>();
				return attr?.PropertyName == PropertyName.Name;
			} );
	}

}

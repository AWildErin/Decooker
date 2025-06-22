namespace Decooker.Objects.Enums;

[Flags]
public enum EPackageFlags : uint
{
	PKG_AllowDownload = 0x00000001, // Allow downloading package.
	PKG_ClientOptional = 0x00000002,    // Purely optional for clients.
	PKG_ServerSideOnly = 0x00000004,   // Only needed on the server side.
	PKG_Cooked = 0x00000008,    // Whether this package has been cooked for the target platform.
	PKG_Unsecure = 0x00000010,   // Not trusted.
	PKG_SavedWithNewerVersion = 0x00000020, // Package was saved with newer version.
	PKG_Need = 0x00008000,  // Client needs to download this package.
	PKG_Compiling = 0x00010000, // package is currently being compiled
	PKG_ContainsMap = 0x00020000,   // Set if the package contains a ULevel/ UWorld object
	PKG_Trash = 0x00040000, // Set if the package was loaded from the trashcan
	PKG_DisallowLazyLoading = 0x00080000,   // Set if the archive serializing this package cannot use lazy loading
	PKG_PlayInEditor = 0x00100000,  // Set if the package was created for the purpose of PIE
	PKG_ContainsScript = 0x00200000,    // Package is allowed to contain UClasses and unrealscript
	PKG_ContainsDebugInfo = 0x00400000, // Package contains debug info (for UDebugger)
	PKG_RequireImportsAlreadyLoaded = 0x00800000,   // Package requires all its imports to already have been loaded
	PKG_SelfContainedLighting = 0x01000000, // All lighting in this package should be self contained
	PKG_StoreCompressed = 0x02000000,   // Package is being stored compressed, requires archive support for compression
	PKG_StoreFullyCompressed = 0x04000000,  // Package is serialized normally, and then fully compressed after (must be decompressed before LoadPackage is called)
	PKG_ContainsFaceFXData = 0x10000000,    // Package contains FaceFX assets and/or animsets
	PKG_NoExportAllowed = 0x20000000,   // Package was NOT created by a modder.  public data not for export
	PKG_StrippedSource = 0x40000000,    // Source has been removed to compress the package size
	PKG_FilterEditorOnly = 0x80000000,  // Package has editor-only data filtered
}

namespace Decooker.Objects.Enums;

[Flags]
public enum EClassFlags : uint
{
	/** @name Base flags */
	//@{
	CLASS_None = 0x00000000,
	/** Class is abstract and can't be instantiated directly. */
	CLASS_Abstract = 0x00000001,
	/** Script has been compiled successfully. */
	CLASS_Compiled = 0x00000002,
	/** Load object configuration at construction time. */
	CLASS_Config = 0x00000004,
	/** This object type can't be saved; null it out at save time. */
	CLASS_Transient = 0x00000008,
	/** Successfully parsed. */
	CLASS_Parsed = 0x00000010,
	/** Class contains localized text. */
	CLASS_Localized = 0x00000020,
	/** Objects of this class can be safely replaced with default or NULL. */
	CLASS_SafeReplace = 0x00000040,
	/** Class is a native class - native interfaces will have CLASS_Native set, but not RF_Native */
	CLASS_Native = 0x00000080,
	/** Don't export to C++ header. */
	CLASS_NoExport = 0x00000100,
	/** Allow users to create in the editor. */
	CLASS_Placeable = 0x00000200,
	/** Handle object configuration on a per-object basis, rather than per-class. */
	CLASS_PerObjectConfig = 0x00000400,
	/** Replication handled in C++. */
	CLASS_NativeReplication = 0x00000800,
	/** Class can be constructed from editinline New button. */
	CLASS_EditInlineNew = 0x00001000,
	/** Display properties in the editor without using categories. */
	CLASS_CollapseCategories = 0x00002000,
	/** Class is an interface **/
	CLASS_Interface = 0x00004000,
	/**
	 * Indicates that this class contains object properties which are marked 'instanced' (or editinline export).  Set by the script compiler after all properties in the
	 * class have been parsed.  Used by the loading code as an optimization to attempt to instance newly added properties only for relevant classes
	 */
	CLASS_HasInstancedProps = 0x00200000,
	/** Class needs its defaultproperties imported */
	CLASS_NeedsDefProps = 0x00400000,
	/** Class has component properties. */
	CLASS_HasComponents = 0x00800000,
	/** Don't show this class in the editor class browser or edit inline new menus. */
	CLASS_Hidden = 0x01000000,
	/** Don't save objects of this class when serializing */
	CLASS_Deprecated = 0x02000000,
	/** Class not shown in editor drop down for class selection */
	CLASS_HideDropDown = 0x04000000,
	/** Class has been exported to a header file */
	CLASS_Exported = 0x08000000,
	/** Class has no unrealscript counter-part */
	CLASS_Intrinsic = 0x10000000,
	/** Properties in this class can only be accessed from native code */
	CLASS_NativeOnly = 0x20000000,
	/** Handle object localization on a per-object basis, rather than per-class. */
	CLASS_PerObjectLocalized = 0x40000000,
	/** This class has properties that are marked with CPF_CrossLevel */
	CLASS_HasCrossLevelRefs = 0x80000000,

	// deprecated - these values now match the values of the EClassCastFlags enum
	/** IsA UProperty */
	CLASS_IsAUProperty = 0x00008000,
	/** IsA UObjectProperty */
	CLASS_IsAUObjectProperty = 0x00010000,
	/** IsA UBoolProperty */
	CLASS_IsAUBoolProperty = 0x00020000,
	/** IsA UState */
	CLASS_IsAUState = 0x00040000,
	/** IsA UFunction */
	CLASS_IsAUFunction = 0x00080000,
	/** IsA UStructProperty */
	CLASS_IsAUStructProperty = 0x00100000,

	//@}


	/** @name Flags to inherit from base class */
	//@{
	CLASS_Inherit = CLASS_Transient | CLASS_Config | CLASS_Localized | CLASS_SafeReplace | CLASS_PerObjectConfig | CLASS_PerObjectLocalized | CLASS_Placeable
							| CLASS_IsAUProperty | CLASS_IsAUObjectProperty | CLASS_IsAUBoolProperty | CLASS_IsAUStructProperty | CLASS_IsAUState | CLASS_IsAUFunction
							| CLASS_HasComponents | CLASS_Deprecated | CLASS_Intrinsic | CLASS_HasInstancedProps | CLASS_HasCrossLevelRefs,

	/** these flags will be cleared by the compiler when the class is parsed during script compilation */
	CLASS_RecompilerClear = CLASS_Inherit | CLASS_Abstract | CLASS_NoExport | CLASS_NativeReplication | CLASS_Native,

	/** these flags will be inherited from the base class only for non-intrinsic classes */
	CLASS_ScriptInherit = CLASS_Inherit | CLASS_EditInlineNew | CLASS_CollapseCategories,
	//@}

	CLASS_AllFlags = 0xFFFFFFFF,
};

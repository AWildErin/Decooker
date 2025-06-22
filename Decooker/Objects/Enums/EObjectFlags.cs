namespace Decooker.Objects.Enums;

[Flags]
public enum EObjectFlags : ulong
{
	/** In a singular function. */
	RF_InSingularFunc = 0x0000000000000002,
	/** Object did a state change. */
	RF_StateChanged = 0x0000000000000004,
	/** For debugging PostLoad calls. */
	RF_DebugPostLoad = 0x0000000000000008,
	/** For debugging Serialize calls. */
	RF_DebugSerialize = 0x0000000000000010,
	/** For debugging FinishDestroy calls. */
	RF_DebugFinishDestroyed = 0x0000000000000020,
	/** Object is selected in one of the editors browser windows. */
	RF_EdSelected = 0x0000000000000040,
	/** This component's template was deleted, so should not be used. */
	RF_ZombieComponent = 0x0000000000000080,
	/** Property is protected (may only be accessed from its owner class or subclasses) */
	RF_Protected = 0x0000000000000100,
	/** this object is its class's default object */
	RF_ClassDefaultObject = 0x0000000000000200,
	/** this object is a template for another object - treat like a class default object */
	RF_ArchetypeObject = 0x0000000000000400,
	/** Forces this object to be put into the export table when saving a package regardless of outer */
	RF_ForceTagExp = 0x0000000000000800,
	/** Set if reference token stream has already been assembled */
	RF_TokenStreamAssembled = 0x0000000000001000,
	/** Object's size no longer matches the size of its C++ class (only used during make, for native classes whose properties have changed) */
	RF_MisalignedObject = 0x0000000000002000,
	/** Object will not be garbage collected, even if unreferenced. */
	RF_RootSet = 0x0000000000004000,
	/** BeginDestroy has been called on the object. */
	RF_BeginDestroyed = 0x0000000000008000,
	/** FinishDestroy has been called on the object. */
	RF_FinishDestroyed = 0x0000000000010000,
	/** Whether object is rooted as being part of the root set (garbage collection) */
	RF_DebugBeginDestroyed = 0x0000000000020000,
	/** Marked by content cooker. */
	RF_MarkedByCooker = 0x0000000000040000,
	/** Whether resource object is localized. */
	RF_LocalizedResource = 0x0000000000080000,
	/** whether InitProperties has been called on this object */
	RF_InitializedProps = 0x0000000000100000,
	/** script patcher: indicates that this struct will receive additional member properties from the script patcher */
	RF_PendingFieldPatches = 0x0000000000200000,
	/** This object has been pointed to by a cross-level reference, and therefore requires additional cleanup upon deletion */
	RF_IsCrossLevelReferenced = 0x0000000000400000,
	// unused DECLARE_UINT64(0x0000000000800000)
	// unused DECLARE_UINT64(0x0000000001000000)
	// unused DECLARE_UINT64(0x0000000002000000)
	// unused DECLARE_UINT64(0x0000000004000000)
	// unused DECLARE_UINT64(0x0000000008000000)
	// unused DECLARE_UINT64(0x0000000010000000)
	// unused DECLARE_UINT64(0x0000000020000000)
	// unused DECLARE_UINT64(0x0000000040000000)
	/** Object has been saved via SavePackage. Temporary. */
	RF_Saved = 0x0000000080000000,
	/** Object is transactional. */
	RF_Transactional = 0x0000000100000000,
	/** Object is not reachable on the object graph. */
	RF_Unreachable = 0x0000000200000000,
	/** Object is visible outside its package. */
	RF_Public = 0x0000000400000000,
	/** Temporary import tag in load/save. */
	RF_TagImp = 0x0000000800000000,
	/** Temporary export tag in load/save. */
	RF_TagExp = 0x0000001000000000,
	/** Object marked as obsolete and should be replaced. */
	RF_Obsolete = 0x0000002000000000,
	/** Check during garbage collection. */
	RF_TagGarbage = 0x0000004000000000,
	/** Object is being disregard for GC as its static and itself and all references are always loaded. */
	RF_DisregardForGC = 0x0000008000000000,
	/** Object is localized by instance name, not by class. */
	RF_PerObjectLocalized = 0x0000010000000000,
	/** During load, indicates object needs loading. */
	RF_NeedLoad = 0x0000020000000000,
	/** Object is being asynchronously loaded. */
	RF_AsyncLoading = 0x0000040000000000,
	/** During load, indicates that the object still needs to instance subobjects and fixup serialized component references */
	RF_NeedPostLoadSubobjects = 0x0000080000000000,
	/** @warning: Mirrored in UnName.h. Suppressed log name. */
	RF_Suppress = 0x0000100000000000,
	/** Within an EndState call. */
	RF_InEndState = 0x0000200000000000,
	/** Don't save object. */
	RF_Transient = 0x0000400000000000,
	/** Whether the object has already been cooked */
	RF_Cooked = 0x0000800000000000,
	/** In-file load for client. */
	RF_LoadForClient = 0x0001000000000000,
	/** In-file load for client. */
	RF_LoadForServer = 0x0002000000000000,
	/** In-file load for client. */
	RF_LoadForEdit = 0x0004000000000000,
	/** Keep object around for editing even if unreferenced. */
	RF_Standalone = 0x0008000000000000,
	/** Don't load this object for the game client. */
	RF_NotForClient = 0x0010000000000000,
	/** Don't load this object for the game server. */
	RF_NotForServer = 0x0020000000000000,
	/** Don't load this object for the editor. */
	RF_NotForEdit = 0x0040000000000000,
	// unused DECLARE_UINT64(0x0080000000000000)
	/** Object needs to be postloaded. */
	RF_NeedPostLoad = 0x0100000000000000,
	/** Has execution stack. */
	RF_HasStack = 0x0200000000000000,
	/** Native (UClass only). */
	RF_Native = 0x0400000000000000,
	/** Marked (for debugging). */
	RF_Marked = 0x0800000000000000,
	/** ShutdownAfterError called. */
	RF_ErrorShutdown = 0x1000000000000000,
	/** Objects that are pending destruction (invalid for gameplay but valid objects) */
	RF_PendingKill = 0x2000000000000000,
	/** Temporarily marked by content cooker - should be cleared. */
	RF_MarkedByCookerTemp = 0x4000000000000000,
	/** This object was cooked into a startup package. */
	RF_CookedStartupObject = 0x8000000000000000,

	/** All context flags. */
	RF_ContextFlags = (RF_NotForClient | RF_NotForServer | RF_NotForEdit),
	/** Flags affecting loading. */
	RF_LoadContextFlags = (RF_LoadForClient | RF_LoadForServer | RF_LoadForEdit),
	/** Flags to load from Unrealfiles. */
	RF_Load = (RF_ContextFlags | RF_LoadContextFlags | RF_Public | RF_Standalone | RF_Native | RF_Obsolete | RF_Protected | RF_Transactional | RF_HasStack | RF_PerObjectLocalized | RF_ClassDefaultObject | RF_ArchetypeObject | RF_LocalizedResource),
	/** Flags to persist across loads. */
	RF_Keep = (RF_Native | RF_Marked | RF_PerObjectLocalized | RF_MisalignedObject | RF_DisregardForGC | RF_RootSet | RF_LocalizedResource),
	/** Script-accessible flags. */
	RF_ScriptMask = (RF_Transactional | RF_Public | RF_Transient | RF_NotForClient | RF_NotForServer | RF_NotForEdit | RF_Standalone),
	/** Undo/ redo will store/ restore these */
	RF_UndoRedoMask = (RF_PendingKill),
	/** Sub-objects will inherit these flags from their SuperObject. */
	RF_PropagateToSubObjects = (RF_Public | RF_ArchetypeObject | RF_Transactional),

	RF_AllFlags = 0xFFFFFFFFFFFFFFFF
}

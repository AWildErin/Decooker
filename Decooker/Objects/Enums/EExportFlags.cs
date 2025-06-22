namespace Decooker.Objects.Enums;

[Flags]
public enum EExportFlags : uint
{
	/** No flags*/
	EF_None = 0x00000000,
	/** Whether the export was forced into the export table via RF_ForceTagExp.	*/
	EF_ForcedExport = 0x00000001,
	/** indicates that this export was added by the script patcher, so this object's data will come from memory, not disk */
	EF_ScriptPatcherExport = 0x00000002,
	/** indicates that this export is a UStruct which will be patched with additional member fields by the script patcher */
	EF_MemberFieldPatchPending = 0x00000004,
	/** All flags */
	EF_AllFlags = 0xFFFFFFFF
}

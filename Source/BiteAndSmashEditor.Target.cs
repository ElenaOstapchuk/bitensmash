using UnrealBuildTool;

public class BiteAndSmashEditorTarget : TargetRules
{
	public BiteAndSmashEditorTarget(TargetInfo Target) : base(Target)
	{
		Type = TargetType.Editor;
		DefaultBuildSettings = BuildSettingsVersion.Latest;
		IncludeOrderVersion = EngineIncludeOrderVersion.Latest;
		ExtraModuleNames.Add("BiteAndSmash");
	}
}

using UnrealBuildTool;

public class BiteAndSmashTarget : TargetRules
{
	public BiteAndSmashTarget(TargetInfo Target) : base(Target)
	{
		Type = TargetType.Game;
		DefaultBuildSettings = BuildSettingsVersion.Latest;
		IncludeOrderVersion = EngineIncludeOrderVersion.Latest;
		ExtraModuleNames.Add("BiteAndSmash");
	}
}

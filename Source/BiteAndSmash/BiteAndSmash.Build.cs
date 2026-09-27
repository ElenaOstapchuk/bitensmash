using UnrealBuildTool;

public class BiteAndSmash : ModuleRules
{
	public BiteAndSmash(ReadOnlyTargetRules Target) : base(Target)
	{
		PCHUsage = PCHUsageMode.UseExplicitOrSharedPCHs;

		PublicDependencyModuleNames.AddRange(new string[]
		{
			"Core", "CoreUObject", "Engine", "InputCore",
			"EnhancedInput",      // player input (fly / land / crawl)
			"AIModule",           // human state machine, perception
			"NavigationSystem",   // human patrol / hunt on NavMesh
			"UMG", "Slate", "SlateCore" // reservoir / stress / detection gauges
		});
	}
}

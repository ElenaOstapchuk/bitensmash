#pragma once

#include "CoreMinimal.h"
#include "GameFramework/GameModeBase.h"
#include "BiteAndSmashGameMode.generated.h"

/** Base game mode. Blueprint children (BP_BedroomGameMode) set pawn/HUD classes. */
UCLASS()
class BITEANDSMASH_API ABiteAndSmashGameMode : public AGameModeBase
{
	GENERATED_BODY()
};

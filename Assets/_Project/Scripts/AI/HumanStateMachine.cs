using UnityEngine;
using MosquitoGame.Systems;

namespace MosquitoGame.AI
{
    public enum HumanState { Sleeping, Alert, BlindSwat, Hunting, CalmingDown }

    /// <summary>
    /// State machine for a single human. See project brief section 2 for the
    /// full design: Sleeping -> Alert -> (BlindSwat | Hunting) -> CalmingDown -> Sleeping.
    /// Stress (0-100) escalates Alert into Hunting; it decays smoothly, unlike
    /// the mosquito's reservoir which only fills.
    /// </summary>
    public class HumanStateMachine : MonoBehaviour
    {
        [Header("State")]
        public HumanState currentState = HumanState.Sleeping;

        [Header("Stress")]
        [Range(0, 100)] public float stress = 0f;
        public float stressPerFailedSwat = 27f;
        public float stressDecayPerSecond = 4f;
        public float stressThresholdForHunting = 100f;

        [Header("Hearing")]
        [Tooltip("Chance (0-1) that a blind swat actually hits the mosquito.")]
        [Range(0f, 1f)] public float blindSwatHitChance = 0.15f;

        [Header("References")]
        public SoundEmitter trackedMosquitoSound;
        public HumanFieldOfView fieldOfView; // used only while Hunting (light on)

        private float silenceTimer;
        [Tooltip("Seconds of silence before assuming the mosquito is dead / calming down.")]
        public float silenceTimeoutSleeping = 3f;
        public float silenceTimeoutHunting = 8f;

        private void Update()
        {
            switch (currentState)
            {
                case HumanState.Sleeping:
                    TickSleeping();
                    break;
                case HumanState.Alert:
                    TickAlert();
                    break;
                case HumanState.BlindSwat:
                    // Handled as a short one-shot action; see TriggerBlindSwat().
                    break;
                case HumanState.Hunting:
                    TickHunting();
                    break;
                case HumanState.CalmingDown:
                    TickCalmingDown();
                    break;
            }
        }

        private bool CanHearMosquito()
        {
            if (trackedMosquitoSound == null || !trackedMosquitoSound.IsEmittingSound) return false;
            float dist = Vector3.Distance(transform.position, trackedMosquitoSound.Position);
            return dist <= trackedMosquitoSound.CurrentRadius;
        }

        private void TickSleeping()
        {
            if (CanHearMosquito())
            {
                currentState = HumanState.Alert;
            }
        }

        private void TickAlert()
        {
            if (!CanHearMosquito())
            {
                silenceTimer += Time.deltaTime;
                if (silenceTimer >= silenceTimeoutSleeping)
                {
                    ResetToSleep();
                }
                return;
            }

            silenceTimer = 0f;
            TriggerBlindSwat();
        }

        private void TriggerBlindSwat()
        {
            currentState = HumanState.BlindSwat;
            bool hit = Random.value <= blindSwatHitChance;
            if (hit)
            {
                // TODO: kill mosquito / notify GameManager.
            }
            else
            {
                stress = Mathf.Clamp(stress + stressPerFailedSwat, 0f, 100f);
                if (stress >= stressThresholdForHunting)
                {
                    currentState = HumanState.Hunting;
                    if (fieldOfView != null) fieldOfView.enabled = true;
                }
                else
                {
                    currentState = HumanState.Alert;
                }
            }
        }

        private void TickHunting()
        {
            bool canSee = fieldOfView != null && fieldOfView.CanSeeMosquito;
            if (CanHearMosquito() || canSee)
            {
                silenceTimer = 0f;
                stress = Mathf.Clamp(stress - stressDecayPerSecond * Time.deltaTime * 0.25f, 0f, 100f);
                return;
            }

            silenceTimer += Time.deltaTime;
            stress = Mathf.Clamp(stress - stressDecayPerSecond * Time.deltaTime, 0f, 100f);
            if (silenceTimer >= silenceTimeoutHunting)
            {
                currentState = HumanState.CalmingDown;
            }
        }

        private void TickCalmingDown()
        {
            stress = Mathf.Clamp(stress - stressDecayPerSecond * Time.deltaTime, 0f, 100f);
            if (stress <= 0f)
            {
                ResetToSleep();
            }
            // Re-alert if noise resumes while calming down.
            if (CanHearMosquito())
            {
                currentState = HumanState.Alert;
            }
        }

        private void ResetToSleep()
        {
            currentState = HumanState.Sleeping;
            silenceTimer = 0f;
            if (fieldOfView != null) fieldOfView.enabled = false;
        }

        /// <summary>Called by BodyDetectionMeter when the mosquito is caught mid-feed on this human.</summary>
        public void InstantPreciseSwat()
        {
            // 100% hit — no chance roll, unlike TriggerBlindSwat.
            // TODO: kill mosquito / notify GameManager.
        }
    }
}

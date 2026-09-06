using UnityEngine;

namespace MosquitoGame.Player
{
    /// <summary>
    /// Central state for the mosquito: reservoir fill, size/growth, and the
    /// derived sound radius used by HumanHearing. Size grows with reservoir
    /// progress across levels (level 5 = comically oversized, per design doc).
    /// </summary>
    public class MosquitoStats : MonoBehaviour
    {
        [Header("Reservoir")]
        [Range(0, 100)] public float reservoir = 0f;
        [Tooltip("How much reservoir % one successful feeding bout adds (5-7 bouts to fill).")]
        public float reservoirPerBout = 16f;

        [Header("Size / growth")]
        [Tooltip("1 = base size, grows toward ~5-6x by level 5 (owl/crow sized).")]
        public float sizeMultiplier = 1f;
        public float baseHearingRadius = 2.5f;

        [Header("Sound")]
        [Tooltip("Flight buzz is constant volume regardless of context (landing, etc).")]
        public float flightSoundVolume = 1f;

        public bool IsFull => reservoir >= 100f;

        /// <summary>Radius (meters) at which a human can hear this mosquito while flying.</summary>
        public float CurrentHearingRadius => baseHearingRadius * sizeMultiplier;

        public void AddFeedingBout()
        {
            reservoir = Mathf.Clamp(reservoir + reservoirPerBout, 0f, 100f);
        }

        public void GrowForNextLevel(float multiplierIncrease)
        {
            sizeMultiplier += multiplierIncrease;
        }
    }
}

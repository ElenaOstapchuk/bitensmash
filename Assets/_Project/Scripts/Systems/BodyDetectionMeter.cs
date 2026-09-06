using UnityEngine;
using MosquitoGame.AI;
using MosquitoGame.Player;

namespace MosquitoGame.Systems
{
    /// <summary>
    /// Tracks the "felt presence" gauge (0-100) while the mosquito feeds on
    /// a specific human. Starts filling immediately on landing; the first
    /// ~3 seconds are a safe window before real risk builds (tune via curve).
    /// At 100 the human gets an instant, guaranteed swat (unlike the 15%
    /// blind-swat chance while airborne/hidden).
    /// </summary>
    public class BodyDetectionMeter : MonoBehaviour
    {
        public HumanStateMachine human;
        public MosquitoController mosquito;

        [Range(0, 100)] public float detection = 0f;
        [Tooltip("Seconds from landing before detection risk meaningfully ramps up.")]
        public float safeWindowSeconds = 3f;
        [Tooltip("How fast detection fills once past the safe window.")]
        public float fillRatePerSecond = 60f;

        private float timeOnBody;
        public bool IsActive { get; private set; }

        public void BeginFeeding()
        {
            IsActive = true;
            timeOnBody = 0f;
            detection = 0f;
        }

        public void EndFeeding()
        {
            IsActive = false;
            detection = 0f;
            timeOnBody = 0f;
        }

        private void Update()
        {
            if (!IsActive) return;

            timeOnBody += Time.deltaTime;
            if (timeOnBody <= safeWindowSeconds)
            {
                return; // safe window: gauge shown but not filling yet
            }

            detection = Mathf.Clamp(detection + fillRatePerSecond * Time.deltaTime, 0f, 100f);
            if (detection >= 100f)
            {
                human.InstantPreciseSwat();
                EndFeeding();
            }
        }
    }
}

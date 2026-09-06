using UnityEngine;

namespace MosquitoGame.Player
{
    public enum MosquitoState { Flying, Landed, Crawling, FeedingOnBody }

    /// <summary>
    /// Movement/state skeleton for the mosquito. Flying makes noise (via
    /// SoundEmitter), Landed/idle is silent, Crawling is silent unless on
    /// the human's body (see BodyDetectionMeter).
    /// </summary>
    [RequireComponent(typeof(MosquitoStats))]
    public class MosquitoController : MonoBehaviour
    {
        public MosquitoState CurrentState { get; private set; } = MosquitoState.Flying;

        [Header("Flight")]
        public float flySpeed = 3.5f;
        public float verticalFlySpeed = 2f;

        [Header("Crawl")]
        public float crawlSpeed = 0.8f;

        private MosquitoStats stats;

        private void Awake()
        {
            stats = GetComponent<MosquitoStats>();
        }

        private void Update()
        {
            // TODO: wire to Input System actions (move/fly/land/crawl toggle).
            switch (CurrentState)
            {
                case MosquitoState.Flying:
                    HandleFlight();
                    break;
                case MosquitoState.Crawling:
                    HandleCrawl();
                    break;
                case MosquitoState.Landed:
                case MosquitoState.FeedingOnBody:
                    // Stationary: no movement, no sound (SoundEmitter checks state).
                    break;
            }
        }

        private void HandleFlight()
        {
            // TODO: apply flySpeed/verticalFlySpeed from input.
        }

        private void HandleCrawl()
        {
            // TODO: apply crawlSpeed from input, silent movement.
        }

        public void Land() => CurrentState = MosquitoState.Landed;
        public void TakeOff() => CurrentState = MosquitoState.Flying;
        public void StartCrawling() => CurrentState = MosquitoState.Crawling;
        public void StartFeedingOnBody() => CurrentState = MosquitoState.FeedingOnBody;

        public bool IsMakingSound => CurrentState == MosquitoState.Flying;
    }
}

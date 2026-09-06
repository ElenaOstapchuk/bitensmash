using UnityEngine;
using MosquitoGame.Player;

namespace MosquitoGame.Systems
{
    /// <summary>
    /// Exposes the mosquito's current audible radius to any listener
    /// (HumanHearing). Silent while landed/crawling off-body, per design.
    /// </summary>
    [RequireComponent(typeof(MosquitoController), typeof(MosquitoStats))]
    public class SoundEmitter : MonoBehaviour
    {
        private MosquitoController controller;
        private MosquitoStats stats;

        private void Awake()
        {
            controller = GetComponent<MosquitoController>();
            stats = GetComponent<MosquitoStats>();
        }

        public bool IsEmittingSound => controller.IsMakingSound;

        public float CurrentRadius => stats.CurrentHearingRadius;

        public Vector3 Position => transform.position;
    }
}

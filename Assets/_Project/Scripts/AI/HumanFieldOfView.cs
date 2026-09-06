using UnityEngine;
using MosquitoGame.Systems;

namespace MosquitoGame.AI
{
    /// <summary>
    /// Visual detection cone, active only while the human is Hunting (light on).
    /// Combine angle check + raycast for line-of-sight against furniture.
    /// </summary>
    public class HumanFieldOfView : MonoBehaviour
    {
        public SoundEmitter trackedMosquito; // reused as a position reference
        public float viewDistance = 8f;
        [Range(0, 180)] public float viewAngle = 60f;
        public LayerMask obstacleMask;

        public bool CanSeeMosquito { get; private set; }

        private void Update()
        {
            CanSeeMosquito = false;
            if (!enabled || trackedMosquito == null) return;

            Vector3 toTarget = trackedMosquito.Position - transform.position;
            float dist = toTarget.magnitude;
            if (dist > viewDistance) return;

            float angle = Vector3.Angle(transform.forward, toTarget);
            if (angle > viewAngle * 0.5f) return;

            if (Physics.Raycast(transform.position, toTarget.normalized, out RaycastHit hit, dist, obstacleMask))
            {
                return; // blocked by furniture etc.
            }

            CanSeeMosquito = true;
        }
    }
}

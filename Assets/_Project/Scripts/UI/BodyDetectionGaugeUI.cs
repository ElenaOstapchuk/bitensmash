using UnityEngine;
using UnityEngine.UI;
using MosquitoGame.Systems;

namespace MosquitoGame.UI
{
    /// <summary>
    /// World-space canvas gauge that follows the mosquito and only shows
    /// while feeding on a human's body (BodyDetectionMeter.IsActive).
    /// Fades via CanvasGroup on enter/exit.
    /// </summary>
    [RequireComponent(typeof(CanvasGroup))]
    public class BodyDetectionGaugeUI : MonoBehaviour
    {
        public BodyDetectionMeter meter;
        public Image fillImage;
        public Transform followTarget;
        public float fadeSpeed = 6f;

        private CanvasGroup canvasGroup;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            canvasGroup.alpha = 0f;
        }

        private void LateUpdate()
        {
            if (followTarget != null)
            {
                transform.position = followTarget.position;
            }

            bool show = meter != null && meter.IsActive;
            float targetAlpha = show ? 1f : 0f;
            canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, targetAlpha, fadeSpeed * Time.deltaTime);

            if (show && fillImage != null)
            {
                fillImage.fillAmount = meter.detection / 100f;
            }
        }
    }
}

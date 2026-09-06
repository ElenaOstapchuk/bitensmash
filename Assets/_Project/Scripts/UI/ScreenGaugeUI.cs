using UnityEngine;
using UnityEngine.UI;

namespace MosquitoGame.UI
{
    /// <summary>
    /// Drives a single Image(type=Filled) gauge in screen space.
    /// Used for both the reservoir (solid red) and the stress bar
    /// (gradient sprite green->yellow->red, fill-only, no marker line).
    /// </summary>
    public class ScreenGaugeUI : MonoBehaviour
    {
        public Image fillImage;
        [Range(0, 100)] public float value;

        public void SetValue01(float value01)
        {
            value = Mathf.Clamp01(value01) * 100f;
            if (fillImage != null)
            {
                fillImage.fillAmount = Mathf.Clamp01(value01);
            }
        }

        public void SetValue0to100(float value0to100)
        {
            SetValue01(value0to100 / 100f);
        }
    }
}

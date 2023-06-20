using UnityEngine;
using UnityEngine.UI;

namespace Arcube.TimelineAnimator
{
    [RequireComponent(typeof(Image))]
    [AddComponentMenu("TimelineEditor/FillAnimate")]
    public class FillAnimate : Animatable
    {
        [SerializeField] private float []points;
        [SerializeField] private Image i_image;
        private void OnValidate()
        {
            i_image = GetComponent<Image>();
        }

        public override void Animate(float progress, int from, int to)
        {
            var delta = points[to] - points[from];
            var val = points[from] + (delta * progress);
            i_image.fillAmount = val;
        }

        public override void AnimateChildren(float progress, AnimationCurve curve, int from, int to, bool ascendingOrder = true)
        {
        }
    }
}
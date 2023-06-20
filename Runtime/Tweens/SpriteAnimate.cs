using UnityEngine;
using UnityEngine.UI;

namespace Arcube.TimelineAnimator
{
    [RequireComponent(typeof(Image))]
    [AddComponentMenu("TimelineEditor/SpriteAnimate")]
    public class SpriteAnimate : Animatable
    {
        [SerializeField] private float[] points;
        [SerializeField] private Image i_image;
        [SerializeField] private Sprite []sprites;
        private void OnValidate()
        {
            i_image = GetComponent<Image>();
        }

        public override void Animate(float progress, int from, int to)
        {
            var delta = points[to] - points[from];
            var val = points[from] + (delta * progress);
            i_image.sprite = sprites[(int)val];
        }

        public override void AnimateChildren(float progress, AnimationCurve curve, int from, int to, bool ascendingOrder = true)
        {
        }
    }
}
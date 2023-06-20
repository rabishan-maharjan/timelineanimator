using UnityEngine;

namespace Arcube.TimelineAnimator
{
    public class RectSizeAnimate : Animatable
    {
        [SerializeField] private Vector2[] sizes;
        public override void Animate(float progress, int from, int to)
        {
            var deltaSize = sizes[to] - sizes[from];
            var size = sizes[from] + (deltaSize * progress);
            (transform as RectTransform).sizeDelta = size;
        }

        public override void AnimateChildren(float progress, AnimationCurve curve, int from, int to, bool ascendingOrder = true)
        {
            var delta = 1f / transform.childCount;
            foreach (Transform t in transform)
            {
                var child = t.GetSiblingIndex();
                if (!ascendingOrder) child = transform.childCount - child - 1;

                var childProgress = (progress - (delta * child)) / delta;
                childProgress = Mathf.Clamp(childProgress, 0, 1);
                var curveProgress = curve.Evaluate(childProgress);
                var deltaSize = sizes[to] - sizes[from];
                var size = sizes[from] + (deltaSize * curveProgress);
                (t as RectTransform).sizeDelta = size;
            }
        }
    }
}
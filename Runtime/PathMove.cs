using UnityEngine;

namespace Arcube.TimelineAnimator
{
    [AddComponentMenu("TimelineEditor/PathMove")]
    public class PathMove : Animatable
    {
        [SerializeField] private Transform[] points;
        public override void Animate(float progress, int from, int to)
        {
            var deltaPos = points[to].position - points[from].position;
            var pos = points[from].position + (deltaPos * progress);
            transform.SetPosition(pos);
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
                var deltaPos = points[to].position - points[from].position;
                var pos = points[from].position + (deltaPos * curveProgress);
                t.SetPosition(pos);
            }
        }
    }
}
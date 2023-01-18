using UnityEngine;

namespace Arcube.TimelineAnimator
{
    [AddComponentMenu("TimelineEditor/PathJump")]
    public class PathJump : Animatable
    {
        [SerializeField] private Transform[] points;
        [SerializeField] private float jumpMaxPosition = 0;

        public override void Animate(float progress, int from, int to)
        {
            var pos = Vector3.Lerp(points[from].position, points[to].position, progress);
            pos.y += Mathf.Sin(progress * Mathf.PI) * jumpMaxPosition;
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

                var value = Vector3.Lerp(points[from].position, points[to].position, curveProgress);
                t.SetPositionLocal(value);
            }
        }
    }
}
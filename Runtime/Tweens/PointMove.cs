using UnityEngine;
using System.Collections.Generic;

namespace Arcube.TimelineAnimator
{
    [AddComponentMenu("TimelineEditor/PointMove")]
    public class PointMove : Animatable
    {
        [SerializeField] private List<Vector3> points;
        public void AddPoint() => points.Add(transform.localPosition);
        public override void Animate(float progress, int from, int to)
        {
            var deltaPos = points[to] - points[from];
            var pos = points[from] + (deltaPos * progress);
            if (transform.TryGetComponent(out RectTransform rt))
            {
                rt.SetPosition(new Vector2(pos.x, pos.y));
            }
            else
            {
                transform.SetPositionLocal(pos);
            }
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
                var deltaPos = points[to] - points[from];
                var pos = points[from] + (deltaPos * curveProgress);
                if (t.TryGetComponent(out RectTransform rt))
                {
                    rt.SetPosition(new Vector2(pos.x, pos.y));
                }
                else
                {
                    t.SetPositionLocal(pos);
                }
            }
        }
    }
}
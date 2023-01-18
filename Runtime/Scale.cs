using UnityEngine;
using System.Collections.Generic;

namespace Arcube.TimelineAnimator
{
    [AddComponentMenu("TimelineEditor/Scale")]
    public class Scale : MonoBehaviour, IAnimatable
    {
        [SerializeField] private List<Vector3> points = new List<Vector3>();
        public void AddPoint() => points.Add(transform.localScale);
        public void Animate(float progress, int from, int to)
        {
            var deltaScale = points[to] - points[from];
            transform.localScale = points[from] + (deltaScale * progress);
        }

        public void AnimateChildren(float progress, AnimationCurve curve, int from, int to, bool ascendingOrder = true)
        {
            var delta = 1f / transform.childCount;
            foreach (Transform t in transform)
            {
                var child = t.GetSiblingIndex();
                if (!ascendingOrder) child = transform.childCount - child - 1;

                var childProgress = (progress - (delta * child)) / delta;
                childProgress = Mathf.Clamp(childProgress, 0, 1);
                var curveProgress = curve.Evaluate(childProgress);
                var deltaScale = points[to] - points[from];
                t.localScale = points[from] + (deltaScale * curveProgress);
            }
        }
    }
}
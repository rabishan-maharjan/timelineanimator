using UnityEngine;
using System.Collections.Generic;

namespace Arcube.TimelineAnimator
{
    [AddComponentMenu("TimelineEditor/Rotate")]
    public class Rotate : MonoBehaviour, IAnimatable
    {
        [SerializeField] private List<Vector3> points = new List<Vector3>();
        public void AddPoint() => points.Add(transform.localEulerAngles);
        public void Animate(float progress, int from, int to)
        {
            var deltaRotation = points[to] - points[from];
            transform.localEulerAngles = points[from] + (deltaRotation * progress);
        }

        public void AnimateChildren(float progress, AnimationCurve curve, int from, int to, bool ascendingOrder = true)
        {
            var delta = 1f / transform.childCount;
            foreach (Transform t in transform)
            {
                int child = t.GetSiblingIndex();
                if (!ascendingOrder) child = transform.childCount - child - 1;

                var childProgress = (progress - (delta * child)) / delta;
                childProgress = Mathf.Clamp(childProgress, 0, 1);
                var curveProgress = curve.Evaluate(childProgress);
                var deltaRotation = points[to] - points[from];
                t.localEulerAngles = points[from] + (deltaRotation * curveProgress);
            }
        }
    }
}
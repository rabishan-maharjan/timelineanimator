using UnityEngine;
using System.Collections.Generic;

namespace Arcube.TimelineAnimator {
    [AddComponentMenu("TimelineEditor/PointMove")]
    public class PointMove : MonoBehaviour, IAnimatable {
        [SerializeField] List<Vector3> points;
        public void AddPoint() => points.Add(transform.localPosition);
        public void Animate(float progress, int from, int to) {
            Vector3 pos = Vector3.Lerp(points[from], points[to], progress);
            transform.SetPositionLocal(pos);
        }

        public void AnimateChildren(float progress, AnimationCurve curve, int from, int to) {
            float delta = 1f / transform.childCount;
            foreach (Transform t in transform) {
                int child = t.GetSiblingIndex();
                float childProgress = (progress - (delta * child)) / delta;
                childProgress = Mathf.Clamp(childProgress, 0, 1);
                float curveProgress = curve.Evaluate(childProgress);

                Vector3 value = Vector3.Lerp(points[from], points[to], curveProgress);
                t.SetPositionLocal(value);
            }
        }
    }
}
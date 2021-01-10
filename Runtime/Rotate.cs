using UnityEngine;
using System.Collections.Generic;

namespace Arcube.TimelineAnimator {
    [AddComponentMenu("TimelineEditor/Rotate")]
    public class Rotate : MonoBehaviour, IAnimatable {
        [SerializeField] List<Vector3> points = new List<Vector3>();
        public void AddPoint() => points.Add(transform.localEulerAngles);
        public void Animate(float progress, int from, int to) {
            Vector3 value = Vector3.Lerp(points[from], points[to], progress);
            transform.localEulerAngles = value;
        }

        public void AnimateChildren(float progress, AnimationCurve curve, int from, int to) {
            float delta = 1f / transform.childCount;
            foreach (Transform t in transform) {
                int child = t.GetSiblingIndex();
                float childProgress = (progress - (delta * child)) / delta;
                childProgress = Mathf.Clamp(childProgress, 0, 1);
                float curveProgress = curve.Evaluate(childProgress);

                Vector3 value = Vector3.Lerp(points[from], points[to], curveProgress);
                t.localEulerAngles = value;
            }
        }
    }
}
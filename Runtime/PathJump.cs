using UnityEngine;

namespace Arcube.TimelineAnimator {
    [AddComponentMenu("TimelineEditor/PathJump")]
    public class PathJump : MonoBehaviour, IAnimatable {
        [SerializeField] Transform []points;
        [SerializeField] float jumpMaxPosition = 0;

        public void Animate(float progress, int from, int to) {
            Vector3 pos = Vector3.Lerp(points[from].position, points[to].position, progress);
            pos.y += Mathf.Sin(progress * Mathf.PI) * jumpMaxPosition;
            transform.SetPosition(pos);
        }

        public void AnimateChildren(float progress, AnimationCurve curve, int from, int to) {
            float delta = 1f / transform.childCount;
            foreach (Transform t in transform) {
                int child = t.GetSiblingIndex();
                float childProgress = (progress - (delta * child)) / delta;
                childProgress = Mathf.Clamp(childProgress, 0, 1);
                float curveProgress = curve.Evaluate(childProgress);

                Vector3 value = Vector3.Lerp(points[from].position, points[to].position, curveProgress);
                t.SetPositionLocal(value);
            }
        }
    }
}
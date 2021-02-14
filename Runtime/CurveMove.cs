using UnityEngine;

namespace Arcube.TimelineAnimator {
    [AddComponentMenu("TimelineEditor/CurveMove")]
    public class CurveMove : MonoBehaviour, IAnimatable {
        [SerializeField] float []points = null;
        [SerializeField] float height = 5;
        public void Animate(float progress, int from, int to) {
            
        }

        public void AnimateChildren(float progress, AnimationCurve curve, int from, int to, bool ascendingOrder = true) {
            float curveProgress = curve.Evaluate(progress);

            if (TryGetComponent(out RectTransform rt)) {
                Vector2 value = rt.anchoredPosition;
                value.x = Mathf.Lerp(points[from], points[to], progress);
                value.y = curveProgress * height;

                rt.anchoredPosition = value;
            } else {
                Vector3 value = transform.position;
                value.x = Mathf.Lerp(points[from], points[to], progress);
                value.y = curveProgress * height;

                transform.SetPositionLocal(value);
            }
        }
    }
}
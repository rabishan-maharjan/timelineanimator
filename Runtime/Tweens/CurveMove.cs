using UnityEngine;

namespace Arcube.TimelineAnimator {
    [AddComponentMenu("TimelineEditor/CurveMove")]
    public class CurveMove : MonoBehaviour, IAnimatable {
        [SerializeField] Transform []points = null;
        [SerializeField] float height = 5;
        public void Animate(float progress, int from, int to) {            
        }

        public void AnimateChildren(float progress, AnimationCurve curve, int from, int to, bool ascendingOrder = true) {
            float curveProgress = curve.Evaluate(progress);
            var value = transform.position;
            value.x = Mathf.Lerp(points[from].localPosition.x, points[to].localPosition.x, progress);
            value.y = curveProgress * height;

            transform.SetPositionLocal(value);
        }
    }
}
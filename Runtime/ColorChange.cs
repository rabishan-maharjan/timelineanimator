using UnityEngine;

namespace Arcube.TimelineAnimator {
    [AddComponentMenu("TimelineEditor/ColorChange")]
    public class ColorChange : MonoBehaviour, IAnimatable {
        [SerializeField] Color[] points;
        public void Animate(float progress, int from, int to) {
            Color c = Color.Lerp(points[from], points[to], progress);
            if (transform.TryGetComponent(out UnityEngine.UI.Image image)) {
                image.color = c;
            } else if (transform.TryGetComponent(out TMPro.TMP_Text text)) {
                text.color = c;
            }else if (transform.TryGetComponent(out Renderer renderer)) {
                renderer.material.SetColor("_BaseColor", c);
            }else if(transform.TryGetComponent(out CanvasGroup group)) {
                group.alpha = c.a;
            }
        }

        public void AnimateChildren(float progress, AnimationCurve curve, int from, int to, bool ascendingOrder = true) {
            float delta = 1f / transform.childCount;
            foreach (Transform t in transform) {
                int child = t.GetSiblingIndex();
                if (!ascendingOrder) child = transform.childCount - child - 1;

                float childProgress = (progress - (delta * child)) / delta;
                childProgress = Mathf.Clamp(childProgress, 0, 1);
                float curveProgress = curve.Evaluate(childProgress);
                Color c = Color.Lerp(points[from], points[to], curveProgress);

                if (t.TryGetComponent(out UnityEngine.UI.Image image)) {
                    image.color = c;
                } else if (t.TryGetComponent(out TMPro.TMP_Text text)) {
                    text.color = c;
                } else if (t.TryGetComponent(out Renderer renderer)) {
                    renderer.material.SetColor("_BaseColor", c);
                } else if (t.TryGetComponent(out CanvasGroup group)) {
                    group.alpha = c.a;
                }
            }
        }
    }
}
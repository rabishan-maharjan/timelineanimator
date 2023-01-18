using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Arcube.TimelineAnimator
{
    [AddComponentMenu("TimelineEditor/ColorChange")]
    public class ColorChange : MonoBehaviour, IAnimatable
    {
        [SerializeField] Color[] points;
        public void Animate(float progress, int from, int to)
        {
            var c = Color.Lerp(points[from], points[to], progress);
            if (transform.TryGetComponent(out Image image))
            {
                image.color = c;
            }
            else if (transform.TryGetComponent(out TMP_Text text))
            {
                text.color = c;
            }
            else if (transform.TryGetComponent(out Renderer renderer))
            {
                renderer.material.SetColor("_BaseColor", c);
            }
            else if (transform.TryGetComponent(out CanvasGroup group))
            {
                group.alpha = c.a;
            }
        }

        public void AnimateChildren(float progress, AnimationCurve curve, int from, int to, bool ascendingOrder = true)
        {
            float delta = 1f / transform.childCount;
            foreach (Transform t in transform)
            {
                var child = t.GetSiblingIndex();
                if (!ascendingOrder) child = transform.childCount - child - 1;

                var childProgress = (progress - (delta * child)) / delta;
                childProgress = Mathf.Clamp(childProgress, 0, 1);
                var curveProgress = curve.Evaluate(childProgress);
                var c = Color.Lerp(points[from], points[to], curveProgress);

                if (t.TryGetComponent(out Image image))
                {
                    image.color = c;
                }
                else if (t.TryGetComponent(out TMP_Text text))
                {
                    text.color = c;
                }
                else if (t.TryGetComponent(out Renderer renderer))
                {
                    renderer.material.SetColor("_BaseColor", c);
                }
                else if (t.TryGetComponent(out CanvasGroup group))
                {
                    group.alpha = c.a;
                }
            }
        }
    }
}
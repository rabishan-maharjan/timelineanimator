using TMPro;
using UnityEngine;

namespace Arcube.TimelineAnimator
{
    [RequireComponent(typeof(TMP_Text))]
    [AddComponentMenu("TimelineEditor/TextChange")]
    public class TextChange : MonoBehaviour, IAnimatable
    {
        private TMP_Text t_text;
        [SerializeField] private string[] m_texts;
        private void OnValidate()
        {
            t_text = GetComponent<TMP_Text>();
        }

        public void Animate(float progress, int from, int to)
        {
            if (progress == 0) t_text.text = m_texts[from];
            else t_text.text = m_texts[to];
        }

        public void AnimateChildren(float progress, AnimationCurve curve, int from, int to, bool ascendingOrder = true)
        {
        }
    }
}
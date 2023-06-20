using TMPro;
using UnityEngine;

namespace Arcube.TimelineAnimator
{
    [RequireComponent(typeof(TMP_Text))]
    [AddComponentMenu("TimelineEditor/LetterAnimate")]
    public class LetterAnimate : MonoBehaviour, IAnimatable
    {
        private TMP_Text t_text;
        [SerializeField] private string m_text;
        private void OnValidate()
        {
            t_text = GetComponent<TMP_Text>();
        }

        public void Animate(float progress, int from, int to)
        {
            if (progress == 0) t_text.text = "";
            else if (progress == 1) t_text.text = m_text;
            else
            {
                var len = m_text.Length;
                var c = (int)(progress * (len - 1));
                t_text.text = m_text.Remove(c + 1);
            }
        }

        public void AnimateChildren(float progress, AnimationCurve curve, int from, int to, bool ascendingOrder = true)
        {
        }
    }
}
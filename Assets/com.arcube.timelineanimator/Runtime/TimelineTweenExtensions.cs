using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Arcube.TimelineAnimator {
    public static class Extensions {
        public static void UpdateFade(this Image image, float value) {
            Color color = image.color;
            color.a = value;
            image.color = color;
        }

        public static void UpdateMove(this RectTransform rt, Vector2 value) => rt.anchoredPosition = value;

        public static void UpdateMove(this Transform t, Vector3 value) => t.position = value;

        public static void UpdateText(this TMP_Text text, float progress) => text.maxVisibleCharacters = (int)(progress * text.textInfo.characterCount);

        public static void UpdateScale(this Transform t, Vector3 value) => t.localScale = value;
    }
}
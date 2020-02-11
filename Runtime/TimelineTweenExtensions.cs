using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Arcube.TimelineAnimator {
    public static class Extensions {
        public static void SetFade(this Image image, float value) {
            Color color = image.color;
            color.a = value;
            image.color = color;
        }

        public static void SetFade(this Text text, float value) {
            Color c = new Color(text.color.r, text.color.g, text.color.b, value);
            text.color = c;
        }

        public static void SetFade(this CanvasGroup canvasGroup, float value) => canvasGroup.alpha = value;

        public static void SetPosition(this RectTransform rt, Vector2 value) => rt.anchoredPosition = value;

        public static void SetPosition(this Transform t, Vector3 value) => t.position = value;

        public static void UpdateText(this TMP_Text text, float progress) => text.maxVisibleCharacters = (int)(progress * text.textInfo.characterCount);

        public static void SetScale(this Transform t, Vector3 value) => t.localScale = value;

        public static void SetRotate(this Transform t, Vector3 angle) => t.eulerAngles = new Vector3(angle.x, angle.y, angle.z);

        public static void SetFill(this Image image, float value) => image.fillAmount = value;
    }
}
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Arcube.TimelineAnimator {
    public static class Extensions {
        public static void SetFade(this Image image, float value) => image.color = image.color.SetAlpha(value);

        public static Color SetAlpha(this Color color, float value) {
            color.a = value;
            return color;
        }

        public static void SetColor(this Image image, Vector3 value) => image.color = new Color(value.x, value.y, value.z);

        public static void SetFade(this TMP_Text text, float value) => text.color = text.color.SetAlpha(value);

        public static void SetColor(this TMP_Text text, Vector3 value) => text.color = new Color(value.x, value.y, value.z);

        public static void SetFade(this CanvasGroup canvasGroup, float value) => canvasGroup.alpha = value;

        public static void SetPosition(this RectTransform rt, Vector2 value) => rt.position = value;

        public static void SetPosition(this Transform t, Vector3 value) => t.localPosition = value;

        public static void UpdateText(this TMP_Text text, float progress) => text.maxVisibleCharacters = (int)(progress * text.textInfo.characterCount);

        public static void SetScale(this Transform t, Vector3 value) => t.localScale = value;

        public static void SetRotate(this Transform t, Vector3 angle) => t.eulerAngles = new Vector3(angle.x, angle.y, angle.z);

        public static void SetFill(this Image image, float value) => image.fillAmount = value;
    }
}
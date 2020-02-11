using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;

namespace Arcube.TimelineAnimator {
    [CustomPropertyDrawer(typeof(TimelineAnimatorBehaviour))]
    public class TimelineAnimatorDrawer : PropertyDrawer {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            int fieldCount = 1;
            return fieldCount * EditorGUIUtility.singleLineHeight;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            SerializedProperty target = property.FindPropertyRelative("target");
            SerializedProperty curve = property.FindPropertyRelative("curve");
            SerializedProperty from = property.FindPropertyRelative("from");
            SerializedProperty to = property.FindPropertyRelative("to");
            SerializedProperty tweenType = property.FindPropertyRelative("tweenType");

            //SerializedProperty start = property.FindPropertyRelative("start");
            //SerializedProperty end = property.FindPropertyRelative("end");
            //SerializedProperty duration = property.FindPropertyRelative("duration");

            Rect singleFieldRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
            EditorGUILayout.PropertyField(curve);
            EditorGUILayout.PropertyField(from);
            EditorGUILayout.PropertyField(to);
            EditorGUILayout.PropertyField(tweenType);

            //EditorGUILayout.FloatField("Start", start.floatValue);
            //EditorGUILayout.FloatField("End", end.floatValue);
            //EditorGUILayout.FloatField("Duration", duration.floatValue);

            if (GUILayout.Button("Set From")) {
                from.vector3Value = GetValue(GetObject(), tweenType.enumValueIndex);
            }

            if (GUILayout.Button("Set To")) {
                to.vector3Value = GetValue(GetObject(), tweenType.enumValueIndex);
            }
        }

        public static GameObject GetObject() {
            var obj = Selection.activeObject;
            GameObject gameObject = null;
            if (obj != null) {
                var fi = obj.GetType().GetField("m_Clip", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
                if (fi != null) {
                    if (fi.GetValue(obj) is TimelineClip clip) {
                        TimelineAnimatorClip asset = clip.asset as TimelineAnimatorClip;
                        gameObject = asset._targetReference;
                    }
                }
            }

            return gameObject;
        }

        public static Vector3 GetValue(GameObject obj, int tweenType) {
            if (obj == null) tweenType = 99;
            switch ((Tween)tweenType) {
                case Tween.Color:
                    if (obj.TryGetComponent(out Image image)) {
                        return new Vector3(image.color.r, image.color.g, image.color.b);
                    } else if(obj.TryGetComponent(out TMPro.TMP_Text text)) {
                        return new Vector3(text.color.r, text.color.g, text.color.b);
                    }
                    return Vector3.zero;
                case Tween.Scale:
                    return obj.transform.localScale;
                case Tween.Move:
                    if (obj.TryGetComponent(out RectTransform rect)) {
                        return new Vector3(rect.position.x, rect.position.y, 0);
                    }
                    return obj.transform.localPosition;
                default:
                    Debug.LogError("Timeline animator value set wrong!");
                    return Vector3.zero;
            }
        }
    }
}
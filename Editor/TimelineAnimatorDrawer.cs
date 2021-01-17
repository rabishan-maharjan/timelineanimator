using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.Timeline;

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
            SerializedProperty type = property.FindPropertyRelative("type");
            SerializedProperty targetChildren = property.FindPropertyRelative("targetChildren");
            SerializedProperty ascendingOrder = property.FindPropertyRelative("ascendingOrder");

            EditorGUILayout.PropertyField(curve);
            EditorGUILayout.PropertyField(from);
            EditorGUILayout.PropertyField(to);
            EditorGUILayout.PropertyField(type);
            EditorGUILayout.PropertyField(targetChildren);
            if(targetChildren.boolValue) EditorGUILayout.PropertyField(ascendingOrder);
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
    }
}
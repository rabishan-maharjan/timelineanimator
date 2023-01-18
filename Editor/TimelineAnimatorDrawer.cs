using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.Timeline;

namespace Arcube.TimelineAnimator
{
    [CustomPropertyDrawer(typeof(TimelineAnimatorBehaviour))]
    public class TimelineAnimatorDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight;
        }

        private int _choiceIndex;
        private string[] _choices;
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var target = property.FindPropertyRelative("target");
            var curve = property.FindPropertyRelative("curve");
            var from = property.FindPropertyRelative("from");
            var to = property.FindPropertyRelative("to");
            var scriptType = property.FindPropertyRelative("scriptType");
            var targetChildren = property.FindPropertyRelative("targetChildren");
            var ascendingOrder = property.FindPropertyRelative("ascendingOrder");

            //scriptType.enumValueIndex = (int)(AnimationTypes)EditorGUI.EnumPopup(position, "scriptType", (AnimationTypes)scriptType.intValue);
            EditorGUILayout.PropertyField(curve);
            EditorGUILayout.PropertyField(from);
            EditorGUILayout.PropertyField(to);
            
            _choices = GetAnimatableList();
            _choiceIndex = EditorGUI.Popup(position, _choiceIndex, _choices);
            if (EditorGUI.EndChangeCheck())
            {
                scriptType.stringValue = _choices[_choiceIndex];
            }

            EditorGUILayout.PropertyField(targetChildren);
            if (targetChildren.boolValue) EditorGUILayout.PropertyField(ascendingOrder);
        }

        private Object old;
        private string[] GetAnimatableList()
        {   
            var obj = Selection.activeObject;
            if(obj == old)
            {
                return _choices;
            }

            if (obj != null)
            {
                var choices = new List<string>();
                var type = obj.GetType();
                var fi = type.GetField("m_Clip", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
                if (fi != null)
                {
                    var clip = fi.GetValue(obj) as TimelineClip;
                    var asset = clip.asset as TimelineAnimatorClip;
                    var targetRef = asset._targetReference;
                    if (targetRef != null)
                    {
                        foreach (var v in targetRef.GetComponentsInChildren<IAnimatable>())
                        {
                            choices.Add(v.GetType().Name);
                        }
                    }
                }

                return choices.ToArray();
            }

            old = obj;
            return null;
        }

        public static GameObject GetObject()
        {
            var obj = Selection.activeObject;
            GameObject gameObject = null;
            if (obj != null)
            {
                var fi = obj.GetType().GetField("m_Clip", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
                if (fi != null)
                {
                    if (fi.GetValue(obj) is TimelineClip clip)
                    {
                        var asset = clip.asset as TimelineAnimatorClip;
                        gameObject = asset._targetReference;
                    }
                }
            }

            return gameObject;
        }
    }
}
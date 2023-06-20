using UnityEditor;
using UnityEngine;

namespace Arcube.TimelineAnimator
{
    [CustomPropertyDrawer(typeof(TimelineAnimatorBehaviour))]
    public class TimelineAnimatorDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var target = property.FindPropertyRelative("target");
            var script = property.FindPropertyRelative("script");
            var curve = property.FindPropertyRelative("curve");
            var from = property.FindPropertyRelative("from");
            var to = property.FindPropertyRelative("to");
            var targetChildren = property.FindPropertyRelative("targetChildren");
            var ascendingOrder = property.FindPropertyRelative("ascendingOrder");

            //scriptType.enumValueIndex = (int)(AnimationTypes)EditorGUI.EnumPopup(position, "scriptType", (AnimationTypes)scriptType.intValue);
            EditorGUILayout.PropertyField(script);
            EditorGUILayout.PropertyField(curve);
            EditorGUILayout.PropertyField(from);
            EditorGUILayout.PropertyField(to);
            EditorGUILayout.PropertyField(targetChildren);
            if (targetChildren.boolValue) EditorGUILayout.PropertyField(ascendingOrder);
        }

        //private int _choiceIndex;
        //private string[] _choices;
        //private Object old;
        //private string[] GetAnimatableList()
        //{   
        //    var obj = Selection.activeObject;
        //    if(obj == old)
        //    {
        //        return _choices;
        //    }

        //    old = obj;
        //    if (obj != null)
        //    {
        //        var choices = new List<string>();
        //        var type = obj.GetType();
        //        var fi = type.GetField("m_Clip", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
        //        if (fi != null)
        //        {
        //            var clip = fi.GetValue(obj) as TimelineClip;
        //            var asset = clip.asset as TimelineAnimatorClip;
        //            var targetRef = asset._targetReference;
        //            if (targetRef != null)
        //            {
        //                foreach (var v in targetRef.GetComponentsInChildren<IAnimatable>())
        //                {
        //                    choices.Add(v.GetType().Name);
        //                }
        //            }
        //        }

        //        return choices.ToArray();
        //    }

        //    return null;
        //}
    }
}
using UnityEngine;
using NaughtyAttributes;
using System.Collections.Generic;

namespace Arcube.TimelineAnimator {
    [AddComponentMenu("TimelineEditor/Rotate")]
    public class Rotate : MonoBehaviour, IAnimatable {
        [ReorderableList] [SerializeField] List<Vector3> points = new List<Vector3>();

        [Button] public void AddPoint() => points.Add(transform.localRotation.eulerAngles);

        public void Animate(float progress, int from, int to) {
            Vector3 value = Vector3.Lerp(points[from], points[to], progress);
            Quaternion rot = transform.localRotation;
            rot.eulerAngles = value;
            transform.localRotation = rot;
        }
    }
}
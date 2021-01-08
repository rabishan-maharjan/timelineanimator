using UnityEngine;
using NaughtyAttributes;
using System.Collections.Generic;

namespace Arcube.TimelineAnimator {
    [AddComponentMenu("TimelineEditor/PointMove")]
    public class PointMove : MonoBehaviour, IAnimatable {
        [ReorderableList] [SerializeField] List<Vector3> points;

        [Button] public void AddPoint() => points.Add(transform.localPosition);

        public void Animate(float progress, int from, int to) {
            Vector3 pos = Vector3.Lerp(points[from], points[to], progress);
            transform.SetPositionLocal(pos);
        }
    }
}
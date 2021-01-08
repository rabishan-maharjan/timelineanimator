using NaughtyAttributes;
using UnityEngine;

namespace Arcube.TimelineAnimator {
    [AddComponentMenu("TimelineEditor/PathMove")]
    public class PathMove : MonoBehaviour, IAnimatable {
        [ReorderableList] [SerializeField] Transform []points;
        public void Animate(float progress, int from, int to) {
            Vector3 pos = Vector3.Lerp(points[from].position, points[to].position, progress);
            transform.SetPosition(pos);
        }
    }
}
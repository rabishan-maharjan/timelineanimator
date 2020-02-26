﻿using NaughtyAttributes;
using UnityEngine;

namespace Arcube.TimelineAnimator {
    public class PathJump : MonoBehaviour, IAnimatable {
        [ReorderableList] [SerializeField] Transform []points;
        [SerializeField] float jumpMaxPosition = 0;
        [SerializeField] System.Type type;
        public void Animate(float progress, int from, int to) {
            Vector3 pos = Vector3.Lerp(points[from].position, points[to].position, progress);
            pos.y += Mathf.Sin(progress * Mathf.PI) * jumpMaxPosition;
            transform.SetPosition(pos);
        }
    }
}
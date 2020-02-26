﻿using UnityEngine;
using NaughtyAttributes;
using System.Collections.Generic;

namespace Arcube.TimelineAnimator {
    public class Scale : MonoBehaviour, IAnimatable {
        [ReorderableList] [SerializeField] List<Vector3> points;

        [Button]
        public void AddPoint() => points.Add(transform.localScale);

        public void Animate(float progress, int from, int to) {
            Vector3 value = Vector3.Lerp(points[from], points[to], progress);
            transform.localScale = value;
        }
    }
}
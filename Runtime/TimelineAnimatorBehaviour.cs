using System;
using UnityEngine;
using UnityEngine.Playables;

namespace Arcube.TimelineAnimator {
    [Serializable]
    public class TimelineAnimatorBehaviour : PlayableBehaviour {
        public GameObject target;
        public Vector3 fromPoint, toPoint;
        public AnimationCurve curve;
        public float duration;
        public Tween tweenType;
        public double start;
        public double end;
    }
}
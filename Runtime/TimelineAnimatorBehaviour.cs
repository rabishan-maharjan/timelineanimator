using System;
using UnityEngine;
using UnityEngine.Playables;

namespace Arcube.TimelineAnimator {
    [Serializable]
    public class TimelineAnimatorBehaviour : PlayableBehaviour {
        public GameObject target;
        public Vector3 from;
        public Vector3 to;
        public AnimationCurve curve;
        public float duration;
        public Tween tweenType;
        public double start;
        public double end;
    }
}
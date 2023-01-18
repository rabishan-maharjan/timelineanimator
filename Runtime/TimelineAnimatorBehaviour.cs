using System;
using UnityEngine;
using UnityEngine.Playables;

namespace Arcube.TimelineAnimator {
    [Serializable]
    public class TimelineAnimatorBehaviour : PlayableBehaviour {
        public GameObject target;
        public int from = 0;
        public int to = 1;
        public AnimationCurve curve;
        public float duration;
        public string scriptType;
        public double start;
        public double end;
        public float clipLength;

        public bool targetChildren = false;
        public bool ascendingOrder = true;
    }
}
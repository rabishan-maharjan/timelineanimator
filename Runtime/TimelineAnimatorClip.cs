using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Arcube.TimelineAnimator
{
    [Serializable]
    public class TimelineAnimatorClip : PlayableAsset, ITimelineClipAsset
    {
        public TimelineAnimatorBehaviour template = new TimelineAnimatorBehaviour();
        public ExposedReference<GameObject> target;
        [HideInInspector] public GameObject _targetReference;

        public ClipCaps clipCaps
        {
            get { return ClipCaps.Looping | ClipCaps.Extrapolation | ClipCaps.SpeedMultiplier | ClipCaps.Blending; }
        }

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<TimelineAnimatorBehaviour>.Create(graph, template);
            TimelineAnimatorBehaviour clone = playable.GetBehaviour();
            clone.target = target.Resolve(graph.GetResolver());
            _targetReference = clone.target;
            return playable;
        }
    }
}
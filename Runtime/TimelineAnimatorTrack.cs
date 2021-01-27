using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Arcube.TimelineAnimator {
    [TrackColor(0.5188679f, 0.119927f, 0.1310735f)]
    [TrackClipType(typeof(TimelineAnimatorClip))]
    public class TimelineAnimatorTrack : TrackAsset {
        public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount) {
            foreach (var clip in GetClips()) {
                var myAsset = clip.asset as TimelineAnimatorClip;
                if (myAsset) {
                    myAsset.template.duration = (float)clip.duration;
                    myAsset.template.start = clip.start;
                    myAsset.template.end = clip.end;
                }

                clip.displayName = myAsset.target.Resolve(graph.GetResolver()).name; //update name based on target object
            }

            return ScriptPlayable<TimelineAnimatorMixerBehaviour>.Create(graph, inputCount);
        }
    }
}
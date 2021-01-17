using UnityEngine.Playables;

namespace Arcube.TimelineAnimator {
    public class TimelineAnimatorMixerBehaviour : PlayableBehaviour {
        // NOTE: This function is called at runtime and edit time.  Keep that in mind when setting the values of properties.
        public override void ProcessFrame(Playable playable, FrameData info, object playerData) {
            int inputCount = playable.GetInputCount();

            for (int i = 0; i < inputCount; i++) {
                ScriptPlayable<TimelineAnimatorBehaviour> inputPlayable = (ScriptPlayable<TimelineAnimatorBehaviour>)playable.GetInput(i);
                TimelineAnimatorBehaviour input = inputPlayable.GetBehaviour();
                
                if (!input.target || !input.target.activeSelf) continue;

                if (inputPlayable.GetPlayState() != PlayState.Playing) continue;

                // Use the above variables to process each frame of this playable.
                float timeNormalized = (float)(inputPlayable.GetTime() / input.duration);
                Animate(input, timeNormalized);
            }
        }

        void Animate(TimelineAnimatorBehaviour input, float progress) {            
            if (input.target.GetComponent(input.type) is IAnimatable animatable) {
                if (input.targetChildren) {
                    animatable.AnimateChildren(progress, input.curve, input.from, input.to, input.ascendingOrder);
                } else {
                    float curveProgress = input.curve.Evaluate(progress);
                    animatable.Animate(curveProgress, input.from, input.to);
                }
            }
        }
    }
}
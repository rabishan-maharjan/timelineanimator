using UnityEngine.Playables;

namespace Arcube.TimelineAnimator
{
    public class TimelineAnimatorMixerBehaviour : PlayableBehaviour
    {
        // NOTE: This function is called at runtime and edit time.  Keep that in mind when setting the values of properties.
        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            var inputCount = playable.GetInputCount();

            for (var i = 0; i < inputCount; i++)
            {
                var inputPlayable = (ScriptPlayable<TimelineAnimatorBehaviour>)playable.GetInput(i);
                var input = inputPlayable.GetBehaviour();

                if (!input.target || !input.target.activeSelf) continue;

                if (inputPlayable.GetPlayState() != PlayState.Playing) continue;

                // Use the above variables to process each frame of this playable.
                var timeNormalized = (float)(inputPlayable.GetTime() / input.duration);
                Animate(input, timeNormalized);
            }
        }

        private void Animate(TimelineAnimatorBehaviour input, float progress)
        {
            if (input.target.GetComponent(input.scriptType) is IAnimatable animatable)
            {
                if (input.targetChildren)
                {
                    animatable.AnimateChildren(progress, input.curve, input.from, input.to, input.ascendingOrder);
                }
                else
                {
                    var curveProgress = input.curve.Evaluate(progress);
                    animatable.Animate(curveProgress, input.from, input.to);
                }
            }
        }
    }
}
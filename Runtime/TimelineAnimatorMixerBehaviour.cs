using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

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

                //double time = playable.GetTime();
                //if (input.start >= time || input.end <= time) continue;

                // Use the above variables to process each frame of this playable.
                float timeNormalized = (float)(inputPlayable.GetTime() / input.duration);

                //float progress = input.curve.Evaluate(timeNormalized);
                Animate(input, timeNormalized);
            }
        }

        void Animate(TimelineAnimatorBehaviour input, float progress) {
            float curveProgress = input.curve.Evaluate(progress);
            Vector3 value = Vector3.Lerp(input.from, input.to, curveProgress);

            switch (input.tweenType) {
                case Tween.Text:
                    input.target.GetComponent<TMP_Text>().UpdateText(progress);
                    break;
                case Tween.Move:
                    if (input.target.TryGetComponent(out RectTransform rectTransform)) {
                        rectTransform.SetPosition(new Vector2(value.x, value.y));
                    } else {
                        input.target.transform.SetPosition(new Vector2(value.x, value.y));
                    }                    
                    break;
                case Tween.Scale:
                    input.target.transform.SetScale(value);
                    break;
                case Tween.Fade:
                    if (input.target.TryGetComponent(out Image image)) {
                        image.SetFade(value.x);
                    } else if (input.target.TryGetComponent(out TMP_Text text)) {
                        text.SetFade(value.x);
                    }
                    break;
                case Tween.Color:
                    if (input.target.TryGetComponent(out image)) {
                        image.SetColor(value);
                    } else if (input.target.TryGetComponent(out TMP_Text text)) {
                        text.SetColor(value);
                    }
                    break;
            }
        }
    }
}
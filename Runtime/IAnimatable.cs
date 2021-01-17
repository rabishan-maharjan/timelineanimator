using UnityEngine;

public interface IAnimatable {
    void Animate(float progress, int from, int to);

    void AnimateChildren(float progress, AnimationCurve curve, int from, int to, bool ascendingOrder = true);
}
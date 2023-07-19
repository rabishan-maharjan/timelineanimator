using UnityEngine;

public abstract class Animatable : MonoBehaviour, IAnimatable
{
    public abstract void Animate(float progress, int from, int to);

    public abstract void AnimateChildren(float progress, AnimationCurve curve, int from, int to, bool ascendingOrder = true);
}
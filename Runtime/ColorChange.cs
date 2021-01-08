﻿using NaughtyAttributes;
using UnityEngine;

namespace Arcube.TimelineAnimator {
    [AddComponentMenu("TimelineEditor/ColorChange")]
    public class ColorChange : MonoBehaviour, IAnimatable {
        [ReorderableList] [SerializeField] Color[] points;
        public void Animate(float progress, int from, int to) {
            Color c = Color.Lerp(points[from], points[to], progress);
            if (transform.TryGetComponent(out UnityEngine.UI.Image image)) {
                image.color = c;
            } else if (transform.TryGetComponent(out TMPro.TMP_Text text)) {
                text.color = c;
            }else if (transform.TryGetComponent(out Renderer renderer)) {
                renderer.material.SetColor("_BaseColor", c);
            }else if(transform.TryGetComponent(out CanvasGroup group)) {
                group.alpha = c.a;
            }
        }
    }
}
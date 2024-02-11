using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ui
{
    [RequireComponent(typeof(CanvasGroup))]
    public class FadePanel : MonoBehaviour
    {
        [SerializeField] private float duration = 0.5f;

        private CanvasGroup _canvasGroup;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public void FadeOut(Action callback)
        {
            if (!gameObject.activeSelf)
                _canvasGroup.alpha = 0f;
            else
                StartCoroutine(FadeCoroutine(1f, 0f, duration, callback));
            
        }

        public void FadeIn(Action callback)
        {
            StartCoroutine(FadeCoroutine(0f, 1f, duration, () =>
            {
                callback?.Invoke();
            }));
        }

        public void SetVisible()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _canvasGroup.alpha = 1f;
        }

        private IEnumerator FadeCoroutine(float from, float to, float duration, Action callback)
        {
            var elapsedTime = 0f;
            while (elapsedTime < duration)
            {
                _canvasGroup.alpha = Mathf.Lerp(from, to, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            _canvasGroup.alpha = to;
            callback?.Invoke();
        }
    }
}
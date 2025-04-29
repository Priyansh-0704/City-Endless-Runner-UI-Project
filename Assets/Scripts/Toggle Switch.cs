using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class ToggleSwitch : MonoBehaviour, IPointerClickHandler
{
    public bool IsOn { get; private set; }

    [SerializeField] private float animationDuration = 0.25f;
    [SerializeField] private AnimationCurve slideEase = AnimationCurve.EaseInOut(0, 0, 1, 1);

    [Header("Events")]
    [SerializeField] private UnityEvent onToggleOn;
    [SerializeField] private UnityEvent onToggleOff;

    private Slider slider;
    private Coroutine animRoutine;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        slider.minValue = 0f;
        slider.maxValue = 1f;
        slider.wholeNumbers = false;
        slider.interactable = false;
        slider.value = 0f;

        var colors = slider.colors;
        colors.disabledColor = Color.white;
        slider.colors = colors;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SetState(!IsOn);
    }

    public void SetState(bool state)
    {
        if (IsOn == state) return;

        IsOn = state;

        if (IsOn)
            onToggleOn?.Invoke();
        else
            onToggleOff?.Invoke();

        if (animRoutine != null)
            StopCoroutine(animRoutine);

        animRoutine = StartCoroutine(AnimateSlider(IsOn ? 1f : 0f));
    }

    private IEnumerator AnimateSlider(float target)
    {
        float start = slider.value;
        float elapsed = 0f;

        while (elapsed < animationDuration)
        {
            elapsed += Time.deltaTime;
            float t = slideEase.Evaluate(elapsed / animationDuration);
            slider.value = Mathf.Lerp(start, target, t);
            yield return null;
        }

        slider.value = target;
    }
}
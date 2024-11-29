using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ToggleButton : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Image toggleMainImage;
    [SerializeField] private Sprite toggleMainOn, toggleMainOff;

    [SerializeField] private Transform toggleKnob;
    [SerializeField] private Transform knobOnPosition, knobOffPosition;

    [SerializeField] private bool value;
    [SerializeField] private UnityEvent<bool> OnValueChange;

    public void SetValue(bool state)
    {
        value = state;

        toggleMainImage.sprite = value ? toggleMainOn : toggleMainOff;
        toggleKnob.DOMove(value ? knobOnPosition.position : knobOffPosition.position, .1f);
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        SetValue(!value);
        
        GameManager.Instance.audioManager.PlayUIButtonClick();
        HapticManager.GenerateHaptic(PresetType.Selection);
        OnValueChange?.Invoke(value);
    }
}

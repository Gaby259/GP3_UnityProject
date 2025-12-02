using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonsTest : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Tweener _tween;
    [SerializeField] float duration = 0.2f;
    [SerializeField] private float buttonScale = 2.0f;
    
    private Vector3 _originalScale;

    private void Awake()
    {
        _originalScale = transform.localScale; // Save the original scale of the button 
    }

    private void OnEnable()
    {
        transform.localScale = _originalScale;
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        _tween= this.transform.DOScale(buttonScale, duration).SetUpdate(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _tween?.Kill();// Must kill old tween
        //Previous transform is PERMANENT, not just an animation. so we scale it back by hand
        this.transform.DOScale(_originalScale, duration).SetUpdate(true);
    }

    public void OnClick()
    {
        // Create a sequence 
        // We will set it so that the whole duration is 6
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DORotate(new Vector3(0, 45, 0), duration));
        //Add a horizontal relative move tween that will last the whole Sequence's duration 
        sequence.SetLoops(-1, LoopType.Yoyo);
    }
    
    
}

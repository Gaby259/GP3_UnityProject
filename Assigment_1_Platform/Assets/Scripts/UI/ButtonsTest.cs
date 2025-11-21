using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonsTest : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Tweener _tween;
    [SerializeField] float _duration = 0.2f;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        _tween= this.transform.DOScale(1.5f, _duration);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _tween?.Kill();// Must kill old tween
        //Previous transform is PERMANENT, not just an animation. so we scale it back by hand
        this.transform.DOScale(1f, _duration);
    }

    public void OnClick()
    {
        // Create a sequence 
        // We will set it so that the whole duration is 6
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DORotate(new Vector3(0, 45, 0), _duration));
        //Add a horizontal relative move tween that will last the whole Sequence's duration 
        sequence.SetLoops(-1, LoopType.Yoyo);
    }
}

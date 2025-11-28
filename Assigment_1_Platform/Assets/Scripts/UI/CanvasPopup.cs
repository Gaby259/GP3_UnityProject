using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CanvasPopup : MonoBehaviour
{
    [SerializeField] private Image  _image;
    [SerializeField] private Transform _popUplocation;
    private float _fadeDuration = 0.5f;
    
    private void Start()
    {
        Color color = _image.color;
        color.a = 0;
        _image.color = color;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           FadeIn();
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FadeOut();
        }
    }
    private void FadeIn()
    {
        _image.DOFade(1f, _fadeDuration).SetEase(Ease.Linear);
    }
    private void FadeOut()
    {
        _image.DOFade(0f, _fadeDuration).SetEase(Ease.Linear);
    }
}


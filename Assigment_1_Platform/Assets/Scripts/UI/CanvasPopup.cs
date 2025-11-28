using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CanvasPopup : MonoBehaviour
{
    [SerializeField] private Image  _image;
    [SerializeField] private Transform _popUplocation;
    private float _fadeduration = 2f; 
    

    private void Start()
    {
        _image.gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _image.gameObject.SetActive(true);
            
            
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _image.gameObject.SetActive(false);
        }
    }
}


using System;
using UnityEngine;
using UnityEngine.UI;

public class ShopView : MonoBehaviour
{
    public event Action CloseButtonClicked;

    [SerializeField] private Button _outsideButton;
    [SerializeField] private Button _closeButton;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _closeButton.onClick.AddListener(OnCloseButtonClicked);
        _outsideButton?.onClick.AddListener(OnCloseButtonClicked);
    }
    private void OnDisable()
    {
        _closeButton.onClick.RemoveListener(OnCloseButtonClicked);
        _outsideButton?.onClick.RemoveListener(OnCloseButtonClicked);
    }

    private void OnCloseButtonClicked() => CloseButtonClicked?.Invoke();
}

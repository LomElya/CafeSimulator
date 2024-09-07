using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Modification;
using System;

public class ModificationView : MonoBehaviour
{
    [SerializeField] private Button _buyButton;
    [SerializeField] private TMP_Text _price;
    [SerializeField] private TMP_Text _maxText;
    [SerializeField] private TMP_Text _modificationLevel;
    [SerializeField] private string _levelText = "Lvl";

    public event Action TryBuy;
    public event Action Opened;

    private void OnEnable()
    {
        Opened?.Invoke();
        _buyButton.onClick.AddListener(OnBuyButtonClicked);
    }

    private void OnDisable() => _buyButton.onClick.RemoveListener(OnBuyButtonClicked);

    public void Render<T>(ModificationData<T> modificationData, int level)
    {
        _price.text = modificationData.Price.ToString();
        _modificationLevel.text = $"{_levelText} {level}";

        if (_buyButton.interactable == false)
            _buyButton.interactable = true;

        _buyButton.gameObject.SetActive(true);
        _maxText.enabled = false;
    }

    public void RenderLocked()
    {
        _buyButton.gameObject.SetActive(true);
        _buyButton.interactable = false;
    }

    public void RenderCompleted(int level)
    {
        _modificationLevel.text = $"{_levelText} {level}";
        _buyButton.gameObject.SetActive(false);
        _maxText.enabled = true;
    }

    private void OnBuyButtonClicked() => TryBuy?.Invoke();
}

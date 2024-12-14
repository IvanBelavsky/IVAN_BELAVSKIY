using System;
using UnityEngine;
using Button = UnityEngine.UI.Button;
using DG.Tweening;

public class StartButton : MonoBehaviour
{
    public event Action OnClick;
    
    [SerializeField] private Button _button;
    [SerializeField] private float _jumpHight;
    [SerializeField] private float _jumpDuration;
    [SerializeField] private float _fallingPosition;
    [SerializeField] private float _fallingDuration;

    private Sequence _sequence;

    private void Start()
    {
        _button.onClick.AddListener(Click);
        _sequence = DOTween.Sequence();
    }

    private void Click()
    {
        _button.onClick.RemoveListener(Click);
        OnClick?.Invoke();

        RectTransform rectTransform = (RectTransform)transform;
        _sequence
            .Append(rectTransform.DOAnchorPosY(_jumpHight, _jumpDuration))
            .Append(rectTransform.DOAnchorPosY(_fallingPosition, _fallingDuration))
            .onComplete += () => gameObject.SetActive(false);
    }
}
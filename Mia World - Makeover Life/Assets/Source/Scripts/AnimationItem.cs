using System;
using DG.Tweening;
using UnityEngine;

[Serializable]
public class AnimationItem
{
    [SerializeField] private float duration = 1f;
    [SerializeField] private int bounceCount = 1;

    private Sequence _cuttentSequence;

    public void Animate(DraggableItem item)
    {
        if (_cuttentSequence != null && _cuttentSequence.IsActive())
        {
            DestroySequence();
        }

        _cuttentSequence = DOTween.Sequence();

        float bounceDuration = duration / (bounceCount * 2);

        for (int i = 0; i < bounceCount; i++)
        {
            _cuttentSequence.Append(item.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), bounceDuration)
                    .SetEase(Ease.OutBounce))
                .Append(item.transform.DOScale(Vector3.one, bounceDuration).SetEase(Ease.OutBounce));
        }

        _cuttentSequence.Play();
    }

    public void DestroySequence()
    {
        if (_cuttentSequence != null)
        {
            _cuttentSequence.Kill();
            _cuttentSequence = null;
        }
    }
}
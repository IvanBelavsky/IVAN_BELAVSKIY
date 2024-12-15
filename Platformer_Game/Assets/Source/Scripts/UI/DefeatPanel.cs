using System;
using UnityEngine;
using UnityEngine.UI;

public class DefeatPanel : MonoBehaviour
{
   public event Action OnRestart;
   
   [SerializeField] private Button _restartButton;

   private void OnEnable()
   {
      _restartButton.onClick.AddListener(OnRestartClick);
   }

   private void OnDisable()
   {
      _restartButton.onClick.RemoveListener(OnRestartClick);
   }

   public void Show() => gameObject.SetActive(true);
   public void Hide() => gameObject.SetActive(false);

   private void OnRestartClick()
   {
      OnRestart?.Invoke();
   }
}

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestatrButton : MonoBehaviour
{
   [SerializeField] private Button _restartButton;

   private void OnEnable()
   {
      _restartButton.onClick.AddListener(RestartScene);
   }

   private void OnDisable()
   {
      _restartButton.onClick.RemoveListener(RestartScene);
   }

   private void RestartScene()
   {
      SceneManager.LoadScene("Scenes/SampleScene");
   }
}

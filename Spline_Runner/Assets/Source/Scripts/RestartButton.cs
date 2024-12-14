using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    public event Action OnClickRestatr;

    [SerializeField] private Player _player;
    [SerializeField] private Button _button;

    private void Awake()
    {
        gameObject.SetActive(false);
        _player.OnDied += SetActiveRestart;
    }

    private void Start()
    {
        _button.onClick.AddListener(Click);
    }

    private void OnDisable()
    {
        _player.OnDied -= SetActiveRestart;
    }

    private void SetActiveRestart()
    {
        gameObject.SetActive(true);
    }

    private void Click()
    {
        _button.onClick.RemoveListener(Click);
        OnClickRestatr?.Invoke();
        gameObject.SetActive(false);
        RestartScene();
    }

    private void RestartScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
}
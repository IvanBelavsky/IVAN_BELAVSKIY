using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Zenject;

public class GameBehaviourUI : MonoBehaviour
{
    public event Action OnClickMainMenuButton;

    [SerializeField] private Button _pause;
    [SerializeField] private Button _play;
    [SerializeField] private Button _mainMenu;
    [SerializeField] private Button _playSound;
    [SerializeField] private Button _disableSound;
    [SerializeField] private float _minVolumeSound = -80, _volumeSound = 0;

    private AudioMixer _audioMixer;
    private SceneService _sceneService;
    private PauseService _pauseService;
    private SaveService _saveService;
    private LoadingServiceGame _loading;
    private float _startVolumeSound;
    private bool _isDisableSound;
    private bool _isPause;

    [Inject]
    public void Constructor(SceneService sceneService, PauseService pauseService, SaveService saveService, LoadingServiceGame loadingServiceGame)
    {
        _sceneService = sceneService;
        _pauseService = pauseService;
        _saveService = saveService;
        _loading = loadingServiceGame;
    }

    private void Awake()
    {
        _audioMixer = Resources.Load<AudioMixer>(AssetsPath.Audio.AudioMixer);
        _audioMixer.GetFloat("MasterVolume", out _startVolumeSound);
    }

    private void OnEnable()
    {
        _audioMixer.SetFloat("MasterVolume", _startVolumeSound);
        _pause.onClick.AddListener(ClickPause);
        _play.onClick.AddListener(ClickPlay);
        _mainMenu.onClick.AddListener(MainMenu);
        _playSound.onClick.AddListener(DisableSound);
        _disableSound.onClick.AddListener(PlaySound);
    }

    private void OnDisable()
    {
        _pause.onClick.RemoveListener(ClickPause);
        _play.onClick.RemoveListener(ClickPlay);
        _mainMenu.onClick.RemoveListener(MainMenu);
        _playSound.onClick.RemoveListener(DisableSound);
        _disableSound.onClick.RemoveListener(PlaySound);
    }

    private void ClickPause()
    {
        _isPause = true;
        _pause.gameObject.SetActive(false);
        _play.gameObject.SetActive(true);
        _audioMixer.SetFloat("MasterVolume", _minVolumeSound);
        _pauseService.Pause();
    }

    private void ClickPlay()
    {
        _isPause = false;
        _pause.gameObject.SetActive(true);
        _play.gameObject.SetActive(false);
        if (!_isDisableSound)
            _audioMixer.SetFloat("MasterVolume", _startVolumeSound);
        _pauseService.Continue();
    }

    private void MainMenu()
    {
        OnClickMainMenuButton?.Invoke();
        _saveService.SaveObjects();
        ClickPause();
        _loading.Loaouding(false);
        _sceneService.LoadScene("MenuScene");
    }

    private void DisableSound()
    {
        _isDisableSound = true;
        _playSound.gameObject.SetActive(false);
        _disableSound.gameObject.SetActive(true);
        if (!_isPause)
            _audioMixer.GetFloat("MasterVolume", out _startVolumeSound);
        _audioMixer.SetFloat("MasterVolume", _minVolumeSound);
    }

    private void PlaySound()
    {
        _isDisableSound = false;
        _playSound.gameObject.SetActive(true);
        _disableSound.gameObject.SetActive(false);
        if (!_isPause && !_isDisableSound)
            _audioMixer.SetFloat("MasterVolume", _startVolumeSound);
    }
}
using System;
using Cinemachine;
using UnityEngine;

public class BootstrapGame : MonoBehaviour
{
    [Header("Reference")] [SerializeField] private DefeatPanel _defeatPanel;
    [SerializeField] private Transform _pointCharacter;
    [SerializeField] private CinemachineVirtualCamera _cinemachineVirtual;

    private Level _level;
    private CharacterFactory _characterFactory;
    private Character _character;
    private GamePlayMediator _gamePlayMediator;

    private void Awake()
    {
        _characterFactory = new CharacterFactory();
        _level = new Level();
        _character = _characterFactory.CreateCharacter(_pointCharacter);
        _gamePlayMediator = new GamePlayMediator(_level, _character, _defeatPanel);
        _level.Start();
    }

    private void OnEnable()
    {
        _cinemachineVirtual.Follow = _character.transform;
    }

    private void Update()
    {
        if(_gamePlayMediator == null)
            _gamePlayMediator.OnDestroy();
    }
}
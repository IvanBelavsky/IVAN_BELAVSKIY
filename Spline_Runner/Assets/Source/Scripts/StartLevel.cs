using System;
using UnityEngine;

public class StartLevel : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private StartButton _startButton;

    private void Start()
    {
        _startButton.OnClick += OnClick;
    }

    private void OnClick() 
    {
        _startButton.OnClick -= OnClick;
        _player.StartLevel();
    }
}
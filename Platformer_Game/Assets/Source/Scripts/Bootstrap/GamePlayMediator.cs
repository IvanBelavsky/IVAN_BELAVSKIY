using System;
using UnityEngine;

public class GamePlayMediator : MonoBehaviour
{
    [SerializeField] private DefeatPanel _defeatPanel;
    [SerializeField] private Character _character;

    private Level _level;

    public void Initialize(Level level)
    {
        _level = level;
        _level.Defeat += Defeat;
        _defeatPanel.OnRestart += RestartLeevel;
        _character.OnDie += Defeat;
    }

    private void OnDestroy()
    {
        _level.Defeat -= Defeat;
        _defeatPanel.OnRestart -= RestartLeevel;
        _character.OnDie -= Defeat;
    }

    private void Defeat() => _defeatPanel.Show();


    private void RestartLeevel()
    {
        _defeatPanel.Hide();
        _level.Restart();
    }
}
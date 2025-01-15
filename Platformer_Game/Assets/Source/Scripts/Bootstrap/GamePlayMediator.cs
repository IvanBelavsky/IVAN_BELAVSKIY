using System;
using UnityEngine;

public class GamePlayMediator 
{ 
    private DefeatPanel _defeatPanel; 
    private Character _character;
    private Level _level;

    public GamePlayMediator(Level level, Character character, DefeatPanel defeatPanel)
    {
        _defeatPanel = defeatPanel;
        _character = character;
        _level = level;
        _level.Defeat += Defeat;
        _defeatPanel.OnRestart += RestartLeevel;
        _character.OnDie += Defeat;
    }

    public void OnDestroy()
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
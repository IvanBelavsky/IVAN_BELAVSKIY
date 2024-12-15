using UnityEngine;

public class BootstrapGame : MonoBehaviour
{
    [SerializeField] private GamePlayMediator _gamePlayMediator;
    [SerializeField] private Character _character;
    [SerializeField] private DefeatPanel _defeatPanel;

    private Level _level;
    
    private void Awake()
    {
        _level = new Level();
        _gamePlayMediator.Initialize(_level);
         
        _level.Start();
    }
}
using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Character: MonoBehaviour
{
    public event Action OnDie;
    
    [SerializeField] private CharacterConfig _characterConfig;
    [SerializeField] private CharacterView _characterView;
    [SerializeField] private GroundChecker _groundChecker;
    
    private PlayerInput _playerInput;
    private CharacterStateMachine _stateMachine;
    private CharacterController _characterController;

    public PlayerInput PlayerInput => _playerInput;
    public CharacterController CharacterController => _characterController;
    public CharacterConfig CharacterConfig => _characterConfig;
    public CharacterView CharacterView => _characterView;
    public GroundChecker GroundChecker => _groundChecker;

    private void Awake()
    {
        _characterView.Initialize();
        _characterController = GetComponent<CharacterController>();
        _playerInput = new PlayerInput();
        _stateMachine = new CharacterStateMachine(this);
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void Update()
    {
        _stateMachine.HandleInput();
        
        _stateMachine.Update();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out DieZone dieZone))
        {
            Die();
        }
    }

    private void Die()
    {
        OnDie?.Invoke();
        Destroy(gameObject);
    }
}
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameView : MonoBehaviour, IGameView
{
    [SerializeField] private Camera _gameCamera;

    [Header("Movement Settings")] [SerializeField]
    private float _horizontalScrollSpeed = 10f;

    [SerializeField] private float _verticalScrollSpeed = 10f;

    [Header("Input Settings")] [SerializeField]
    private float _keyboardScrollSpeed = 1f;

    [SerializeField] private float _mouseScrollThreshold = 50f;

    [Header("Camera Bounds")] [SerializeField]
    private float _minY = 0f;

    [SerializeField] private float _maxY = 0f;
    [SerializeField] private float _maxX = 5f;
    [SerializeField] private float _minX = -5f;

    private Dictionary<string, DraggableItem> _draggableItems;

    public event Action<string, Vector3> OnItemDragStart;
    public event Action<string, Vector3> OnItemDragEnd;
    public event Action<string, Vector3, float> OnItemDrag;
    public event Action<float, float> OnScroll;
    
    private void Awake()
    {
        _draggableItems = new Dictionary<string, DraggableItem>();

        if (_gameCamera == null)
            _gameCamera = Camera.main;
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        float horizontalScroll = 0f;
        float verticalScroll = 0f;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            horizontalScroll = -_keyboardScrollSpeed;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            horizontalScroll = _keyboardScrollSpeed;
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            verticalScroll = -_keyboardScrollSpeed;
        }
        else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            verticalScroll = _keyboardScrollSpeed;
        }

        Vector2 mousePosition = Input.mousePosition;
        if (mousePosition.x < _mouseScrollThreshold)
        {
            horizontalScroll = -1f;
        }
        else if (mousePosition.x > Screen.width - _mouseScrollThreshold)
        {
            horizontalScroll = 1f;
        }

        if (mousePosition.y < _mouseScrollThreshold)
        {
            verticalScroll = -1f;
        }
        else if (mousePosition.y > Screen.height - _mouseScrollThreshold)
        {
            verticalScroll = 1f;
        }

        if (horizontalScroll != 0f || verticalScroll != 0f)
        {
            OnScroll?.Invoke(horizontalScroll, verticalScroll);
        }
    }

    public void AddDraggableItem(string id, DraggableItem item)
    {
        _draggableItems[id] = item;
        item.OnDragStart += (position) => OnItemDragStart?.Invoke(id, position);
        item.OnDragEnd += (position) => OnItemDragEnd?.Invoke(id, position);
        item.OnDrag += (position, depth) => OnItemDrag?.Invoke(id, position, depth);
    }

    public void UpdateItemPosition(string id, Vector3 position)
    {
        if (_draggableItems.ContainsKey(id))
        {
            _draggableItems[id].UpdatePosition(position);
        }
    }

    public void UpdateItemDepth(string id, float depth)
    {
        if (_draggableItems.ContainsKey(id))
        {
            _draggableItems[id].UpdateDepth(depth);
        }
    }

    public void SetItemGrabbed(string id, bool isGrabbed)
    {
        if (_draggableItems.ContainsKey(id))
        {
            _draggableItems[id].SetGrabbed(isGrabbed);
        }
    }

    public void SetItemOnShelf(string id, bool isOnShelf)
    {
        if (_draggableItems.ContainsKey(id))
        {
            _draggableItems[id].SetOnShelf(isOnShelf);
        }
    }

    public void ScrollScene(float horizontalAmount, float verticalAmount)
    {
        Vector3 pos = _gameCamera.transform.position;

        float newX = pos.x + horizontalAmount * _horizontalScrollSpeed * Time.deltaTime;
        pos.x = Mathf.Clamp(newX, _minX, _maxX);
        float newY = pos.y + verticalAmount * _verticalScrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(newY, _minY, _maxY);

        _gameCamera.transform.position = pos;
    }
}
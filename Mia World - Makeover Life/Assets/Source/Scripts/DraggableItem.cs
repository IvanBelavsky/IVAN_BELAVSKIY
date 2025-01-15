using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class DraggableItem : MonoBehaviour
{
    [Header("Dragging Settings")] 
    [SerializeField] private float _depthMin = -5f;
    [SerializeField] private float _depthMax = 5f;
    [SerializeField] private float _dragSpeed = 10f;
    [SerializeField] private float _gravityScale = 1f;

    [Header("AnimationItem")] [SerializeField]
    private AnimationItem _animationItem;

    private Camera _mainCamera;
    private Vector3 _offset;
    private Rigidbody2D _rigidbody;
    private float _zCoord;
    private bool _isDragging = false;
    private bool _isOnShelf = false;
    
    public event Action<Vector3> OnDragStart;
    public event Action<Vector3> OnDragEnd;
    public event Action<Vector3, float> OnDrag;

    private void Start()
    {
        _mainCamera = Camera.main;
        _rigidbody = GetComponent<Rigidbody2D>();

        SetupRigidbody();
    }

    private void SetupRigidbody()
    {
        _rigidbody.gravityScale = _gravityScale;
        _rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        _rigidbody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }

    public void UpdatePosition(Vector3 position)
    {
        transform.position = position;
    }

    public void UpdateDepth(float depth)
    {
        Vector3 pos = transform.position;
        pos.z = depth;
        transform.position = pos;
    }

    public void SetGrabbed(bool grabbed)
    {
        _isDragging = grabbed;
        _rigidbody.gravityScale = grabbed || _isOnShelf ? 0f : _gravityScale;
        if (!grabbed) _rigidbody.velocity = Vector2.zero;
    }

    public void SetOnShelf(bool onShelf)
    {
        _isOnShelf = onShelf;
        _rigidbody.gravityScale = onShelf ? 0f : (_isDragging ? 0f : _gravityScale);
    }

    private void OnMouseDown()
    {
        _zCoord = _mainCamera.WorldToScreenPoint(transform.position).z;
        Vector3 mousePos = GetMouseWorldPosition();
        _offset = transform.position - mousePos;
        OnDragStart?.Invoke(transform.position);
    }

    private void OnMouseDrag()
    {
        if (!_isDragging) return;

        Vector3 position = GetMouseWorldPosition() + _offset;
        float scrollDelta = Input.GetAxis("Mouse ScrollWheel");
        position.z = Mathf.Clamp(position.z + scrollDelta * _dragSpeed, _depthMin, _depthMax);

        OnDrag?.Invoke(position, position.z);
    }

    private void OnMouseUp()
    {
        _animationItem.Animate(this);
        OnDragEnd?.Invoke(transform.position);
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = _zCoord;
        return _mainCamera.ScreenToWorldPoint(mousePoint);
    }
}
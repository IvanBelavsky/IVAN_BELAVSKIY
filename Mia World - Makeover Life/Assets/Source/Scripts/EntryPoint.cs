using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EntryPoint : MonoBehaviour
{
    const float DEPTHSTEP = 0.1f;

    [Header("Prefabs")] 
    [SerializeField] private DraggableItem _cherryPrefab;
    [SerializeField] private SpriteRenderer _backgroundPrefab;

    [Header("Scene References")] [SerializeField]
    private List<GameObject> _shelves;

    [SerializeField] private GameView _gameView;

    [Header("Scene Settings")] [SerializeField]
    private Vector2 _sceneSize;

    [SerializeField] private int _applesCount = 5;

    private GameModel _gameModel;
    private GamePresenter _gamePresenter;

    private void Awake()
    {
        _cherryPrefab = Resources.Load<DraggableItem>("Prefabs/Cherry");
    }

    private void Start()
    {
        SetupScene();
        InitializeShelves();
    }

    private void SetupScene()
    {
        _gameModel = new GameModel();

        // CreateBackground();
        SetupCamera();
        CreateApples();

        _gamePresenter = new GamePresenter(_gameModel, _gameView);
    }

    private void InitializeShelves()
    {
        for (int i = 0; i < _shelves.Count; i++)
        {
            if (_shelves[i] != null)
            {
                SpriteRenderer shelfRenderer = _shelves[i].GetComponent<SpriteRenderer>();
                if (shelfRenderer != null)
                {
                    Bounds bounds = shelfRenderer.bounds;
                    Rect shelfBounds = new Rect(
                        bounds.min.x,
                        bounds.min.y,
                        bounds.size.x,
                        bounds.size.y
                    );
                    _gameModel.AddShelf(shelfBounds, i * DEPTHSTEP);
                }
            }
        }
    }

    private void CreateBackground()
    {
        SpriteRenderer background = Instantiate(_backgroundPrefab);
        background.transform.localScale = new Vector3(_sceneSize.x, _sceneSize.y, 1f);
        background.sortingOrder = -100;
    }

    private void SetupCamera()
    {
        Camera.main.orthographic = true;
        Camera.main.transform.position = new Vector3(0f, 0f, -10f);
    }

    private void CreateApples()
    {
        float spawnWidth = _sceneSize.x * 0.8f;

        for (int i = 0; i < _applesCount; i++)
        {
            Vector3 position = new Vector3(
                Random.Range(-spawnWidth / 2f, spawnWidth / 2f),
                _sceneSize.y / 2f + 1f,
                0f
            );

            string itemId = $"apple_{i}";
            _gameModel.AddItem(itemId, "apple", position);

            DraggableItem apple = Instantiate(_cherryPrefab, position, Quaternion.identity);
            _gameView.AddDraggableItem(itemId, apple);
        }
    }
}
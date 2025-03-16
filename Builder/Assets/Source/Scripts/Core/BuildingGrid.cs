using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BuildingGrid : MonoBehaviour
{
    private Building[,] _grid;
    private Building _flyBuilding;
    private Camera _camera;
    private InputSystem _inputSystem;
    private Building _selectedBuilding;
    private bool _isAvalible;
    private int x, y;
    private bool isDeleteMode = false;
    private List<Building> _buildings = new List<Building>();
    [field: SerializeField] public Vector2Int GridSize { get; private set; } = new Vector2Int(10, 10);

    private void Awake()
    {
        _camera = Camera.main;
        _inputSystem = new InputSystem();
        _inputSystem.Enable();
        _grid = new Building[GridSize.x, GridSize.y];
    }

    private void OnEnable()
    {
        _inputSystem.InputUI.ClickInput.performed += ClickInputOnperformedObject;
        _inputSystem.InputUI.DeleteInput.performed += DeleteInputOnperformed;
    }

    private void Update()
    {
        if (_flyBuilding != null)
        {
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (groundPlane.Raycast(ray, out float position))
            {
                Vector3 pointPosition = ray.GetPoint(position);
                x = Mathf.RoundToInt(pointPosition.x);
                y = Mathf.RoundToInt(pointPosition.z);
                _isAvalible = true;
                if (x < 0 || x > GridSize.x - _flyBuilding.Size.x) _isAvalible = false;
                if (y < 0 || y > GridSize.y - _flyBuilding.Size.y) _isAvalible = false;
                if (_isAvalible && IsTakePlace(x, y)) _isAvalible = false;
                _flyBuilding.transform.position = new Vector3(x, 0, y);
                _flyBuilding.SetTransperent(_isAvalible);
            }
        }
    }

    private void OnDisable()
    {
        _inputSystem.InputUI.ClickInput.performed -= ClickInputOnperformedObject;
        _inputSystem.InputUI.DeleteInput.performed -= DeleteInputOnperformed;
    }

    public void DeleteSelectedBuilding()
    {
        if (_selectedBuilding != null)
        {
            _buildings.Remove(_selectedBuilding);
            int placeX = Mathf.RoundToInt(_selectedBuilding.transform.position.x);
            int placeY = Mathf.RoundToInt(_selectedBuilding.transform.position.z);
            for (int i = 0; i < _selectedBuilding.Size.x; i++)
            {
                for (int j = 0; j < _selectedBuilding.Size.y; j++)
                {
                    if (placeX + i < GridSize.x && placeY + j < GridSize.y)
                    {
                        _grid[placeX + i, placeY + j] = null;
                    }
                }
            }

            Destroy(_selectedBuilding.gameObject);
            _selectedBuilding = null;
            Debug.Log("Selected building deleted.");
        }
        else
        {
            Debug.LogWarning("No building selected for deletion.");
        }
    }

    public void SetDeleteMode(bool deleteMode)
    {
        isDeleteMode = deleteMode;
    }

    public void PlacingBuilding(Building building)
    {
        if (_flyBuilding != null)
        {
            Destroy(_flyBuilding.gameObject);
        }

        _flyBuilding = Instantiate(building);
        _buildings.Add(_flyBuilding);
    }

    public void SaveBuilding()
    {
        SaveSystem.SaveBuildings(_buildings);
    }

    public void LoadBuilding()
    {
        ClearCurrentBuildings();
        List<BuildingSaveData> data = SaveSystem.LoadBuildings();
        if (data == null || data.Count == 0)
        {
            Debug.LogError("No building data available to load.");
            return;
        }

        foreach (var buildingData in data)
        {
            string prefabName = buildingData.Name.Replace("(Clone)", "").Trim();
            Building prefab = Resources.Load<Building>("Prefabs/" + prefabName);
            if (prefab != null)
            {
                Building newBuilding = Instantiate(prefab);
                newBuilding.LoadBuilding(buildingData);
                _buildings.Add(newBuilding);
                PlaceBuildingInGrid(newBuilding);
            }
            else
            {
                Debug.LogError("Prefab not found: " + prefabName);
            }
        }
    }

    private void ClearCurrentBuildings()
    {
        foreach (var building in _buildings)
        {
            Destroy(building.gameObject);
        }

        _buildings.Clear();
        _grid = new Building[GridSize.x, GridSize.y];
    }

    private void PlaceBuildingInGrid(Building building)
    {
        int placeX = Mathf.RoundToInt(building.transform.position.x);
        int placeY = Mathf.RoundToInt(building.transform.position.z);
        for (int i = 0; i < building.Size.x; i++)
        {
            for (int j = 0; j < building.Size.y; j++)
            {
                _grid[placeX + i, placeY + j] = building;
            }
        }
    }

    private void DeleteInputOnperformed(InputAction.CallbackContext obj)
    {
        if (isDeleteMode)
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Building building = hit.collider.GetComponent<Building>();
                if (building != null)
                {
                    _selectedBuilding = building;
                    DeleteSelectedBuilding();
                }
            }
        }
    }

    private void ClickInputOnperformedObject(InputAction.CallbackContext obj)
    {
        if (_isAvalible && _flyBuilding != null)
        {
            PlaceFlyBuilding(x, y);
        }
    }

    private bool IsTakePlace(int placeX, int placeY)
    {
        if (_flyBuilding == null) return false;
        for (int i = 0; i < _flyBuilding.Size.x; i++)
        {
            for (int j = 0; j < _flyBuilding.Size.y; j++)
            {
                if (placeX + i >= 0 && placeX + i < GridSize.x && placeY + j >= 0 && placeY + j < GridSize.y)
                {
                    if (_grid[placeX + i, placeY + j] != null) return true;
                }
            }
        }

        return false;
    }

    private void PlaceFlyBuilding(int placeX, int placeY)
    {
        if (_flyBuilding == null) return;
        for (int i = 0; i < _flyBuilding.Size.x; i++)
        {
            for (int j = 0; j < _flyBuilding.Size.y; j++)
            {
                _grid[placeX + i, placeY + j] = _flyBuilding;
            }
        }

        _flyBuilding.SetNormalColor();
        _flyBuilding = null;
    }
}
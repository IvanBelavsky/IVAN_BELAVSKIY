using System;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class Building : MonoBehaviour
{
    private const int ONEVALUE = 1;

    [SerializeField] private MeshRenderer _renderer;
    [SerializeField] private Building _prefab;
    private int _id;

    [field: SerializeField] public Vector2Int Size { get; private set; } = Vector2Int.one;
    public int ID => _id;
    public Building Prefab => _prefab;

    private void Awake()
    {
        _id = Random.Range(0, 10000);
    }

    public void LoadBuilding(BuildingSaveData buildingData)
    {
        _id = buildingData.ID;

        Vector3 position;
        position.x = buildingData.Position[0];
        position.y = buildingData.Position[1];
        position.z = buildingData.Position[2];

        transform.position = position;
        SetNormalColor();
    }

    public void SetTransperent(bool avalible)
    {
        if (avalible)
        {
            _renderer.material.color = Color.green;
        }
        else
        {
            _renderer.material.color = Color.red;
        }
    }

    public void SetNormalColor()
    {
        _renderer.material.color = Color.white;
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < Size.x; i++)
        {
            for (int j = 0; j < Size.y; j++)
            {
                Gizmos.color = new Color(0.5f, 0.5f, 0.07f, 0.59f);
                Gizmos.DrawCube(transform.position + new Vector3(i, 0, j), new Vector3(ONEVALUE, .1f, ONEVALUE));
            }
        }
    }
}

[Serializable]
public class BuildingSaveData
{
    public int ID;
    public int[] Position;
    public string Name;

    public BuildingSaveData(Building building)
    {
        ID = building.ID;
        Position = new int[3];
        Position[0] = Mathf.RoundToInt(building.transform.position.x);
        Position[1] = Mathf.RoundToInt(building.transform.position.y);
        Position[2] = Mathf.RoundToInt(building.transform.position.z);
        
        Name = building.Prefab.name.Replace("(Clone)", "").Trim();
    }
}
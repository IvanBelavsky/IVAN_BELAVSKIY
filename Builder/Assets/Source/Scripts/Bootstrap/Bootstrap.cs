using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private BuildingGrid _buildingGrid; 
    [SerializeField] private UIController _uiController; 

    private void Awake()
    {
        InitializeCore();
        InitializeUI();
    }

    private void InitializeCore()
    {
        Debug.Log("Initializing Core...");
    }

    private void InitializeUI()
    {
        _uiController.Initialize(_buildingGrid);
    }
}

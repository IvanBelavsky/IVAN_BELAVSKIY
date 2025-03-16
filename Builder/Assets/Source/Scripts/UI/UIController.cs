using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Button deleteButton;
    private bool isDeleteMode = false;
    private BuildingGrid _buildingGrid;

    public void Initialize(BuildingGrid buildingGrid)
    {
        _buildingGrid = buildingGrid;
        deleteButton.onClick.AddListener(ToggleDeleteMode);
    }

    private void ToggleDeleteMode()
    {
        isDeleteMode = !isDeleteMode;
        if (isDeleteMode)
        {
            deleteButton.GetComponent<Image>().color = Color.red;
            Debug.Log("Delete mode activated.");
        }
        else
        {
            deleteButton.GetComponent<Image>().color = Color.white;
            Debug.Log("Delete mode deactivated.");
        }

        _buildingGrid.SetDeleteMode(isDeleteMode);
    }
}
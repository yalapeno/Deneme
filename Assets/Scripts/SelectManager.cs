using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectManager : MonoBehaviour
{
    GameObject instantiatedObject;
    bool placingMode = false;
    Camera camera;
    Vector2 objectSize;
    BoxCollider2D objectCollider;
    bool placable = false;
    PathFinder pathFinder;

    private void Start()
    {
        camera = Camera.main; // Cache the main camera. 
        pathFinder = FindObjectOfType<PathFinder>();
    }


    private void Update()
    {
        PlaceBuilding();
    }


    private void CancelPlacement()
    {
        Destroy(instantiatedObject);
        placingMode = false;
    }

    private void PlaceBuilding()
    {

        if (!placingMode)
            return;

        if (Input.GetKeyDown(KeyCode.Escape)) // Cancel placement when escape button pressed
            CancelPlacement();

        if (EventSystem.current.IsPointerOverGameObject()) // When hovering over UI elements
            return;

        placable = !objectCollider.IsTouchingLayers(LayerMask.GetMask("Buildings"));


        Vector2 newPos = camera.ScreenToWorldPoint(Input.mousePosition);

        // Snap to grid
        newPos.x = Mathf.RoundToInt(newPos.x - objectSize.x / 2);
        newPos.y = Mathf.RoundToInt(newPos.y - objectSize.y / 2);

        instantiatedObject.transform.position = newPos;

        // Place on left-click
        if (Input.GetMouseButtonDown(0) && placable)
        {
            placingMode = false;
            instantiatedObject.GetComponent<Building>().SetPlaced(true);
            pathFinder.SetWall(newPos, instantiatedObject.GetComponent<Building>().GetBuildingSize());
        }
    }


    public void SetActiveObject(GameObject selectedObjectPrefab)
    {
        if (placingMode)
            CancelPlacement();

        instantiatedObject = Instantiate(selectedObjectPrefab, transform.position, Quaternion.identity);
        instantiatedObject.GetComponent<Building>().SetPlaced(false);
        objectCollider = instantiatedObject.GetComponent<BoxCollider2D>();
        objectSize = instantiatedObject.GetComponent<Building>().GetBuildingSize();
        placingMode = true;
    }
}

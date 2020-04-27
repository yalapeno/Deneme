using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingButton : MonoBehaviour
{
    [SerializeField] GameObject buttonGameObjectPrefab;
    SelectManager manager;

    private void Start()
    {
        manager = FindObjectOfType<SelectManager>();
    }
    public void OnButtonClicked()
    {
        manager.SetActiveObject(buttonGameObjectPrefab);
    }
}

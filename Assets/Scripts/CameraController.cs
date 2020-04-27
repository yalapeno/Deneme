using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        Vector2Int gridDimentions = FindObjectOfType<PathFinder>().GetGridDimentions();
        transform.position = new Vector3(gridDimentions.x * .5f, gridDimentions.y * .5f, -10); // TODO: serialize constants
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfindind : MonoBehaviour
{
    [SerializeField]Transform chunk;
    [SerializeField]Camera cam;
    [SerializeField] LayerMask grid;

    [SerializeField]bool isSelected;

    void Start()
    {
        
    }


    void Update()
    {
        selectGrid();
    }
    void selectGrid()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetMouseButton(0))
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                Physics.Raycast(ray, out hit, Mathf.Infinity, grid);
                chunk = hit.collider.GetComponent<Transform>();
                chunk.position = new Vector3(chunk.position.x, 0f,chunk.position.z);
                isSelected = true;
            }
        }
    }
    
}

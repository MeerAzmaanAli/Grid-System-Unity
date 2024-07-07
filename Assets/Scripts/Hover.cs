using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Hover : MonoBehaviour
{
    [SerializeField] LayerMask grid;
    [SerializeField] Camera cam;
    [SerializeField] TextMeshProUGUI chunkTxt;
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray,out hit, Mathf.Infinity, grid))
            {
                Chunk chunk = hit.collider.gameObject.GetComponent<Chunk>();
                chunkTxt.text = "Chunk: "+ chunk.Xpos+":"+chunk.Zpos;//write X:Y pos of Chunk

            }
        }
    }
}

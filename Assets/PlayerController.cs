using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform[]Pos;
    [SerializeField] Transform jumpPos;
    public Chunk[] chunks;
    public LayerMask grid;
    public float speed = 10;

    public float jumpHeight = 5f; // Height of the jump arc
    public float jumpDuration = 1f; // Duration of the jump
    private bool isJumping = false;

    // Update is called once per frame
    private void Start()
    {
        Look();
    }
    void Update()
    {
        JumpInput();
        
    }
    
    void JumpInput()
    {
        //Handles Input
        if (Input.GetKey(KeyCode.W))
        {
            if (chunks[0] == null) return;
            if (chunks[0].hasObst) return;
            StartCoroutine(JumpToPosition(chunks[0].transform.position));

        } else if (Input.GetKey(KeyCode.D))
        {
            if (chunks[1]==null) return;
            if (chunks[1].hasObst) return;
            StartCoroutine(JumpToPosition(chunks[1].transform.position));
        }
        else if (Input.GetKey(KeyCode.A))
        {
            if (chunks[2] == null) return;
            if (chunks[2].hasObst) return;
            StartCoroutine(JumpToPosition(chunks[2].transform.position));
        }
        else if (Input.GetKey(KeyCode.S))
        {
            if (chunks[3] == null) return;
            if (chunks[3].hasObst) return;
            StartCoroutine(JumpToPosition(chunks[3].transform.position));
        }
    }
    void Look()
    {
        //Updates jump Positions
        RaycastHit hit;
        Array.Clear(chunks, 0, chunks.Length);
        Debug.Log("lookin");
        for (int i = 0; i < Pos.Length; i++) 
        {
            
            Physics.Raycast(Pos[i].transform.position,Vector3.down , out hit, Mathf.Infinity, grid);
            if(hit.collider!=null){ chunks[i]= hit.collider.GetComponent<Chunk>();}
            else { chunks[i] = null; }

        }
    }

    IEnumerator JumpToPosition(Vector3 targetPosition)
    {
        //jump mechanism using parabolic tregectory
        isJumping = true;

        Vector3 startPosition = transform.position;
        targetPosition = new Vector3(targetPosition.x,0, targetPosition.z);
        float elapsedTime = 0f;

        while (elapsedTime < jumpDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / jumpDuration;
            float height = Mathf.Sin(Mathf.PI * t) * jumpHeight;

            Vector3 currentPos = Vector3.Lerp(startPosition, targetPosition, t);
            currentPos.y += height;
            transform.position = currentPos;

            yield return null;
        }
        transform.position = targetPosition;
        jumpPos.transform.position = new Vector3(transform.position.x, jumpPos.position.y, transform.position.z);
        Look();

        isJumping = false;
        
    }

}

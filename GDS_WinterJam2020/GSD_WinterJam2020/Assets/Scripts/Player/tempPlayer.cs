using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempPlayer : MonoBehaviour
{
    public float moveSpeed;


    private float hoMovement;
    private float vertMovement;
    private Vector3 moveDir;

    void Start()
    {
        
    }
    
    void Update()
    {
        hoMovement = Input.GetAxisRaw("Horizontal");
        vertMovement = Input.GetAxisRaw("Vertical");
        moveDir = new Vector3(hoMovement, vertMovement).normalized;
    }

    private void FixedUpdate()
    {
        transform.position += moveSpeed * Time.deltaTime * moveDir;
    }
}

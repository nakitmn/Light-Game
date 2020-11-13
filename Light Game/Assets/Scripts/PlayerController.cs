using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;
using System;

public class PlayerController : MonoBehaviour
{
    NavMeshAgent playerAgent;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        playerAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if(animator) UpdateAnimation(x,z);

        if (x != 0 || z != 0)
        {
            Vector3 moveDirection = new Vector3(x, 0, z);
            Vector3 movePosition = transform.position + moveDirection;

            playerAgent.SetDestination(movePosition);
        }
        else playerAgent.SetDestination(transform.position);
    }

    private void UpdateAnimation(float x,float z)
    {
        if (x!=0 || z!=0)
        {
            animator.SetBool("isWalking",true);
        }
        else animator.SetBool("isWalking", false);
    }
}

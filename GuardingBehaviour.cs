using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GuardingBehaviour : MonoBehaviour
{
    FollowingBehaviour followingBehaviour;
    public bool isGuarding = false;

    public bool GetIsGuarding()
    {
        return isGuarding;
    }

    // Start is called before the first frame update
    void Start()
    {
        followingBehaviour = GetComponent<FollowingBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGuarding)
        {
            StartGuarding();
            followingBehaviour.StopFollowing();
        }
        else
        {
            EndGuarding();
        }
    }
    
    void OnGuard(InputValue value)
    {
        if(value.isPressed)
        {
            isGuarding = !isGuarding;
        }
    }

    void StartGuarding()
    {
        
    }

    void EndGuarding()
    {

    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GuardingBehaviour : MonoBehaviour
{
    [SerializeField] bool isGuarding = false;

    FollowingBehaviour followingBehaviour;
    
    public bool GetIsGuarding()
    {
        return isGuarding;
    }

    void Start()
    {
        followingBehaviour = GetComponent<FollowingBehaviour>();
    }
    void Update()
    {
        if (isGuarding)
        {
            followingBehaviour.StopFollowing();
        }
    }
    
    void OnGuard(InputValue value)
    {
        if(value.isPressed)
        {
            isGuarding = !isGuarding;
        }
    }
}
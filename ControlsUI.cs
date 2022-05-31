using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsUI : MonoBehaviour
{   
    GameObject moveUI;
    GameObject switchUI;
    GameObject resurrect1UI;
    GameObject resurrect2UI;
    GameObject followerControls1UI;
    GameObject followerControls2UI;

    void Start() 
    {
        moveUI = transform.Find("Move").gameObject;
        switchUI = transform.Find("Switch").gameObject;
        resurrect1UI = transform.Find("Resurrect 1").gameObject;
        resurrect2UI = transform.Find("Resurrect 2").gameObject;
        followerControls1UI = transform.Find("Follower 1").gameObject;
        followerControls2UI = transform.Find("Follower 2").gameObject;
    }

    public void UpdateMoveUI(bool state)
    {
        moveUI.SetActive(state);
    }
        public void UpdateSwitchUI(bool state)
    {
        switchUI.SetActive(state);
    }
    public void UpdateResurrect1UI(bool state)
    {
        resurrect1UI.SetActive(state);
    }
        public void UpdateResurrect2UI(bool state)
    {
        resurrect2UI.SetActive(state);
    }

    public void UpdateFollowerControls1UI(bool state)
    {
        followerControls1UI.SetActive(state);
    }
    public void UpdateFollowerControls2UI(bool state)
    {
        followerControls2UI.SetActive(state);
    }
}

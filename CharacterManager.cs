using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] List<GameObject> characters;
    [SerializeField] CinemachineTargetGroup targetGroup;
    int currentCharacterIndex = 0;
    GameObject currentCharacter;
    public GameObject GetCurrentCharacter()
    {
        return characters[currentCharacterIndex];
    }

    void Start()
    {
        UpdatePlayerControl();
    }
    public void SwitchRight()
    {
        if (characters.Count > 1 && currentCharacterIndex < characters.Count - 1)
        {
            currentCharacterIndex++;
        }
        else
        {
            currentCharacterIndex = 0;
        }

        UpdatePlayerControl();
        UpdateCinemachineTargetGroup();
    }

    public void SwitchLeft()
    {
        if (characters.Count > 1 && currentCharacterIndex > 0)
        {
            currentCharacterIndex --;
        }
        else
        {
            currentCharacterIndex = characters.Count - 1;
        }
        
        UpdatePlayerControl();
        UpdateCinemachineTargetGroup();
    }
    
    void UpdatePlayerControl()
    {
        foreach (GameObject character in characters)
        {
            if (currentCharacterIndex != characters.IndexOf(character))
            {
                character.GetComponent<PlayerController>().DeactivateControls();
                character.GetComponent<PlayerController>().enabled = false;
                character.GetComponent<PlayerController>().SetToCurrentCharacter(false);
            }
            else
            {
                character.GetComponent<PlayerController>().enabled = true;
                character.GetComponent<PlayerController>().ActivateControls();
                character.GetComponent<PlayerController>().SetToCurrentCharacter(true);
            }
        }
    }

    void UpdateCinemachineTargetGroup()
    {
        for (int i = 0; i < targetGroup.m_Targets.Length; i++)
        {
            if (i == currentCharacterIndex)
            {
                targetGroup.m_Targets[i].weight = 1;
            }
            else
            {
                targetGroup.m_Targets[i].weight = 0;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] int currentCharacterIndex = 0;
    [SerializeField] List<GameObject> characters;
    [SerializeField] CinemachineTargetGroup targetGroup;

    GameObject currentCharacter;
    Health health;

    public GameObject GetCurrentCharacter()
    {
        return characters[currentCharacterIndex];
    }

    void Start()
    {
        UpdatePlayerController();
    }

    public void AddCharacter(GameObject character)
    {
        characters.Add(character);
        health = character.GetComponent<Health>();
        health.SetCharacterIndex(characters.Count - 1);

        UpdatePlayerController();
        UpdateCinemachineTargetGroup();
    }
    public void RemoveCharacter(GameObject character)
    {
        print("theres something wrong");
        // recalculate character index

        health = character.GetComponent<Health>();
        characters.RemoveAt(health.GetCharacterIndex());

        UpdatePlayerController();
        UpdateCinemachineTargetGroup();
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

        UpdatePlayerController();
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

        UpdatePlayerController();
        UpdateCinemachineTargetGroup();
    }
    void UpdatePlayerController()
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
        List<CinemachineTargetGroup.Target> targets = new List<CinemachineTargetGroup.Target>();
        targetGroup.m_Targets = targets.ToArray();

        foreach (GameObject character in characters)
        {
            targetGroup.AddMember(character.transform, 0f, 0f);
        }

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
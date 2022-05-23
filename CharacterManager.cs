using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] List<GameObject> characters;
    int currentCharacterIndex = 0;

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
    }

    public void SwitchLeft()
    {
        print("switchleft");
        if (characters.Count > 1 && currentCharacterIndex > 0)
        {
            currentCharacterIndex --;
        }
        else
        {
            currentCharacterIndex = characters.Count - 1;
        }
        
        UpdatePlayerControl();
    }
    
    private void UpdatePlayerControl()
    {
        foreach (GameObject character in characters)
        {
            if (currentCharacterIndex != characters.IndexOf(character))
            {
                character.GetComponent<PlayerController>().DeactivateControls();
                character.GetComponent<PlayerController>().enabled = false;
                //character.GetComponent<PlayerController>().SetToCurrentCharacter(false);
            }
            else
            {
                character.GetComponent<PlayerController>().enabled = true;
                character.GetComponent<PlayerController>().ActivateControls();
                //character.GetComponent<PlayerController>().SetToCurrentCharacter(true);
            }
        }
    }
}

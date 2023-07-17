using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSwitch : MonoBehaviour

{
    [SerializeField] CharacterShopDB characterDB;
    [SerializeField] SpriteRenderer spriteRenderer;
    int index;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("SelectedItem"))
        {
            int index = PlayerPrefs.GetInt("SelectedItem");
            SwitchCharacter(index);
        }
        else
        {
            SwitchCharacter(0);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SwitchCharacter(int index)
    {
        Debug.Log(index);
        Character character = characterDB.GetCharacter(index);
        spriteRenderer.sprite = character.image;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterShopUI : MonoBehaviour
{
    [Header("Layout Settings")]
    [Header("UI elements")]
    [SerializeField] GameObject shopUI;
    [SerializeField] Transform ShopMenu, ShopItemsContainer;
    [SerializeField] GameObject itemPrefab;
    [SerializeField] TMP_Text coinAmountText;
    [Space(20)]
    [SerializeField] CharacterShopDB characterDB;

    int newSelectedItemIndex = 0;
    int previousSelectedItemIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        //Fill the shop's UI list with items
        GenerateShopItemsUI();

        //PlayerPrefs.SetInt("coinAmount", 2000);
        //ShopControlScript.AddCoins(PlayerPrefs.GetInt("Coins"));
    }


    void Update()
    {
        coinAmountText.text = PlayerPrefs.GetInt("Coins").ToString();
    }

    void GenerateShopItemsUI()
    {
        int count = 0;
        //Generate Items
        for (int i = 0; i < characterDB.CharactersCount; i++)
        {
            //Create a Character and its corresponding UI element (uiItem)
            Character character = characterDB.GetCharacter(i);
            CharacterItemUI uiItem = Instantiate(itemPrefab, ShopItemsContainer).GetComponent<CharacterItemUI>();

            //Set Item name in Hierarchy (Not required)
            uiItem.gameObject.name = "Item" + i + "-" + character.name;

            //Add information to the UI (one item)
            uiItem.SetCharacterName(character.name);
            uiItem.SetCharacterImage(character.image);
            uiItem.SetCharacterPrice(character.price);

            if (character.isPurchased)
            {
                count++;
                //Character is Purchased
                uiItem.SetCharacterAsPurchased();
                uiItem.OnItemSelect(i, OnItemSelected);
            }
            else
            {
                //Character is not Purchased yet
                uiItem.OnItemPurchase(i, OnItemPurchased);
            }
        }
        if (count == 1 && previousSelectedItemIndex == 0)
        {
            CharacterItemUI prevUiItem = GetItemUI(previousSelectedItemIndex);
            prevUiItem.SelectItem();
            PlayerPrefs.SetInt("PreviouslySelectedItem", 0);


        }
        else
        {
            int SelectedIndex = PlayerPrefs.GetInt("SelectedItem");
            CharacterItemUI UIItem = GetItemUI(SelectedIndex);
            UIItem.SelectItem();
            PlayerPrefs.SetInt("PreviouslySelectedItem", SelectedIndex);

        }

    }


    void OnItemSelected(int index)
    {
        SelectItemUI(index);

    }

    void SelectItemUI(int itemIndex)
    {
        previousSelectedItemIndex = PlayerPrefs.GetInt("PreviouslySelectedItem");
        newSelectedItemIndex = itemIndex;

        CharacterItemUI prevUiItem = GetItemUI(previousSelectedItemIndex);
        CharacterItemUI newUiItem = GetItemUI(newSelectedItemIndex);

        prevUiItem.DeselectItem();
        newUiItem.SelectItem();
        PlayerPrefs.SetInt("SelectedItem", newSelectedItemIndex);
        PlayerPrefs.SetInt("PreviouslySelectedItem", newSelectedItemIndex);


    }


    CharacterItemUI GetItemUI(int index)
    {
        return ShopItemsContainer.GetChild(index).GetComponent<CharacterItemUI>();
    }

    void OnItemPurchased(int index)
    {

        Character character = characterDB.GetCharacter(index);
        CharacterItemUI uiItem = GetItemUI(index);

        if (ShopControlScript.CanSpendCoins(character.price))
        {
            ShopControlScript.SpendCoins(character.price);
            characterDB.PurchaseCharacter(index);
            uiItem.SetCharacterAsPurchased();
            uiItem.OnItemSelect(index, OnItemSelected);
        }


    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InformationMenu : MonoBehaviour
{
    Text entityNameText;
    Image entityImageImage;
    RectTransform entityImageRectTransform;
    [SerializeField] Button produceSoldierButton;

    // Start is called before the first frame update
    void Start()
    {
        //Transform child = canvas.transform.Find("InformationPanel/EntityNameTextBackground/EntityNameText");
        entityNameText = transform.Find("EntityNameTextBackground/EntityNameText").GetComponent<Text>(); // TODO: Enum Paths
        entityImageImage = transform.Find("EntityImage").GetComponent<Image>();
        entityImageRectTransform = transform.Find("EntityImage").GetComponent<RectTransform>();

    }

    public void InitilizeWithBuilding(Building building)
    {
        entityNameText.text = building.GetBuildingName();
        Vector2 buildingSize = building.transform.GetComponent<Building>().GetBuildingSize();
        entityImageImage.sprite = building.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;

        entityImageRectTransform.sizeDelta = new Vector2(
            entityImageRectTransform.sizeDelta.y*(buildingSize.x/buildingSize.y), // Set image width consistent with height
            entityImageRectTransform.sizeDelta.y);

        AddSoldierProductionButtons(building);
    }

    private void AddSoldierProductionButtons(Building building) 
    {
        Soldier[] soldierPrefabs = building.GetSoldiers();

        for (int i = 0; i<soldierPrefabs.Length; i++)
        {
            AddSoldierProductionButton(building, soldierPrefabs, i);
        }

    }

    private void AddSoldierProductionButton(Building building, Soldier[] soldierPrefabs, int i)
    {
        Button instantiatedProduceSoldierButton = Instantiate(produceSoldierButton, transform);
        Image buttonImage = instantiatedProduceSoldierButton.GetComponent<Image>();
        Sprite soldierSprite = soldierPrefabs[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
        Rect spriteRect = soldierSprite.rect;
        buttonImage.sprite = soldierSprite;

        instantiatedProduceSoldierButton.GetComponent<RectTransform>().localPosition =
            new Vector2(0f, 0f - i * 1.5f * spriteRect.height);

        instantiatedProduceSoldierButton.onClick.AddListener(() => { building.SpawnSoldierAtIndex(i); });
    }
}

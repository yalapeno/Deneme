using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    bool placed = true;
    [SerializeField] string buildingName = "evv"; //for debugging
    [SerializeField] Vector2 buildingSize = new Vector2(0f, 0f);
    [SerializeField] Soldier[] soldiers;

    public Vector2 GetBuildingSize()
    {
        return buildingSize;
    }

    public string GetBuildingName()
    {
        return buildingName;
    }

    public Soldier[] GetSoldiers()
    {
        return soldiers;
    }

    public void SetPlaced(bool placed)
    {
        this.placed = placed;
    }

    public void SpawnSoldierAtIndex(int i)
    {
        Soldier soldier = Instantiate(soldiers[i], new Vector2(1f, 0f), Quaternion.identity);
    }

    private void Start()
    {
        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Buildings") && !placed)
            spriteRenderer.color = Color.red;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Buildings"))
            spriteRenderer.color = Color.white;
    }

    private void OnMouseDown()
    {
        if (placed) // TODO: find a better way to avoid selecting the building on placement
            FindObjectOfType<InformationMenu>().InitilizeWithBuilding(this);
    }
}

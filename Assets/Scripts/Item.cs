using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Item : MonoBehaviour
{
    [SerializeField]
    private string description;
    private bool active;
    public bool inInventory;
    private Text txt;

    private bool drag;
    private GameObject inventory;

    private RectTransform activeArea;
    private RectTransform rect;

    private InvetoryManager invManager;

    public Vector3 startPos;

    public int index;
    public bool colInv;
    // Use this for initialization
    void Start()
    {
        startPos = GetComponent<RectTransform>().localPosition;
        rect = GetComponent<RectTransform>();
        txt = GameObject.Find("Description").GetComponent<Text>();
        inventory = GameObject.Find("Inventory");
        activeArea = inventory.GetComponent<RectTransform>();
        invManager = GameObject.Find("LevelManager").GetComponent<InvetoryManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (active && inInventory)
        {
            txt.text = description;
        }

        if (drag)
            transform.position = Input.mousePosition;

        if (colInv)
        {
            if (drag == false && invManager.hasSpace > 0)
            {
                inInventory = true;
                if (invManager.invObj.Contains(this.gameObject) == false)
                    invManager.AddItem(this.gameObject);

                else
                    invManager.ReAlocate(this.gameObject);
            }

            else if (invManager.hasSpace == 0 && drag == false && inInventory == false)
                rect.localPosition = startPos;
        }

        else if(colInv == false)
        {
            if (invManager.invObj.Contains(this.gameObject))
            {
                invManager.DropThis(this.gameObject);
                txt.text = "";
            }
            inInventory = false;
        }
    }

    public void ShowDescription()
    {
        active = true;
    }

    public void HideDescription()
    {
        active = false;
    }

    public void DragStart()
    {
        drag = true;
        transform.SetAsLastSibling();
    }

    public void DragFinish()
    {
        drag = false;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Inventory")
            colInv = true;
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Inventory")
            colInv = false;
    }
}

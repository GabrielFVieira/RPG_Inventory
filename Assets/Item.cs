using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Item : MonoBehaviour {
    public string description;
    public bool active;
    public bool inInventory;
    private Text txt;

    private bool drag;
    private GameObject inventory;
    public GameObject outside;

    private RectTransform activeArea;
    private RectTransform rect;

    private InvetoryManager invManager;

    public Vector3 startPos;

    public int index;
    // Use this for initialization
    void Start () {
        startPos = GetComponent<RectTransform>().localPosition;
        activeArea = GameObject.Find("ActiveArea").GetComponent<RectTransform>();
        rect = GetComponent<RectTransform>();
        txt = GameObject.Find("Description").GetComponent<Text>();
        inventory = GameObject.Find("Inventory");
        outside = GameObject.Find("OutsideInventory");
        invManager = GameObject.Find("LevelManager").GetComponent<InvetoryManager>();
    }
	
	// Update is called once per frame
	void Update () {
		if(active && inInventory)
        {
            txt.text = description;
        }        

        if (drag)
            transform.position = Input.mousePosition;

        Debug.Log(GetComponent<RectTransform>().localPosition);

        if(rect.position.x > activeArea.position.x - (activeArea.rect.width / 2) && rect.position.x < activeArea.position.x + (activeArea.rect.width / 2) && rect.position.y > activeArea.position.y - (activeArea.rect.height / 2) && rect.position.y < activeArea.position.y + (activeArea.rect.height / 2))
        {
            if(drag == false)
            {
                inInventory = true;
                if (invManager.invObj.Contains(this.gameObject) == false)
                    invManager.AddItem(this.gameObject);
            }
        }

        else
        {
            transform.SetParent(outside.transform);
            inInventory = false;
            if (invManager.invObj.Contains(this.gameObject))
            {
                invManager.DropThis(this.gameObject);
                txt.text = "";
            }
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
    }

    public void DragFinish()
    {
        drag = false;
    }
}

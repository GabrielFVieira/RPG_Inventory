using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvetoryManager : MonoBehaviour {
    public List<GameObject> invObj = new List<GameObject>();
    //public List<int> invObjAmount = new List<int>();
    public Vector3[] pos;
    public bool[] posEnabled;
    // Use this for initialization
    void Start () {
        MakeBoolsTrue();
    }
	
	// Update is called once per frame
	void Update () {
	}

    public void AddItem(GameObject obj)
    {
        for (int x = 0; x < posEnabled.Length; x++)
        {
            if(posEnabled[x])
            {
                obj.GetComponent<RectTransform>().localPosition = pos[x];
                obj.GetComponent<Item>().index = x;
                posEnabled[x] = false;
                x = posEnabled.Length;
            }
        }
        invObj.Add(obj);
    }

    public void DropAll()
    {
        foreach (GameObject go in invObj)
        {
            go.GetComponent<RectTransform>().localPosition = go.GetComponent<Item>().startPos;
            go.transform.SetParent(go.GetComponent<Item>().outside.transform);
            go.GetComponent<Item>().inInventory = false;
        }
        MakeBoolsTrue();
        invObj.Clear();
    }

    public void DropThis(GameObject obj)
    {
        posEnabled[obj.GetComponent<Item>().index] = true;
        invObj.Remove(obj.gameObject);
    }

    public void InsertIten(GameObject obj)
    {
        invObj.Add(obj.gameObject);
    }

    private void MakeBoolsTrue()
    {
        for (int y = 0; y < posEnabled.Length; y++)
        {
            posEnabled[y] = true;
        }
    }
}

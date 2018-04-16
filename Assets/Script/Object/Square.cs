using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Square : MonoBehaviour
{
    [System.NonSerialized]
    public int id;
    [System.NonSerialized]
    public int height;


    private GameObject gameRoot;

    public void Create(int id)
    {
        this.id = id;
        this.height = 0;
    }

	// Use this for initialization
	void Start () {
        gameRoot = GameObject.Find("GameRoot");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ToSelectable()
    {
        // 高さが2以下なら選択可
        if (height <= 2)
        {
            gameObject.SetActive(true);
        }
    }

    public void ToUnselectable()
    {
        gameObject.SetActive(false);
    }


    public void OnClick(BaseEventData data)
    {
        gameRoot.SendMessage("OnSquareClick", this.gameObject);
        upHeight();
    }

    public void upHeight()
    {
        // マスがクリックされたときheightを+1する
        Vector3 pos = transform.position;
        height++;
        transform.position = new Vector3(pos.x, height * 1.0f - 0.5f, pos.z);
    }

    public void downHeight()
    {
        // マスがクリックされたときheightを+1する
        Vector3 pos = transform.position;
        height--;
        transform.position = new Vector3(pos.x, height * 1.0f - 0.5f, pos.z);
    }
}

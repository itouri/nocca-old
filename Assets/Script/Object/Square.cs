using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Square : MonoBehaviour
{
    private int id;
    private int height;

    public void Create(int id)
    {
        this.id = id;
        this.height = 0;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnClick(BaseEventData data)
    {
        Debug.Log(id + ":" + height);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Piece : MonoBehaviour {
    public Material whiteMaterial;
    public Material blackMaterial;

    private Util.PEACE_COLOR color;
    private int id;
    private bool isMovable;

    public void Create(Util.PEACE_COLOR color, int id)
    {
        this.color = color;
        this.id = id;
        this.GetComponent<Renderer>().material = (color == Util.PEACE_COLOR.WHITE) ? whiteMaterial : blackMaterial;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Move(int id)
    {
        iTween.MoveTo(this.gameObject, iTween.Hash("posotion", Util.Id2Pos(id)));
    }

    public void OnClick(BaseEventData data)
    {
        Debug.Log("CUBE:" + id);
    }
}

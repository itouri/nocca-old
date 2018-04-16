using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Piece : MonoBehaviour {
    public Material whiteMaterial;
    public Material blackMaterial;
    public Material ChoicingMaterial;

    [System.NonSerialized]
    public int id;

    private Util.PEACE_COLOR color;
    private bool isMovable;

    private GameObject gameRoot;

    public void Create(Util.PEACE_COLOR color, int id)
    {
        this.color = color;
        this.id = id;
        setOwnMaterial();
    }

    // Use this for initialization
    void Start() {
        gameRoot = GameObject.Find("GameRoot");
    }

    // Update is called once per frame
    void Update() {

    }

    public void MoveToID(int id, int height)
    {
        this.id = id;
        var vec = Util.Id2Pos(id);
        vec.y = 1.0f * height;
        transform.position = vec;
        //iTween.MoveTo(this.gameObject, iTween.Hash("posotion", Util.Id2Pos(id)));
    }

    public void OnClick(BaseEventData data)
    {
        this.GetComponent<Renderer>().material = ChoicingMaterial;
        this.gameObject.tag = "chocing";
        gameRoot.SendMessage("OnPieceClick", this.gameObject);
    }

    public void setOwnMaterial()
    {
        this.GetComponent<Renderer>().material = (color == Util.PEACE_COLOR.WHITE) ? whiteMaterial : blackMaterial;
    }
}

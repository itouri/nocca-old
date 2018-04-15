using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoot : MonoBehaviour {
    public GameObject square;
    public GameObject piece;

    // Use this for initialization
    void Start () {
        MakeSquares();
        MakePieces();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // マス目を生成
    private void MakeSquares()
    {
        GameObject squares = new GameObject("Squares");
        for (int i=0; i<25; i++)
        {
            GameObject go = Instantiate(square, Util.Id2Pos(i), Quaternion.identity) as GameObject;
            go.name = ""+i;
            go.transform.parent = squares.transform;
            Square sq = go.GetComponent<Square>();
            sq.Create(i);
        }
    }

    // コマを生成
    private void MakePieces()
    {
        GameObject pieces = new GameObject("Pieces");
        for (int i = 0; i < 5; i++)
        {
            GameObject gW = Instantiate(piece, Util.Id2Pos(i, 0), Quaternion.identity) as GameObject;
            GameObject gB = Instantiate(piece, Util.Id2Pos(i, 4), Quaternion.identity) as GameObject;
            gW.transform.parent = pieces.transform;
            gB.transform.parent = pieces.transform;
            Piece pW = gW.GetComponent<Piece>();
            Piece pB = gB.GetComponent<Piece>();
            pW.Create(Util.PEACE_COLOR.WHITE, i);
            pB.Create(Util.PEACE_COLOR.BLACK, i+20);
        }
    }
}

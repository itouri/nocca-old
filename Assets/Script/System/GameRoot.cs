using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoot : MonoBehaviour {
    public GameObject square;
    public GameObject piece;

    private Square[] squares;

    private GameObject selectedPiece;
    private List<int> selectedSquareIDs;

    // Use this for initialization
    void Start () {
        MakeSquares();
        MakePieces();
        selectedSquareIDs = new List<int>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPieceClick(GameObject piece)
    {
        Piece pi = piece.GetComponent<Piece>();
        if (selectedPiece != null)
        {
            Piece p = selectedPiece.GetComponent<Piece>();
            // 選択済のPieceをクリックしても何もしない
            if (p.id == pi.id)
            {
                return;
            }
            p.setOwnMaterial();
        }
        
        var nextIDs = Util.nextIDs(pi.id);


        // 前回の隣接していたマスを選択不可能にする
        foreach (int id in selectedSquareIDs)
        {
            squares[id].ToUnselectable();
        }

        // 隣接したマスに選択可能にする
        foreach (int id in nextIDs)
        {
            squares[id].ToSelectable();
        }

        selectedSquareIDs = nextIDs;
        selectedPiece = piece;
    }

    public void OnSquareClick(GameObject square)
    {
        Piece p = selectedPiece.GetComponent<Piece>();
        Square s = square.GetComponent<Square>();

        //移動前のPieceの場所の高さを1つ下げる
        squares[p.id].downHeight();

        p.MoveToID(s.id, s.height);

        // 選択済のPieceを選択解除
        selectedPiece = null;
        p.setOwnMaterial();

        // 前回の隣接していたマスを選択不可能にする
        foreach (int id in selectedSquareIDs)
        {
            squares[id].ToUnselectable();
        }
    }

    // マス目を生成
    private void MakeSquares()
    {
        GameObject enptySquares = new GameObject("Squares");
        squares = new Square[25];
        for (int i=0; i<25; i++)
        {
            GameObject go = Instantiate(square, Util.Id2Pos(i), Quaternion.identity) as GameObject;
            go.name = ""+i;
            go.SetActive(false);
            go.transform.parent = enptySquares.transform;
            Square sq = go.GetComponent<Square>();
            sq.Create(i);
            squares[i] = sq;
            // 初期配置は最初から高さ+1
            if (i <= 4 || 20 <= i)
            {
                sq.upHeight();
            }
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

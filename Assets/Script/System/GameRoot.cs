using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameRoot : MonoBehaviour {
    public GameObject square;
    public GameObject piece;

    public Text textYouWhite;
    public Text textYouBlack;
    public Button buttonCameraResetBlack;
    public Button buttonCameraResetWhite;

    public Button buttonOneMore;

    private Square[] squares;
    private Piece[] pieces;

    private GameObject selectedPiece;
    private List<int> selectedSquareIDs;

    private State state;
    private Util.PIECE_COLOR turn;

    private GameObject enptySquares;
    private GameObject enptyPieces;

    // Use this for initialization
    void Start () {
        selectedSquareIDs = new List<int>();
        enptySquares = new GameObject("Squares");
        enptyPieces = new GameObject("Pieces");
        init(true);
    }

    public void OnPieceClick(GameObject piece)
    {
        Piece pi = piece.GetComponent<Piece>();

        //　現在のターンと違う色のクリックは受け付けない
        if (pi.color != turn) {
            return;
        }
        pi.SetChoicing();

        if (selectedPiece != null)
        {
            Piece p = selectedPiece.GetComponent<Piece>();
            // 選択済のPieceをクリックしたら選択解除
            if (p.id == pi.id)
            {
                DeselectPiece();
                return;
            }
            p.setOwnMaterial();
        }
        
        var nextIDs = Util.NextIDs(pi.id, turn);


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


        // 効果音を鳴らす
        SoundManager.GetInstance().PlaySE(Sound.ID.SELECTING_PIECE);
    }

    public void OnSquareClick(GameObject square)
    {
        Piece p = selectedPiece.GetComponent<Piece>();
        Square s = square.GetComponent<Square>();

        //移動元のSquareの場所の高さを1つ下げる
        squares[p.id].downHeight();

        p.MoveToID(s.id, s.height);
        squares[s.id].upHeight();

        // 動けなくなるPieceの確認
        foreach (Piece piece in pieces)
        {
            bool movable = (piece.height == squares[piece.id].height - 1);
            piece.setMovable(movable);
        }

        // 選択済のPieceを選択解除
        DeselectPiece();

        // 前回の隣接していたマスを選択不可能にする
        foreach (int id in selectedSquareIDs)
        {
            squares[id].ToUnselectable();
        }

        // idが勝利マスだったら終了，または動ける駒をなくす
        // 勝者は現在のturn変数の中身
        Util.PIECE_COLOR tmp = (Util.PIECE_COLOR)((int)turn * -1);
        if (s.id == 25 || s.id == 26 || !IsExistMovable(tmp))
        {
            GotoGameFinish();
            return;
        }

        // 効果音を鳴らす
        SoundManager.GetInstance().PlaySE(Sound.ID.PUTTING_PIECE);
        turn = (Util.PIECE_COLOR)((int)turn * -1);

        switchTurnUI();
    }

    private void GotoGameFinish()
    {
        state = State.FINISH;
        string text = "You Win!";

        if (this.turn == Util.PIECE_COLOR.WHITE)
        {
            this.textYouWhite.text = text;
        }
        else
        {
            this.textYouBlack.text = text;
        }

        buttonOneMore.gameObject.SetActive(true);
    }

    public void OnClickOneMore()
    {
        init(false);
        buttonOneMore.gameObject.SetActive(false);
    }

    private void init(bool isFirst)
    {
        //TODO 繰り返しが多い
        MakeSquares(isFirst);
        MakePieces(isFirst);
        selectedSquareIDs = new List<int>();
        state = State.GAME;
        turn = Util.PIECE_COLOR.WHITE;
        textYouBlack.text = "You";
        textYouWhite.text = "You";
        switchTurnUI();
    }

    // マス目を生成
    private void MakeSquares(bool isFirst)
    {
        // enptySquaresの子オブジェクトを全削除
        foreach (Transform s in enptySquares.transform)
        {
            GameObject.Destroy(s.gameObject);
        }
        squares = new Square[27];
        for (int i=0; i<25; i++)
        {
            Vector3 tmp = Util.Id2Pos(i);
            tmp.y += 0.05f;
            GameObject go = Instantiate(square, tmp, Quaternion.identity) as GameObject;
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
        Vector3[] poses = new Vector3[2]{
            new Vector3(0, -0.45f, -4.5f),
            new Vector3(0, -0.45f, 4.5f),
        };
        // 25,26に特別なマスを追加
        for (int i = 25; i < 27; i++)
        {
            GameObject go = Instantiate(square, poses[i-25], Quaternion.identity) as GameObject;
            go.name = "" + i;
            go.SetActive(false);
            go.transform.parent = enptySquares.transform;
            // TODO localとglobalScaleの違いは?
            go.transform.localScale = new Vector3(7, 0.05f, 2);
            Square sq = go.GetComponent<Square>();
            sq.Create(i);
            squares[i] = sq;
        }
    }

    // コマを生成
    private void MakePieces(bool isFirst)
    {
        // enptySquaresの子オブジェクトを全削除
        foreach (Transform p in enptyPieces.transform)
        {
            GameObject.Destroy(p.gameObject);
        }
        pieces = new Piece[10];
        for (int i = 0; i < 5; i++)
        {
            GameObject gW = Instantiate(piece, Util.Id2Pos(i, 0), Quaternion.identity) as GameObject;
            GameObject gB = Instantiate(piece, Util.Id2Pos(i, 4), Quaternion.identity) as GameObject;
            gW.transform.parent = enptyPieces.transform;
            gB.transform.parent = enptyPieces.transform;
            Piece pW = gW.GetComponent<Piece>();
            Piece pB = gB.GetComponent<Piece>();
            pW.Create(Util.PIECE_COLOR.WHITE, i);
            pB.Create(Util.PIECE_COLOR.BLACK, i+20);
            pieces[i*2] = pW;
            pieces[i*2+1] = pB;
        }
    }

    private void switchTurnUI()
    {
        // TODO もっときれいな書き方ないかな
        if (this.turn == Util.PIECE_COLOR.WHITE)
        {
            this.textYouWhite.enabled = true;
            this.buttonCameraResetWhite.gameObject.SetActive(true);

            this.textYouBlack.enabled = false;
            this.buttonCameraResetBlack.gameObject.SetActive(false);
        } else
        {
            this.textYouWhite.enabled = false;
            this.buttonCameraResetWhite.gameObject.SetActive(false);

            this.textYouBlack.enabled = true;
            this.buttonCameraResetBlack.gameObject.SetActive(true);
        }
    }

    private bool IsExistMovable(Util.PIECE_COLOR turn)
    {
        for (int i=0; i<5; i++)
        {
            // 意図的に色を変えてる
            int index = (turn == Util.PIECE_COLOR.WHITE) ? i * 2 : i * 2 + 1;
            if (pieces[index].getMovable())
            {
                return true;
            }
        }
        return false;
    }

    public void DeselectPiece()
    {
        if (selectedPiece == null)
        {
            return;
        }
        // 前回の隣接していたマスを選択不可能にする
        foreach (int id in selectedSquareIDs)
        {
            squares[id].ToUnselectable();
        }

        Piece p = selectedPiece.GetComponent<Piece>();
        selectedPiece = null;
        p.setOwnMaterial();
    }

    private static GameRoot instance = null;

    public static GameRoot GetInstance()
    {
        if (GameRoot.instance == null)
        {
            // TODO 自分自身を返すんだから Find しなくてもいいのでは?
            GameRoot.instance = GameObject.Find("GameRoot").GetComponent<GameRoot>();
        }
        return (GameRoot.instance);
    }

}

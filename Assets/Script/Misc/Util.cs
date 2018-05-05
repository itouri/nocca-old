using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


// TODO 適切なところへ移動
public enum State
{
    GAME = 1,
    FINISH,
    END,
}

static public class Util{
    // マス目をIDからポジションに変換
    static public Vector3 Id2Pos (int id)
    {
        if ( id == 25 )
        {
            return new Vector3(0, -0.5f, -4.5f);
        }
        if ( id == 26 )
        {
            return new Vector3(0, -0.5f, 4.5f);
        }
        float margin = 0.3F;
        float pieceSize = 1.0F;
        float x = (id % 5 - 2) * (pieceSize + margin);
        float z = (id / 5 - 2) * (pieceSize + margin);
        return new Vector3(x, -pieceSize/2, z);
    }

    // マス目をIDからポジションに変換
    static public Vector3 Id2Pos(int x, int y)
    {
        Vector3 vec = Id2Pos(x + 5 * y);
        vec.y = 0;
        return vec;
    }

    //TODO 汚い
    // 引数のidと隣接したidを返す
    static public List<int> NextIDs(int id, PIECE_COLOR turn)
    {
        var list = new List<int>();
        for (int i=0; i < 3; i++)
        {
            list.Add(id - 6 + i);
            list.Add(id - 1 + i);
            list.Add(id + 4 + i);
        }
        list.Remove(id);
        if (id % 5 == 0) { //up
            list.Remove(id - 6);
            list.Remove(id - 1);
            list.Remove(id + 4);
        }
        if (id % 5 == 4) //down
        {
            list.Remove(id - 4);
            list.Remove(id + 1);
            list.Remove(id + 6);
        }
        if (0 <= id && id <= 4) //left
        {
            list.Remove(id - 6);
            list.Remove(id - 5);
            list.Remove(id - 4);
            if (turn == PIECE_COLOR.BLACK)
            {
                list.Add(25);
            }
        }
        if (20 <= id && id <= 24) //right
        {
            list.Remove(id + 4);
            list.Remove(id + 5);
            list.Remove(id + 6);
            if ( turn == PIECE_COLOR.WHITE)
            {
                list.Add(26);
            }
        }
        return list;
    }

    public enum PIECE_COLOR
    {
        WHITE = -1,
        BLACK = 1,
    }
}
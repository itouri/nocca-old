using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

static public class Util{
    // マス目をIDからポジションに変換
    static public Vector3 Id2Pos (int id)
    {
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
    static public List<int> nextIDs(int id)
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
        }
        if (20 <= id && id <= 24) //right
        {
            list.Remove(id + 4);
            list.Remove(id + 5);
            list.Remove(id + 6);
        }
        return list;
    }

    public enum PEACE_COLOR
    {
        WHITE = 0,
        BLACK,
        END,
    }
}
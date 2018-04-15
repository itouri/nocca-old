using System.Collections;
using System.Collections.Generic;
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

    public enum PEACE_COLOR
    {
        WHITE = 0,
        BLACK,
        END,
    }
}
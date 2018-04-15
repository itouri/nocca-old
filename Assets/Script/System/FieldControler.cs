using System; //TODO Systemの全インポートは重い
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldControler : MonoBehaviour {
    private int[] height;

	// Use this for initialization
	void Start () {
        height = new int[25];
        Init();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void Init()
    {
        Array.Clear(height, 0, height.Length);
        for (int i=0; i < 5; i++)
        {
            // 白色の高さ
            height[i * 5] = 1;
            // 黒色の高さ
            height[4 + i * 5] = 1;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMgr : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnGUI() {
        if(Enemy.Count == 0) {
            // 敵が全滅した
            // フォントサイズ設定
            GUIStyle gUIStyle = new GUIStyle();
            gUIStyle.fontSize = 32;
            gUIStyle.alignment = TextAnchor.MiddleCenter;
            // フォントの位置
            float w = 127;
            float h = 32;
            float px = Screen.width / 2 - w / 2;
            float py = Screen.height / 2 - h / 2;

            // フォント描画
            Rect rect = new Rect(px, py, w, h);

            GUI.Label(rect, "Game Clear!", gUIStyle);

            rect.y += 60;
            if (GUI.Button(rect, "Back to Title")) {
                SceneManager.LoadScene("Title");
            }
      
        }
    }
}

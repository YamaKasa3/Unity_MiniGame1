using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour {

    //public GameObject gObj;
    public GameObject pPrefab;

    // 開始。コルーチンで処理を行う
    IEnumerator Start() {
        // 移動する方向と速さをランダムに決める
        float dir = Random.Range(0, 359);
        float spd = Random.Range(10.0f, 20.0f);
        Vector2 v = new Vector2(Util.CosEx(dir) * spd, Util.SinEx(dir) * spd);
        pPrefab.GetComponent<Rigidbody2D>().velocity = v;

        // 見なくなるまで小さくする
        while(pPrefab.GetComponent<Transform>().localScale.x > 0.01f) {
            // 0.01秒毎にゲームループに制御を返す
            yield return new WaitForSeconds(0.01f);
            // だんだん小さくする
            pPrefab.GetComponent<Transform>().localScale *= 0.9f;
            // だんだん減速する
            pPrefab.GetComponent<Rigidbody2D>().velocity *= 0.9f;
        }
        Destroy(pPrefab);
    }

    public  Particle Add(float x, float y) {
        // prefabからインスタンスを生成
        pPrefab = Resources.Load("Prefabs/" + "Particle") as GameObject;
        Vector3 p = new Vector3(x, y, 0);
        GameObject g = Instantiate(pPrefab, p, Quaternion.identity) as GameObject;

        Vector2 v0 = new Vector2(0, 0);
        g.GetComponent<Rigidbody2D>().velocity = v0;
        
        return g.GetComponent<Particle>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

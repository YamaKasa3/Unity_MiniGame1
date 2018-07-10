using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    
    public GameObject enemy;    // Enemyのゲームオブジェクト
    public static int Count = 0;

    private Rigidbody2D rbody;
    private SpriteRenderer eRenderer;
    private float wid;
    private float hei;
	// Use this for initialization
	void Start () {
        // 生存数を増やす
        Count++;
        // ランダムな方向に移動する
        // 方向をランダムに決める 0~359度
        float dir = Random.Range(0, 359);
        // 速さは2
        float spd = 2;
        // x軸, y軸の速さを計算し、Rigidbodyに設定する。
        Vector2 v = new Vector2(Util.CosEx(dir)*spd, Util.SinEx(dir)*spd);
        rbody = GetComponent<Rigidbody2D>();
        rbody.velocity = v;
        eRenderer = enemy.GetComponent<SpriteRenderer>();
        wid = eRenderer.bounds.size.x / 2;
        hei = eRenderer.bounds.size.y / 2;


        
	}
	
	// Update is called once per frame
	void Update () {
        // 画面の端で跳ね返るようにする。

        // ビューポート座標からワールド座標へ変換する。
        // ビューポート座標では、左下が(0, 0)、右上が(1, 1)となる。
        // カメラの左下座標を取得する。
        Vector2 min = Camera.main.ViewportToWorldPoint(Vector2.zero);
        // 自身の大きさを考慮する
        min.x += wid;
        min.y += hei;
        // カメラの右上座標を取得
        Vector2 max = Camera.main.ViewportToWorldPoint(Vector2.one);
        // 自身の大きさを考慮
        max.x -= wid;
        max.y -= hei;

        // 敵の速度を一定(=2)に保つ。他のたこ焼きとぶつかると速度が変わるため。弾性衝突？
        float v_temp = Mathf.Sqrt(Mathf.Pow(rbody.velocity.x, 2) + Mathf.Pow(rbody.velocity.y, 2));
        rbody.velocity = new Vector2(rbody.velocity.x * 2 / v_temp, rbody.velocity.y * 2/ v_temp);

        // オブジェクトの座標を取得
        float X = enemy.transform.position.x;
        float Y = enemy.transform.position.y;
        if (X < min.x || X > max.x) {
            // 画面外に出たので、X移動量を反転する。
            rbody.velocity = new Vector2(rbody.velocity.x * -1, rbody.velocity.y);

            // 画面内に収まるように制限をかける.
            X = Mathf.Clamp(X, min.x, max.x);
            Y = Mathf.Clamp(Y, min.y, max.y);
            Vector2 pos = transform.position;
            pos.x = X;
            pos.y = Y;

            enemy.GetComponent<Transform>().position = pos;
        }
        if(Y < min.y || Y > max.y) {
            // 画面外に出たので、Y移動量を反転する。
            rbody.velocity = new Vector2(rbody.velocity.x, rbody.velocity.y * -1);

            // 画面内に収まるように制限をかける.
            X = Mathf.Clamp(X, min.x, max.x);
            Y = Mathf.Clamp(Y, min.y, max.y);

            Vector2 pos = transform.position;
            pos.x = X;
            pos.y = Y;

            enemy.GetComponent<Transform>().position = pos;
        }

    }

    public void OnMouseDown() {
        // 生存数を減らす
        Count--;
        Particle p = new Particle();
        for(int i = 0; i < 32; i++) {
            p.Add(enemy.transform.position.x, enemy.transform.position.y);
        }
        Destroy(enemy);
    }
}

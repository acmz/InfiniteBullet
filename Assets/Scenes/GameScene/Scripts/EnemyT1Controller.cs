﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyT1Controller : MonoBehaviour {

    //点数
    private const int enemyScore = 10;

    //敵弾発射の間隔
    private const float SHOOT_INTERVAL = 2.0f;
    private float shootTime;

    //敵のレベル設定
    private const int ENEMY_LEVEL = 5;

    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {

        //ゲームが終了したら何もしない。
        //if(this.gameDirectorObj.GetComponent<GameDirector>().IsGameEnd) {
        //    return;
        //}

        //敵弾発射
        this.shootTime += Time.deltaTime;
        if(this.shootTime >= SHOOT_INTERVAL) {

            //敵の位置を基に、敵弾を発射
            Vector2 enemyPos = gameObject.transform.position;
            GameObject eBulletObj = GameObject.Find("E_Bullet_Generator");
            eBulletObj.GetComponent<EBulletGenerator>().EBulletGenerate(
                enemyPos
                ,ENEMY_LEVEL
                ,EBulletGenerator.EBulletType.straight);

            //発射間隔をリセット
            this.shootTime = 0f;

        }

    }

    //自弾に当たったら、点数を加算して自分自身を消す
    private void OnTriggerEnter2D(Collider2D collision) {

        //Debug.Log("collision = " + collision.gameObject.name);

        //自弾に当たったら消滅
        if (collision.gameObject.name == "P_Bullet_Prefab(Clone)") {

            //Directorと連携
            GameObject gameDirectorObj = GameObject.Find("GameDirector");

            //倒されたこと、加算する点数をDirectorに伝える
            gameDirectorObj.GetComponent<GameDirector>().EnemyDestroyNumPlus();
            gameDirectorObj.GetComponent<GameDirector>().ScorePlus(enemyScore);

            //自分自身を消す
            Destroy(gameObject);

        }

    }

}

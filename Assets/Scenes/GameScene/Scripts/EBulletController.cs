using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBulletController : MonoBehaviour
{

    //敵弾のオブジェクト
    Rigidbody2D eBulletBody;

    //敵弾の移動スピード
    private const float E_BULLET_MOVE_SPEED = 5.0f;
    private const float E_BULLET_MOVE_SPEED_PLUS = 5.0f;

    //敵弾の発射角度
    private const float E_BULLET_MOVE_ANGLE_X = 1.0f;
    private const float E_BULLET_MOVE_ANGLE_Y = 0.5f;

    //敵弾表示限界
    private const float E_BULLET_DESTROY_POS_LEFT = -10.0f;
    private const float E_BULLET_DESTROY_POS_RIGHT = 10.0f;
    private const float E_BULLET_DESTROY_POS_UP = 6.0f;
    private const float E_BULLET_DESTROY_POS_DOWN = -6.0f;

    //敵弾の発射方向
    public enum EBulletDirection {
        left, up, down
    }

    //敵弾のスピード
    public enum EBulletSpeed {
        low, hight
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //画面外に出たら自分自身を破棄する
        if (transform.position.x < E_BULLET_DESTROY_POS_LEFT
            || transform.position.x > E_BULLET_DESTROY_POS_RIGHT
            || transform.position.y > E_BULLET_DESTROY_POS_UP
            || transform.position.y < E_BULLET_DESTROY_POS_DOWN) {

            Destroy(gameObject);

        }

    }

    //敵弾の発射（直進）
    public void EBulletShoot(Vector2 inBulletPos, EBulletDirection inDirection) {

        //敵弾の発射方向を求める
        Vector2 startPos = inBulletPos;
        Vector2 endPos = inBulletPos;
        Vector2 shootPos;

        endPos.x -= E_BULLET_MOVE_ANGLE_X;
        switch (inDirection) {

            case EBulletDirection.left:
                //正面
                //角度調整不要
                break;

            case EBulletDirection.up:
                //斜め上
                endPos.y += E_BULLET_MOVE_ANGLE_Y; 
                break;

            case EBulletDirection.down:
                //斜め下
                endPos.y -= E_BULLET_MOVE_ANGLE_Y;
                break;

        }
        shootPos = endPos - startPos;

        //敵弾Rigidbody取得
        eBulletBody = GetComponent<Rigidbody2D>();

        //敵弾の発射角度（敵弾の発射方向.normalized）とスピードを設定
        eBulletBody.velocity = shootPos.normalized * E_BULLET_MOVE_SPEED;

        //敵弾に力を加え、発射
        eBulletBody.AddForce(shootPos.normalized);

    }

    //敵弾の発射（ホーミング）
    public void EBulletShoot(Vector2 inEBulletPos, Vector2 inPlayerPos, EBulletSpeed inEBulletSpeed) {

        //自機と敵弾の座標から、敵弾の発射方向を求める
        Vector2 shootPos = inPlayerPos - inEBulletPos;

        //敵弾Rigidbody取得
        eBulletBody = GetComponent<Rigidbody2D>();

        //敵弾の発射角度（敵弾の発射方向.normalized）とスピードを設定
        float eBulletSpeedNum = E_BULLET_MOVE_SPEED;

        switch (inEBulletSpeed) {

            case EBulletSpeed.low:
                //弾速変えない
                break;

            case EBulletSpeed.hight:
                //弾速UP
                eBulletSpeedNum += E_BULLET_MOVE_SPEED_PLUS;
                break;

        }
        eBulletBody.velocity = shootPos.normalized * eBulletSpeedNum;

        //敵弾に力を加え、発射
        eBulletBody.AddForce(shootPos.normalized);

    }


}

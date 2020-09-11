using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumping : MonoBehaviour
{
    private int playerNumber;

    private float gravity = -9.8f;    //重力
    [SerializeField]private float flightTime = 1;  //滞空時間
    [SerializeField]private float speedRate = 1;   //滞空時間を基準とした移動速度倍率

    [SerializeField] private Camera myCamera = null;   //滞空時間を基準とした移動速度倍率

    private PlayerCamera playerCamera;

    public void Awake()
    {
        playerCamera = new PlayerCamera(myCamera);
    }

    public  IEnumerator Jump(int jumpPower)
    {   
        var startPos = transform.position; // 初期位置
        var endPos = startPos + new Vector3(0, 0, jumpPower);
        var diffY = (endPos - startPos).y; // 始点と終点のy成分の差分
        var vn = (diffY - gravity * 0.5f * flightTime * flightTime) / flightTime; // 鉛直方向の初速度vn

        // 放物運動
        for (var t = 0f; t < flightTime; t += (Time.deltaTime * speedRate))
        {
            var p = Vector3.Lerp(startPos, endPos, t / flightTime);   //水平方向の座標を求める (x,z座標)
            p.y = startPos.y + vn * t + 0.5f * gravity * t * t; // 鉛直方向の座標 y
            transform.position = p;

            //カメラ追従
            playerCamera.MoveCamera(this.gameObject);

            yield return null; //1フレーム経過
        }
        // 終点座標へ補正
        transform.position = endPos;
    }

}

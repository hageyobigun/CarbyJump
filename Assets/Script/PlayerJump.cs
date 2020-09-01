using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerJump : MonoBehaviour
{
    Animator animator;

    private Vector3 endPos;  //終点座標
    private float gravity = -9.8f;    //重力
    private bool isJump = true;

    [SerializeField] float flightTime = 1;  //滞空時間
    [SerializeField] float speedRate = 1;   //滞空時間を基準とした移動速度倍率
    [SerializeField] int playerNumber;//何Pか

    public Camera playerCamera; //プレイヤー毎のカメラ

    private static readonly Joycon.Button[] m_buttons =
    Enum.GetValues(typeof(Joycon.Button)) as Joycon.Button[];

    private List<Joycon> m_joycons;
    private Joycon m_joyconL;
    private Joycon m_joyconR;

    private int [] buttonNumber = new int[2];

    // Start is called before the first frame update
    void Start()
    {
        joyconSet();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (StartCount.isStart == true)
        {

            if (m_joycons[playerNumber].GetButton(m_buttons[buttonNumber[0]]) && isJump)//１マスジャンプ
            {
                animator.SetTrigger("isJump");
                NextJumpPoint(1);
                StartCoroutine(Jump(endPos, flightTime, speedRate, gravity));
            }
            else if (m_joycons[playerNumber].GetButton(m_buttons[buttonNumber[1]]) && isJump)//2マスジャンプ
            {
                animator.SetTrigger("isJump");
                NextJumpPoint(2);
                StartCoroutine(Jump(endPos, flightTime, speedRate, gravity));
            }
        }
    }

    //次の飛ぶ場所
    private void NextJumpPoint(int jumpPower)
    {
        int z = (int)this.transform.position.z + jumpPower;
        if (JumpStageManeger.courceBlocks[playerNumber, z] != null)
        {
            endPos = JumpStageManeger.courceBlocks[playerNumber, z].transform.position;
        }
        else
        {
            z = z - 1;
            endPos = JumpStageManeger.courceBlocks[playerNumber, z].transform.position;
        }
        if (this.transform.position.z >= 10)
        {
            for (int i = 0; i < jumpPower; i++)//いらないブロック削除
            {
                int DestoryPos = z - 10 - jumpPower + i;
                Destroy(JumpStageManeger.courceBlocks[playerNumber, DestoryPos]);
            }
        }
        //なんかエラー
        //JumpStageManeger.InstantiateCource(this.transform.position, jumpPower, playerNumber);
    }


    // 現在位置からのジャンプ　
    private IEnumerator Jump(Vector3 endPos, float flightTime, float speedRate, float gravity)
    {
        isJump = false;
        var startPos = transform.position; // 初期位置
        var diffY = (endPos - startPos).y; // 始点と終点のy成分の差分
        var vn = (diffY - gravity * 0.5f * flightTime * flightTime) / flightTime; // 鉛直方向の初速度vn

        // 放物運動
        for (var t = 0f; t < flightTime; t += (Time.deltaTime * speedRate))
        {
            var p = Vector3.Lerp(startPos, endPos, t / flightTime);   //水平方向の座標を求める (x,z座標)
            p.y = startPos.y + vn * t + 0.5f * gravity * t * t; // 鉛直方向の座標 y
            transform.position = p;

            //カメラ追従
            playerCamera.transform.position = new Vector3(playerCamera.transform.position.x,
                                                          playerCamera.transform.position.y,
                                                          this.transform.position.z + 3);
            yield return null; //1フレーム経過
        }
        // 終点座標へ補正
        transform.position = endPos;
        isJump = true;
    }

    
    private void OnTriggerEnter(Collider collider)
    {
        var trap = collider.gameObject.GetComponent<IAction>();
        if(trap != null)
        {
            trap.TakeDamage(collider, this.gameObject);
        }
    }

    private void joyconSet()
    {
        m_joycons = JoyconManager.Instance.j;

        if (m_joycons == null || m_joycons.Count <= 0) return;

        m_joyconL = m_joycons.Find(c => c.isLeft);
        m_joyconR = m_joycons.Find(c => !c.isLeft);
        m_joycons[0] = m_joyconL;
        m_joycons[1] = m_joyconR;
        if (playerNumber == 0)
        {
            buttonNumber[0] = 3;
            buttonNumber[1] = 0;
        }
        else
        {
            buttonNumber[0] = 0;
            buttonNumber[1] = 3;
        }
    }
}

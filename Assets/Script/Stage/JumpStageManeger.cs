using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpStageManeger : MonoBehaviour
{
    public static GameObject[,] courceBlocks = new GameObject[4, 103];//作ったコース格納
    private int[] courceNumber; //コースの構成
    public GameObject startBlock;
    public GameObject goalBlock;
    public List<GameObject> blockList;//生成ブロック
    [SerializeField] int courceLength;//ブロックの種類の数
    [SerializeField] int numberType;//ブロックの種類の数
    [SerializeField] Transform satgePlace;//コースの長さ

    private int nextCource = 0;

    // Start is called before the first frame update
    void Start()
    {
        SettingCource();
        StartCource();
    }

    //コース設定
    private void SettingCource()
    {
        courceNumber = new int[courceLength];

        //コースの構成を決める
        for (int i = 0; i < courceLength - 1; i++)
        {
            courceNumber[i] = Random.Range(0, numberType);
            if (courceNumber[i] != 0)//通常のブロックでなければ次は通常のブロック
            {
                i++;
                courceNumber[i] = 0;
            }
        }
    }

    //スタート時のコース生成
    private void StartCource()
    {
        for (int i = 0; i < 2; i++)
        {
            courceBlocks[i, 0] = Instantiate(startBlock, new Vector3(nextCource, 0, 0), Quaternion.identity);
            courceBlocks[i, 0].transform.SetParent(satgePlace, false);
            for (int z = 1; z < courceLength; z++)
            {
                courceBlocks[i, z] = Instantiate(blockList[courceNumber[z]], new Vector3(nextCource, 0, z), Quaternion.identity);
                courceBlocks[i, z].transform.SetParent(satgePlace, false);
            }

            courceBlocks[i, courceLength] = Instantiate(goalBlock, new Vector3(nextCource, 0, courceLength), Quaternion.identity);
            courceBlocks[i, courceLength].transform.SetParent(satgePlace, false);
            nextCource += 20;
        }
    }



}

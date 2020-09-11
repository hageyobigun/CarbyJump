using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleBlocks : MonoBehaviour
{
    public GameObject MeshChild;
    public GameObject FaceMesh;
    public Camera playerCamera;

    private float flashTime = 0;
    private float IntervalTime = 0; //点滅周期
    private bool isReturn = false;
    public int playerPos;

    // Update is called once per frame
    void Update()
    {
        if(this.transform.position.y < 0 && isReturn == false)
        {
            StartCoroutine(InHoleBlock());
        }

        if (isReturn == true && this.transform.position.y > 0)
        {

            flashTime += Time.deltaTime;

            if (flashTime <= 1)
            {
                //点滅周期
                IntervalTime += Time.deltaTime;

                if (IntervalTime > 0.1)
                {
                    Flashing();
                }
            }
            else
            {
                FinishFlash();
                isReturn = false;
                flashTime = 0;
            }
        }
    }

    //落下
    private IEnumerator InHoleBlock()
    {
        isReturn = true;
        this.GetComponent<PlayerJumping>().enabled = false;

        yield return new WaitForSeconds(1.5f);

        this.transform.position = new Vector3(playerPos, 1f, this.transform.position.z - 1);//復帰
        playerCamera.transform.position = this.transform.position;
        playerCamera.transform.position += new Vector3(0f, 4f, 3f);
        this.GetComponent<PlayerJumping>().enabled = true;
    }


    ////点滅
    private void Flashing()
    {
        foreach (Transform child in MeshChild.transform)
        {
            child.gameObject.GetComponent<SkinnedMeshRenderer>().enabled =
                !child.gameObject.GetComponent<SkinnedMeshRenderer>().enabled;
        }
        FaceMesh.GetComponent<SkinnedMeshRenderer>().enabled =
            !FaceMesh.GetComponent<SkinnedMeshRenderer>().enabled;

        IntervalTime = 0;
    }

    private void FinishFlash()
    {
        //点滅終了
        foreach (Transform child in MeshChild.transform)
        {
            child.gameObject.GetComponent<SkinnedMeshRenderer>().enabled = true;
        }
        FaceMesh.GetComponent<SkinnedMeshRenderer>().enabled = true;
    }

}

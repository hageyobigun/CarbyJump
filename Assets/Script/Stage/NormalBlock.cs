using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBlock : MonoBehaviour, IAction
{
    Animator animator;


    public void TakeDamage(Collider collider, GameObject player)
    {
        if (collider.tag == "normal")
        {
            OnNormalBlock(player);
        }

    }

    private void OnNormalBlock(GameObject player)
    {
        animator = player.GetComponent<Animator>();
        animator.SetTrigger("isGround");//着地アニメーション補正
    }

}

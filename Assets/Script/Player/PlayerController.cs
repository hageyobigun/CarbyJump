using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerJumping playerJumping;
    Animator animator;
    private bool isJump;

    // Start is called before the first frame update
    void Awake()
    {
        playerInput = new PlayerInput();
        playerJumping = GetComponent<PlayerJumping>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Standing@loop")) 
        {
            isJump = true;
        }
        else
        {
            isJump = false;
        }
        if (StartCount.isStart)
        {
            if (playerInput.IsOneStepJump() && isJump)//１マスジャンプ
            {
                animator.SetTrigger("isJump");
                StartCoroutine(playerJumping.Jump(1));
            }
            else if (playerInput.IsTwoStepJump() && isJump)//2マスジャンプ
            {
                animator.SetTrigger("isJump");
                StartCoroutine(playerJumping.Jump(2));
            }
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        var trap = collider.gameObject.GetComponent<IAction>();
        if (trap != null)
        {
            trap.TakeDamage(collider, this.gameObject);
        }
    }
}

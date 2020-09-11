using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banana : MonoBehaviour, IAction
{
    Animator animator;
    public float downTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void TakeDamage(Collider collider, GameObject player)
    {
        if(collider.tag == "banana")
        {
            HitBanana(player);
        }

    }

    public void HitBanana(GameObject player)
    {
        animator = player.GetComponent<Animator>();
        animator.SetTrigger("isDown");
        StartCoroutine(DownTime(player));
    }

    private IEnumerator DownTime(GameObject player)
    {
        player.GetComponent<PlayerJumping>().enabled = false;
        yield return new WaitForSeconds(downTime);
        this.gameObject.transform.position = new Vector3(0, 100f, 0);
        player.GetComponent<PlayerJumping>().enabled = true;
        Destroy(this.gameObject);
    }

}

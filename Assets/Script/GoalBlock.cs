using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalBlock : MonoBehaviour, IAction
{
    public Text winText;

    public void TakeDamage(Collider collider, GameObject player)
    {
        if (collider.tag == "goal")
        {
            OnGoal(player);
        }

    }

    private void OnGoal(GameObject player)
    {
        if (player.tag == "1P")
        {
            winText.text = "1P GOAL";
            StartCount.isStart = false;
        }
        if (player.tag == "2P")
        {
            winText.text = "2P GOAL";
            StartCount.isStart = false;
        }
        player.GetComponent<PlayerJump>().enabled = false;
    }
}

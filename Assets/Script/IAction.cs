using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//障害物インターフェース
public interface IAction
{
    void TakeDamage(Collider collider,GameObject player); //障害物
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartCount : MonoBehaviour
{
    public Text CountText;

    public static bool isStart = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartCountDown());
    }


    IEnumerator StartCountDown()
    {
        CountText.text = "     3";
        yield return new WaitForSeconds(1f);

        CountText.text = "     2";
        yield return new WaitForSeconds(1f);

        CountText.text = "     1";
        yield return new WaitForSeconds(1f);


        CountText.text = "START";

        yield return new WaitForSeconds(1f);
        isStart = true;
        Destroy(CountText);
    }
}

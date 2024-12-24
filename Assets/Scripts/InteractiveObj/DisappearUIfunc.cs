using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisappearUIfunc :MonoBehaviour
{
    

    // 在Start中开始协程
    
    // 协程函数，用于延迟UI的消失
    public static IEnumerator DisappearUI(TextMeshProUGUI tips)
    {
        // 等待指定的时间
        yield return new WaitForSeconds(1);
        // 时间过后，将UI游戏对象设置为不激活，从而消失
        tips.gameObject.SetActive(false);
    }
}

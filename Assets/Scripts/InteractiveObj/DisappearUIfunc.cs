using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisappearUIfunc :MonoBehaviour
{
    

    // ��Start�п�ʼЭ��
    
    // Э�̺����������ӳ�UI����ʧ
    public static IEnumerator DisappearUI(TextMeshProUGUI tips)
    {
        // �ȴ�ָ����ʱ��
        yield return new WaitForSeconds(1);
        // ʱ����󣬽�UI��Ϸ��������Ϊ������Ӷ���ʧ
        tips.gameObject.SetActive(false);
    }
}

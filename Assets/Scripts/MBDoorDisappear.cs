using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MBDoorDisappear : Listener
{
    private Interactive interactiveScript; // ���� Interactive �ű�
    public string requiredItemName; // ��Ҫ����Ʒ����
    private PickUp pickUpObj; // ����ֲ���Ʒ�������
    public Bag myBag; // ��ұ���

    protected override void Start()
    {
        base.Start();

        // ��ȡ Interactive �ű�
        interactiveScript = objWithInteractive.GetComponent<Interactive>();
        if (interactiveScript != null)
        {
            interactiveScript.eventMBDoorOpen += Dosomething; // �����¼�
        }

        // ��ȡ PickUp �ű�
        pickUpObj = FindObjectOfType<PickUp>();
        if (pickUpObj == null)
        {
            Debug.LogError("PickUp script not found in the scene!");
        }
    }

    protected override void Dosomething()
    {
        // �����������Ƿ�����ض���Ʒ
        if (pickUpObj != null && pickUpObj.handObj != null &&
            pickUpObj.handObj.name == requiredItemName)
        {
            Debug.Log("The door is disappearing...");

            // �����Ŷ���
            Destroy(gameObject);

            // ����������е���Ʒ
            Destroy(pickUpObj.handObj.gameObject);

            // �ӱ������Ƴ���Ӧ����Ʒ
            ItemOnWorld itemOnWorld = pickUpObj.handObj.GetComponent<ItemOnWorld>();
            if (itemOnWorld != null && myBag != null)
            {
                myBag.items.Remove(itemOnWorld.thisItem);
                BagManager.RemoveItemSlot(itemOnWorld.thisItem);
            }

            // ��������ֲ�״̬
            pickUpObj.handObj = null;
            pickUpObj.handEmpty = true;
            GetAItem.inHandObj = null;
        }
        else
        {
            Debug.Log("Player does not hold the required item to make the door disappear.");
        }
    }
}
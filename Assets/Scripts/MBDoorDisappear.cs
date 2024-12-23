using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MBDoorDisappear : Listener
{
    private Interactive interactiveScript; // 引用 Interactive 脚本
    public string requiredItemName; // 需要的物品名称
    private PickUp pickUpObj; // 玩家手部物品管理组件
    public Bag myBag; // 玩家背包

    protected override void Start()
    {
        base.Start();

        // 获取 Interactive 脚本
        interactiveScript = objWithInteractive.GetComponent<Interactive>();
        if (interactiveScript != null)
        {
            interactiveScript.eventMBDoorOpen += Dosomething; // 订阅事件
        }

        // 获取 PickUp 脚本
        pickUpObj = FindObjectOfType<PickUp>();
        if (pickUpObj == null)
        {
            Debug.LogError("PickUp script not found in the scene!");
        }
    }

    protected override void Dosomething()
    {
        // 检查玩家手中是否持有特定物品
        if (pickUpObj != null && pickUpObj.handObj != null &&
            pickUpObj.handObj.name == requiredItemName)
        {
            Debug.Log("The door is disappearing...");

            // 销毁门对象
            Destroy(gameObject);

            // 销毁玩家手中的物品
            Destroy(pickUpObj.handObj.gameObject);

            // 从背包中移除对应的物品
            ItemOnWorld itemOnWorld = pickUpObj.handObj.GetComponent<ItemOnWorld>();
            if (itemOnWorld != null && myBag != null)
            {
                myBag.items.Remove(itemOnWorld.thisItem);
                BagManager.RemoveItemSlot(itemOnWorld.thisItem);
            }

            // 重置玩家手部状态
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
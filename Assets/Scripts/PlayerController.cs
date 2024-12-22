using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //控制移动、跳跃速度大小
    public float moveSpeed, jumpSpeed;
    //控制下蹲时前进速度
    float finalMoveSpeed;
    //存储鼠标输入
    float horizon, vercital;
    Vector3 move, velocity;
    //用于地面判定
    public Transform groundCheck;
    bool isGround;
    public float checkGroundRadius;
    public LayerMask groundLayer;
    CharacterController cc;
    public float gravity;
    //用于下蹲判定
    Vector3 ccOriginCenter, camPos;
    bool isCrouch, isCanStand, nextFrameStand;
    //头部碰撞判断
    public Transform headCheck;
    //下蹲高度，时间，高度差，站立高度
    [Range(0, 2)]
    public float crouchHeight;
    public float crouchTime;
    float standHeight, heightDifference;
    //角色视角
    public Camera mainCam;
    //存储协程用来判断协程进行状态
    Coroutine cameraCrouch;


    private void Start()
    {
        cc = GetComponent<CharacterController>();
        finalMoveSpeed = moveSpeed;
        isCrouch = false;
        standHeight = cc.height;
        heightDifference = standHeight - crouchHeight;
        ccOriginCenter = cc.center;
        camPos = mainCam.transform.localPosition;
        cameraCrouch = null;
        isCanStand = true;
    }



    void MoveAndJump()
    {

        //获取移动键的输入使玩家移动
        horizon = Input.GetAxis("Horizontal") * finalMoveSpeed * Time.deltaTime;
        vercital = Input.GetAxis("Vertical") * finalMoveSpeed * Time.deltaTime;
        move = new Vector3(horizon, 0, vercital);
        move = transform.TransformDirection(move);
        cc.Move(move);
        //进行跳跃
        isGround = Physics.CheckSphere(groundCheck.position, checkGroundRadius, groundLayer);
        velocity.y -= gravity * Time.deltaTime;
        if (isGround) velocity.y = 0;
        //if (Input.GetButtonDown("Jump") && isGround && !isCrouch)
        //{
        //    velocity.y += jumpSpeed;
        //}
        cc.Move(velocity * Time.deltaTime);

    }

    private void Update()
    {
        //更新下蹲状态
        IsCrouch();
        MoveAndJump();
    }

    void IsCrouch()
    {
        //处于蹲下状态时才更新isCanStand
        if (isCrouch) isCanStand = IsCanStand();
        if (Input.GetKeyDown(KeyCode.LeftControl) && !isCrouch)//不处于下蹲状态时按蹲键
        {
            isCrouch = true;
            //速度减半
            finalMoveSpeed = moveSpeed / 2;
            //存储碰撞箱最终位置
            Vector3 ccFinalCenter = cc.center - new Vector3(0, heightDifference / 2, 0);
            //摄像机视角下降,碰撞箱变小（平滑进行）
            MyStartCoroutine(mainCam.transform.localPosition, new Vector3(camPos.x, camPos.y - heightDifference, camPos.z), ccFinalCenter, crouchHeight);
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))//松开蹲键时判断能否起立
        {
            if (isCanStand)
            {
                isCrouch = false;
                //速度恢复
                finalMoveSpeed = moveSpeed;
                //摄像机视角恢复,碰撞箱高度恢复（平滑进行）
                MyStartCoroutine(mainCam.transform.localPosition, camPos, ccOriginCenter, standHeight);
            }
            else
            {
                nextFrameStand = true;
            }
        }
        else if (isCrouch && isCanStand && nextFrameStand)
        {
            isCrouch = false;
            nextFrameStand = false;
            finalMoveSpeed = moveSpeed;
            MyStartCoroutine(mainCam.transform.localPosition, camPos, ccOriginCenter, standHeight);
        }
    }

    //在开启协程前检查是否有另一个协程正在进行
    void MyStartCoroutine(Vector3 camOriginPos, Vector3 camFinalPos, Vector3 ccFinalCenter, float ccFinalHeight)
    {
        if (cameraCrouch != null)
        {
            //如果正在下蹲或起立则停止当前协程
            StopCoroutine(cameraCrouch);
        }
        //开启新协程
        cameraCrouch = StartCoroutine(Crouch(camOriginPos, camFinalPos, ccFinalCenter, ccFinalHeight));
    }

    IEnumerator Crouch(Vector3 camOriginPos, Vector3 camFinalPos, Vector3 ccFinalCenter, float ccFinalHeight)
    {
        Vector3 newCamPos = camOriginPos;
        Vector3 newCcCenter = cc.center;
        float currentCrouchTime = 0; // 记录已经蹲下的时间
        float crouchSpeed = 1f / crouchTime; // 计算每秒的插值进度
        while (mainCam.transform.localPosition.y != camFinalPos.y)
        {
            currentCrouchTime += Time.deltaTime; // 更新蹲下的时间
            float t = currentCrouchTime * crouchSpeed; // 计算当前的插值进度
            newCamPos.y = Mathf.Lerp(camOriginPos.y, camFinalPos.y, t);
            newCcCenter.y = Mathf.Lerp(cc.center.y, ccFinalCenter.y, t);
            cc.height = Mathf.Lerp(cc.height, ccFinalHeight, t);
            //更新center和cam位置
            cc.center = newCcCenter;
            mainCam.transform.localPosition = newCamPos;
            yield return null;
        }

    }

    bool IsCanStand()
    {
        Collider[] colliders = Physics.OverlapBox(headCheck.position, headCheck.localScale / 2);
        foreach (Collider collider in colliders)
        {
            //忽略角色自身和所有子集碰撞体
            if (!collider.transform.IsChildOf(transform))
            {
                return false;
            }
        }
        return true;
    }
}
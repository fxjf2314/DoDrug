using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //控制移动、跳跃速度大小
    public float moveSpeed, jumpSpeed;
    float horizon, vercital;
    Vector3 move,velocity;
    //检测是否接触地面
    public Transform groundCheck;
    bool isGround;
    public float checkRadius;
    public LayerMask groundLayer;
    CharacterController cc;
    public float gravity;
    

    private void Start()
    {
        cc = GetComponent<CharacterController>();
        
    }

    

    void MoveAndJump()
    {
        //获取移动键的输入使玩家移动
        horizon = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        vercital = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        move = new Vector3(horizon, 0, vercital);
        move = transform.TransformDirection(move);
        cc.Move(move);
        //进行跳跃
        isGround = Physics.CheckSphere(groundCheck.position, checkRadius, groundLayer);
        velocity.y -= gravity * Time.deltaTime;
        if(isGround)velocity.y = 0;
        if(Input.GetButtonDown("Jump") && isGround)
        {
            velocity.y += jumpSpeed;            
        }
        cc.Move(velocity * Time.deltaTime);
        
    }

    private void Update()
    {
        MoveAndJump();
        
    }
}

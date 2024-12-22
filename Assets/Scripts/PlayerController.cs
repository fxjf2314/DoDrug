using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //�����ƶ�����Ծ�ٶȴ�С
    public float moveSpeed, jumpSpeed;
    //�����¶�ʱǰ���ٶ�
    float finalMoveSpeed;
    //�洢�������
    float horizon, vercital;
    Vector3 move, velocity;
    //���ڵ����ж�
    public Transform groundCheck;
    bool isGround;
    public float checkGroundRadius;
    public LayerMask groundLayer;
    CharacterController cc;
    public float gravity;
    //�����¶��ж�
    Vector3 ccOriginCenter, camPos;
    bool isCrouch, isCanStand, nextFrameStand;
    //ͷ����ײ�ж�
    public Transform headCheck;
    //�¶׸߶ȣ�ʱ�䣬�߶Ȳվ���߶�
    [Range(0, 2)]
    public float crouchHeight;
    public float crouchTime;
    float standHeight, heightDifference;
    //��ɫ�ӽ�
    public Camera mainCam;
    //�洢Э�������ж�Э�̽���״̬
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

        //��ȡ�ƶ���������ʹ����ƶ�
        horizon = Input.GetAxis("Horizontal") * finalMoveSpeed * Time.deltaTime;
        vercital = Input.GetAxis("Vertical") * finalMoveSpeed * Time.deltaTime;
        move = new Vector3(horizon, 0, vercital);
        move = transform.TransformDirection(move);
        cc.Move(move);
        //������Ծ
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
        //�����¶�״̬
        IsCrouch();
        MoveAndJump();
    }

    void IsCrouch()
    {
        //���ڶ���״̬ʱ�Ÿ���isCanStand
        if (isCrouch) isCanStand = IsCanStand();
        if (Input.GetKeyDown(KeyCode.LeftControl) && !isCrouch)//�������¶�״̬ʱ���׼�
        {
            isCrouch = true;
            //�ٶȼ���
            finalMoveSpeed = moveSpeed / 2;
            //�洢��ײ������λ��
            Vector3 ccFinalCenter = cc.center - new Vector3(0, heightDifference / 2, 0);
            //������ӽ��½�,��ײ���С��ƽ�����У�
            MyStartCoroutine(mainCam.transform.localPosition, new Vector3(camPos.x, camPos.y - heightDifference, camPos.z), ccFinalCenter, crouchHeight);
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))//�ɿ��׼�ʱ�ж��ܷ�����
        {
            if (isCanStand)
            {
                isCrouch = false;
                //�ٶȻָ�
                finalMoveSpeed = moveSpeed;
                //������ӽǻָ�,��ײ��߶Ȼָ���ƽ�����У�
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

    //�ڿ���Э��ǰ����Ƿ�����һ��Э�����ڽ���
    void MyStartCoroutine(Vector3 camOriginPos, Vector3 camFinalPos, Vector3 ccFinalCenter, float ccFinalHeight)
    {
        if (cameraCrouch != null)
        {
            //��������¶׻�������ֹͣ��ǰЭ��
            StopCoroutine(cameraCrouch);
        }
        //������Э��
        cameraCrouch = StartCoroutine(Crouch(camOriginPos, camFinalPos, ccFinalCenter, ccFinalHeight));
    }

    IEnumerator Crouch(Vector3 camOriginPos, Vector3 camFinalPos, Vector3 ccFinalCenter, float ccFinalHeight)
    {
        Vector3 newCamPos = camOriginPos;
        Vector3 newCcCenter = cc.center;
        float currentCrouchTime = 0; // ��¼�Ѿ����µ�ʱ��
        float crouchSpeed = 1f / crouchTime; // ����ÿ��Ĳ�ֵ����
        while (mainCam.transform.localPosition.y != camFinalPos.y)
        {
            currentCrouchTime += Time.deltaTime; // ���¶��µ�ʱ��
            float t = currentCrouchTime * crouchSpeed; // ���㵱ǰ�Ĳ�ֵ����
            newCamPos.y = Mathf.Lerp(camOriginPos.y, camFinalPos.y, t);
            newCcCenter.y = Mathf.Lerp(cc.center.y, ccFinalCenter.y, t);
            cc.height = Mathf.Lerp(cc.height, ccFinalHeight, t);
            //����center��camλ��
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
            //���Խ�ɫ����������Ӽ���ײ��
            if (!collider.transform.IsChildOf(transform))
            {
                return false;
            }
        }
        return true;
    }
}
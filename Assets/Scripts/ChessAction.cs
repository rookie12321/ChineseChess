using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessAction : MonoBehaviour
{

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
/*        if (Input.GetMouseButtonDown(0))
        {
            Vector3 truelocation;
            Debug.Log(Input.mousePosition);
            truelocation = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            truelocation.z = (float)0;
            Debug.Log(truelocation);
            Ray ray = Camera.main.ScreenPointToRay(truelocation);
            if(Physics.Raycast(ray,out RaycastHit hit))
            {
                Debug.Log("GetMouseButtonDown");
            }
        }*/
    }
   /* [Header("��Ҫ��������ƶ�����Ϸ����")]
    public GameObject targetPos;

    Vector3 screenPosition;//���������������ת��Ϊ��Ļ����
    Vector3 mousePositionOnScreen;//��ȡ�������Ļ����Ļ����
    Vector3 mousePositionInWorld;//�������Ļ����Ļ����ת��Ϊ��������
    private float closex = (float)-3.2;
    private float closey = (float)-3.3;
    private float step = (float)0.8;
    private int cnt = 0;
    private void LateUpdate()
    {
 *//*       if (Input.GetMouseButton(0))
        {
            MouseFollow();
        }*//*
    }

    /// <summary>
    /// ��ȡ���������ķ���
    /// </summary>
    public Vector3 MouseFollow()
    {
        //��ȡ��Ϸ���������������е�λ�ã���ת��Ϊ��Ļ���ꣻ
        screenPosition = Camera.main.WorldToScreenPoint(targetPos.transform.position);

        //��ȡ����ڳ���������
        mousePositionOnScreen = Input.mousePosition;

        //����������Z������ ���� ��������Ϸ�����Z������
        mousePositionOnScreen.z = screenPosition.z;

        //��������Ļ����ת��Ϊ��������
        mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePositionOnScreen);

        //����Ϸ����������Ϊ�����������꣬�����������ƶ�
        Vector3 vector = new(closex + step*cnt, closey + step*cnt, 0);
        targetPos.transform.position = vector;
        Debug.Log(cnt);
        cnt++;
        //����������X���ƶ�
        return new Vector3(mousePositionInWorld.x, mousePositionInWorld.y, mousePositionInWorld.z);
    }
    // Update is called once per frame*/
}

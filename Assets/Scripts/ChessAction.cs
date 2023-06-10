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
   /* [Header("需要跟随鼠标移动的游戏对象")]
    public GameObject targetPos;

    Vector3 screenPosition;//将物体从世界坐标转换为屏幕坐标
    Vector3 mousePositionOnScreen;//获取到点击屏幕的屏幕坐标
    Vector3 mousePositionInWorld;//将点击屏幕的屏幕坐标转换为世界坐标
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
    /// 获取鼠标点击坐标的方法
    /// </summary>
    public Vector3 MouseFollow()
    {
        //获取游戏对象在世界坐标中的位置，并转换为屏幕坐标；
        screenPosition = Camera.main.WorldToScreenPoint(targetPos.transform.position);

        //获取鼠标在场景中坐标
        mousePositionOnScreen = Input.mousePosition;

        //让鼠标坐标的Z轴坐标 等于 场景中游戏对象的Z轴坐标
        mousePositionOnScreen.z = screenPosition.z;

        //将鼠标的屏幕坐标转化为世界坐标
        mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePositionOnScreen);

        //将游戏对象的坐标改为鼠标的世界坐标，物体跟随鼠标移动
        Vector3 vector = new(closex + step*cnt, closey + step*cnt, 0);
        targetPos.transform.position = vector;
        Debug.Log(cnt);
        cnt++;
        //物体跟随鼠标X轴移动
        return new Vector3(mousePositionInWorld.x, mousePositionInWorld.y, mousePositionInWorld.z);
    }
    // Update is called once per frame*/
}

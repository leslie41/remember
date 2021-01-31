using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchMove : MonoBehaviour
{
   // public GameObject inputField;
    //public GameObject obstacle;
    //private TimeSpan spanA;


    Vector2 m_screenPos = new Vector2(); //记录手指触碰的位置



    void Start()
    {
        Input.multiTouchEnabled = true;//开启多点触碰
    }

    void Update()
    {


        /* if (Input.touchCount>0)
         {
             Touch touch = Input.GetTouch(0);
             //转换坐标系

             Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
             touchPosition.z = 0f;
             transform.position = touchPosition;
         }*/

        /*for (int i = 0; i < Input.touchCount; i++)
        {
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Input.touches[i].position);
            Debug.DrawLine(Vector3.zero,touchPosition,Color.red);
        }*/



        if (Input.touchCount <= 0)
            return;
        if (Input.touchCount == 1 || Input.GetMouseButtonDown(0)) //单点触碰移动摄像机
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                m_screenPos = Input.touches[0].position;   //记录手指刚触碰的位置


                Debug.Log(m_screenPos);
                //inputField.GetComponent<Text>().text = m_screenPos.ToString();
            }


            
        }


    }
}

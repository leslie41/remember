using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using UnityEngine.Timeline;
using UnityEngine.SceneManagement;

public class CreateMessegePanel : MonoBehaviour
{
    public GameObject panelPrefab;
    public GameObject fitTimePanelPrefab;
    public GameObject unFitTimePanelPrefab;
    private GameObject parent;
    private Vector3 offset = new Vector3(10,10,0);

    public Joystick joystick;
    private string worldName;
    private string dirpath;

    private string brickFileName;
    private string clickTimesFileName;
    private string t_filename;
    private string messageFileName;

    private DateTime targerDate;
    public TimeSpan span;
    private string targetMessage;
    private string targetName;


    public Sprite brickImage1;
    public Sprite brickImage2;


    public class PanelClass
    {
        public string dateNow ;
        public string Messege;
        public string PlayerName;

    }

    public class BrickClass
    {
        public string brickName;

    }

    private void Start()
    {
        parent = GameObject.Find("MainCanvas");


        worldName = SceneManager.GetActiveScene().name;
        dirpath = Application.persistentDataPath + "/Save" + "/" + worldName;

       
        brickFileName = dirpath + "/BrickClick.sav";
        clickTimesFileName = dirpath + "/ClickTimes.save";
        t_filename = dirpath + "/temporary.save";
        messageFileName =  dirpath + "/" + gameObject.name + "/Panel.sav";

        gameObject.GetComponent<BoxCollider2D>().size = new Vector2(4f,0.9f);

    }



    void OnMouseDown()
    {
        if (joystick.Vertical == 0 && joystick.Horizontal == 0)
        {

            //创建中转文件
            if (!File.Exists(t_filename))
            {
                IOHelper.CreateDirectory(dirpath);
                IOHelper.CreateFile(t_filename);

            }

            //创建 存放message的文件夹 的文件夹名字文件
            if (!File.Exists(brickFileName))
            {
                IOHelper.CreateDirectory(dirpath);
                IOHelper.CreateFile(brickFileName);

            }

            //判断是否存在记录点击的文件
            if (!File.Exists(clickTimesFileName))
            {
                IOHelper.CreateDirectory(dirpath);
                IOHelper.CreateFile(clickTimesFileName);
            }


            string clickCount = IOHelper.GetRawData(clickTimesFileName);


            //第一次点击砖块
            if (!clickCount.Contains(gameObject.name))
            {
                GameObject Instance = Instantiate(panelPrefab, gameObject.transform.position + offset, Quaternion.identity);
                Instance.transform.SetParent(parent.transform);
                Debug.Log("改变砖块的颜色");
                //改变砖块的颜色，也就是贴图
                gameObject.GetComponent<SpriteRenderer>().sprite = brickImage1;

            }
            //第一次之后的点击
            else
            {
                Debug.Log("判断时间，如果满足条件再显示");
                //读取存储的内容和名字
                if (File.Exists(messageFileName))
                {
                    PanelClass dt = (PanelClass)IOHelper.GetData(messageFileName, typeof(PanelClass));
                    targerDate = Convert.ToDateTime(dt.dateNow);
                    targetMessage = dt.Messege;
                    targetName = dt.PlayerName;
                    Debug.Log("Date: " + targerDate + "messege: " + targetMessage + " name: " + targetName);
                }

                span = DateTime.Now - targerDate;

              //  Debug.Log("span :   "+span.TotalSeconds);
                //判断是否符合时间条件
                if (span.TotalDays < 360)
                {
                  //  Debug.Log("span :   " + span.TotalSeconds);
                    double time = 365 - span.TotalDays;
                    time = Math.Round(time, 1);
                    string timeString = time.ToString();
                    GameObject Instance = Instantiate(unFitTimePanelPrefab, gameObject.transform.position + offset, Quaternion.identity);
                    Instance.transform.SetParent(parent.transform);
                    Instance.GetComponentInChildren<Text>().text = "Still need " + timeString + " Days";
                }
                else
                {
                   // Debug.Log("span : 1234    " + span.TotalSeconds);
                    GameObject Instance = Instantiate(fitTimePanelPrefab, gameObject.transform.position + offset, Quaternion.identity);
                    Instance.transform.SetParent(parent.transform);
                    Instance.GetComponentInChildren<Text>().text = targetMessage + "\n" + targetName;
                    gameObject.GetComponent<SpriteRenderer>().sprite = brickImage2;
                }

            }

      


            BrickClass bc = new BrickClass();
            bc.brickName = gameObject.name;

            //覆盖
            IOHelper.SetData(brickFileName, bc);
            //中转文件添加
            IOHelper.AddData(t_filename, gameObject.name.ToString());

            //Debug.Log(IOHelper.GetRawData(clickTimesFileName));
            //Debug.Log("save brick correct");

        }

    }


    string SubString(String str)
    {

        str = str.Replace("{\"brickName\":\"", "");
        str = str.Replace("\"}", "");
        return str;
    }
}

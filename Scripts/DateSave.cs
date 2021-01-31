using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using UnityEngine.Timeline;
using UnityEngine.SceneManagement;


public class DateSave : MonoBehaviour
{
    private string worldName;
    private string dirpath;
    private string messegeFileName;
    private string brickFileName;
    private string brickName;
    private string clickTimesFileName;
    private string t_filename;


    public GameObject messegeField;
    public GameObject nameField;

    private string message;
    private string playerName;
    public class PanelClass
    {
        public string dateNow = DateTime.Now.ToString();
        public string Messege = GameObject.Find("MessageField").GetComponent<InputField>().text;
        public string PlayerName = GameObject.Find("NameField").GetComponent<InputField>().text;

    }
    private void Start()
    {

        worldName = SceneManager.GetActiveScene().name;
        dirpath = Application.persistentDataPath + "/Save" + "/" + worldName;
        clickTimesFileName =  dirpath + "/ClickTimes.save";  
        t_filename = dirpath + "/temporary.save";

        brickFileName = dirpath + "/BrickClick.sav";
        brickName  = SubString(IOHelper.GetRawData(brickFileName));  //读取原生数据并字符串截取


        dirpath = dirpath + "/" + brickName;
        messegeFileName = dirpath  + "/Panel.sav";
       // Debug.Log("panel: "+messegeFileName);

   ;
 
       //获取文本框内容
        message = messegeField.GetComponent<InputField>().text;
        Debug.Log(message);
        playerName = nameField.GetComponent<InputField>().text;
        Debug.Log(playerName);
    }


    public void Onclick() 
    {
        //如果文本框内有内容
        if (message != null && playerName  != null )
        {
            SaveDate();
            //将中转文件的内容导入计数文件
            IOHelper.AddData(clickTimesFileName, IOHelper.GetRawData(t_filename)); 
        }
       
    }

    //存储message数据
    void SaveDate()
    {
        if (!File.Exists(messegeFileName))
        {
           
            IOHelper.CreateDirectory(dirpath);
            IOHelper.CreateFile(messegeFileName);

            PanelClass t_date = new PanelClass();
            IOHelper.SetData(messegeFileName, t_date);


           // Debug.Log("save panel correct" + messegeFileName);

        }

        
    }

    //字符串截取
    string SubString(String str)
    {

        str = str.Replace("{\"brickName\":\"","");
        str = str.Replace("\"}","");
        return str;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using UnityEngine.Timeline;
using UnityEngine.SceneManagement;


public class BrickManager : MonoBehaviour

{
    public List<GameObject> brickList = new List<GameObject>();

    private DateTime today;
    private string worldName;
    private string dirpath;

    private string messageFileName;
    private DateTime targerDate;
    private TimeSpan span;

    private bool isExist;

    public class PanelClass
    {
        public string dateNow;
        public string Messege;
        public string PlayerName;

    }
    // Start is called before the first frame update
    void Start()
    {
        

        worldName = SceneManager.GetActiveScene().name;
        dirpath = Application.persistentDataPath + "/Save" + "/" + worldName;

        messageFileName = dirpath + "/brick" + "/Panel.sav";

        

        
    }

    // Update is called once per frame
    void Update()
    {

        isExist = File.Exists(messageFileName);
     
        if (isExist)
        {

            PanelClass dt = (PanelClass)IOHelper.GetData(messageFileName, typeof(PanelClass));
            targerDate = Convert.ToDateTime(dt.dateNow);
            today = DateTime.Now;
        }
        span = today - targerDate;


        if (span != TimeSpan.Zero)
        {
            
            double count = Math.Round( span.TotalDays,1) / 7f;

           
            double index =  Math.Truncate(count);
           
            if (index<1)
            {
                index = 0;
            }
            
            if (index  < brickList.Count)
            {
                brickList[Convert.ToInt32(index)].SetActive(true);
            }
            else
            {
                foreach ( GameObject gm in brickList)
                {
                    gm.SetActive(true);
                }
            }
        }
    }
}

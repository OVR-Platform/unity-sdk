using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum OvrInfoCanvasType
{
    Controls
}

[Serializable]
public class Infos
{
    public GameObject obj;
    public OvrInfoCanvasType type;
}

public class OvrInfoCanvas : MonoBehaviour
{
    public List<Infos> infos;
    public int autoHideTime = 5;
    
    public void Start()
    {
        ShowInfo(OvrInfoCanvasType.Controls);
    }

    public void ShowInfo(OvrInfoCanvasType type)
    {
        foreach (var info in infos)
        {
            if (info.type == type)
            {
                info.obj.SetActive(true);
                StartCoroutine(HideInfoAfterSeconds(info.obj, autoHideTime));
            }
            else
            {
                info.obj.SetActive(false);
            }
        }
    }

    private IEnumerator HideInfoAfterSeconds(GameObject obj, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        obj.SetActive(false);
    }
    

}

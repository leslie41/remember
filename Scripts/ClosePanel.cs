using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosePanel : MonoBehaviour
{
    private float fadeInTime = 1f;
    public void OnClick()
    {
        Destroy(this.gameObject, fadeInTime * Time.deltaTime);
    }
}

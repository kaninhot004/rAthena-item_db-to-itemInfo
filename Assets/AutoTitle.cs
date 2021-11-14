using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoTitle : MonoBehaviour
{
    [SerializeField] Text title;
    void Start()
    {
        title.text = "item_db to itemInfo v." + Application.version;
    }
}

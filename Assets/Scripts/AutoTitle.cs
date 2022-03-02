using UnityEngine;
using UnityEngine.UI;

public class AutoTitle : MonoBehaviour
{
    [SerializeField] Text _txtTitle;

    void Start()
    {
        _txtTitle.text = "item_db to itemInfo v." + Application.version;
    }
}

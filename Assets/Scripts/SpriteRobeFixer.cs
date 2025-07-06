using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using EasyButtons;

public class SpriteRobeFixer : MonoBehaviour
{
    public const string JOB_NAME_PATH = "Assets/Resources/sprite_robe_fixer/_job_name/";
    public const string TO_FIX_PATH = "Assets/Resources/sprite_robe_fixer/to_fix/";
    public const string MALE_ENCODE_874 = "ณฒ";
    public const string FEMALE_ENCODE_874 = "ฟฉ";

    [SerializeField] Button _fix;

    private void Start()
    {
        _fix.onClick.AddListener(Fix);
    }

    [Button()]
    public void Fix()
    {
        List<string> maleList = new List<string>();
        List<string> femaleList = new List<string>();

        DirectoryInfo dir = new DirectoryInfo(JOB_NAME_PATH + MALE_ENCODE_874);
        var files = dir.GetFiles("*");
        foreach (var file in files)
        {
            if (!maleList.Contains(file.Name) && !file.Name.Contains(".meta"))
                maleList.Add(file.Name);
        }

        dir = new DirectoryInfo(JOB_NAME_PATH + FEMALE_ENCODE_874);
        files = dir.GetFiles("*");
        foreach (var file in files)
        {
            if (!femaleList.Contains(file.Name) && !file.Name.Contains(".meta"))
                femaleList.Add(file.Name);
        }

        dir = new DirectoryInfo(TO_FIX_PATH);
        var folders = dir.GetDirectories("*");
        foreach (var folder in folders)
        {
            string sprToCopy = string.Empty;
            string actToCopy = string.Empty;

            foreach (var jobFileName in maleList)
            {
                var checkPath = TO_FIX_PATH + folder.Name + "/" + MALE_ENCODE_874 + "/" + jobFileName;

                if (File.Exists(checkPath) && checkPath.Contains(".spr"))
                    sprToCopy = checkPath;

                if (File.Exists(checkPath) && checkPath.Contains(".act"))
                    actToCopy = checkPath;

                if (!string.IsNullOrEmpty(sprToCopy) && !string.IsNullOrEmpty(actToCopy))
                    break;
            }

            if (string.IsNullOrEmpty(sprToCopy) || string.IsNullOrEmpty(actToCopy))
            {
                Debug.LogWarning("Can't fix male folder " + folder.Name + " because not found " + (string.IsNullOrEmpty(sprToCopy) ? "spr" : "act") + " to copy");
                return;
            }
            else
            {
                foreach (var jobFileName in maleList)
                {
                    var checkPath = TO_FIX_PATH + folder.Name + "/" + MALE_ENCODE_874 + "/" + jobFileName;

                    if (!File.Exists(checkPath) && checkPath.Contains(".spr"))
                    {
                        Debug.Log("Fixing spr male folder " + folder.Name + " file name " + jobFileName);

                        File.Copy(sprToCopy, TO_FIX_PATH + folder.Name + "/" + MALE_ENCODE_874 + "/" + jobFileName);
                    }

                    if (!File.Exists(checkPath) && checkPath.Contains(".act"))
                    {
                        Debug.Log("Fixing act male folder " + folder.Name + " file name " + jobFileName);

                        File.Copy(actToCopy, TO_FIX_PATH + folder.Name + "/" + MALE_ENCODE_874 + "/" + jobFileName);
                    }
                }
            }

            sprToCopy = string.Empty;
            actToCopy = string.Empty;

            foreach (var jobFileName in femaleList)
            {
                var checkPath = TO_FIX_PATH + folder.Name + "/" + FEMALE_ENCODE_874 + "/" + jobFileName;

                if (File.Exists(checkPath) && checkPath.Contains(".spr"))
                    sprToCopy = checkPath;

                if (File.Exists(checkPath) && checkPath.Contains(".act"))
                    actToCopy = checkPath;

                if (!string.IsNullOrEmpty(sprToCopy) && !string.IsNullOrEmpty(actToCopy))
                    break;
            }

            if (string.IsNullOrEmpty(sprToCopy) || string.IsNullOrEmpty(actToCopy))
            {
                Debug.LogWarning("Can't fix female folder " + folder.Name + " because not found " + (string.IsNullOrEmpty(sprToCopy) ? "spr" : "act") + " to copy");
                return;
            }
            else
            {
                foreach (var jobFileName in femaleList)
                {
                    var checkPath = TO_FIX_PATH + folder.Name + "/" + FEMALE_ENCODE_874 + "/" + jobFileName;

                    if (!File.Exists(checkPath) && checkPath.Contains(".spr"))
                    {
                        Debug.Log("Fixing spr female folder " + folder.Name + " file name " + jobFileName);

                        File.Copy(sprToCopy, TO_FIX_PATH + folder.Name + "/" + FEMALE_ENCODE_874 + "/" + jobFileName);
                    }

                    if (!File.Exists(checkPath) && checkPath.Contains(".act"))
                    {
                        Debug.Log("Fixing act female folder " + folder.Name + " file name " + jobFileName);

                        File.Copy(actToCopy, TO_FIX_PATH + folder.Name + "/" + FEMALE_ENCODE_874 + "/" + jobFileName);
                    }
                }
            }
        }
    }
}

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
    [SerializeField] Text _status;

    private void Start()
    {
        _fix.onClick.AddListener(Fix);
    }

    [Button()]
    public void Fix()
    {
        int fixCount = 0;

        List<string> maleList = new List<string>();
        List<string> femaleList = new List<string>();

        _status.text = "Fetching directory <color=yellow>'male'</color> job name";
        DirectoryInfo dir = new DirectoryInfo(JOB_NAME_PATH + MALE_ENCODE_874);
        _status.text = "Fetching files <color=yellow>'male'</color> job name";
        var files = dir.GetFiles("*");
        _status.text = "Adding <color=yellow>'male'</color> job name";
        foreach (var file in files)
        {
            if (!maleList.Contains(file.Name) && !file.Name.Contains(".meta"))
                maleList.Add(file.Name);
        }

        _status.text = "Fetching directory <color=yellow>'female'</color> job name";
        dir = new DirectoryInfo(JOB_NAME_PATH + FEMALE_ENCODE_874);
        _status.text = "Fetching files <color=yellow>'female'</color> job name";
        files = dir.GetFiles("*");
        _status.text = "Adding <color=yellow>'female'</color> job name";
        foreach (var file in files)
        {
            if (!femaleList.Contains(file.Name) && !file.Name.Contains(".meta"))
                femaleList.Add(file.Name);
        }

        _status.text = "Fetching <color=yellow>'to fix'</color> folders";
        dir = new DirectoryInfo(TO_FIX_PATH);
        var folders = dir.GetDirectories("*");
        foreach (var folder in folders)
        {
            string sprToCopy = string.Empty;
            string actToCopy = string.Empty;
            string lastJobFileName = string.Empty;
            _status.text = "Fetching <color=yellow>'to fix'</color> folders " + folder.Name;
            foreach (var jobFileName in maleList)
            {
                lastJobFileName = jobFileName;
                var checkPath = TO_FIX_PATH + folder.Name + "/" + MALE_ENCODE_874 + "/" + jobFileName;
                _status.text = "Fetching <color=yellow>'to fix'</color> folders " + folder.Name + " spr file to copy";
                if (File.Exists(checkPath) && checkPath.Contains(".spr"))
                    sprToCopy = checkPath;
                _status.text = "Fetching <color=yellow>'to fix'</color> folders " + folder.Name + " act file to copy";
                if (File.Exists(checkPath) && checkPath.Contains(".act"))
                    actToCopy = checkPath;
                if (!string.IsNullOrEmpty(sprToCopy) && !string.IsNullOrEmpty(actToCopy))
                    break;
            }

            if (string.IsNullOrEmpty(sprToCopy) || string.IsNullOrEmpty(actToCopy))
            {
                _status.text = "Can't fix <color=yellow>'male'</color> folder " + folder.Name + " because not found " + (string.IsNullOrEmpty(sprToCopy) ? "spr" : "act") + " to copy\nPath:" + TO_FIX_PATH + folder.Name + "/" + MALE_ENCODE_874 + "/" + lastJobFileName;
                return;
            }
            else
            {
                foreach (var jobFileName in maleList)
                {
                    var checkPath = TO_FIX_PATH + folder.Name + "/" + MALE_ENCODE_874 + "/" + jobFileName;

                    if (!File.Exists(checkPath) && checkPath.Contains(".spr"))
                    {
                        fixCount++;
                        _status.text = "Fixing spr <color=yellow>'male'</color> folder " + folder.Name + " file name " + jobFileName;
                        File.Copy(sprToCopy, TO_FIX_PATH + folder.Name + "/" + MALE_ENCODE_874 + "/" + jobFileName);
                    }

                    if (!File.Exists(checkPath) && checkPath.Contains(".act"))
                    {
                        fixCount++;
                        _status.text = "Fixing act <color=yellow>'male'</color> folder " + folder.Name + " file name " + jobFileName;
                        File.Copy(actToCopy, TO_FIX_PATH + folder.Name + "/" + MALE_ENCODE_874 + "/" + jobFileName);
                    }
                }
            }

            sprToCopy = string.Empty;
            actToCopy = string.Empty;

            _status.text = "Fetching <color=yellow>'to fix'</color> folders " + folder.Name;
            foreach (var jobFileName in femaleList)
            {
                var checkPath = TO_FIX_PATH + folder.Name + "/" + FEMALE_ENCODE_874 + "/" + jobFileName;
                _status.text = "Fetching <color=yellow>'to fix'</color> folders " + folder.Name + " spr file to copy";
                if (File.Exists(checkPath) && checkPath.Contains(".spr"))
                    sprToCopy = checkPath;
                _status.text = "Fetching <color=yellow>'to fix'</color> folders " + folder.Name + " act file to copy";
                if (File.Exists(checkPath) && checkPath.Contains(".act"))
                    actToCopy = checkPath;
                if (!string.IsNullOrEmpty(sprToCopy) && !string.IsNullOrEmpty(actToCopy))
                    break;
            }

            if (string.IsNullOrEmpty(sprToCopy) || string.IsNullOrEmpty(actToCopy))
            {
                _status.text = "Can't fix <color=yellow>'female'</color> folder " + folder.Name + " because not found " + (string.IsNullOrEmpty(sprToCopy) ? "spr" : "act") + " to copy";
                return;
            }
            else
            {
                foreach (var jobFileName in femaleList)
                {
                    var checkPath = TO_FIX_PATH + folder.Name + "/" + FEMALE_ENCODE_874 + "/" + jobFileName;

                    if (!File.Exists(checkPath) && checkPath.Contains(".spr"))
                    {
                        fixCount++;
                        _status.text = "Fixing spr <color=yellow>'female'</color> folder " + folder.Name + " file name " + jobFileName;
                        File.Copy(sprToCopy, TO_FIX_PATH + folder.Name + "/" + FEMALE_ENCODE_874 + "/" + jobFileName);
                    }

                    if (!File.Exists(checkPath) && checkPath.Contains(".act"))
                    {
                        fixCount++;
                        _status.text = "Fixing act <color=yellow>'female'</color> folder " + folder.Name + " file name " + jobFileName;
                        File.Copy(actToCopy, TO_FIX_PATH + folder.Name + "/" + FEMALE_ENCODE_874 + "/" + jobFileName);
                    }
                }
            }
        }

        _status.text = "Done fixing <color=yellow>" + fixCount + "</color> sprite robe";
    }
}

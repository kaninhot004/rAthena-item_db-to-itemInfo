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

        _status.text = "Fetching directory 'male' job name";
        DirectoryInfo dir = new DirectoryInfo(JOB_NAME_PATH + MALE_ENCODE_874);
        _status.text = "Fetching files 'male' job name";
        var files = dir.GetFiles("*");
        _status.text = "Adding 'male' job name";
        foreach (var file in files)
        {
            if (!maleList.Contains(file.Name) && !file.Name.Contains(".meta"))
                maleList.Add(file.Name);
        }

        _status.text = "Fetching directory 'female' job name";
        dir = new DirectoryInfo(JOB_NAME_PATH + FEMALE_ENCODE_874);
        _status.text = "Fetching files 'female' job name";
        files = dir.GetFiles("*");
        _status.text = "Adding 'female' job name";
        foreach (var file in files)
        {
            if (!femaleList.Contains(file.Name) && !file.Name.Contains(".meta"))
                femaleList.Add(file.Name);
        }

        _status.text = "Fetching 'to fix' folders";
        dir = new DirectoryInfo(TO_FIX_PATH);
        var folders = dir.GetDirectories("*");
        foreach (var folder in folders)
        {
            string sprToCopy = string.Empty;
            string actToCopy = string.Empty;
            string lastJobFileName = string.Empty;
            _status.text = "Fetching 'to fix' folders " + folder.Name;
            foreach (var jobFileName in maleList)
            {
                lastJobFileName = jobFileName;
                var checkPath = TO_FIX_PATH + folder.Name + "/" + MALE_ENCODE_874 + "/" + jobFileName;
                _status.text = "Fetching 'to fix' folders " + folder.Name + " spr file to copy";
                if (File.Exists(checkPath) && checkPath.Contains(".spr"))
                    sprToCopy = checkPath;
                _status.text = "Fetching 'to fix' folders " + folder.Name + " act file to copy";
                if (File.Exists(checkPath) && checkPath.Contains(".act"))
                    actToCopy = checkPath;
                if (!string.IsNullOrEmpty(sprToCopy) && !string.IsNullOrEmpty(actToCopy))
                    break;
            }

            if (string.IsNullOrEmpty(sprToCopy) || string.IsNullOrEmpty(actToCopy))
            {
                _status.text = "Can't fix 'male' folder " + folder.Name + " because not found " + (string.IsNullOrEmpty(sprToCopy) ? "spr" : "act") + " to copy\nPath:" + TO_FIX_PATH + folder.Name + "/" + MALE_ENCODE_874 + "/" + lastJobFileName;
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
                        _status.text = "Fixing spr 'male' folder " + folder.Name + " file name " + jobFileName;
                        File.Copy(sprToCopy, TO_FIX_PATH + folder.Name + "/" + MALE_ENCODE_874 + "/" + jobFileName);
                    }

                    if (!File.Exists(checkPath) && checkPath.Contains(".act"))
                    {
                        fixCount++;
                        _status.text = "Fixing act 'male' folder " + folder.Name + " file name " + jobFileName;
                        File.Copy(actToCopy, TO_FIX_PATH + folder.Name + "/" + MALE_ENCODE_874 + "/" + jobFileName);
                    }
                }
            }

            sprToCopy = string.Empty;
            actToCopy = string.Empty;

            _status.text = "Fetching 'to fix' folders " + folder.Name;
            foreach (var jobFileName in femaleList)
            {
                var checkPath = TO_FIX_PATH + folder.Name + "/" + FEMALE_ENCODE_874 + "/" + jobFileName;
                _status.text = "Fetching 'to fix' folders " + folder.Name + " spr file to copy";
                if (File.Exists(checkPath) && checkPath.Contains(".spr"))
                    sprToCopy = checkPath;
                _status.text = "Fetching 'to fix' folders " + folder.Name + " act file to copy";
                if (File.Exists(checkPath) && checkPath.Contains(".act"))
                    actToCopy = checkPath;
                if (!string.IsNullOrEmpty(sprToCopy) && !string.IsNullOrEmpty(actToCopy))
                    break;
            }

            if (string.IsNullOrEmpty(sprToCopy) || string.IsNullOrEmpty(actToCopy))
            {
                _status.text = "Can't fix 'female' folder " + folder.Name + " because not found " + (string.IsNullOrEmpty(sprToCopy) ? "spr" : "act") + " to copy";
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
                        _status.text = "Fixing spr 'female' folder " + folder.Name + " file name " + jobFileName;
                        File.Copy(sprToCopy, TO_FIX_PATH + folder.Name + "/" + FEMALE_ENCODE_874 + "/" + jobFileName);
                    }

                    if (!File.Exists(checkPath) && checkPath.Contains(".act"))
                    {
                        fixCount++;
                        _status.text = "Fixing act 'female' folder " + folder.Name + " file name " + jobFileName;
                        File.Copy(actToCopy, TO_FIX_PATH + folder.Name + "/" + FEMALE_ENCODE_874 + "/" + jobFileName);
                    }
                }
            }
        }

        _status.text = "Done fix " + fixCount + " sprite robe";
    }
}

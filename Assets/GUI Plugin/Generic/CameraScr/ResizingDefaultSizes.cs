 using UnityEngine;
using System.Collections;

// carries the default resolution sizes to be used in other scripts

public class ResizingDefaultSizes : MonoBehaviour 
{
    public static ResizingDefaultSizes _instance = null;
    public float DefaultResolutionWidth = 426;
    public float DefaultResolutionHeight = 320;

    void Awake()
    {
        _instance = this;
    }

    public static ResizingDefaultSizes Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject temp = new GameObject();
                _instance = temp.AddComponent<ResizingDefaultSizes>();
                temp.name = "ResizingDefaultSizes";
                DontDestroyOnLoad(temp);
            }

            return _instance;
        }
    }
}

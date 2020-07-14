using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QualitySet : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        QualitySettings.SetQualityLevel(5);
    }

}

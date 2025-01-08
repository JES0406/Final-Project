using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAtCharacterScript : MonoBehaviour
{
    [SerializeField] private GameObject objectToTrack;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (objectToTrack is not null)
        {
            transform.LookAt(objectToTrack.transform);
        }
    }
}

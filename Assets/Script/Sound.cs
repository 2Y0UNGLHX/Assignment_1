using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;




public class Sound : MonoBehaviour
{
    // import the function swordSwing() in Sound.dll
    [DllImport("Sound")]
    private static extern void swordSwing();

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Attack"))
        {
            swordSwing();
        }
    }
}

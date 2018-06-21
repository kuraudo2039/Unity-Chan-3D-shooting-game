using UnityEngine;
using System.Collections;

public class Hair_Physical : MonoBehaviour
{
    void Update()
    {
        //髪の物理演算(rigitbody&joint)が等速直線運動以外にも対応しやすいように
        Time.fixedDeltaTime = Time.deltaTime/3.0f;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public void TPIsReady()
    {
        GameObject.FindGameObjectWithTag("Player").SendMessage("TPIsReady");
    }
}

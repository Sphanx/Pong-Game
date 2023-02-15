using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoalScripts : MonoBehaviour
{
    public GameObject goalCounterObj;
    private int counter;

    private void Start() {
        goalCounterObj.GetComponent<TextMeshProUGUI>().text = "0";
    }
    
    private void OnCollisionEnter2D(Collision2D other) {
        counter ++;
        goalCounterObj.GetComponent<TextMeshProUGUI>().text = counter.ToString();
    }
}

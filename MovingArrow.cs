using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingArrow : MonoBehaviour
{
    [SerializeField] float speed;
    float maxposX;
    Vector3 currentpos = new Vector3();
    Vector3 updatedpos = new Vector3();
    void Start()
    {
        currentpos = transform.position;
        maxposX = currentpos.x + 12;
        
    }
    void Update()
    {
        if(updatedpos.x<=maxposX)
        {
            updatedpos = transform.position;
            updatedpos.x = updatedpos.x + (Time.deltaTime * speed);
            transform.position = updatedpos;
            Debug.Log(transform.position);
        }
        else
        { 
            transform.position = currentpos;
            updatedpos = transform.position;
        }
    }
}

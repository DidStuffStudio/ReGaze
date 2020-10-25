using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallsInTheBucket : MonoBehaviour
{
    public GameObject[] balls;
    public GameObject[] buckets;
    public float Ldistance = 6.8f;
    public float Mdistance = 4.0f;
    public float Sdistance = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < balls.Length; i++)
        {
            if (Vector3.Distance(balls[i].transform.position, buckets[0].transform.position) <= Ldistance)
            {
                Debug.Log("Large Bucket Hit");
                buckets[0].GetComponentInChildren<MeshRenderer>().material.SetColor("_Color", Color.green);
            }

            if (Vector3.Distance(balls[i].transform.position, buckets[1].transform.position) <= Mdistance)
            {
                Debug.Log("Medium Bucket Hit");
                buckets[1].GetComponentInChildren<MeshRenderer>().material.SetColor("_Color", Color.green);
            }
            if (Vector3.Distance(balls[i].transform.position, buckets[2].transform.position) <= Sdistance)
            {
                Debug.Log("Small Bucket Hit");
                buckets[2].GetComponentInChildren<MeshRenderer>().material.SetColor("_Color", Color.green);
            }
        }
    }


}

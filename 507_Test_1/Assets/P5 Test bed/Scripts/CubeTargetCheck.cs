using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeTargetCheck : MonoBehaviour
{
    public GameObject[] cubes;
    public GameObject[] cubeTargets;
    public int cubeToTargetDist = 8;

    // Start is called before the first frame update
    void Start()
    {
        /*for (int i = 1; i < cubes.Length; i++)
        {
            cubes[i].SetActive(false);
            cubeTargets[i].SetActive(false);
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        checkCubePos();
        //cubes[checkCubePos()].SetActive(true);
        //cubeTargets[checkCubePos()].SetActive(true);
    }

    private int checkCubePos()
    {
        int cubePos = 0;

        if (Vector3.Distance(cubes[0].transform.position , cubeTargets[0].transform.position) < cubeToTargetDist)
        {
            cubePos = 1;
            Debug.Log("Blue cube on target");
        }
        
        if (Vector3.Distance(cubes[1].transform.position, cubeTargets[1].transform.position) < cubeToTargetDist)
        {
            cubePos = 2;
            Debug.Log("Blue cube on target");
        }
        
        if (Vector3.Distance(cubes[2].transform.position, cubeTargets[2].transform.position) < cubeToTargetDist)
        {
            cubePos = 3;
            Debug.Log("Blue cube on target");
        }
        
        if (Vector3.Distance(cubes[3].transform.position, cubeTargets[3].transform.position) < cubeToTargetDist)
        {
            cubePos = 4;
            Debug.Log("Blue cube on target");
        }


        return cubePos;

    }
}

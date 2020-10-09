//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//namespace Techstr33tBo11s { 

//public class Teleportation : MonoBehaviour
//{
//    [SerializeField] private Transform [] targets;
//    [SerializeField] private float speed = 1.0f;

//    [SerializeField] private TobiiXR_Initializer tobiiXR_Initializer;
//    [SerializeField] private GestureRecognition gestureRecognition;

//    public bool hasFocus = false;
//    public bool hasBlinked = false;

//    // Start is called before the first frame update
//    void Start()
//    {
        
//    }

//    // Update is called once per frame
//    void Update()
//    {

//        if (tobiiXR_Initializer.isBlinking) StartCoroutine(BlinkingTimer());
//        if(hasFocus && tobiiXR_Initializer.isBlinking)
//        {
//            float step = speed * Time.deltaTime; // calculate distance to move
//            //transform.position = Vector3.MoveTowards(transform.position, targets.position, step);
           

//        }
//        /*
//        if(hasBlinked)
//        {
//            Debug.Log(tobiiXR_Initializer.targetPos);
//            transform.position = tobiiXR_Initializer.targetPos;
//            hasBlinked = false;
//        }
//        */

//        if(gestureRecognition.gestureRecognized)
//        {
//            transform.position = new Vector3(  tobiiXR_Initializer.targetPos.x, -2.6f, tobiiXR_Initializer.targetPos.z);
//            gestureRecognition.gestureRecognized = false;
//        }
//        /*
//        if(hasBlinked && tobiiXR_Initializer.raycastHitObject != null)
//        {
//            foreach(var target in targets)
//            {
//                if(target == tobiiXR_Initializer.raycastHitObject)
//                {
//                    transform.position = tobiiXR_Initializer.raycastHitObject.transform.position;
//                }
//            }
//        }
//        */

//    }
    
//    IEnumerator BlinkingTimer()
//    {
//        // alternative --> while blinking --> Time.time counter --> If bigger than certain amount of time --> Teleport
//        yield return new WaitForSeconds(0.5f);
//        if (tobiiXR_Initializer.isBlinking) hasBlinked = true;
//    }
//}
//}

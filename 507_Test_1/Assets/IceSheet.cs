using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSheet : MonoBehaviour
{
    public GameObject iceShardsPrefab;
    public AudioSource source;

    public AudioClip breaking, cracking;
    public float breakForce = 10;

    private bool steppedOn;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer == 12 && !steppedOn) //Play cracking
        {
            steppedOn = true;
            source.clip = cracking;
            source.Play();
        }

        else if (collision.collider.gameObject.layer == 13) //Play break and break
        {
            var shards = Instantiate(iceShardsPrefab, transform.position, transform.rotation, null);
            //shards.transform.localScale = Vector3.one;
            source.clip = breaking;
            source.Play();

            foreach (var rb in shards.GetComponentsInChildren<Rigidbody>())
            {
                rb.AddForceAtPosition(Vector3.down * breakForce, collision.gameObject.transform.position);
            }
            gameObject.SetActive(false);
 

        }
    }
}

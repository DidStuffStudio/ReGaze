using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IceSheet : MonoBehaviour
{
    public GameObject iceShardsPrefab;
    public AudioSource source;

    public AudioClip breaking, cracking;
    public float breakForce = 10;

    private bool steppedOn;

    public UnityEvent OnBreak;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer == 12 && !steppedOn) //Play cracking
        {
            steppedOn = true;
            source.clip = cracking;
            source.Play();
        }

        else if (collision.collider.gameObject.CompareTag("Icicle")) //Play break and break
        {
            var shards = Instantiate(iceShardsPrefab, transform.position, transform.rotation, null); 
            shards.transform.localScale = Vector3.one/2;
            source.clip = breaking;
            source.Play();

            foreach (var rb in shards.GetComponentsInChildren<Rigidbody>())
            {
                rb.AddForceAtPosition(Vector3.down * breakForce, collision.gameObject.transform.position);
            }
            gameObject.SetActive(false);
            StartCoroutine(WaitForEnd());
 

        }
    }

    IEnumerator WaitForEnd()
    {
        yield return new WaitForSeconds(2);
        OnBreak?.Invoke();
    }
}

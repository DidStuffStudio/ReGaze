using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class IceSheet : MonoBehaviour
{
    public GameObject iceShardsPrefab;
    private AudioSource source;

    public float breakForce = 10;

    public bool breakTest;
    private bool broken;

    public UnityEvent OnBreak;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (breakTest && !broken)
        {
            Break(null);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("Icicle") && !broken) //Play break and break
        {
            Break(collision);
        }
    }

    void Break(Collision col)
    {
            broken = true;
        
            var shards = Instantiate(iceShardsPrefab, transform.position, transform.rotation, null);
            shards.transform.localScale = transform.localScale;
            source.Play();

        if (col != null)
        {
            foreach (var rb in shards.GetComponentsInChildren<Rigidbody>())
            {
                rb.AddForceAtPosition(Vector3.down * breakForce, col.gameObject.transform.position);
            }
        }


            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;

            StartCoroutine(WaitForEnd());
    }

    IEnumerator WaitForEnd()
    {
        yield return new WaitForSeconds(2);
        OnBreak?.Invoke();
        gameObject.SetActive(false);
    }
}

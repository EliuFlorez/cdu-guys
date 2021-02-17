using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountController : MonoBehaviour
{
    // Public
    public CountSystem countSystem;
    public coinType type;

    public enum coinType { coin, gem };

    private void Awake()
    {
        countSystem = FindObjectOfType<CountSystem>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //
    }

    // Update is called once per frame
    void Update()
    {
        // Rotation
        transform.Rotate(new Vector3(0f, 250f, 0f) * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (coinType.coin == type)
            {
                countSystem.Increase(other.transform.position, "coin");
            }
            else
            {
                countSystem.Increase(other.transform.position, "gem");
            }
            Destroy(gameObject);
        }
    }
}

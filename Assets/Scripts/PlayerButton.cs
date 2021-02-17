using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerButton : MonoBehaviour
{
    [Header("Components")]
    public GameObject _objectToUp;
    public Transform interaction;

    // Private
    private GameObject _objectUp;
    private bool _actionButton;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_objectToUp != null)
        {
            if (_objectToUp.GetComponent<ObjectController>().isUp == true && _objectUp == null)
            {
                if (Input.GetKeyDown(KeyCode.F) || _actionButton == true)
                {
                    _objectUp = _objectToUp;
                    _objectUp.GetComponent<ObjectController>().isUp = false;
                    _objectUp.transform.SetParent(interaction);
                    _objectUp.transform.position = interaction.position;
                    _objectUp.GetComponent<Rigidbody>().useGravity = false;
                    _objectUp.GetComponent<Rigidbody>().isKinematic = true;
                }
            }
            else if (_objectUp != null)
            {
                if (Input.GetKeyDown(KeyCode.F) || _actionButton == true)
                {
                    _objectUp.GetComponent<ObjectController>().isUp = true;
                    _objectUp.transform.SetParent(null);
                    _objectUp.GetComponent<Rigidbody>().useGravity = true;
                    _objectUp.GetComponent<Rigidbody>().isKinematic = false;
                    _objectUp = null;
                }
            }
        }
    }

    private void LateUpdate()
    {
        _actionButton = false;
    }

    public void ActionButtonB()
    {
        _actionButton = true;
    }
}

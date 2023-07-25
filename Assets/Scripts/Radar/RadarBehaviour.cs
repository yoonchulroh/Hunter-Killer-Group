using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarBehaviour : MonoBehaviour
{
    protected List<GameObject> _detectedUboats = new List<GameObject>();

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Uboat")
        {
            _detectedUboats.Add(collision.gameObject);
            GameManager.Instance.detectionManager.AddUboat(collision.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Uboat")
        {
            _detectedUboats.Remove(collision.gameObject);
            GameManager.Instance.detectionManager.RemoveUboat(collision.gameObject);
        }
    }
}

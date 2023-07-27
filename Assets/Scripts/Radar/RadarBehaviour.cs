using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarBehaviour : MonoBehaviour
{
    protected List<GameObject> _detectedUboats = new List<GameObject>();

    protected RadarData _radarProperties;

    private GameObject _backgroundClickDetector;

    void Start()
    {
        _backgroundClickDetector = GameObject.FindWithTag("BackgroundClickDetector");
        gameObject.transform.localScale = new Vector3(_radarProperties.range, _radarProperties.range, _radarProperties.range);
    }

    public void SetRadarProperties(RadarData radarProperties)
    {
        _radarProperties = radarProperties;
    }
    
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

    void OnMouseDown()
    {
        _backgroundClickDetector.GetComponent<BackgroundClickDetector>().PassMouseDown();
    }
}

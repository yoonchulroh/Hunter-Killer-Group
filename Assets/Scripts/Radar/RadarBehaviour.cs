using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarBehaviour : MonoBehaviour
{
    protected List<GameObject> _detectedEntities = new List<GameObject>();

    protected RadarData _radarProperties;
    protected GameObject _parent;

    private GameObject _backgroundClickDetector;

    void Start()
    {
        _backgroundClickDetector = GameObject.FindWithTag("BackgroundClickDetector");
        gameObject.transform.localScale = new Vector3(_radarProperties.range, _radarProperties.range, _radarProperties.range);

        Hide();
    }

    void Update()
    {   
        transform.position = new Vector3(_parent.transform.position.x, _parent.transform.position.y, 1);
    }

    public void SetRadarProperties(RadarData radarProperties)
    {
        _radarProperties = radarProperties;
    }

    public void SetRadarParent(GameObject parent)
    {
        _parent = parent;
    }

    public void Reveal()
    {
        GetComponent<SpriteRenderer>().color = new Color(6, 245, 3, 0.72f);
    }

    public void Hide()
    {
        GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (_radarProperties.enemyTags.Contains(collision.gameObject.tag))
        {
            _detectedEntities.Add(collision.gameObject);

            if (_parent.tag == "Uboat")
            {
                GameManager.Instance.detectionManager.AddFriendly(collision.gameObject);
            } else {
                GameManager.Instance.detectionManager.AddUboat(collision.gameObject);
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (_radarProperties.enemyTags.Contains(collision.gameObject.tag))
        {
            _detectedEntities.Remove(collision.gameObject);

            if (_parent.tag == "Uboat")
            {
                GameManager.Instance.detectionManager.RemoveFriendly(collision.gameObject);
            } else {
                GameManager.Instance.detectionManager.RemoveUboat(collision.gameObject);
            }
        }
    }

    void OnMouseDown()
    {
        _backgroundClickDetector.GetComponent<BackgroundClickDetector>().PassMouseDown();
    }

    public void RemoveRadar()
    {
        foreach(GameObject entity in _detectedEntities)
        {
            if (_parent.tag == "Uboat")
            {
                GameManager.Instance.detectionManager.RemoveFriendly(entity);
            } else {
                GameManager.Instance.detectionManager.RemoveUboat(entity);
            }
        }
        Destroy(gameObject);
    }
}

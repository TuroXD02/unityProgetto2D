using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField] int damageDealt;
    public List<Transform> points;
    public int nextID = 0;
    int idChangeValue = 1;
    public float speed = 2f;

    void Reset()
    {
        Init();
    }

    void Update()
    {
        MoveToNextPoint();
    }

    void Init()
    {

        GetComponent<BoxCollider2D>().isTrigger = true;
        //creazione root nemico
        GameObject root = new GameObject("Enemy_" + name);
        root.transform.position = transform.position;
        transform.SetParent(root.transform);
        //creazione root waypoints
        GameObject waypoints = new GameObject("Waypoints");
        waypoints.transform.SetParent(root.transform);
        //creazione waypoints
        GameObject p1 = new GameObject("Point 1"); p1.transform.SetParent(waypoints.transform);
        GameObject p2 = new GameObject("Point 2"); p2.transform.SetParent(waypoints.transform);
        p1.transform.position = transform.position;
        p2.transform.position = transform.position;
        //aggiungi points alla lista
        points = new List<Transform>();
        points.Add(p1.transform);
        points.Add(p2.transform);
    }

    void MoveToNextPoint()
    {
        Transform goalPoint = points[nextID];
        if(goalPoint.transform.position.x > transform.position.x)
            transform.localScale = new Vector3(4, 4, 1);
        else
            transform.localScale = new Vector3(-4, 4, 1);

        transform.position = Vector2.MoveTowards(transform.position,goalPoint.position, speed *Time.deltaTime);

        if(Vector2.Distance(transform.position, goalPoint.position)<.2f)
        {
            if (nextID == points.Count - 1)
                idChangeValue = -1;
            if (nextID == 0)
                idChangeValue = 1;
            nextID += idChangeValue;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            FindObjectOfType<HealthSystem>().LoseHealth(damageDealt);
        }
    }

}

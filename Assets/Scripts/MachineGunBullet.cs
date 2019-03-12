using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGunBullet : MonoBehaviour
{

    public float speed = 100;
    public Rigidbody2D rb;
    public float spray = 20;
    public float lifeTime = 1;
    private float timeLived = 0;
    // Start is called before the first frame update
    void Start()
    {
        float sprayAngle = Random.Range(-spray, spray);

        Quaternion quart = gameObject.transform.rotation * Quaternion.Euler(sprayAngle, 0, sprayAngle);

        rb.simulated = true;
        Vector3 rot = rb.transform.localRotation.eulerAngles;
        rot = quart.eulerAngles;
        rb.velocity = RotateVector(Vector2.right, rot.z) * speed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger Destroy");
        if (collision.gameObject.tag == "Bullet") return;

        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Player hit");
            collision.gameObject.SendMessage("ApplyDamage", 10);
        }
        
        Destroy(gameObject);
    }

    void Update()
    {
        timeLived = timeLived + Time.deltaTime;
        Debug.Log(timeLived);

        if (timeLived > lifeTime)
        {
            Destroy(gameObject);
        }
    }
    public Vector2 RotateVector(Vector2 v, float angle)
    {
        float radian = angle * Mathf.Deg2Rad;
        float _x = v.x * Mathf.Cos(radian) - v.y * Mathf.Sin(radian);
        float _y = v.x * Mathf.Sin(radian) + v.y * Mathf.Cos(radian);
        return new Vector2(_x, _y);
    }
}

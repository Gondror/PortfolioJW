using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private float lifeTime = 5f;
    private float timer = 0f;
    private Renderer bulletRend;

    public ParticleSystem exploBulle;

    private void Awake()
    {
        bulletRend = gameObject.GetComponent<Renderer>();
    }

    private void Update()
    {
        Movemement();
        SetTime();
        CheckVisible();

        if (timer >= lifeTime)
        {
            Autodestroy();
        }
    }

    private void Movemement()
    {
        transform.localPosition += transform.right * Time.deltaTime * speed;
    }

    private void SetTime()
    {
        timer += Time.deltaTime;
    }

    private void Autodestroy()
    {

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IHittable hittableObject = collision.gameObject.GetComponent<IHittable>();

        if (hittableObject != null)
        {
            hittableObject.GetHit();


            ParticleSystem instExplo = Instantiate(exploBulle, gameObject.transform.position, gameObject.transform.rotation);

            

            Autodestroy();
        }
    }

    private void CheckVisible()
    {
        if (!bulletRend.isVisible)
        {
            Autodestroy();
        }
    }
}

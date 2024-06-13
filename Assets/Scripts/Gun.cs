using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Gun : MonoBehaviour
{
    public enum GunType { Semi, Burst, Auto }
    public GunType gunType;
    public float rpm;





    public Transform spawn;
    private LineRenderer tracer;




    public AudioClip shootSound; // Asegúrate de asignar un AudioClip en el Inspector

    // System
    private float secondsBetweenShots;
    private float nextPossibleShootTime;

    // Components
    private AudioSource audioSource;

    void Start()
    {
        secondsBetweenShots = 60 / rpm;
        audioSource = GetComponent<AudioSource>();

        if (GetComponent<LineRenderer>())
        {
            tracer = GetComponent<LineRenderer>();
        }
    }

    public void Shoot()
    {
        if (CanShoot())
        {
            Ray ray = new Ray(spawn.position, spawn.forward);
            RaycastHit hit;

            float shotDistance = 20;

            if (Physics.Raycast(ray, out hit, shotDistance))
            {
                shotDistance = hit.distance;


                // Si golpea un enemigo, destrúyelo
                if (hit.collider.CompareTag("Enemy"))
                {
                    Destroy(hit.collider.gameObject);

                    // Incrementa el contador de enemigos destruidos
                    EnemyCounter.Instance.EnemyDestroyed();
                }



            }

            nextPossibleShootTime = Time.time + secondsBetweenShots;

            // Reproducir el sonido del disparo
            if (shootSound != null)
            {
                audioSource.PlayOneShot(shootSound);
            }

            if (tracer)
            {
                StartCoroutine("RenderTracer", ray.direction * shotDistance);
            }


        }

    }

    public void ShootContinuous()
    {
        if (gunType == GunType.Auto)
        {
            Shoot();
        }
    }

    private bool CanShoot()
    {
        bool canShoot = true;

        if (Time.time < nextPossibleShootTime) // Corregir esta línea
        {
            canShoot = false;
        }

        return canShoot;
    }

    IEnumerator RenderTracer(Vector3 hitPoint)
    {

        tracer.enabled = true;
        tracer.SetPosition(0,spawn.position);
        tracer.SetPosition(1, spawn.position + hitPoint);
        yield return new WaitForSeconds(0.05f); // Esperar 0.05 segundos
        tracer.enabled = false;



    }
        
        
        



}

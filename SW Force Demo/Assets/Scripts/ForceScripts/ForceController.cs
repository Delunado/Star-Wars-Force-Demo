using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceController : MonoBehaviour
{
    public float range = 100f;
    public float launchForce = 10f;

    [Tooltip("Speed of bringing closer or further the target")]
    public float drawTargetSpeed = 15f;

    private float timer;
    public float timeToUseForce = 0.5f;

    bool canUseForce;

    private new AudioSource audio;
    public AudioClip forcePushFX;
    public AudioClip telekinesisFX;
    public AudioClip telekinesisPushFX;

    public Camera fpsCam;
    public GameObject forceField;
    public Transform lookRoot;

    Target target;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > timeToUseForce)
        {
            canUseForce = true;    
        } else
        {
            timer += Time.deltaTime;
        }

        //Lanzar objeto
        if (Input.GetButtonDown(ActionsTags.USE_FORCE_FIELD) && target && canUseForce)
        {
            target.Launch(launchForce);
            DropObject();
            audio.PlayOneShot(telekinesisPushFX);
        }

        //Coger objeto
        if (Input.GetButtonDown(ActionsTags.USE_FORCE) && !target && canUseForce)
        {
            CatchObject();
            timer = 0f;
            canUseForce = false;
        }

        if (Input.GetButtonDown(ActionsTags.USE_FORCE) && target && canUseForce)
        {
            DropObject();
        }

        //Mover objeto adelante y atras
        if (Input.GetAxis(ActionsTags.ZOOM_FORCE) != 0f)
        {
            if (target)
            {
                float step = Time.deltaTime * drawTargetSpeed * -Mathf.Sign(Input.GetAxis(ActionsTags.ZOOM_FORCE));
                target.transform.position = Vector3.MoveTowards(target.transform.position, fpsCam.transform.position, step);
            }
        }

        //Usar campo de fuerza
        if (Input.GetButtonDown(ActionsTags.USE_FORCE_FIELD) && !target && canUseForce)
        {
            GameObject force = Instantiate(forceField, lookRoot.position, lookRoot.rotation);
            force.GetComponent<ForceFieldController>().SetMovement(Camera.main.transform.forward);
            audio.PlayOneShot(forcePushFX);
        }
    }


    private void DropObject()
    {
        target.IsForced(false);
        target = null;
        timer = 0f;
        canUseForce = false;

        audio.loop = false;
        audio.Stop();
    }

    private void CatchObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            target = hit.transform.GetComponent<Target>();

            if (target)
            {
                target.SetCamera(fpsCam);

                target.transform.parent = transform;
                target.IsForced(true);

                audio.loop = true;
                audio.PlayOneShot(telekinesisFX);
            }
        }
    }
}

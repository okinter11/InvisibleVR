using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    public GameObject[] FlickeringObjects;

    public Light light1;
    public Light light2;
    public float minTime;
    public float maxTime;
    public float timer;
    public Renderer light_render;
    public Renderer light_render1;
    public int enterCount = 0;

    bool isClicked = false;
    public AudioSource aus;
    public AudioClip lightAudio;

    // Start is called before the first frame update
    void Start()
    {
        timer = Random.Range(minTime, maxTime);
        if (light_render != null) light_render.material.EnableKeyword("_EMISSION");
        if (light_render != null) light_render1.material.EnableKeyword("_EMISSION");
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && enterCount==0)
        {
            FlickerLight();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        light_render.material.SetColor("_EmissionColor", new Color32(191, 171, 138, 255));
        light_render1.material.SetColor("_EmissionColor", new Color32(191, 171, 138, 255));
        light1.GetComponent<Light>().color = new Color32(241, 208, 209, 255);
        light2.GetComponent<Light>().color = new Color32(241, 208, 209, 255);
        light1.enabled = true;
        light2.enabled = true;
        enterCount++;
    }

    void FlickerLight()
    {
        light1.GetComponent<Light>().color = Color.red;
        light2.GetComponent<Light>().color = Color.red;
        light_render.material.SetColor("_EmissionColor", Color.red);
        light_render1.material.SetColor("_EmissionColor", Color.red);
        if (timer > 0)
            timer -= Time.deltaTime;
        if (timer < 0)
        {
            light1.enabled = !light1.enabled;
            light2.enabled = !light2.enabled;
            light_render.material.SetColor("_EmissionColor", Color.black);
            light_render1.material.SetColor("_EmissionColor", Color.black);
            timer = Random.Range(minTime, maxTime);
            aus.PlayOneShot(lightAudio);
        }
    }

    public void LightOnOff()
    {
        for (int i = 0; i < FlickeringObjects.Length; i++)
        {
            FlickeringObjects[i].GetComponent<Light>().color = Color.black;
            if (i == 1)
            {
                FlickeringObjects[i].GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.black);
            }
        }
    }

    public void ButtonClickSoundPlay()
    {
        if (!isClicked)
        {
            isClicked = true;
            aus.Play();
        }
    }
}

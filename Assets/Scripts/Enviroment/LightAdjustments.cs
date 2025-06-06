using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightAdjustments : MonoBehaviour
{
    public GameObject GlobalLight;
    private Light2D light2D;

    public float targetIntensity;
    public float transitionTime;

    private float currentTarget;
    private float transitionSpeed;
    private bool isTransitioning = false;

    private void Start()
    {
        light2D = GlobalLight.GetComponent<Light2D>();
        currentTarget = light2D.intensity;
    }

    private void Update()
    {
        if (isTransitioning)
        {
           
            light2D.intensity = Mathf.MoveTowards(light2D.intensity, currentTarget, transitionSpeed * Time.deltaTime);

            if (Mathf.Approximately(light2D.intensity, currentTarget))
            {
                isTransitioning = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) StartTransition(targetIntensity, transitionTime);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) StartTransition(0.72f, transitionTime);
    }

    private void StartTransition(float newTarget, float time)
    {
        currentTarget = newTarget;
        transitionSpeed = Mathf.Abs(currentTarget - light2D.intensity) / time;
        isTransitioning = true;
    }
}

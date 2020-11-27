using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonSatellite : MonoBehaviour
{
    [SerializeField] GameObject destinationGameObject;
    [SerializeField] float moveSpeed = 5.0f;
    [SerializeField] float delayUntilGoingBackToOrigin = 5.0f;
    [SerializeField] float timeToDisplayIndicatorLine = 7.0f;
    [SerializeField] float initialTimeBetweenIndicatorLineFlashes = 1.0f;
    [SerializeField] float rotationAnglePerFrame = 1.5f;
    [SerializeField] Material indicatorLineMaterial;
    [SerializeField] Gradient indicatorLineToDestinationGradient;
    [SerializeField] Gradient indicatorLineToOriginGradient;
    [SerializeField] string sfxName = "Bling";

    private Vector2 origin;
    private Vector2 destination;
    private LineRenderer indicatorLineRenderer;
    private AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        origin = transform.position;
        destination = destinationGameObject.transform.position;
        audioManager = AudioManager.instance;

        CreateIndicatorLineRenderer();
        StartCoroutine(Rotate());
    }

    public IEnumerator Attack()
    {
        // Display path indicators
        indicatorLineRenderer.colorGradient = indicatorLineToDestinationGradient;
        Coroutine indicatorLineCoroutine = StartCoroutine(ShowIndicatorLine());

        yield return new WaitForSeconds(timeToDisplayIndicatorLine);

        // Hide path indicators
        StopCoroutine(indicatorLineCoroutine);
        indicatorLineRenderer.enabled = false;

        audioManager.PlaySoundEffect(sfxName);

        while (!HasReachedTarget(destination))
        {
            transform.position = Vector2.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);

            yield return null;
        }

        yield return new WaitForSeconds(delayUntilGoingBackToOrigin);

        // Display path indicators
        indicatorLineRenderer.colorGradient = indicatorLineToOriginGradient;
        indicatorLineCoroutine = StartCoroutine(ShowIndicatorLine());

        yield return new WaitForSeconds(timeToDisplayIndicatorLine);

        // Hide path indicators
        StopCoroutine(indicatorLineCoroutine);
        indicatorLineRenderer.enabled = false;

        audioManager.PlaySoundEffect(sfxName);

        while (!HasReachedTarget(origin))
        {
            transform.position = Vector2.MoveTowards(transform.position, origin, moveSpeed * Time.deltaTime);

            yield return null;
        }
    }

    private bool HasReachedTarget(Vector2 target)
    {
        return Vector2.Distance(transform.position, target) <= 0.1f;
    }

    private void CreateIndicatorLineRenderer()
    {
        Vector3[] points = new Vector3[2];
        points[0] = new Vector3(origin.x, origin.y, 0.0f);
        points[1] = new Vector3(destination.x, destination.y, 0.0f);

        indicatorLineRenderer = gameObject.AddComponent<LineRenderer>();
        indicatorLineRenderer.widthMultiplier = 0.5f;
        indicatorLineRenderer.positionCount = 2;
        indicatorLineRenderer.SetPositions(points);
        indicatorLineRenderer.material = indicatorLineMaterial;
        indicatorLineRenderer.enabled = false;
    }

    private IEnumerator ShowIndicatorLine()
    {
        float timeBetweenIndicatorLineFlashes = initialTimeBetweenIndicatorLineFlashes;

        while (true)
        {
            indicatorLineRenderer.enabled = !indicatorLineRenderer.enabled;
            // Slowly flash faster and faster
            timeBetweenIndicatorLineFlashes -= 0.1f;
            timeBetweenIndicatorLineFlashes = Mathf.Max(timeBetweenIndicatorLineFlashes, 0.07f);

            yield return new WaitForSeconds(timeBetweenIndicatorLineFlashes);
        }
    }

    private IEnumerator Rotate()
    {
        float zRotation = transform.rotation.z;

        while (true)
        {
            zRotation += rotationAnglePerFrame;

            if (zRotation > 360)
            {
                zRotation = 0;
            }

            transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, zRotation);

            yield return null;
        }
    }
}

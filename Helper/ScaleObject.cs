using System.Collections;
using UnityEngine;

public class ScaleObject : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] private float scaleFrom = 1f;
    [SerializeField] private float scaleTo = 1.5f;

    [SerializeField] private float duration = 5f;

    private Coroutine zoomCoroutine;

    void Start()
    {
 
    }

    void OnEnable()
    {
        if (target == null)
            target = transform;

        target.localScale = new Vector3(scaleFrom, scaleFrom, scaleFrom);

        ZoomIn();
    }

    public void ZoomIn()
    {
        StartZoom(scaleFrom, scaleTo);
    }

    public void ZoomOut()
    {
        StartZoom(scaleTo, scaleFrom);
    }

    public void StartZoom(float from, float to)
    {
        if (zoomCoroutine != null)
            StopCoroutine(zoomCoroutine);

        zoomCoroutine = StartCoroutine(ZoomRoutine(from, to));
    }

    private IEnumerator ZoomRoutine(float from, float to)
    {
        float time = 0f;

        target.localScale = new Vector3(from, from, from);

        while (time < duration)
        {
            time += Time.deltaTime;

            float t = Mathf.Clamp01(time / duration);

            target.localScale = new Vector3(Mathf.Lerp(from, to, t), Mathf.Lerp(from, to, t), Mathf.Lerp(from, to, t));

            yield return null;
        }

        target.localScale = new Vector3(to, to, to);
        zoomCoroutine = null;
    }

    private void OnDisable()
    {
        if(zoomCoroutine != null)
        {
            StopCoroutine(zoomCoroutine);
            zoomCoroutine = null;
        }
    }
}

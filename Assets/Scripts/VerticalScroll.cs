using System.Collections;
using UnityEngine;

public class VerticalScroll : MonoBehaviour
{   
    [Tooltip ("Game units per second")]
    [SerializeField] private float scrollRate = 0.2f;
    [SerializeField] private float scrollRateAfterDelay = 0.4f;
    [SerializeField] private float delayTime = 10f;


    private void Start()
    {
        StartCoroutine(ChangeScrollRateAfterTime(delayTime));
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector2(0f, scrollRate * Time.deltaTime));
    }

    IEnumerator ChangeScrollRateAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        scrollRate = scrollRateAfterDelay;
    }
}

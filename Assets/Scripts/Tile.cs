using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour 
{
    public enum TileObjectColor
    {
        Blue,
        Green,
        Red,
        Yellow
    }

    public TileObjectColor color;

    private void Start()
    {
        StartCoroutine(SpawnAnimation(0.4f));
    }

    public IEnumerator MoveTileToPoint(Vector3 targetPos, float elapsedTime)
    {
        var timePassed = 0.0f;
        var currentPosition = transform.position;

        while (timePassed < elapsedTime)
        {
            transform.position = Vector3.Lerp(currentPosition, targetPos, timePassed / elapsedTime);
            yield return null;
            timePassed += Time.deltaTime;
        }

        transform.position = targetPos;

        yield return null;
    }

    public IEnumerator SpawnAnimation(float elapsedTime)
    {
        var timePassed = 0.0f;
        var currentScale = transform.localScale;
        var startScale = Vector3.zero;

        while (timePassed < elapsedTime)
        {
            transform.localScale = Vector3.Lerp(startScale, currentScale, timePassed / elapsedTime);
            yield return null;
            timePassed += Time.deltaTime;
        }
    }
}

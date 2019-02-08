using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;

public class ProductionGraph : MonoBehaviour
{
    UILineRenderer[] bar;
    private int barCount;
    float[] random;
    float time;
    private void Awake()
    {
        bar = GetComponentsInChildren<UILineRenderer>();

        random = new float[bar.Length];
        for (int i = 0; i < random.Length; i++)
        {
            random[i] = Random.Range(0f, 1f);

        }

    }

    private void Update()
    {
        SetGraph();


    }

    public void SetGraph()
    {
        if (barCount > bar.Length - 1)
        {
            return;
        }

        Vector2[] points = new Vector2[2];

        for (int j = 0; j < 2; j++)
            points[j] = new Vector2(0.5f, 0);

        bar[barCount].Points = points;
        time += Time.deltaTime * 0.5f;
        bar[barCount].Points[1].y += time;
        bar[barCount].SetAllDirty();

        if (bar[barCount].Points[1].y > random[barCount])
        {
            bar[barCount].Points[1].y = random[barCount];
            time = 0;
            barCount++;
        }
    }
}

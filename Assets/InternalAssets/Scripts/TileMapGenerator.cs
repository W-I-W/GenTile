using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapGenerator : MonoBehaviour
{
    [SerializeField] private Tilemap m_Tile;
    [SerializeField] private TileBase m_TileBase;

    [SerializeField, Range(0, 1)] private float m_Noise = 0.2f;
    [SerializeField] private float m_Scale = 0.2f;
    private float m_Seed = 100;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            m_Seed = Random.Range(-1000, 10000);
            StartCoroutine(GenerationUpdate());
        }
    }

    private IEnumerator GenerationUpdate()
    {
        float max = 100;
        for (int x = 0; x < max; x++)
        {
            for (int y = 0; y < max; y++)
            {
                m_Tile.SetTile(new Vector3Int(x, y, 0), null);
                float xx = x / max;
                float yy = y / max;
                float noise = Mathf.PerlinNoise((xx * m_Scale) + m_Seed, yy * m_Scale);
                float m = Mathf.Abs(new Vector2(0.5f - xx, 0.5f - yy).magnitude) * 2f;

                if (noise > m - m_Noise)
                    m_Tile.SetTile(new Vector3Int(x, y, 0), m_TileBase);
            }
        }
        yield return null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralGenerator : MonoBehaviour
{
    [SerializeField] private PlaneGenerator p_Generator;
    [SerializeField] private GameObject tree;

    [SerializeField, Range(0f, 1f)] private float treeSpawnRate = 0.02f;

    public int x, z;
    public Vector2 genCount;
    public float noiseScale;
    public float maxHeight;

    public Color water_C, grass_C, snow_C;

    // Start is called before the first frame update
    void Start()
    {
        Vector2 offset;
        offset.x = Random.value * 100f;
        offset.y = Random.value * 100f;
        p_Generator.Create(x, z);

        for (int x = 0; x < genCount.x; x++)
        {
            for (int z = 0; z < genCount.y; z++)
            {
                float coordX = (x * noiseScale) + offset.x;
                float coordY = (z * noiseScale) + offset.y;
                float noise = Mathf.PerlinNoise(coordX, coordY);
                float height = (noise * maxHeight);


                    Vector3 position = new Vector3(x, height, z);
                    p_Generator.SetHeight(x, z, height);
                    if (height < 0.33f * maxHeight)
                    {
                        p_Generator.SetColor(x, z, water_C);
                        
                    }
                    else if (height < 0.66f * maxHeight)
                    {
                        p_Generator.SetColor(x, z, grass_C);
                       

                    }
                    else
                    {
                        p_Generator.SetColor(x, z, snow_C);
                       
                    }

                    if(Random.value < treeSpawnRate)
                    {
                        Instantiate(tree, position, Quaternion.identity);
                    }


                

            }
        }
        p_Generator.RefreshMesh();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

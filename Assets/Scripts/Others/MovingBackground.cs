using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MovingBackground : MonoBehaviour
{
    
    public float speed = 0.008f;
    public Transform other;

    [SerializeField] private BackgroundData data;

    private Tilemap tilemap;


    private void Awake() 
    {
        tilemap = GetComponent<Tilemap>();    
    }

    private void Start() 
    {
        int bg = GetComponentInParent<RandomBG>().bg;
        for (int i = -4; i <= 3; i++)
            for (int j = -2; j <= 1; j++)
                tilemap.SetTile(new Vector3Int(i,j,0),data.BackgroundTiles[bg]);
        
    }

    private void Update()
    {
        if(transform.position.y < -4*4)
            transform.position = new Vector3(transform.position.x,other.position.y+4*4,0);

        transform.position = new Vector3(transform.position.x,transform.position.y-speed,0);
    }
}

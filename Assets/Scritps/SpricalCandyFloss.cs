using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpricalCandyFloss : MonoBehaviour
{
    [SerializeField]
    private Sprite[] SpriteFlossLevel;
    [SerializeField]
    private GameObject objSprite = null;
    [SerializeField]
    private GameObject Effect = null;
    [SerializeField]
    private GameObject candyFloss;

    public Vector3 spawnPoint;
    private byte _flossLevel;
    public byte flossLevel
    {
        get
        {
            return _flossLevel;
        }
        set
        {
            _flossLevel = value;
            if (_flossLevel == 0)
            {
                spriteRenderer.sprite = SpriteFlossLevel[0];
                spriteRenderer.color = Color.gray;
                Effect.GetComponent<ParticleSystem>().Stop() ;
            }
            else if (_flossLevel <= maxFlossLevel)
            {
                spriteRenderer.sprite = SpriteFlossLevel[_flossLevel];
                spriteRenderer.color = Color.white;
                Effect.GetComponent<ParticleSystem>().Stop();
            }
            else if (_flossLevel+1 > maxFlossLevel)
            {
                spriteRenderer.sprite = SpriteFlossLevel[0];
                spriteRenderer.color = Color.white;
                Effect.GetComponent<ParticleSystem>().Play();
            }
        }
    }
    private SpriteRenderer spriteRenderer = null;
    public byte maxFlossLevel = 6;

    private void Awake()
    {
        spriteRenderer = objSprite.GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        flossLevel = 0;
    }
    private void OnMouseDown()
    {
        if(flossLevel + 1 > maxFlossLevel)
        {
            GameObject temp = Instantiate(candyFloss, spawnPoint, Quaternion.identity);
            SweetUnits temp2 = temp.GetComponent<SweetUnits>();
            temp2.LastPosition = temp.transform.position;
            flossLevel = 0;
        }
    }
}

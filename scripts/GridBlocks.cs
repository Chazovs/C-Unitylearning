using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBlocks : MonoBehaviour {

    public Sprite[] imgs;
    public int currentBlockIndex = 0;

    void ChangeImgs()
    {
        if(imgs.Length > currentBlockIndex)
        {
            GetComponent<SpriteRenderer>().sprite = imgs[currentBlockIndex];
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ChangeImgs();
    }
}

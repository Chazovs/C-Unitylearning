using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGrid : MonoBehaviour
{
    private void Start()
    {
        GridService service = new GridService();
        service.CreateGrid(transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBall : MonoBehaviour
{
    public float maxSpeed = 10f;
    public float minSpeed = 0.1f;
    public float xRange = 20;
    public float yRange = 2;
    public float zRange = 2;
    public float xStart = 30;
    public float yStart = 1;
    public float zStart = 40;

    Vector3 target;
    float alpha;
    float beta;
    float speed;

    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(minSpeed, maxSpeed);
        target = getRandom(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (goTo())
        {
            speed = Random.Range(minSpeed, maxSpeed);
            target = getRandom(); 
        }
    }
    
    Vector3 getRandom()
    {
        float x = Random.value * xRange + xStart;
        float y = Random.value * yRange + yStart;
        float z = Random.value * zRange + zStart;

        Vector3 rv = new Vector3(x,y,z);

        return rv;
    }

    bool goTo()
    {
        // Move our position a step closer to the target.
        float step =  speed * Time.deltaTime; // calculate distance to move
        
        transform.position = Vector3.MoveTowards(transform.position, target, step);

        // Check if the position of the cube and sphere are approximately equal.
        if (Vector3.Distance(transform.position, target) < 0.001f)
        {
            // Swap the position of the cylinder.
            return true;
        }

        return false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSpawner : MonoBehaviour
{
    public GameObject bomb;
    private float platformRaiseTime;
    private float platformLowerTime = 3f;
    private float timeSincePlatformMoved = 0;

    private bool platformIsRaised = false;
    private bool bombIsSpawned = false;

    private void Awake()
    {
        platformRaiseTime = 10f;
    }

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeSincePlatformMoved += Time.deltaTime;
        if (!platformIsRaised && timeSincePlatformMoved >= platformRaiseTime)
        {
            RaisePlatform();
        } else if (platformIsRaised && timeSincePlatformMoved >= platformLowerTime)
        {
            LowerPlatform();
        }
        if (platformIsRaised && !bombIsSpawned)
        {
            SpawnBomb();
        }
    }

    private void SpawnBomb()
    {
        bombIsSpawned = true;
        Instantiate(bomb, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity);
    }

    private void RaisePlatform() {
        if (platformIsRaised == true) return;
        Vector3 targetPosition = new Vector3(transform.position.x, 1.5f, transform.position.z);
        this.transform.position = Vector3.Lerp(transform.position, targetPosition, 10f * Time.deltaTime);
        Debug.Log("Raise Platform");
        if(Mathf.Approximately(transform.position.y, 1.5f))
        {
            platformIsRaised = true;
            timeSincePlatformMoved = 0;
        }
            
    }

    private void LowerPlatform()
    {
        if (platformIsRaised == false) return;
        Vector3 targetPosition = new Vector3(transform.position.x, -1.5f, transform.position.z);
        this.transform.position = Vector3.Lerp(transform.position, targetPosition, 10f * Time.deltaTime);
        Debug.Log("Lower Platform");
        if (Mathf.Approximately(transform.position.y, -1.5f))
        {
            platformIsRaised = false;
            timeSincePlatformMoved = 0;
        }
    }
}

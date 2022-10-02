using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BombSpawner : MonoBehaviour
{
    public GameObject bomb;
    private float platformRaiseTime;
    private float platformLowerTime = 1f;
    private float timeSincePlatformMoved = 0;

    private bool platformIsRaised = false;
    public bool bombIsSpawned = false;
    private float bombCountTime = 0;

    private PlayerController playerController;

    private void Awake()
    {
        platformRaiseTime = 4f;
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        if (playerController.GetIsDead()) return;
        float deltaTime = Time.deltaTime;
        timeSincePlatformMoved += deltaTime;
        bombCountTime += deltaTime;

        if (bombCountTime >= 7f && !bombIsSpawned) {
            SpawnBomb();
		}

        //if (!platformIsRaised && timeSincePlatformMoved >= platformRaiseTime)
        //{
        //    RaisePlatform();

        //} else if (platformIsRaised && timeSincePlatformMoved >= platformLowerTime)
        //{
        //    LowerPlatform();
        //}
    }

    private void SpawnBomb()
    {
        bombIsSpawned = true;
        Instantiate(bomb, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity);
    }

    //private void RaisePlatform() {
    //    if (platformIsRaised == true) return;
    //    Vector3 targetPosition = new Vector3(transform.position.x, 1.5f, transform.position.z);
    //    this.transform.position = Vector3.Lerp(transform.position, targetPosition, 10f * Time.deltaTime);
    //    if(Mathf.Approximately(transform.position.y, 1.5f))
    //    {
    //        platformIsRaised = true;
    //        timeSincePlatformMoved = 0;
    //    }   
    //}

    //private void LowerPlatform()
    //{
    //    if (platformIsRaised == false) return;
    //    if (!bombIsSpawned) {
    //        SpawnBomb();
    //    }
    //    Vector3 targetPosition = new Vector3(transform.position.x, -1.5f, transform.position.z);
    //    this.transform.position = Vector3.Lerp(transform.position, targetPosition, 10f * Time.deltaTime);
    //    if (Mathf.Approximately(transform.position.y, -1.5f))
    //    {
    //        platformIsRaised = false;
    //        bombIsSpawned = false;
    //        timeSincePlatformMoved = 0;
    //    }
    //}

    public float GetPlatformRaiseTime() {
        return platformRaiseTime;
    }

    public float GetPlatformLowerTime() {
        return platformLowerTime;
    }

    public float TimeSincePlatformMoved() {
        return timeSincePlatformMoved;
    }

    public void SetBombCountTime(float time) {
        this.bombCountTime = time;
    }

    public float GetBombCountTime() {
        return bombCountTime;
	}
}

﻿/*
 * Jaden Pleasants
 * Assignment 7
 * Handles spawning stuff
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerX : MonoBehaviour {
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;

    private readonly float spawnRangeX = 10;
    // set min spawn Z
    private readonly float spawnZMin = 15;
    // set max spawn Z
    private readonly float spawnZMax = 25;

    public int enemyCount;
    public int waveCount = 1;

    public GameObject player;

    public static float EXTRA_SPEED = 0;

    // Update is called once per frame
    void Update() {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (enemyCount == 0) {
            SpawnEnemyWave(waveCount);
        }
    }

    // Generate random spawn position for powerups and enemy balls
    Vector3 GenerateSpawnPosition() {
        float xPos = Random.Range(-spawnRangeX, spawnRangeX);
        float zPos = Random.Range(spawnZMin, spawnZMax);
        return new Vector3(xPos, 0, zPos);
    }


    void SpawnEnemyWave(int enemiesToSpawn) {
        // make powerups spawn at player end
        Vector3 powerupSpawnOffset = new Vector3(0, 0, -15);

        // If no powerups remain, spawn a powerup
        if (GameObject.FindGameObjectsWithTag("Powerup").Length == 0) {
            Instantiate(powerupPrefab, GenerateSpawnPosition() + powerupSpawnOffset, powerupPrefab.transform.rotation);
        }

        // Spawn number of enemy balls based on wave number
        for (int i = 0; i < enemiesToSpawn; i++) {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }

        waveCount++;

        // extra speed should just be the natural log of the current wave.
        EXTRA_SPEED = Mathf.Log(waveCount);

        // put player back at start
        ResetPlayerPosition();
    }

    // Move player back to position in front of own goal
    void ResetPlayerPosition() {
        player.transform.position = new Vector3(0, 1, -7);
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }
}

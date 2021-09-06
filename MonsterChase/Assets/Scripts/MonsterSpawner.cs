using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour {
  [SerializeField] private GameObject[] monterReference;
  [SerializeField] private Transform leftPos, rightPos;

  private GameObject spawnedMonster;
  private int randomIndex;
  private int randomSide;

  void Start() {
    StartCoroutine(SpawnMonsters());
  }

  IEnumerator SpawnMonsters() {
    yield return new WaitForSeconds(Random.Range(1, 5));
    randomIndex = Random.Range(0, monterReference.Length);
    randomSide = Random.Range(0, 2);

    spawnedMonster = Instantiate(monterReference[randomIndex]);
    if (randomSide == 0) {
      spawnedMonster.transform.position = leftPos.position;
      spawnedMonster.GetComponent<Monster>().speed = Random.Range(3, 7);
    } else {
      spawnedMonster.transform.position = rightPos.position;
      spawnedMonster.GetComponent<Monster>().speed = -Random.Range(3, 7);
      spawnedMonster.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
    }
    StartCoroutine(SpawnMonsters());
  }
}

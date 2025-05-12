using UnityEngine;

public class TitleCharacterSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] characterPrefabs; // 1から9
    [SerializeField] private RuntimeAnimatorController[] animationControllers; // アニメ9種
    [SerializeField] private int spawnCount = 12;
    [SerializeField] private Rect spawnArea;

    void Start()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            Vector2 pos = new Vector2(
                Random.Range(spawnArea.xMin, spawnArea.xMax),
                Random.Range(spawnArea.yMin, spawnArea.yMax)
            );

            int numberIndex = Random.Range(0, characterPrefabs.Length);
            GameObject obj = Instantiate(characterPrefabs[numberIndex], pos, Quaternion.identity, transform);

            int animIndex = Random.Range(0, animationControllers.Length);
            obj.GetComponent<Animator>().runtimeAnimatorController = animationControllers[animIndex];
        }
    }
}

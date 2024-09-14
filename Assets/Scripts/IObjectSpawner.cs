using UnityEngine;

public interface IObjectSpawner
{
    GameObject SpawnObject(GameObject prefab, Vector3 position, Quaternion rotation);
}

public class ObjectSpawner : IObjectSpawner
{
    public GameObject SpawnObject(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        return GameObject.Instantiate(prefab, position, rotation);
    }
}


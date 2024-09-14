using UnityEngine;

public interface IObjectScaler
{
    void ScaleObject(GameObject obj, Vector3 cellSize, float cellWidth, float cellLength, bool scaleUniformly);
}

public class ObjectScaler : IObjectScaler
{
    public void ScaleObject(GameObject obj, Vector3 cellSize, float cellWidth, float cellLength, bool scaleUniformly)
    {
        Vector3 originalScale = obj.transform.localScale;
        float scaleX = cellSize.x != 0 ? cellWidth / cellSize.x : 1;
        float scaleZ = cellSize.z != 0 ? cellLength / cellSize.z : 1;

        if (scaleUniformly)
        {
            float scaleUniform = Mathf.Min(scaleX, scaleZ);
            obj.transform.localScale = new Vector3(originalScale.x * scaleUniform, originalScale.y * scaleUniform, originalScale.z * scaleUniform);
        }
        else
        {
            float scaleY = Mathf.Min(scaleX, scaleZ);
            obj.transform.localScale = new Vector3(originalScale.x * scaleX, originalScale.y * scaleY, originalScale.z * scaleZ);
        }
    }
}


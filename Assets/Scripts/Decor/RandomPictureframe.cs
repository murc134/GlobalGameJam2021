using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPictureframe : MonoBehaviour
{
    public List<Texture2D> photos = new List<Texture2D>();
    public Vector2 randomScaleRange;

    // Start is called before the first frame update
    void Start()
    {
        var randomTexture = photos[Random.Range(0, photos.Count)];

        var renderer = GetComponent<Renderer>();
        var photoMat = renderer.materials[0];

        photoMat.mainTexture = randomTexture;

        float aspectRatio = (float) randomTexture.width / randomTexture.height;
        transform.localScale = Vector3.Scale(transform.localScale, new Vector3(aspectRatio, 1f, 1f));
        float randomScale = Random.Range(randomScaleRange.x, randomScaleRange.y);
        transform.localScale *= randomScale;
    }
}

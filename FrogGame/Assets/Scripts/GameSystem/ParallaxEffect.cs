using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    float textureUnitSizeY;
    Transform cameraTransform;
    IEnumerator startBackground;
    bool startGame = true;
    [SerializeField][Range(0f, 1f)]
    float parallaxSpeed;
    private void Start()
    {
        cameraTransform = Camera.main.transform;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        textureUnitSizeY = texture.height / sprite.pixelsPerUnit;

        startBackground = BackroundMovement();
        StartCoroutine(startBackground);
    }

// coroutine to start the background parallax effect
    private IEnumerator BackroundMovement()
    {
        while (startGame)
        {
            transform.position += new Vector3(0, parallaxSpeed, 0);

            if (cameraTransform.position.y + transform.position.y >= textureUnitSizeY)
            {
                float offsetPositionY = (cameraTransform.position.y - transform.position.y) % textureUnitSizeY;
                transform.position = new Vector3(cameraTransform.position.x, cameraTransform.position.y + offsetPositionY);
            }
            yield return null;
        }
        yield return null;
    }
}

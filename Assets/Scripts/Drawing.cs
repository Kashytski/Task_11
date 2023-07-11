using System;
using UnityEngine;

public class Drawing : MonoBehaviour
{
    public Action changingSpriteAction;

    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite sprite;
    [SerializeField] int radius = 0;

    private Vector2 zeroPixel = new Vector2();

    private Vector2 previousPixel = new Vector2();

    private void Awake()
    {
        CreateSprite();
    }
    private void Start()
    {
        SpriteChangeEnd();
    }

    private void CreateSprite()
    {
        Texture2D newTexture = new Texture2D(sprite.texture.width, sprite.texture.height, TextureFormat.RGBA32, false);

        newTexture.SetPixels(sprite.texture.GetPixels());
        newTexture.Apply();

        Sprite newSprite = Sprite.Create(newTexture, new Rect(0, 0, newTexture.width, newTexture.height), new Vector2(0.5f, 0.5f));

        spriteRenderer.sprite = newSprite;
        spriteRenderer.sprite.texture.Apply();

        zeroPixel = new Vector2(gameObject.transform.position.x - spriteRenderer.bounds.size.x, gameObject.transform.position.y - spriteRenderer.bounds.size.y) / 2;
    }

    public void Draw(Vector2 point)
    {
        Vector2 pixel = new Vector2(Mathf.Abs(zeroPixel.x - point.x), Mathf.Abs(zeroPixel.y - point.y)) * spriteRenderer.sprite.pixelsPerUnit;

        if (previousPixel != new Vector2())
        {
            Vector2 direction = (previousPixel - pixel).normalized;
            float distance = Vector2.Distance(pixel, previousPixel);

            for (float i = 0; i < distance; i += radius / 2)
            {
                Vector2 position = pixel + direction * i;
                DrawCircle(position);
            }
        }
        else
        {
            DrawCircle(pixel);
        }

        spriteRenderer.sprite.texture.Apply();
        previousPixel = pixel;
    }

    private void DrawCircle(Vector2 pixel)
    {
        for (int x = -radius; x < radius; x++)
            for (int y = -radius; y < radius; y++)
            {
                Vector2 position = new Vector2(pixel.x + x, pixel.y + y);
                if (Vector2.Distance(position, new Vector2(pixel.x, pixel.y)) > radius || spriteRenderer.sprite.texture.width < position.x || position.x < 0 || spriteRenderer.sprite.texture.height < position.y || position.y < 0)
                    continue;
                else
                    spriteRenderer.sprite.texture.SetPixel((int)position.x, (int)position.y, new Color(0, 0, 0, 0));
            }
    }

    public void SpriteChangeEnd()
    {
        previousPixel = new Vector2();
        changingSpriteAction?.Invoke();
    }
}

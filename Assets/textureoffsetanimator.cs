using UnityEngine;

public class TextureOffsetAnimator : MonoBehaviour
{
    public Material material;  // The material to animate
    public Vector2 startOffset = Vector2.zero;  // Initial texture offset
    public Vector2 endOffset = new Vector2(1f, 0f);  // Final texture offset
    public float duration = 5f;  // Duration for the animation
    public float amt=0;

    private void Awake()
    {
        // Set initial offset
        if (material != null)
        {
            material.mainTextureOffset = startOffset;
        }
    }
    void Update(){
        SetTextureOffset(amt);
    }
    

    // Method to set the texture offset at a given time (for animation)


    public void SetTextureOffset(float t)
    {
        if (material != null)
        {
            material.mainTextureOffset = Vector2.Lerp(startOffset, endOffset, t);
        }
    }
}

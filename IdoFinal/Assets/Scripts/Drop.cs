using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    [SerializeField] private SpriteRenderer rend;
    [SerializeField] private float height;
    [SerializeField] private float floatTime;

    public void SetUp(Sprite givenSprite)
    {
        rend.sprite = givenSprite;
    }

    private void OnEnable()
    {
        FloatUp();
    }

    [ContextMenu("tween")]
    public void FloatUp()
    {
        LeanTween.move(gameObject, transform.position + new Vector3(0, height, 0), floatTime).setEaseOutBounce();
        StartCoroutine(TurnOffCountdown());
    }

    private IEnumerator TurnOffCountdown()
    {
        yield return new WaitForSeconds(floatTime);
        gameObject.SetActive(false);
    }
}

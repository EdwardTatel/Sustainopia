using System.Collections;
using UnityEngine;

public class MedalAnimationScript : MonoBehaviour
{
    private float rotationSpeed = 50f; // Speed of rotation in degrees per second

    void Update()
    {

        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }

    public void MedalDown()
    {
        StartCoroutine(ShowMedal());
        
    }

    IEnumerator ShowMedal()
    {
        yield return new WaitForSeconds(1f);
        LeanTween.move(transform.parent.gameObject.GetComponent<RectTransform>(), new Vector3(0, -915f, 0), 1f).setEase(LeanTweenType.easeInOutQuad);
    }

    public void MedalUp()
    {
        LeanTween.move(transform.parent.gameObject.GetComponent<RectTransform>(), new Vector3(0, 236f, 0), 1f).setEase(LeanTweenType.easeInOutQuad);
    }

}
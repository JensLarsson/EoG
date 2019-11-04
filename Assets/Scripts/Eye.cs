using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye : MonoBehaviour
{
    public Transform target;
    public SpriteRenderer sprRen;
    public Sprite[] sprites = new Sprite[1];

    private void Start()
    {
        EventManager.Subscribe("EyeColour", ShiftColour);
        EventManager.Subscribe("ChangeEyeTarget", ChangeTarget);
    }

    private void OnDisable()
    {

        EventManager.UnSubscribe("EyeColour", ShiftColour);
        EventManager.UnSubscribe("ChangeEyeTarget", ShiftColour);
    }

    void Update()
    {
        if (target != null) transform.right = target.position - transform.position;
    }

    void ShiftColour(EventParameter eventParam)
    {
        StartCoroutine(ColourShift(eventParam.colourParam, eventParam.floatParam));
    }

    IEnumerator ColourShift(Color colour, float time)
    {
        float ElapsedTime = 0.0f;
        Color col = sprRen.color;
        while (ElapsedTime < time)
        {
            ElapsedTime += Time.deltaTime;
            sprRen.color = Color.Lerp(col, colour, (ElapsedTime / time));
            yield return null;
        }
    }
    public void ChangeTarget(Transform transform, float shiftTime)
    {
        StartCoroutine(ShiftTarget(transform, shiftTime));
    }
    public void ChangeTarget(GameObject gObject)
    {
        target = transform;
    }
    public void ChangeTarget(EventParameter eParam)
    {
        StartCoroutine(ShiftTarget(eParam.transformParam, eParam.floatParam));
    }
    //Right now there is no implementation for avoiding potential isses connected calls to this while the enumeration is alredy running
    IEnumerator ShiftTarget(Transform tran, float time)
    {
        float ElapsedTime = 0.0f;
        Vector2 startPos = target.transform.position;
        target = null;
        while (ElapsedTime < time)
        {
            ElapsedTime += Time.deltaTime;
            transform.right = Vector2.Lerp(startPos, tran.position, (ElapsedTime / time)) - new Vector2(transform.position.x, transform.position.y);
            yield return null;
        }
        target = tran;
    }
    //Unsure how this will be implemented. I'm thinking multiplying the size of the sprite array with a float which is clamped to 0 to 1, 
    //then parsing the return value to an int which would always be withing the array.
    //Then using the time float for controlling how fast an IEnumerator would itterate through the sprite array until it's on the correct end sprite.

    public void ChangeEyeOpenness(float openness, float time)
    {

    }
}

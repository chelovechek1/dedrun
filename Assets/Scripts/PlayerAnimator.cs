using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerAnimator : MonoBehaviour
{
    Sprite[] PlayerSprites;
    private Dictionary<string, Sprite> dictionary;

    [SerializeField] private BoxCollider2D layedCollider;
    [SerializeField] private BoxCollider2D normalCollider;

    private SpriteRenderer DedSprite;
    private float rotation;

    private int state = 1;
    private bool layed;
    public bool canRun = true;
    private float bubilda;

    private int State
    {
        get { return state; }
        set
        {
            if (value < 1) value = 1;
            if (value > 9) value = 1;
            state = value;
        }
    }
    private void Start()
    {
        DedSprite = GetComponent<SpriteRenderer>();
        PlayerSprites = Resources.LoadAll<Sprite>("MAIN");
        dictionary = PlayerSprites.ToDictionary(k => k.name, v => v);

        StartCoroutine(Run());
    }
    void Update()
    {
        rotation = Input.GetAxis("Horizontal");
        if (rotation < 0) DedSprite.flipX = true;
        else if (rotation > 0) DedSprite.flipX = false;

        if (Input.GetKeyDown(KeyCode.LeftControl) && bubilda > 0.9f)
        {
            bubilda = 0;
            if (!layed)
            {
                StartCoroutine(ToLay());
            }
            else
            {
                StartCoroutine(ToLay());
            }

        }
        bubilda += Time.deltaTime;
    }

    IEnumerator ToLay()
    {
        if (!layed)
        {
            normalCollider.isTrigger = true;
            layedCollider.isTrigger = false;
            state = 1;
            canRun = false;
            while (state != 4)
            {
                state++;
                DedSprite.sprite = dictionary["tolay" + State];
                yield return new WaitForSeconds(0.20f);
            }
            canRun = true;
            layed = true;
        }
        else
        {
            layedCollider.isTrigger = true;
            normalCollider.isTrigger = false;
            canRun = false;
            state = 4;
            while (state != 1)
            {
                state--;
                DedSprite.sprite = dictionary["tolay" + State];
                yield return new WaitForSeconds(0.20f);
            }
            layed = false;
            canRun = true;
        }
        yield break;
    }

    IEnumerator Run()
    {

        while (true)
        {
            if (canRun)
            {
                if (!layed)
                {
                    if (Input.GetAxis("Horizontal") != 0)
                    {
                        State++;
                        DedSprite.sprite = dictionary["run" + State];
                        yield return new WaitForSeconds(0.10f);
                    }
                    else
                    {
                        DedSprite.sprite = dictionary["idle"];
                        yield return null;
                    }
                }
                else
                {

                    if (Input.GetAxis("Horizontal") != 0)
                    {
                        State++;
                        DedSprite.sprite = dictionary["lay" + State];
                        yield return new WaitForSeconds(0.10f);
                    }
                    if (State == 7) State = 1;

                    else yield return null;
                }
            }
            else yield return null;
        }
    }
}
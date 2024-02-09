using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Puzzle : MonoBehaviour
{
    public Slider timer;
    public List<Button> boxsPic;
    public List<Sprite> spritesPic;
    public Image empty;
    public Sprite emptySprite;
    public List<int> valueOfPic;
    string[] tages = {"box (0)", "box (1)", "box (2)", "box (3)", "box (4)", "box (5)", "box (6)", "box (7)", "empty" };
    public List<string> tagesAdd = new List<string>();
    bool isStart = false;
    float time = 0f;
    public Text timePlay;
    public Button startButton;
    public GameObject tapPlay;
    public GameObject Panel;
    public Text Alert;

    public AudioSource playGameAudio, mouseClick;

    // Start is called before the first frame update
    void Start()
    {
        playGameAudio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (isStart)
        {
            time += Time.deltaTime;
            timePlay.text = Mathf.Round(time).ToString();
            timer.value = time;
            if(time >= 90) 
            {

                if (boxsPic[0].image.sprite == spritesPic[1] &&
            boxsPic[1].image.sprite == spritesPic[2] &&
            boxsPic[2].image.sprite == spritesPic[3] &&
            boxsPic[3].image.sprite == spritesPic[4] &&
            boxsPic[4].image.sprite == spritesPic[5] &&
            boxsPic[5].image.sprite == spritesPic[6] &&
            boxsPic[6].image.sprite == spritesPic[7] &&
            boxsPic[7].image.sprite == spritesPic[8] &&
            boxsPic[8].image.sprite == spritesPic[0]
        )
                {

                }
                else
                {
                    StartCoroutine(WaitWin("FAIL", Color.red));
                    StartCoroutine(StartUpGame());
                }
                    
            }
        }
    }
    IEnumerator StartUpGame()
    {
        EnableBox(false);
        tagesAdd.Clear();
        
        valueOfPic.Clear();
        isStart = false;
        yield return new WaitForSeconds(1.5f);
        startButton.enabled = true;
        // isStart = true;
        time = 0f;
        timer.value = time;
        
        tapPlay.SetActive(true);
    }

    void ChangeAllBoxToEmptySprite()
    {
        for(int i = 0; i < boxsPic.Count; i++)
        {
            boxsPic[i].image.sprite = emptySprite;
        }
    }

    void EnableBox(bool butonIs)
    {
        for (int i = 0; i<boxsPic.Count; i++)
        {
            boxsPic[i].enabled = butonIs;
        }
    }

    public void StartButton()
    {
        Panel.SetActive(false);
        DestroyComponentsOnObject();
        EnableBox(true);
        isStart = true;
        SuffleOfPic();
        tapPlay.SetActive(false);
    }

    void DestroyComponentsOnObject()
    {
        for(int i = 0; i<boxsPic.Count;i++)
        {
            Destroy(boxsPic[i].GetComponent<Rigidbody2D>());
            Destroy(boxsPic[i].GetComponent<EmptyDetect>());
        }
    }

    void SuffleOfPic()
    {
        for(int i = 0; i < 9; i++)
        {
            int value = Random.Range(0, 9);
            while(valueOfPic.Contains(value)) value = Random.Range(0, 9);
            valueOfPic.Add(value);
        }
        GetPicOn();
    }
    
    void GetPicOn()
    {
        for (int i = 0; i < valueOfPic.Count; i++)
        {
            for(int j = 0; j < spritesPic.Count; j++)
            {
                if (valueOfPic[i] == j)
                {
                    boxsPic[i].image.sprite = spritesPic[j];
                    boxsPic[i].tag = tages[j];
                    tagesAdd.Add(tages[j]);
                }
            }
        }
        // Add Rigidbody2D on empty Boxs
        AddRigidbody2DOnEmptyBox();

    }

    void AddRigidbody2DOnEmptyBox()
    {
        for (int j = 0; j < boxsPic.Count; j++)
        {
            if (boxsPic[j].tag.Equals("empty"))
            {
                boxsPic[j].gameObject.AddComponent<Rigidbody2D>();
                boxsPic[j].gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
                boxsPic[j].gameObject.GetComponent<Rigidbody2D>().freezeRotation = true;
                boxsPic[j].enabled = false;
            }
            else
            {
                boxsPic[j].gameObject.AddComponent<EmptyDetect>();
                boxsPic[j].enabled = false;
            }
        }
    }

    public void BackUpEmptyBox(int box, int boxs)
    {
        for(int i = 0; i < boxsPic.Count; i++)
        {
            if (i == box)
            {
                Destroy(boxsPic[box].GetComponent<Rigidbody2D>());
                boxsPic[box].enabled = false;
                boxsPic[box].AddComponent<Rigidbody2D>();
                boxsPic[box].GetComponent<Rigidbody2D>().gravityScale = 0;
                boxsPic[box].GetComponent<Rigidbody2D>().freezeRotation = true;
                Destroy(boxsPic[box].GetComponent<EmptyDetect>());

            }
        }

        for(int j = 0; j < boxsPic.Count; j++)
        {
            if (j == boxs)
            {
                Destroy(boxsPic[boxs].GetComponent<Rigidbody2D>());
                boxsPic[boxs].gameObject.AddComponent<EmptyDetect>();
            }

        }
    }

    IEnumerator WaitWin(string text, Color color)
    {   
        Panel.SetActive(true);
        Alert.text = text;
        Alert.GetComponent<Text>().color = color;
        yield return new WaitForSeconds(1.5f);
        ChangeAllBoxToEmptySprite();
        DestroyComponentsOnObject();
        time = 90f;
    }

    void CheckSwapBoxs()
    {
        if ( boxsPic[0].image.sprite == spritesPic[1] &&
             boxsPic[1].image.sprite == spritesPic[2] &&
             boxsPic[2].image.sprite == spritesPic[3] &&
             boxsPic[3].image.sprite == spritesPic[4] &&
             boxsPic[4].image.sprite == spritesPic[5] &&
             boxsPic[5].image.sprite == spritesPic[6] &&
             boxsPic[6].image.sprite == spritesPic[7] &&
             boxsPic[7].image.sprite == spritesPic[8] &&
             boxsPic[8].image.sprite == spritesPic[0]
         )
        {
            StartCoroutine(WaitWin("SUCCESS",Color.green));
            StartCoroutine(StartUpGame());
        }
    }

    public void Onclicks(int n)
    {
        mouseClick.Play();
        string tage, temp;
        Sprite spriteTemp;
        tage = boxsPic[n].tag;
       // print(tage);
        for (int j = 0; j < boxsPic.Count; j++)
        {
            if (boxsPic[j].tag.Equals(tage)) // tag = j => k: empty-tag
            {
                for (int k = 0; k < boxsPic.Count; k++)
                {
                    if (boxsPic[k].tag.Equals("empty")) // empty: tag = k => j: tag
                    {
                        spriteTemp = boxsPic[j].image.sprite;
                        boxsPic[j].image.sprite = boxsPic[k].image.sprite;
                        boxsPic[k].image.sprite = spriteTemp;

                        temp = boxsPic[j].tag;
                        boxsPic[j].tag = boxsPic[k].tag;
                        boxsPic[k].tag = temp;

                        //print("k: " + boxsPic[k].tag);
                        //print("j: " + boxsPic[j].tag);
                        //print("n: " + n);
                        ClearDeteted();
                        BackUpEmptyBox(j,k);
                         CheckSwapBoxs();
                        temp = "";
                        tage = "";
                        spriteTemp = null;

                        break;
                    }
                }
            }
        }
       
    }

    void ClearDeteted()
    {
        for(int j = 0;j < boxsPic.Count; j++)
        {
            boxsPic[j].enabled = false;
        }
    }
    public void BackHome()
    {
        SceneManager.LoadScene("Home");
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Onclicks : MonoBehaviour
{
    public void Onclick()
    {
        string tage, temp;
        Sprite spriteTemp;
        tage = this.gameObject.tag;
        for (int j = 0; j < FindObjectOfType<Puzzle>().boxsPic.Count; j++)
        {
            if (FindObjectOfType<Puzzle>().boxsPic[j].tag.Equals(tage))
            {
                for (int k = 0; k < FindObjectOfType<Puzzle>().boxsPic.Count; k++)
                {
                    if (FindObjectOfType<Puzzle>().boxsPic[k].tag.Equals("empty"))
                    {
                        spriteTemp = FindObjectOfType<Puzzle>().boxsPic[k].image.sprite;
                        FindObjectOfType<Puzzle>().boxsPic[j].image.sprite = FindObjectOfType<Puzzle>().boxsPic[k].image.sprite;
                        FindObjectOfType<Puzzle>().boxsPic[k].image.sprite = spriteTemp;

                        temp = FindObjectOfType<Puzzle>().boxsPic[k].tag;
                        FindObjectOfType<Puzzle>(). boxsPic[j].tag = FindObjectOfType<Puzzle>().boxsPic[k].tag;
                        FindObjectOfType<Puzzle>().boxsPic[k].tag = temp;

                        
                    }
                }
            }
        }
    }

}

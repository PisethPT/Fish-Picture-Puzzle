using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmptyDetect : MonoBehaviour
{
    string tages;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("empty"))
        {
            tages = this.tag;

            for (int j = 0; j < FindObjectOfType<Puzzle>().boxsPic.Count; j++)
            {
                if (FindObjectOfType<Puzzle>().boxsPic[j].tag.Equals(tages))
                {
                    FindObjectOfType<Puzzle>().boxsPic[j].enabled = true;
                    //print("Detect: " + FindObjectOfType<Puzzle>().boxsPic[j].tag);
                }
            }
        }
        else
        {
            tages = this.tag;

            for (int j = 0; j < FindObjectOfType<Puzzle>().boxsPic.Count; j++)
            {
                if (FindObjectOfType<Puzzle>().boxsPic[j].tag.Equals(tages))
                {
                    FindObjectOfType<Puzzle>().boxsPic[j].enabled = false;
                    print("Undetect: " + FindObjectOfType<Puzzle>().boxsPic[j].tag);
                }
            }
        }
    }


}

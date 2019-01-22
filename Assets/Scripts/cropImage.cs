using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEditor;
using System.IO;

public class cropImage : MonoBehaviour
{
    private Texture2D originalImage;
    public int shuffleTimes;
    private int blank_i;
    private int blank_j;
    public float upleft_x;
    public float upleft_y;
    public GameObject control;

    void Start()
    {
        control = GameObject.Find("Control");
    }

    public void crop()
    {
        //string path = EditorUtility.OpenFilePanel("Select a picture.", "", "jpg");
        //if (path.Length != 0)
        //{
        //    byte[] fileContent = File.ReadAllBytes(path);
        //    originalImage = new Texture2D(2, 2);
        //    originalImage.LoadImage(fileContent);
        //}

        originalImage = Resources.Load<Texture2D>("Pictures/" + GetComponent<Image>().sprite.texture.name);

        string transparence = GameObject.Find("Control").GetComponent<swapSprites>().blank;
        GameObject trans = GameObject.Find(transparence);
        if (trans)
        {
            trans.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }

        // Crop image into 9 pieces.
        int shorter = Mathf.Min(originalImage.height, originalImage.width) / 3;
        int white = Random.Range(1, 10);
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                int num = j * 3 + i + 1;
                if (num == white)
                {
                    GameObject blank = GameObject.Find("Cropped" + " (" + num + ")");
                    blank.GetComponent<Image>().sprite = null;
                    blank.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0);
                    GameObject.Find("Control").GetComponent<swapSprites>().blank = blank.name;
                    blank_i = j;
                    blank_j = i;
                }
                else
                {
                    Color[] c = originalImage.GetPixels(0 + j * shorter, shorter * 2 - i * shorter, shorter, shorter);
                    Texture2D croppedIMage = new Texture2D(shorter, shorter);
                    croppedIMage.SetPixels(c);
                    croppedIMage.Apply();
                    GameObject.Find("Cropped" + " (" + num + ")").GetComponent<Image>().sprite = Sprite.Create(croppedIMage, new Rect(0, 0, croppedIMage.width, croppedIMage.height), new Vector2(0.5f, 0.5f), 100.0f);
                }
            }
        }

        // Shuffle
        int[,] matrix = new int[3, 3];
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                matrix[i, j] = i * 3 + j + 1;
            }
        }
        print(blank_i.ToString() + ' ' + blank_j.ToString());
        for (int i = 0; i < shuffleTimes; i++)
        {
            int direction = Random.Range(1, 5);
            int last = 0;
            switch (direction)
            {
                case 1:
                    if (blank_j - 1 >= 0 && last != 2)
                    {
                        int temp = matrix[blank_i, blank_j];
                        matrix[blank_i, blank_j] = matrix[blank_i, blank_j - 1];
                        matrix[blank_i, blank_j - 1] = temp;
                        blank_j--;
                        last = 1;
                    }
                    break;
                case 2:
                    if (blank_j + 1 < 3 && last != 1)
                    {
                        int temp = matrix[blank_i, blank_j];
                        matrix[blank_i, blank_j] = matrix[blank_i, blank_j + 1];
                        matrix[blank_i, blank_j + 1] = temp;
                        blank_j++;
                        last = 2;
                    }
                    break;
                case 3:
                    if (blank_i - 1 >= 0 && last != 4)
                    {
                        int temp = matrix[blank_i, blank_j];
                        matrix[blank_i, blank_j] = matrix[blank_i - 1, blank_j];
                        matrix[blank_i - 1, blank_j] = temp;
                        blank_i--;
                        last = 3;
                    }
                    break;
                case 4:
                    if (blank_i + 1 < 3 && last != 3)
                    {
                        int temp = matrix[blank_i, blank_j];
                        matrix[blank_i, blank_j] = matrix[blank_i + 1, blank_j];
                        matrix[blank_i + 1, blank_j] = temp;
                        blank_i++;
                        last = 4;
                    }
                    break;
                default:
                    break;
            }
        }
        print(blank_i.ToString() + ' ' + blank_j.ToString());
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                GameObject.Find("Cropped" + " (" + matrix[i, j] + ")").GetComponent<RectTransform>().localPosition = new Vector3(upleft_x + 307 * i, upleft_y - 307 * j, 0);
            }
        }

        GameObject previousimage = control.GetComponent<previousImage>().previousimage;
        if (previousimage)
        {
            previousimage.GetComponent<Outline>().enabled = false;
        }
        GetComponent<Outline>().enabled = true;
        control.GetComponent<previousImage>().previousimage = GameObject.Find(this.name);

        control.GetComponent<restoration>().playing = true;
    }
}

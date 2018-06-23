using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;




public class GeneratorFromFile : MonoBehaviour {


    private FileInfo file;
    private StreamReader reader;

    public GameObject cubered;
    public GameObject cubeteal;
    public GameObject cubegreen;
    public float size = 1.0f;
    public float numberOfRows = 5;
    public bool alignedToY;
    // Use this for initialization
    void Start () {
        file = new FileInfo("Assets/Map.txt");
        reader = file.OpenText();
        build();
    }

    // Update is called once per frame
    void Update () {

    }


    public void build() {
        for (int i = 0; i < numberOfRows; ++i) {
            float height = (numberOfRows+(numberOfRows*0.5f))- i*size;
            string text = reader.ReadLine();
            for (int j = 0; j < text.Length; ++j) {
                GameObject obj = null;
                switch (text[j]) {
                    case '1':
                        obj = cubered;
                        break;
                    case '2':
                        obj = cubeteal;
                        break;
                    case '3':
                        obj = cubegreen;
                        break;
                }
                if (obj)
                {
                    if (alignedToY) GameObject.Instantiate(obj, new Vector3((j * size + (size * 0.5f)), height, 0.0f), Quaternion.identity);
                    else GameObject.Instantiate(obj, new Vector3((j * size + (size * 0.5f)), 0.0f, height), Quaternion.identity);
                }
            }
        }
    }
}

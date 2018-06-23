using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GeneratorFromFileWithCodification : MonoBehaviour {


    private FileInfo file;
    private StreamReader reader;

    public float size = 1.0f;
    public float numberOfRows = 5;
    public bool alignedToY;
    public GameObject[] mapAssets;
    // Use this for initialization
    void Start()
    {
        file = new FileInfo("Assets/MapCodified.txt");
        reader = file.OpenText();
        build();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void build()
    {
        for (int i = 0; i < numberOfRows; ++i)
        {
            float height = (numberOfRows + (numberOfRows * 0.5f)) - i * size;
            string text = reader.ReadLine();
            string[] processedText = text.Split(',');
            for (int j = 0; j < processedText.Length; ++j)
            {
                GameObject obj = null;
                int mapId = int.Parse(processedText[j]);
                int rotation = mapId % 10;
                int index = (mapId / 10) -1;
                //Debug.Log("index = "+index+" : mapid = "+mapId);
                if (index >= 0 && index < mapAssets.Length) {
                    obj = mapAssets[index];
                }
                Quaternion rotationQuaternion = Quaternion.identity;
                Debug.Log(rotation);
                switch (rotation) {
                    case 0:
                        rotationQuaternion = new Quaternion(0.0f, 0.0f, 0.0f,1);
                        break;
                    case 1:
                        rotationQuaternion = new Quaternion(0.0f, 90.0f, 0.0f, 1);
                        break;
                    case 2:
                        rotationQuaternion = new Quaternion(0.0f, 180.0f, 0.0f, 1);
                        break;
                    case 3:
                        rotationQuaternion = new Quaternion(0.0f, 270.0f, 0.0f, 1);
                        break;
                    case 4:
                        rotationQuaternion = new Quaternion(0.0f, 0.0f, 0.0f, 1);
                        break;
                    case 5:
                        rotationQuaternion = new Quaternion(0.0f, 0.0f, 90.0f, 1);
                        break;
                    case 6:
                        rotationQuaternion = new Quaternion(0.0f, 0.0f, 180.0f, 1);
                        break;
                    case 7:
                        rotationQuaternion = new Quaternion(0.0f, 0.0f, 270.0f, 1);
                        break;
                }
                if (obj)
                {
                    if (alignedToY) GameObject.Instantiate(obj, new Vector3((j * size + (size * 0.5f)), height, 0.0f), rotationQuaternion * obj.transform.rotation);
                    else GameObject.Instantiate(obj, new Vector3((j * size + (size * 0.5f)), 0.0f, height), rotationQuaternion * obj.transform.rotation);
                }
            }
        }
    }
}

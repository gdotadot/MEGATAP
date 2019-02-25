using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text;
using System.IO;

public class GenerateFaceFromText {

    // CHANGE TO USE
    // the name of the platform prefab you wish to use
    private static string platform = "1x1Platform";
    private static string textfile1 = "Sample_File_1";

    [MenuItem("GameObject/Generate Face", false, 12)]
    private static void Create()
    {
        Debug.Log("Generating Faces");
        Load(textfile1);
    }


    private static bool Load(string file)
    {
        string temp = "XXX";
        char temp1 = (char)temp[0];
        Debug.Log("is it " + temp1.Equals('X'));
        // Handle any problems that might arise when reading the text
        try
        {
            string line;
            int lineNumber = 0; // the line we are on to get height correctly
            // Create a new StreamReader, tell it which file to read and what encoding the file
            // was saved as
            StreamReader theReader = new StreamReader("Assets/TextLevels/" + textfile1 + ".txt", Encoding.Default);
            using (theReader)
            {
                // While there's lines left in the text file, do this:
                do
                {
                    line = theReader.ReadLine();
                    Debug.Log("reading line");
                    if (line != null)
                    {
                        lineNumber++;
                        for(int i = 0; i < 40; i++)
                        {
                            if(line[i].Equals("X"))
                            {
                                GameObject spawnedPlatform = PrefabUtility.InstantiatePrefab(GameObject.Find(platform)) as GameObject;
                                spawnedPlatform.transform.position = new Vector3(i * 2, lineNumber * 2, 0);
                                Debug.Log("spawned platform");
                            }
                        }
                        
                    }
                }
                while (line != null);
                // Done reading, close the reader and return true to broadcast success    
                theReader.Close();
                return true;
            }
        }
        // If anything broke in the try block, we throw an exception with information
        // on what didn't work
        catch (System.Exception e)
        {
            Debug.LogException(e);
            return false;
        }
    }
}


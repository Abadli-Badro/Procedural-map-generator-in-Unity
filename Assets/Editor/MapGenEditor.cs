using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(MapGenerator))]
public class MapGenEditor : Editor
{
    public override void OnInspectorGUI()
    {
        MapGenerator mapGenerator = (MapGenerator)target;

        //If anything changes, the map will be re-generated  
        if (DrawDefaultInspector()) {                              
            if(mapGenerator.autoGen) mapGenerator.generateMap();
        }

        //Adding a button to the inspector that generates the map when pressed
        if (GUILayout.Button ("Create Map")) mapGenerator.generateMap(); 

    }

}

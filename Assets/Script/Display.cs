using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;

public class Display : MonoBehaviour
{
    [SerializeField] private TMP_Text theScript;
    [SerializeField] private TMP_Text theScare;

    // Start is called before the first frame update
    void Start()
    {
        string json = File.ReadAllText("Assets/Script/JSON.json");
        Root root = JsonUtility.FromJson<Root>(json);
        string details = $"The Name = {root.metadata.name}\n" +
                             $"The ID = {root.metadata.id}\n" +
                             $"Private = {root.metadata.@private}\n" +
                             $"Created At = {root.metadata.createdAt}\n" +
                             $"Player Name = {root.record.playerName}\n" +
                             $"Level = {root.record.level}\n" +
                             $"Health = {root.record.health}\n" +
                             $"Position = (x: {root.record.position.x}, y: {root.record.position.y}, z: {root.record.position.z})\n" +
                             "Inventory:\n";

            foreach (var item in root.record.inventory)
            {
                details += $"- Item Name: {item.itemName}, Quantity: {item.quantity}, Weight: {item.weight}\n";
            }
            
            theScript.text = details;

    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(LoadSceneAfterDelay(3f));
    }

    [Serializable]
    public class Inventory
    {
        public string itemName;
        public int quantity;
        public double weight;
    }

    [Serializable]
    public class Metadata
    {
        public string id;
        public bool @private;
        public DateTime createdAt;
        public string name;
    }

    [Serializable]
    public class Position
    {
        public int x;
        public int y;
        public int z;
    }

    [Serializable]
    public class Record
    {
        public string playerName;
        public int level;
        public double health;
        public Position position;
        public List<Inventory> inventory;
    }

    [Serializable]
    public class Root
    {
        public Record record;
        public Metadata metadata;
    }


    IEnumerator LoadSceneAfterDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        theScare.text = "YOU HAVE BEEN EXPOSED!!  RUN";


    }
}

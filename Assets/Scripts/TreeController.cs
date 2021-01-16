using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class TreeController : MonoBehaviour
{
    public int initialNumberOfTrunksRequired = 5;
    private float lastYLocation;
    public List<GameObject> availableTrunks;
    private Dictionary<Trunk, GameObject> availableTrunkSideDictionary = new Dictionary<Trunk, GameObject>();

    private Queue<GameObject> currentTrunks = new Queue<GameObject>();

    [SerializeField] private WoodcutterDatabase database;
    [SerializeField] private CharacterSide characterSide;
    [SerializeField] private SharedBool isDead;
    [SerializeField] private SharedInt score;
    [SerializeField] private AudioManager audioManager;
    private TreeData treeData;
    private void OnCharacterMove()
    {
        audioManager.PlaySound("WoodChop");
        
        score.Value++;
        var currentTrunk = currentTrunks.Peek().GetComponent<Trunk>();
        
        currentTrunks.Dequeue();
        currentTrunk.FlyAwayAndDestroy(characterSide.CurrentSide);
        UpdateTrunkPositions();
        CreateTrunks(1);
        
        switch (characterSide.CurrentSide)
        {
            case CharacterSide.Side.Left:
                if (currentTrunks.Peek().GetComponent<Trunk>().branchLocation == Trunk.BranchLocation.Left)
                {
                    Debug.Log("Killing Player Left");
                    isDead.Value = true;
                }
                break;
            case CharacterSide.Side.Right:
                if (currentTrunks.Peek().GetComponent<Trunk>().branchLocation == Trunk.BranchLocation.Right)
                {
                    Debug.Log("Killing Player Right");
                    isDead.Value = true;
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void UpdateTrunkPositions()
    {
        lastYLocation = 0;

        foreach (var trunk in currentTrunks)
        {
            trunk.transform.localPosition = new Vector3(0, lastYLocation, 0);
            CalculateNextYLocation(trunk);
        }
    }
    
    private void Start()
    {
        treeData = database.ReturnCurrentAppearance().treeData;
        
        availableTrunks.Clear();

        availableTrunks.Add(treeData.LeftTrunk);
        availableTrunks.Add(treeData.RightTrunk);
        availableTrunks.Add(treeData.NeutralTrunk);
        
        foreach (var trunk in availableTrunks)
        {
            availableTrunkSideDictionary.Add(trunk.GetComponent<Trunk>(), trunk);
        }

        lastYLocation = transform.position.y;

        InitializeTrunk();
        characterSide.valueChangeEvent.AddListener(OnCharacterMove);
        score.Value = 0;
    }

    private void OnDestroy()
    {
        characterSide.valueChangeEvent.RemoveListener(OnCharacterMove);
    }

    private void InitializeTrunk()
    {
        var obj = Instantiate(availableTrunks.First(trunk =>
            trunk.GetComponent<Trunk>().branchLocation == Trunk.BranchLocation.None), transform.position, Quaternion.identity, transform);
        CalculateNextYLocation(obj);

        currentTrunks.Enqueue(obj);
        CreateTrunks(initialNumberOfTrunksRequired - 1);
    }

    private void CalculateNextYLocation(GameObject obj)
    {
        var spriteRenderer = obj.GetComponent<SpriteRenderer>();
        lastYLocation += obj.transform.localScale.y * spriteRenderer.bounds.size.y * 2;
    }
    
    private void CreateTrunks(int trunkAmount)
    {
        for (int i = 0; i < trunkAmount; i++)
        {
            var number = Random.Range(0, 10);
            if (number >=7) //Spawn Left
            {
                if (currentTrunks.Last().GetComponent<Trunk>().branchLocation == Trunk.BranchLocation.Right) //If previous was a right leaning tree, give player a buffer to switch sides
                {
                    var buffer = Instantiate(availableTrunks.First(trunk =>
                            trunk.GetComponent<Trunk>().branchLocation == Trunk.BranchLocation.None), 
                        new Vector3(0f, lastYLocation, 0f), 
                        Quaternion.identity, 
                        transform);
                    
                    CalculateNextYLocation(buffer);
                    currentTrunks.Enqueue(buffer);
                }
                else
                {
                    var obj = Instantiate(availableTrunks.First(trunk =>
                            trunk.GetComponent<Trunk>().branchLocation == Trunk.BranchLocation.Left),
                        new Vector3(0f, lastYLocation, 0f),
                        Quaternion.identity,
                        transform);

                    CalculateNextYLocation(obj);
                    currentTrunks.Enqueue(obj);
                }
            }
            else if (number >= 2) ////Spawn Right
            {
                if (currentTrunks.Last().GetComponent<Trunk>().branchLocation == Trunk.BranchLocation.Left) //If previous was a left leaning tree, give player a buffer to switch sides
                {
                    var buffer = Instantiate(availableTrunks.First(trunk =>
                            trunk.GetComponent<Trunk>().branchLocation == Trunk.BranchLocation.None),
                        new Vector3(0f, lastYLocation, 0f),
                        Quaternion.identity,
                        transform);

                    CalculateNextYLocation(buffer);
                    currentTrunks.Enqueue(buffer);
                }
                else
                {
                    var obj = Instantiate(availableTrunks.First(trunk =>
                            trunk.GetComponent<Trunk>().branchLocation == Trunk.BranchLocation.Right),
                        new Vector3(0f, lastYLocation, 0f),
                        Quaternion.identity,
                        transform);

                    CalculateNextYLocation(obj);
                    currentTrunks.Enqueue(obj);
                }
            }
            else //spawn Empty
            {
                var buffer = Instantiate(availableTrunks.First(trunk =>
                        trunk.GetComponent<Trunk>().branchLocation == Trunk.BranchLocation.None), 
                    new Vector3(0f, lastYLocation, 0f), 
                    Quaternion.identity, 
                    transform);
                    
                CalculateNextYLocation(buffer);
                currentTrunks.Enqueue(buffer);
            }
        }
    }
}

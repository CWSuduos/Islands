using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(SpriteRenderer))]
public class WaterProperty : MonoBehaviour
{
    [SerializeField] private bool isWaterSource;
    [SerializeField] private Sprite wetSprite;
    [SerializeField] private Sprite drySprite;
    [SerializeField] private bool canWetRight, canWetLeft, canWetUp, canWetDown;
    [SerializeField] private int id;
    [SerializeField] private bool objectToWin;
    public delegate void OnObjectWetDelegate();
    public event OnObjectWetDelegate OnObjectWet;
    public bool ObjectToWin { get { return objectToWin; } }
    public int Id { get { return id; } }
    private LvlWinManager lvlWinManager;
    private bool isWet;
    private SpriteRenderer spriteRenderer;
    private HashSet<WaterProperty> touchingObjects = new HashSet<WaterProperty>();

    private static int idCounter = 0;

    private void Awake()
    {
        if (id == 0)
        {
            id = GenerateUniqueId();
        }

        if (!TryGetComponent(out spriteRenderer))
        {
            Debug.LogError($"Missing SpriteRenderer on {gameObject.name}");
            enabled = false;
            return;
        }

        if (isWaterSource)
        {
            SetWetState(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out WaterProperty otherWater))
        {
            touchingObjects.Add(otherWater);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out WaterProperty otherWater))
        {
            touchingObjects.Remove(otherWater);
        }
    }

    private void Update()
    {
        UpdateWetState();
    }

    private void UpdateWetState()
    {
        if (isWaterSource) return;

        bool isConnectedToWaterSource = IsConnectedToWaterSource();
        bool shouldBeWet = false;

        if (isConnectedToWaterSource)
        {
            shouldBeWet = true;
        }
        else
        {
            foreach (var touchingObject in touchingObjects)
            {
                if (touchingObject.IsWet && CanTransferWetness(touchingObject))
                {
                    shouldBeWet = true;
                    break;
                }
            }
        }

        SetWetState(shouldBeWet);
    }

    private bool IsConnectedToWaterSource()
    {
        HashSet<WaterProperty> visitedObjects = new HashSet<WaterProperty>();
        return IsConnectedToWaterSourceRecursive(this, visitedObjects);
    }

    private bool IsConnectedToWaterSourceRecursive(WaterProperty currentObject, HashSet<WaterProperty> visitedObjects)
    {
        if (currentObject.IsWaterSource)
        {
            return true;
        }

        visitedObjects.Add(currentObject);

        foreach (var touchingObject in currentObject.touchingObjects)
        {
            if (!visitedObjects.Contains(touchingObject) && CanTransferWetness(touchingObject))
            {
                if (IsConnectedToWaterSourceRecursive(touchingObject, visitedObjects))
                {
                    return true;
                }
            }
        }

        return false;
    }

    

    private void Start()
    {
        lvlWinManager = GameObject.FindObjectOfType<LvlWinManager>();
    }

    private void SetWetState(bool state)
    {
        if (isWet == state) return;

        isWet = state;
        UpdateSprite();

        WaterProperty[] touchingObjectsArray = touchingObjects.ToArray();
        foreach (var touchingObject in touchingObjectsArray)
        {
            touchingObject.UpdateWetState();
        }

        if (state)
        {
            Debug.Log($"Объект с ID {id} стал мокрым");

            if (objectToWin)
            {
                Debug.Log("Объект слежения стал мокрым");
                lvlWinManager.WinLevel();
            }
        }
    }

    private void UpdateSprite()
    {
        spriteRenderer.sprite = isWet ? wetSprite : drySprite;
    }

    private bool CanTransferWetness(WaterProperty other)
    {
        Vector2 directionToOther = (other.transform.position - transform.position).normalized;
        float positionTolerance = 0.1f;

        if (directionToOther.x > 0.5f
            && canWetRight && other.canWetLeft
            && other.transform.position.x > transform.position.x
            && Mathf.Abs(other.transform.position.y - transform.position.y) <= positionTolerance)
        {
            return true;
        }
        else if (directionToOther.x < -0.5f
            && canWetLeft && other.canWetRight
            && other.transform.position.x < transform.position.x
            && Mathf.Abs(other.transform.position.y - transform.position.y) <= positionTolerance)
        {
            return true;
        }
        else if (directionToOther.y > 0.5f
            && canWetUp && other.canWetDown
            && other.transform.position.y > transform.position.y
            && Mathf.Abs(other.transform.position.x - transform.position.x) <= positionTolerance)
        {
            return true;
        }
        else if (directionToOther.y < -0.5f
            && canWetDown && other.canWetUp
            && other.transform.position.y < transform.position.y
            && Mathf.Abs(other.transform.position.x - transform.position.x) <= positionTolerance)
        {
            return true;
        }

        return false;
    }

    public bool IsWet => isWet;
    public bool IsWaterSource => isWaterSource;
    public HashSet<WaterProperty> TouchingObjects => touchingObjects;

    private static int GenerateUniqueId()
    {
        return idCounter++;
    }
}
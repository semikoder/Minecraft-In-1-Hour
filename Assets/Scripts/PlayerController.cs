using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject hand;
    public Transform equipHand;

    public Animator animator;
    public CharacterController cc;
    public InventorySystem inventorySystem;
    public float movementSpeed;
    public float jumpPower;

    public float groundCheckDist;
    public LayerMask groundLm;

    public float hitRange = 4f;

    public bool IsGrounded { get; private set; }

    private float yVelocity;

    private Transform camTransform;

    private Block oldBlock;
    private Item equippedItem;

    private bool hasAttacked;

    private GameObject holdingBlock;

    private void Start()
    {
        camTransform = Camera.main.transform;
    }

    private void Update()
    {

        // Ground check:
        IsGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDist, groundLm);

        // Movement input:
        Vector2 movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        Vector3 moveDir = transform.right * movementInput.x + transform.forward * movementInput.y;

        moveDir *= movementSpeed;

        cc.Move(moveDir * Time.deltaTime);

        if (IsGrounded)
        {
            if (yVelocity < 0)
            {
                yVelocity = -1f;
            }

            // Jump:
            if (Input.GetKeyDown(KeyCode.Space))
            {
                yVelocity = jumpPower;
            }
        }
        else
        {
            // Apply gravity when on air:
            yVelocity += Physics.gravity.y * Time.deltaTime;
        }

        cc.Move(yVelocity * Vector3.up * Time.deltaTime);

        // animator
        animator.SetFloat("Velocity", moveDir.magnitude / movementSpeed);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            // Attack:
            animator.SetBool("Swing", true);
            hasAttacked = true;
        }

        RaycastHit blockInfo;
        if (Physics.Raycast(camTransform.position, camTransform.forward, out blockInfo, hitRange, groundLm))
        {
            // highlight block:
            if (blockInfo.transform.GetComponent<Block>() != null)
            {
                Block selectedBlock = blockInfo.transform.GetComponent<Block>();

                if (selectedBlock != oldBlock)
                {
                    if (oldBlock != null)
                    {
                        // deselect old block:
                        oldBlock.HighlightBlock(false);
                    }
                    selectedBlock.HighlightBlock(true);

                    oldBlock = selectedBlock;
                }

                if (hasAttacked)
                {
                    // Destroy block:
                    selectedBlock.DestroyBlock();
                }

                if (equippedItem != null)
                {
                    if (Input.GetKeyDown(KeyCode.Mouse1))
                    {
                        Vector3Int spawnPoint = Vector3Int.RoundToInt(blockInfo.transform.position + blockInfo.normal);
                        GameObject spawnedBlock = Instantiate(equippedItem.blockPrefab, spawnPoint, Quaternion.identity);

                        inventorySystem.ConsumeItem(equippedItem);

                        if (!inventorySystem.items.ContainsKey(equippedItem))
                        {
                            // No longer available:
                            Equip(null);
                        }
                    }
                }
            }
        }

        hasAttacked = false;
    }

    public void Equip(Item itemToEquip)
    {
        if (holdingBlock != null)
        {
            Destroy(holdingBlock);
        }

        if (itemToEquip == null)
        {
            hand.SetActive(true);
            equippedItem = null;
        }
        else
        {
            hand.SetActive(false);
            holdingBlock = Instantiate(itemToEquip.blockPrefab, equipHand);
            equippedItem = itemToEquip;
        }
    }
}
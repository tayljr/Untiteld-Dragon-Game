using UnityEngine;
[RequireComponent(typeof(BoxCollider))]
public class SkinnedMeshCopy : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;
    private BoxCollider boxCollider;
    // Update is called once per frame
    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }
    void Update()
    {
        Bounds bounds = skinnedMeshRenderer.bounds;

        boxCollider.center = transform.InverseTransformPoint(bounds.center);
        boxCollider.size = bounds.size;
        transform.rotation = skinnedMeshRenderer.rootBone.rotation;
    }
}
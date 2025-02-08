using UnityEngine;

public class Segment : MonoBehaviour
{
    public float width; // Chiều rộng của segment

    void Awake()
    {
        // Tự động tính chiều rộng dựa trên renderer
        if (TryGetComponent(out Renderer renderer))
        {
            width = renderer.bounds.size.x; // Lấy kích thước chiều rộng
        }
        else
        {
            Debug.LogWarning("No Renderer found on this segment! Please add one.");
            width = 0f; // Đặt chiều rộng mặc định là 0 nếu không tìm thấy Renderer
        }
    }
}

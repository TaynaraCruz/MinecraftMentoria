
using UnityEngine;

public class EventsManager : MonoBehaviour
{
    public delegate void InventoryChange();
    public static event InventoryChange OnInventoryChange;
    public static void RaiseOnInventoryChange() {
        if (OnInventoryChange != null) {
            OnInventoryChange();
        }
    }
    
    public delegate void MouseScroll(int index);
    public static event MouseScroll OnMouseScroll;
    public static void RaiseOnMouseScroll(int index) {
        if (OnMouseScroll != null) {
            OnMouseScroll(index);
        }
    }
}

using UnityEngine;
public class PlanetManager : MonoBehaviour
{
 public static PlanetManager current;
 private void Awake()
 {
 if (current == null)
 {
 current = this;
 }
 else
 {
 Destroy(obj: this);
 }
 }
}
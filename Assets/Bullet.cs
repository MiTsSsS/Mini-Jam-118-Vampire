using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Transform target;
    public float speed = 20.0f;
    public int damage = 10;
    public Rigidbody2D rb;

    private void Update() {
        var dir = (target.position - transform.position).normalized;
        transform.position += dir * speed * Time.deltaTime;

        if (Mathf.Clamp(transform.position.x, target.position.x - 0.25f, target.position.x + 0.25f) == transform.position.x) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("Ground") || collision.CompareTag("Bullet")) {
            return;
        }

        Debug.Log(collision.name);
        collision.GetComponent<BaseUnit>().takeDamage(damage);
        Destroy(gameObject);
    }

    public void rotateSprite(Vector2 dirPos) {
        Quaternion rotation = transform.rotation;
        Vector2 initialPosition = UnitManager.instance.player.getOccupiedTile().tilePosition;

        int rotationOffset = 0;

        if (dirPos.x < initialPosition.x) {
            if (dirPos.y < initialPosition.y) {
                rotationOffset = 125;
            } 
            
            else {
                rotationOffset = 45;
            }
        } 
        
        else {
            if(dirPos.y > initialPosition.y) {
                rotationOffset = -45;
            }

            else {
                rotationOffset = -125;
            }
        }

        if(transform.position.x == dirPos.x && dirPos.y < initialPosition.y) {
            rotationOffset = 180;
        }

        if (transform.position.x == dirPos.x && dirPos.y > initialPosition.y) {
            rotationOffset = 0;
        }

        if (transform.position.y == dirPos.y && dirPos.x < initialPosition.x) {
            rotationOffset = 90;
        }

        if(transform.position.y == dirPos.y && dirPos.x > initialPosition.x) {
            rotationOffset = -90;
        }

        rotation.eulerAngles = new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z + rotationOffset);
        transform.rotation = rotation;
    }
}
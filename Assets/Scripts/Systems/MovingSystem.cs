using Core;
using Systems.Interfaces;
using UnityEngine;

namespace Systems
{
    public class MovingSystem :BaseSystem,IMovingSystem
    {
        [SerializeField] private GameObject _player;
        int moveSpeed = 1;
        
        void Update()
        {
            var position = _player.transform.position;
            
            if (Input.GetKeyDown(KeyCode.W))
            {
                _player.transform.position = new Vector3(position.x,position.y,position.z+moveSpeed);
            } 
            else if (Input.GetKeyDown(KeyCode.A)) 
            { 
                _player.transform.position = new Vector3(position.x-moveSpeed,position.y,position.z);
            } 
            else if (Input.GetKeyDown(KeyCode.S)) 
            { 
                _player.transform.position = new Vector3(position.x,position.y,position.z-moveSpeed);
            } 
            else if (Input.GetKeyDown(KeyCode.D)) 
            { 
                _player.transform.position = new Vector3(position.x+moveSpeed,position.y,position.z);
            } 
        }
    }
}
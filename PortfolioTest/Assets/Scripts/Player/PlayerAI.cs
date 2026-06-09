using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System.IO;

namespace Assets.Scripts.Player
{
    public enum PlayerControl
    {
        Auto,
        Manual
    }

    public class PlayerAI : MonoBehaviour
    {
        [SerializeField] StageManager stageManager;
        [SerializeField] PlayerControl control = PlayerControl.Auto;

        private MonsterStatus target;

        private List<MonsterStatus> monsters;
        private int index;

        private List<Vector3> path = new List<Vector3>();



        private void Update()
        {
            
            ControlInput();

            if (control == PlayerControl.Auto)
            {
                FindTarget();
            }
        }

        

        void ControlInput()
        {
            if (Keyboard.current == null) return;

            if (Keyboard.current.tabKey.wasPressedThisFrame)
            {
                if (control == PlayerControl.Auto)
                {
                    ToggleControlMode();
                }
                else
                {
                    SwitchTarget();
                }
            }
        }

        void ToggleControlMode()
        {
            if (control == PlayerControl.Auto)
            {
                control = PlayerControl.Manual;

                monsters = stageManager.GetAliveMonsters();

                if (monsters != null && monsters.Count > 0)
                {
                    index = 0;
                    target = monsters[index];
                }
            }
            else
            {
                control = PlayerControl.Auto;
            }
        }

        void SwitchTarget()
        {
            monsters = stageManager.GetAliveMonsters();

            if (monsters == null || monsters.Count == 0) return;
            
            index = (index + 1) % monsters.Count;

            target = monsters[index];
        }

        void FindTarget()
        {
            if (stageManager == null) return;
            if (control == PlayerControl.Manual) return;

            target = stageManager.ShortMagnitude(transform.position);
            if (target != null)
            {
                
            }
        }
    }
}
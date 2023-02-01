using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterController : MonoBehaviour
{
    #region Variables
    [SerializeField]
    bool IsPlayer = false;
    public bool _IsPlayer { get { return IsPlayer; } }
    #endregion Variables


}

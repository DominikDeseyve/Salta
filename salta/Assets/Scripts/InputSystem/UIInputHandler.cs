﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInputHandler : MonoBehaviour, IInputHandler
{
  public void ProcessInput(Vector3 pInputPosition, GameObject pSelectedObject, Action pCallback) {
        pCallback?.Invoke();
    }
}

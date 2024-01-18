﻿using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
     private List<GameObject> _objectPool;
     private GameObject _gameObject;
     
     public ObjectPool(GameObject gameObject, int initPoolSize)
     {
          _gameObject = gameObject;
          _objectPool = new List<GameObject>(initPoolSize);
          for (int i = 0; i < initPoolSize; i++)
          {
               GameObject o = Object.Instantiate(gameObject);
               o.SetActive(false);
               _objectPool.Add(o);
          }
     }

     public GameObject GetPooledObject()
     {
          
          foreach (GameObject gameObject in _objectPool)
          {
               if (!gameObject.activeSelf)
               {
                    gameObject.SetActive(true);
                    return gameObject;

               }
          }

          GameObject o = Object.Instantiate(_gameObject);
          _objectPool.Add(o);
          return o;


     }
    
}

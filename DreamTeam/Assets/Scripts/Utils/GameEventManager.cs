using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GameEvents;



namespace GameEventManager
 {

   public class GameEventsManager
 
   {

        static private GameEventsManager
     instance;
        static public GameEventsManager
     Instance 
        { 
            get 
            {
                if (instance == null) 
                {
                    instance = new GameEventsManager
                ();
                }
                return instance;
            }
        }

        private Dictionary<Type, GameEvent.Handler> registeredHandlers = new Dictionary<Type, GameEvent.Handler>();


        public void Register<T>(GameEvent.Handler handler) where T : GameEvent 
        {
            Type type = typeof(T);
            if (registeredHandlers.ContainsKey(type))
            {
                registeredHandlers[type] += handler;
            } 
            else 
            {
                registeredHandlers[type] = handler;
            }
        }

        public void Unregister<T>(GameEvent.Handler handler) where T : GameEvent 
        {
            Type type = typeof(T);
            GameEvent.Handler handlers;
            if (registeredHandlers.TryGetValue(type, out handlers)) 
            {
                handlers -= handler;
                if (handlers == null) 
                {
                    registeredHandlers.Remove(type);
                } 
                else 
                {
                    registeredHandlers[type] = handlers;
                }
            }
        }

        public void Fire(GameEvent e) 
        {
            Type type = e.GetType();
            GameEvent.Handler handlers;
            if (registeredHandlers.TryGetValue(type, out handlers)) 
            {
                handlers(e);
            }
        }
    }
}
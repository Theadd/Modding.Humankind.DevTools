using System.Collections.Generic;
using Amplitude.Mercury.Presentation;
using System;
using Amplitude.Mercury.Interop.AI.Entities;

namespace Modding.Humankind.DevTools
{
    public static class AdditionalExtensions
    {
        
        /// <summary>
        ///     This extension provides an easy way to iterate any sequence of <c>IEnumerable</c> collections one by one when pressing <c>[F3]</c> key within the game.
        /// </summary>
        /// <param name="sequence">this</param>
        /// <param name="action">The Action&lt;T&gt; to be executed each iteration.</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> Interactive<T>(this IEnumerable<T> sequence, Action<T> action)
        {
            IEnumerator<T> allEnumerator = sequence.GetEnumerator();
            Action wrappedAction = null;
            
            wrappedAction = () =>
            {
                if (allEnumerator.MoveNext())
                {
                    if (allEnumerator.Current != null)
                    {
                        try
                        {
                            action(allEnumerator.Current);
                        }
                        catch (Exception e)
                        {
                            Loggr.LogError(e.ToString());
                        }
                    }
                    else
                        Loggr.LogWarning("IEnumerator<T>.Current is null in IEnumerable<T> .Interactive(Action<T> action) where T is " + typeof(T).Name);
                }
                else
                {
                    HumankindDevTools.OnIterateNext -= wrappedAction;
                }
            };

            HumankindDevTools.OnIterateNext -= wrappedAction;
            HumankindDevTools.OnIterateNext += wrappedAction;

            return sequence;
        }

        /// <summary>
        ///     Executes an action for each element in the sequence.
        /// </summary>
        /// <param name="sequence">this</param>
        /// <param name="action">The Action&lt;T&gt; to be executed for each element in the sequence.</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> Execute<T>(this IEnumerable<T> sequence,
            Action<T> action)
        {
            foreach (var element in sequence)
            {
                action(element);
            }

            return sequence;
        }

        /// <summary>
        ///     Simulate user click on an army.
        /// </summary>
        /// <param name="army">this</param>
        /// <returns></returns>
        public static Army SelectArmy(this Army army)
        {
            Amplitude.Mercury.Simulation.SimulationEntityGUID entityGUID =
                new Amplitude.Mercury.Simulation.SimulationEntityGUID(army.EntityGUID);
            
            if (Presentation.PresentationCursorController.CurrentCursor is ArmyCursor armyCursor)
                armyCursor.ChangeEntity(entityGUID);
            else
                Presentation.PresentationCursorController.ChangeToArmyCursor(entityGUID);
            
            return army;
        }
        
        /// <summary>
        ///     Center main camera view to this <c>Army</c>'s TileIndex. 
        /// </summary>
        /// <param name="army">this</param>
        /// <returns></returns>
        public static Army CenterToCamera(this Army army)
        {
            HumankindGame.CenterCameraAt(army.TileIndex);
            
            return army;
        }
    }
}
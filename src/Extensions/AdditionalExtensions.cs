using System.Collections.Generic;
using Amplitude.Mercury.Presentation;
using System;

namespace Modding.Humankind.DevTools
{
    public static class AdditionalExtensions
    {
        
        /// <summary>
        ///     This extension provides an easy way to iterate any sequence of <c>IEnumerable</c> collections one by one when pressing <c>[F3]</c> key within the game.
        /// </summary>
        /// <param name="sequence"></param>
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
        
        public static void SelectUnit(Amplitude.Mercury.Simulation.SimulationEntityGUID unitGUID, bool selected)
        {
            BaseArmyCursor baseArmyCursor = Presentation.PresentationCursorController.CurrentCursor as BaseArmyCursor;
            if (baseArmyCursor == null)
            {
                return;
            }
            if (selected)
            {
                baseArmyCursor.SelectUnit(unitGUID);
                return;
            }
            baseArmyCursor.UnselectUnit(unitGUID);
        }
    }
}
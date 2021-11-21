using System;
using System.Collections;
using System.Collections.Generic;
using Amplitude.Framework.Simulation;
using Amplitude.Mercury.Interop.AI.Entities;
using Settlement = Amplitude.Mercury.Simulation.Settlement;

namespace Modding.Humankind.DevTools.Core
{
    public class SettlementList : IReadOnlyList<HumankindSettlement>
    {
        private readonly MajorEmpire MajorEmpire;
        private readonly Amplitude.Mercury.Simulation.MajorEmpire MajorEmpireSimulation;
        
        private ReferenceCollection<Settlement> settlementsSimulation =>
            (ReferenceCollection<Settlement>) R.Fields.SettlementsField.GetValue(MajorEmpireSimulation);

        public SettlementList(MajorEmpire majorEmpireEntity, Amplitude.Mercury.Simulation.MajorEmpire majorEmpireSimulation)
        {
            MajorEmpire = majorEmpireEntity;
            MajorEmpireSimulation = majorEmpireSimulation;
        }

        IEnumerator IEnumerable.GetEnumerator() => (IEnumerator)GetEnumerator();

        public IEnumerator<HumankindSettlement> GetEnumerator() => new SettlementEnumerator(this);

        public int Count => settlementsSimulation.Count;

        public HumankindSettlement this[int index] => HumankindSettlement.Create(MajorEmpire.Settlements[index], settlementsSimulation[index]);

        // Enumerator
        
        public class SettlementEnumerator : IEnumerator<HumankindSettlement>
        {
            private SettlementList _parent;
            private int _count;

            // Enumerators are positioned before the first element
            // until the first MoveNext() call.
            int position = -1;

            public SettlementEnumerator(SettlementList parent)
            {
                _parent = parent;
                _count = parent.Count;
            }

            public bool MoveNext() => ++position < _count;

            public void Reset() => position = -1;

            object IEnumerator.Current => Current;

            public HumankindSettlement Current
            {
                get
                {
                    try
                    {
                        return _parent[position];
                    }
                    catch (IndexOutOfRangeException)
                    {
                        throw new InvalidOperationException();
                    }
                }
            }

            public void Dispose() {}
        }

        
    }
}

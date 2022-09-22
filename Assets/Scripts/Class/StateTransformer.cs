using System;
using Interface;

namespace Class
{
    public class StateTransformer
    {
        public IState To { get; }
        public IState From { get; }
        public Func<bool> Condition { get; }

        public StateTransformer(IState from, IState to, Func<bool> condition)
        {
            From = from;
            To = to;
            Condition = condition;
        }
    }
}
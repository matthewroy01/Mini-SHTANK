using System;

namespace Utility.StateMachine
{
    public class Connection
    {
        public State From => _from;
        public State To => _to;
        
        private readonly State _from;
        private readonly State _to;

        public Connection(State from, State to)
        {
            _from = from;
            _to = to;
        }

        public Connection(State to)
        {
            _from = null;
            _to = to;
        }
    }
}
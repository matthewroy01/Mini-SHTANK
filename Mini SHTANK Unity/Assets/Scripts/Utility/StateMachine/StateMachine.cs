using System.Collections.Generic;

namespace Utility.StateMachine
{
    public class StateMachine
    {
        private State _currentState;
        private readonly List<Connection> _connectionList;
        public State CurrentState => _currentState;

        public StateMachine(State startingState, params Connection[] connections)
        {
            _currentState = startingState;
            _currentState.EnterState();

            _connectionList = new List<Connection>(connections);
        }

        public bool TryChangeState(State to)
        {
            foreach(Connection connection in _connectionList)
            {
                if (connection.To != to)
                    continue;

                if (connection.From != _currentState && connection.From != null)
                    continue;
                
                _currentState.ExitState();
                _currentState = to;
                _currentState.EnterState();
                return true;
            }

            return false;
        }
    }
}